using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Game.Entity.Treasure;
using Game.Facade;
using Game.Utils;

// ReSharper disable InconsistentNaming

namespace Game.Web.Helper
{
    public static class JFTPay
    {
        public static class Config
        {
            public const string JFTH5Url = "http://order.z.jtpay.com/jh-web-order/order/receiveOrder";
            public static string JFTID = AppConfig.GetSetting("JFTID");
            public static string JFTKEY = AppConfig.GetSetting("JFTKEY");
            public static string JFTMODE = AppConfig.GetSetting("JFTMODE");
        }

        /// <summary>
        /// 骏付通支付请求类
        /// </summary>
        public class JFTH5Request
        {
            /// <summary>
            /// 商户在竣付通平台的应用ID
            /// </summary>
            public string p1_yingyongnum => Config.JFTID;

            /// <summary>
            /// 自定义订单号
            /// </summary>
            public string p2_ordernumber { get; set; }

            /// <summary>
            /// 订单金额。支持两种格式，精确到元或精确到分
            /// </summary>
            public string p3_money { get; set; }

            /// <summary>
            /// 商户订单创建时间。格式yyyymmddhhmmss
            /// </summary>
            public string p6_ordertime { get; set; }

            /// <summary>
            /// 终端支付方式 微信：WXZZWAP 支付宝：ZFBZZWAP
            /// </summary>
            public string p7_productcode { get; set; }

            /// <summary>
            /// 签名 
            /// </summary>
            public string p8_sign => Sign();

            /// <summary>
            /// 传GameID
            /// </summary>
            public string p14_customname { get; set; }

            /// <summary>
            /// 传用户IP
            /// </summary>
            public string p16_customip { get; set; }

            public string p17_product { get; set; }
            public string p18_productcat { get; set; }
            public string p19_productnum { get; set; }
            public string p20_pdesc { get; set; }

            /// <summary>
            /// 传终端类型 PC：1 IOS：2 Android：3
            /// </summary>
            public string p25_terminal { get; set; } = "1";
            /// <summary>
            /// 直连用于正式环境此配置无效，直转时才需要此参数
            /// </summary>
            public string paytype { get; set; }

            public JFTH5Request(string orderId, string orderAmount, string productCode, string gameId, string clientIp)
            {
                p2_ordernumber = orderId;
                p3_money = orderAmount;
                p6_ordertime = DateTime.Now.ToString("yyyyMMddHHmmss");
                p7_productcode = productCode;
                p14_customname = gameId.ToString();
                p16_customip = clientIp;
                paytype = Config.JFTMODE;
            }

            public string Sign()
            {
                return Utility.MD5(
                    $"{p1_yingyongnum}&{p2_ordernumber}&{p3_money}&{p6_ordertime}&{p7_productcode}&{Config.JFTKEY}");
            }

            /// <summary>
            /// 转化成支付提交地址
            /// </summary>
            /// <returns></returns>
            public string ToUrl()
            {
                return Config.JFTH5Url + "?" + UrlParams();
            }

            /// <summary>
            /// Url拼接方法
            /// </summary>
            /// <returns></returns>
            public string UrlParams()
            {
                Type t = GetType();
                PropertyInfo[] PropertyList = t.GetProperties();
                string result = "";
                foreach (PropertyInfo item in PropertyList)
                {
                    string name = item.Name;
                    object value = item.GetValue(this, null);
                    if (value != null)
                    {
                        result += $"{name}={value}&";
                    }
                }
                return result.Substring(0, result.Length - 1);
            }

            /// <summary>
            /// 终端类型从Fetch.GetTerminalType 转化成JFT参数
            /// </summary>
            /// <param name="terminal"></param>
            public void SetTerminal(int terminal)
            {
                switch (terminal)
                {
                    case 0:
                    default:
                        p25_terminal = "1";
                        break;
                    case 1:
                        p25_terminal = "3";
                        break;
                    case 2:
                        p25_terminal = "2";
                        break;
                }
            }
        }

        public class JFTH5Notify
        {
            public string p1_yingyongnum { get; set; }

            /// <summary>
            ///  订单号 Y 商户提交的订单号。
            /// </summary>
            public string p2_ordernumber { get; set; }

            /// <summary>
            ///  金额  Y 该次交易金额（以通知金额为准）。无论商户发起支付时金额采用哪种格式，返回金额均保留两位小数。
            /// </summary>
            public string p3_money { get; set; }

            /// <summary>
            /// 支付结果    Y	（必须）支付返回结果1代表成功，其他为失败。
            /// </summary>
            public string p4_zfstate { get; set; }

            public string p5_orderid { get; set; }
            public string p6_productcode { get; set; }
            public string p7_bank_card_code { get; set; }
            public string p8_charset { get; set; }
            public string p9_signtype { get; set; }
            public string p10_sign { get; set; }
            public string p11_pdesc { get; set; }
            public string p12_remark { get; set; }

            public JFTH5Notify(OnLinePayOrder order)
            {
                p1_yingyongnum = Config.JFTID;
                p2_ordernumber = order.OrderID;
                p3_money = order.Amount.ToString("F2");
                p4_zfstate = "1";
                p5_orderid = Fetch.ConvertDateTimeToUnix(DateTime.Now);
                p6_productcode = order.ShareID == 303 ? "ZFB" : "WX";
                p8_charset = "UTF-8";
                p9_signtype = "1";
                p10_sign = MarkSign();
            }

            public JFTH5Notify(HttpRequest request)
            {
                Type t = GetType();
                PropertyInfo[] PropertyList = t.GetProperties();
                foreach (PropertyInfo item in PropertyList)
                {
                    foreach (string key in request.Params.Keys)
                    {
                        if (item.Name == key)
                        {
                            item.SetValue(this, request.Params[key], null);
                        }
                    }
                }
            }

            public bool VerifySign()
            {
                return p10_sign == MarkSign();
            }

            public string MarkSign()
            {
                return Utility.MD5(
                    $"{p1_yingyongnum}&{p2_ordernumber}&{p3_money}&{p4_zfstate}&{p5_orderid}&{p6_productcode}&{p7_bank_card_code}&{p8_charset}&{p9_signtype}&{p11_pdesc}&{Config.JFTKEY}");
            }

            /// <summary>
            /// Url拼接方法
            /// </summary>
            /// <returns></returns>
            public string UrlParams()
            {
                Type t = GetType();
                PropertyInfo[] PropertyList = t.GetProperties();
                string result = "";
                foreach (PropertyInfo item in PropertyList)
                {
                    string name = item.Name;
                    object value = item.GetValue(this, null);
                    if (value != null)
                    {
                        result += $"{name}={value}&";
                    }
                }
                return result.Substring(0, result.Length - 1);
            }

            public string TestNotifyUrl()
            {
                return
                    $"http://{GameRequest.GetCurrentFullHost()}/Notify/JFTPay.aspx?{UrlParams()}";
            }
        }
    }
}
