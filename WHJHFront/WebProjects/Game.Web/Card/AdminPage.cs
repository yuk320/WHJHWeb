using Game.Entity.Accounts;
using Game.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Game.Web.Card
{
    public class AdminPage: Page
    {
        #region 页面加载
        /// <summary>
        /// 用户基本Cookies信息
        /// </summary>
        protected UserTicketInfo userTicket;

        /// <summary>
        /// 初始化并验证用户身份
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            userTicket = Fetch.GetUserCookie();
            if(userTicket == null || userTicket.UserID <= 0)
            {
                Response.Redirect("/Card/LoginOut.aspx");
                Response.End();
            }
        }
        #endregion

        #region 消息对话框
        /// <summary>
        /// 显示消息提示对话框
        /// </summary>
        /// <param name="msg">提示信息</param>
        public void ShowInfo(string msg)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script type='text/javascript'>layer.open({content: '" + msg + "',skin: 'footer'});</script>");
        }
        #endregion
    }
}