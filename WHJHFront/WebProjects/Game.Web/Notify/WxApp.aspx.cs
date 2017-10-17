using Game.Entity.Treasure;
using Game.Facade;
using Game.Utils;
using Game.Web.Helper;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Game.Web.Notify
{
    public partial class WxApp : System.Web.UI.Page
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                WxPayHelper wx = new WxPayHelper();
                string returnMsg = "<xml> <return_code><![CDATA[{0}]]></return_code> <return_msg><![CDATA[{1}]]></return_msg> </xml>";
                SortedDictionary<string, object> dic = wx.GetReturnData();
                Log4Net.WriteInfoLog("收到微信App支付回调：" + new JavaScriptSerializer().Serialize(dic));

                string sign = dic["sign"].ToString();
                if(dic["return_code"].ToString() == "SUCCESS")
                {
                    string signLocal = wx.MakeSign(dic, ApplicationSettings.Get("WXAPPKEY"));
                    if(sign == signLocal)
                    {
                        decimal amount = Convert.ToDecimal(dic["total_fee"]) / 100M;
                        if(dic["result_code"].ToString() == "SUCCESS")
                        {
                            OnLinePayOrder order = new OnLinePayOrder
                            {
                                OrderID = dic["out_trade_no"].ToString(),
                                PayAddress = GameRequest.GetUserIP(),
                                Amount = amount
                            };

                            FacadeManage.aideTreasureFacade.FinishOnLineOrder(order);
                            Response.Write(string.Format(returnMsg, "SUCCESS", "支付成功！"));
                        }
                        else
                        {
                            Response.Write(string.Format(returnMsg, "FAIL", "微信交易失败！"));
                        }
                    }
                    else
                    {
                        Response.Write(string.Format(returnMsg, "FAIL", "签名错误！"));
                    }
                }
                else
                {
                    Response.Write(string.Format(returnMsg, "FAIL", "微信交易失败！"));
                }
            }
        }
    }
}