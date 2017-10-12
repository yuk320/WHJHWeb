using Game.Entity.Treasure;
using Game.Facade;
using Game.Utils;
using Game.Web.Helper;
using System;
using System.Collections.Generic;

namespace Game.Web.Notify
{
    public partial class Alipay : System.Web.UI.Page
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                SortedDictionary<string, string> sPara = AlipayHelper.GetRequestPost();
                if(sPara.Count > 0)//判断是否有带返回参数
                {
                    bool verifyResult = AlipayHelper.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"], Request.Form["sign_type"]);
                    if(verifyResult)
                    {
                        string outTradeNo = Request.Form["out_trade_no"];
                        string tradeStatus = Request.Form["trade_status"];

                        if(tradeStatus == "TRADE_SUCCESS" || tradeStatus == "TRADE_FINISHED")
                        {
                            OnLinePayOrder order = new OnLinePayOrder
                            {
                                OrderID = outTradeNo,
                                PayAddress = GameRequest.GetUserIP(),
                                Amount = Convert.ToDecimal(Request.Form["total_fee"])
                            };

                            FacadeManage.aideTreasureFacade.FinishOnLineOrder(order);
                        }
                        Response.Write("success");
                    }
                    else
                    {
                        Response.Write("fail");
                    }
                }
                else
                {
                    Response.Write("无通知参数");
                }
            }
        }
    }
}