using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Collections.Specialized;
using Game.Utils;

namespace Game.Web.Helper
{
    public class AlipayHelper
    {
        //支付宝签约账号
        private static string Partner => ApplicationSettings.Get("ZFBPARTNER");

        //收款支付宝账号
        //支付宝密钥
        private static string Key => ApplicationSettings.Get("ZFBKEY");

        //支付宝公钥
        // ReSharper disable once InconsistentNaming
        private static string RSAPublicKey => ApplicationSettings.Get("ZFBPUBLICKEY");

        //字符编码格式
        private const string InputCharset = "utf-8";

        //支付宝消息验证地址
        private const string Veryfyurl = "https://mapi.alipay.com/gateway.do?service=notify_verify&";

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public static SortedDictionary<string, string> GetRequestPost()
        {
            int i;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll = HttpContext.Current.Request.Form;

            string[] requestItem = coll.AllKeys;
            for(i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], HttpContext.Current.Request.Form[requestItem[i]]);
            }

            return sArray;
        }

        /// <summary>
        ///  验证消息是否是支付宝发出的合法消息
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <param name="notifyId">通知验证ID</param>
        /// <param name="sign">支付宝生成的签名结果</param>
        /// <param name="signType"></param>
        /// <returns>验证结果</returns>
        public static bool Verify(SortedDictionary<string, string> inputPara, string notifyId, string sign, string signType)
        {
            //获取返回时的签名验证结果
            bool isSign = signType.ToUpper() == "RSA" ? GetSignVeryfyRSA(inputPara, sign) : GetSignVeryfyMD5(inputPara, sign);
            //获取是否是支付宝服务器发来的请求的验证结果
            string responseTxt = "false";
            if(!string.IsNullOrEmpty(notifyId)) { responseTxt = GetResponseTxt(notifyId); }
            return (responseTxt == "true" && isSign);
        }
        /// <summary>
        /// 获取返回时的签名验证结果
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <param name="sign">对比的签名结果</param>
        /// <returns>签名验证结果</returns>
        // ReSharper disable once InconsistentNaming
        private static bool GetSignVeryfyMD5(SortedDictionary<string, string> inputPara, string sign)
        {
            //过滤空值、sign与sign_type参数
            Dictionary<string, string> sPara = FilterPara(inputPara);

            //获取待签名字符串
            string preSignStr = CreateLinkString(sPara);

            //获得签名验证结果
            return Verify(preSignStr, sign, Key, InputCharset);
        }

        /// <summary>
        /// 获取返回时的签名验证结果
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <param name="sign">对比的签名结果</param>
        /// <returns>签名验证结果</returns>
        // ReSharper disable once InconsistentNaming
        private static bool GetSignVeryfyRSA(SortedDictionary<string, string> inputPara, string sign)
        {
            //过滤空值、sign与sign_type参数
            Dictionary<string, string> sPara = FilterPara(inputPara);

            //获取待签名字符串
            string preSignStr = CreateLinkString(sPara);

            //获得签名验证结果
            return AlipayRSA.Verify(preSignStr, sign, RSAPublicKey, InputCharset);
        }

        /// <summary>
        /// 除去数组中的空值和签名参数并以字母a到z的顺序排序
        /// </summary>
        /// <param name="dicArrayPre">过滤前的参数组</param>
        /// <returns>过滤后的参数组</returns>
        public static Dictionary<string, string> FilterPara(SortedDictionary<string, string> dicArrayPre)
        {
            return dicArrayPre.Where(temp => temp.Key.ToLower() != "sign" && temp.Key.ToLower() != "sign_type" && !string.IsNullOrEmpty(temp.Value)).ToDictionary(temp => temp.Key, temp => temp.Value);
        }

        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
        /// </summary>
        /// <param name="dicArray"></param>
        /// <returns>拼接完成以后的字符串</returns>
        public static string CreateLinkString(Dictionary<string, string> dicArray)
        {
            StringBuilder prestr = new StringBuilder();
            foreach(KeyValuePair<string, string> temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + temp.Value + "&");
            }

            //去掉最後一個&字符
            prestr.Remove(prestr.Length - 1, 1);

            return prestr.ToString();
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="prestr">需要签名的字符串</param>
        /// <param name="sign">签名结果</param>
        /// <param name="key">密钥</param>
        /// <param name="inputCharset">编码格式</param>
        /// <returns>验证结果</returns>
        public static bool Verify(string prestr, string sign, string key, string inputCharset)
        {
            string mysign = Sign(prestr, key, inputCharset);
            return mysign == sign;
        }
        /// <summary>
        /// 签名字符串
        /// </summary>
        /// <param name="prestr">需要签名的字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="inputCharset">编码格式</param>
        /// <returns>签名结果</returns>
        public static string Sign(string prestr, string key, string inputCharset)
        {
            StringBuilder sb = new StringBuilder(32);

            prestr = prestr + key;

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding(inputCharset).GetBytes(prestr));
            foreach (byte t1 in t)
            {
                sb.Append(t1.ToString("x").PadLeft(2, '0'));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获取是否是支付宝服务器发来的请求的验证结果
        /// </summary>
        /// <param name="notifyId">通知验证ID</param>
        /// <returns>验证结果</returns>
        private static string GetResponseTxt(string notifyId)
        {
            string veryfyUrl = Veryfyurl + "partner=" + Partner + "&notify_id=" + notifyId;

            //获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
            string responseTxt = Get_Http(veryfyUrl, 120000);

            return responseTxt;
        }
        /// <summary>
        /// 获取远程服务器ATN结果
        /// </summary>
        /// <param name="strUrl">指定URL路径地址</param>
        /// <param name="timeout">超时时间设置</param>
        /// <returns>服务器ATN结果</returns>
        private static string Get_Http(string strUrl, int timeout)
        {
            string strResult;
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(strUrl);
                myReq.Timeout = timeout;
                HttpWebResponse httpWResp = (HttpWebResponse)myReq.GetResponse();
                Stream myStream = httpWResp.GetResponseStream();
                if (myStream != null)
                {
                    StreamReader sr = new StreamReader(myStream, Encoding.Default);
                    StringBuilder strBuilder = new StringBuilder();
                    while (-1 != sr.Peek())
                    {
                        strBuilder.Append(sr.ReadLine());
                    }

                    strResult = strBuilder.ToString();
                }
                else
                {
                    strResult = "";
                }
            }
            catch(Exception exp)
            {
                strResult = "错误：" + exp.Message;
            }

            return strResult;
        }
    }
}