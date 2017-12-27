using Game.Entity.Treasure;
using Game.Facade;
using Game.Utils;
using Game.Web.Helper;
using System;
using System.Web;

namespace Game.Web.Mobile.Pay
{
    public partial class LqReturn : System.Web.UI.Page
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
                        string msg = "【" + pSplit[0] + "】充值成功！";
                        Response.Redirect("/Mobile/Index.aspx?action=payreturn&msg="+ msg);
                        return;
                    }
                }
                Response.Redirect("/Mobile/Index.aspx?action=payreturn&msg=充值失败");
            }
        }
    }
}