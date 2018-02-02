using Game.Entity.Treasure;
using Game.Facade;
using Game.Utils;
using System;

namespace Game.Web.Mobile.Pay
{
    public partial class LqReturn : System.Web.UI.Page
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            string orderId = GameRequest.Request.QueryString["orderId"];

            OnLinePayOrder order = FacadeManage.aideTreasureFacade.GetPayOnLinePayOrder(orderId);

            if (order != null && order.OrderStatus == 1)
            {
                string msg = "【" + orderId + "】充值成功！";
                Response.Redirect("/Mobile/Index.aspx?action=payreturn&msg=" + msg);
                return;
            }
            Response.Redirect("/Mobile/Index.aspx?action=payreturn&msg=充值失败");
        }
    }
}
