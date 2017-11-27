using Game.Entity.Treasure;
using Game.Facade;
using Game.Utils;
using Game.Web.Helper;
using System;
using System.Web;

namespace Game.Web.Notify
{
    public partial class LqPay : System.Web.UI.Page
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
//                Log4Net.WriteInfoLog("零钱支付回掉进来了------query:" + GameRequest.Request.QueryString);

                LQPay.Notify notify = new LQPay.Notify(GameRequest.Request.QueryString);
                if (!string.IsNullOrEmpty(notify.Param))
                {
                    if (notify.IsChecked)
                    {
                        string[] pSplit = notify.Param.Split('|');
                        string retCode = pSplit[1];
                        if (retCode == "0000") //支付成返回值
                        {
                            OnLinePayOrder order = new OnLinePayOrder
                            {
                                OrderID = pSplit[0],
                                PayAddress = GameRequest.GetUserIP(),
                                Amount = Convert.ToInt32(notify.ExtraParam.money) / 100M
                            };
                            FacadeManage.aideTreasureFacade.FinishOnLineOrder(order);
                            Response.Write("OK");
                            return;
                        }
                    }
                }
                Response.Write("FAIL");
            }
        }
    }
}