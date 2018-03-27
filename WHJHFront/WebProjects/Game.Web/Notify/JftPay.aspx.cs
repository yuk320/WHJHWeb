using Game.Entity.Treasure;
using Game.Facade;
using Game.Utils;
using System;

namespace Game.Web.Notify
{
    public partial class JFTPay : System.Web.UI.Page
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            Helper.JFTPay.JFTH5Notify notify = new Helper.JFTPay.JFTH5Notify(GameRequest.Request);
            //验证签名且支付状态为成功
            if (notify.VerifySign() && notify.p4_zfstate == "1")
            {
                OnLinePayOrder order = new OnLinePayOrder
                {
                    OrderID = notify.p2_ordernumber,
                    Amount = Convert.ToDecimal(notify.p3_money)
                };

                FacadeManage.aideTreasureFacade.FinishOnLineOrder(order);
                Response.Write("success");
            } else
            {
                Response.Write("fail");
            }
        }
    }
}
