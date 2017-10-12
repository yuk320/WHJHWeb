using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Facade.DataStruct
{
    public class WxPayInfo
    {
        /// <summary>
        /// 微信APPID
        /// </summary>
        public string AppID { get; set; }
        /// <summary>
        /// 微信商户号
        /// </summary>
        public string Mchid { get; set; }
        /// <summary>
        /// 微信商户密钥
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 微信AppSecret
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// 微信支付订单号
        /// </summary>
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 微信支付金额
        /// </summary>
        public string TotalFee { get; set; }
        /// <summary>
        /// 微信支付时显示内容
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 微信支付回调
        /// </summary>
        public string NotifyUrl { get; set; }
        /// <summary>
        /// 微信支付时使用的微信用户标识
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 微信支付类型
        /// </summary>
        public string TradeType { get; set; }
    }
}
