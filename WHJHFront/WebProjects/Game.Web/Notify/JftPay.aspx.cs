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

                //尝试完成订单
                FacadeManage.aideTreasureFacade.FinishOnLineOrder(order);

                //GET为同步返回模式
                if (Request.HttpMethod == "GET")
                {
                    //查询订单状态
                    OnLinePayOrder orderQuery = FacadeManage.aideTreasureFacade.GetPayOnLinePayOrder(notify.p2_ordernumber);
                    //订单完成则
                    if (orderQuery != null && orderQuery.OrderStatus == 1)
                    {
                        string msg = "【" + notify.p2_ordernumber + "】充值成功！";
                        Response.Redirect("/Mobile/Index.aspx?action=payreturn&msg=" + msg);
                        return;
                    }
                    Response.Redirect("/Mobile/Index.aspx?action=payreturn&msg=请稍后再查询");
                    return;
                }
                
                //回写给通知回调
                Response.Write("success");
            } else
            {
                Response.Write("fail");
            }
        }
    }
}
