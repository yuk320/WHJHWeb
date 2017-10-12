using Game.Facade.DataStruct;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;

namespace Game.Web.Helper
{
    /// <summary>
    /// 微信支付处理类
    /// </summary>
    public class WxPayHelper
    {
        #region 全局字段
        //微信支付信息
        private readonly WxPayInfo _wxInfo;
        #endregion

        #region 构造函数
        public WxPayHelper() { }
        public WxPayHelper(WxPayInfo wxInfo)
        {
            _wxInfo = wxInfo;
        }
        #endregion

        #region 微信处理方法
        /// <summary>
        /// 统一下单接口返回正常的prepay_id，再按签名规范重新生成签名后，将数据传输给APP
        /// </summary>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public string GetPrepayIDSign()
        {
            const string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            SortedDictionary<string, object> mValues = new SortedDictionary<string, object>
            {
                ["body"] = _wxInfo.Body,
                ["attach"] = _wxInfo.Body,
                ["out_trade_no"] = _wxInfo.OutTradeNo,
                ["total_fee"] = _wxInfo.TotalFee,
                ["time_start"] = DateTime.Now.ToString("yyyyMMddHHmmss"),
                ["time_expire"] = DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"),
                ["trade_type"] = _wxInfo.TradeType,
                ["appid"] = _wxInfo.AppID,
                ["mch_id"] = _wxInfo.Mchid,
                ["notify_url"] = _wxInfo.NotifyUrl,
                ["spbill_create_ip"] = "",
                ["nonce_str"] = GenerateNonceStr()
            };
            if (!string.IsNullOrEmpty(_wxInfo.OpenId))
            {
                mValues["openid"] = _wxInfo.OpenId;
            }
            mValues["sign"] = MakeSign(mValues, _wxInfo.Key);
            
            //签名
            string xml = ToXml(mValues);
            string response = WxHttpService.Post(xml, url, 6);
            SortedDictionary<string, object> sDic = FromXml(response);
            object obj;
            sDic.TryGetValue("prepay_id", out obj);
            string prepayId = obj?.ToString() ?? "";
            if(prepayId != "")
            {
                SortedDictionary<string, object> cDic =
                    new SortedDictionary<string, object>
                    {
                        ["appid"] = _wxInfo.AppID,
                        ["noncestr"] = Guid.NewGuid().ToString().Replace("-", ""),
                        ["package"] = "Sign=WXPay",
                        ["partnerid"] = _wxInfo.Mchid,
                        ["prepayid"] = prepayId,
                        ["timestamp"] = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000)
                        .ToString()
                    };
                cDic["sign"] = MakeSign(cDic, _wxInfo.Key);

                return ToJson(cDic);
            }
            return "";
        }

        /**
        * @Dictionary格式化成Json
         * @return json串数据
        */
        public string ToJson(SortedDictionary<string, object> param)
        {
            string jsonStr = new JavaScriptSerializer().Serialize(param);
            return jsonStr;
        }

        /**
        * 生成随机串，随机串包含字母或数字
        * @return 随机串
        */
        public static string GenerateNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        /**
        * @生成签名，详见签名生成算法
        * @return 签名, sign字段不参加签名
        */
        public string MakeSign(SortedDictionary<string, object> param, string key)
        {
            //转url格式
            string str = ToUrl(param);
            //在string后加入API KEY
            str += "&key=" + key;
            //MD5加密
            var md5 = MD5.Create();
            var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder();
            foreach(byte b in bs)
            {
                sb.Append(b.ToString("x2"));
            }
            //所有字符转为大写
            return sb.ToString().ToUpper();
        }

        /**
        * @Dictionary格式转化成url参数格式
        * @ return url格式串, 该串不包含sign字段值
        */
        public string ToUrl(SortedDictionary<string, object> param)
        {
            string buff = "";
            foreach(KeyValuePair<string, object> pair in param)
            {
                if(pair.Value == null)
                {
                    throw new Exception("值为null的字段!");
                }

                if(pair.Key != "sign" && pair.Value.ToString() != "")
                {
                    buff += pair.Key + "=" + pair.Value + "&";
                }
            }
            buff = buff.Trim('&');
            return buff;
        }

        /**
        * @将Dictionary转成xml
        * @return 经转换得到的xml串
        * @throws WxPayException
        **/
        public string ToXml(SortedDictionary<string, object> param)
        {
            //数据为空时不能转化为xml格式
            if(0 == param.Count)
            {
                throw new Exception("数据为空!");
            }

            string xml = "<xml>";
            foreach(KeyValuePair<string, object> pair in param)
            {
                //字段值不能为null，会影响后续流程
                if(pair.Value == null)
                {
                    throw new Exception("值为null的字段!");
                }

                if(pair.Value is int)
                {
                    xml += "<" + pair.Key + ">" + pair.Value + "</" + pair.Key + ">";
                }
                else if(pair.Value is string)
                {
                    xml += "<" + pair.Key + ">" + "<![CDATA[" + pair.Value + "]]></" + pair.Key + ">";
                }
                else//除了string和int类型不能含有其他数据类型
                {
                    throw new Exception("字段数据类型错误!");
                }
            }
            xml += "</xml>";
            return xml;
        }

        /**
        * @将xml转为WxPayData对象并返回对象内部的数据
        * @param string 待转换的xml串
        * @return 经转换得到的Dictionary
        * @throws WxPayException
        */
        public SortedDictionary<string, object> FromXml(string xml)
        {
            SortedDictionary<string, object> mValues = new SortedDictionary<string, object>();
            if(string.IsNullOrEmpty(xml))
            {
                throw new Exception("将空的xml串转换不合法!");
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes;
            foreach(XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                mValues[xe.Name] = xe.InnerText;//获取xml的键值对到WxPayData内部的数据中
            }

            if(mValues["return_code"].ToString() != "SUCCESS")
            {
                return mValues;
            }
            CheckSign(mValues);//验证签名,不通过会抛异常

            return mValues;
        }

        /**
        * 
        * 检测签名是否正确
        * 正确返回true，错误抛异常
        */
        public bool CheckSign(SortedDictionary<string, object> param)
        {
            object obj;
            param.TryGetValue("sign", out obj);
            if(obj == null)
            {
                throw new Exception("签名存在但不合法!");
            }
            else if(obj.ToString() == "")
            {
                throw new Exception("签名存在但不合法!");
            }
            string calSign = MakeSign(param, _wxInfo.Key);
            if(calSign != obj.ToString())
            {
                throw new Exception("签名验证错误!");
            }
            return true;
        }

        /// <summary>
        /// 获取返回数据
        /// </summary>
        /// <returns></returns>
        public SortedDictionary<string, object> GetReturnData()
        {
            SortedDictionary<string, object> dic = new SortedDictionary<string, object>();
            StreamReader reader = new StreamReader(HttpContext.Current.Request.InputStream);
            string xmlData = reader.ReadToEnd();
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlData);
            XmlNode oNode = xml.DocumentElement;
            if (oNode != null)
            {
                XmlNodeList oList = oNode.ChildNodes;
                foreach(XmlNode item in oList)
                {
                    dic.Add(item.Name, item.InnerText);
                }
            }
            return dic;
        }
        #endregion
    }
}