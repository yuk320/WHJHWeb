using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using Game.Entity.Accounts;
using Game.Entity.Treasure;

namespace Game.Web.Card
{
    public partial class SetPassword : AdminPage
    {
        protected string TipInfo = string.Empty;
        protected string TipImg = "/Card/Image/error.png";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string password = Request.Form["password"];
                if(string.IsNullOrEmpty(password))
                {
                    TipInfo = "设置的安全密码不能为空";
                    return;
                }
                AccountsAgentInfo agent = FacadeManage.aideAccountsFacade.GetAccountsAgentInfoByAgentID(userTicket.AgentID);
                if(agent == null || !string.IsNullOrEmpty(agent.Password))
                {
                    TipInfo = "设置的安全密码代理不存在";
                    return;
                }
                int result = FacadeManage.aideAccountsFacade.SetAgentSafePassword(agent.AgentID, Utility.MD5(password));
                if(result > 0)
                {
                    TipImg = "/Card/Image/correct.png";
                    TipInfo = "恭喜您，安全密码设置成功";
                }
                else
                {
                    TipInfo = "抱歉，安全密码设置失败";
                }
            }
        }
    }
}