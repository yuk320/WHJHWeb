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

namespace Game.Web.Card
{
    public partial class UpdatePass : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Card.Site.Menu = 0;
            if(!IsPostBack)
            {
                AccountsAgentInfo agent = FacadeManage.aideAccountsFacade.GetAccountsAgentInfoByAgentID(userTicket.AgentID);
                if(agent == null)
                {
                    Response.Redirect("/Card/SignOut.aspx");
                    return;
                }
                if(string.IsNullOrEmpty(agent.Password))
                {
                    oldpassword.Visible = false;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AccountsAgentInfo agent = FacadeManage.aideAccountsFacade.GetAccountsAgentInfoByAgentID(userTicket.AgentID);

            //获取数据
            string password = CtrlHelper.GetText(txtLoginPass);
            string newpassword = CtrlHelper.GetText(txtNewPass);
            string repassword = CtrlHelper.GetText(txtRePass);

            //数据验证
            if(!string.IsNullOrEmpty(agent.Password))
            {
                if(string.IsNullOrEmpty(password))
                {
                    ShowInfo("抱歉，原密码不能为空");
                    return;
                }
                if(agent.Password != Utility.MD5(password))
                {
                    ShowInfo("抱歉，原密码输入错误");
                    return;
                }
            }

            if(string.IsNullOrEmpty(newpassword))
            {
                ShowInfo("抱歉，新密码不能为空");
                return;
            }
            if(newpassword != repassword)
            {
                ShowInfo("抱歉，两次密码输入不一致");
                return;
            }

            //执行修改操作
            int result = FacadeManage.aideAccountsFacade.SetAgentSafePassword(agent.AgentID, Utility.MD5(newpassword));
            if(result > 0)
            {
                Response.Redirect("/Card/Success.aspx?t=1003");
            }
            else
            {
                ShowInfo("抱歉，修改失败，请稍后重试");
            }
        }
    }
}