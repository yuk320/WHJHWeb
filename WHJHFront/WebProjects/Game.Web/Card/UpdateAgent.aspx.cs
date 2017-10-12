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
    public partial class UpdateAgent : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Card.Site.Menu = 0;
            if(!IsPostBack)
            {
                AccountsAgentInfo agent = FacadeManage.aideAccountsFacade.GetAccountsAgentInfoByAgentID(userTicket.AgentID);
                if(agent != null)
                {
                    CtrlHelper.SetText(txtAddress, agent.ContactAddress);
                    CtrlHelper.SetText(txtPhone, agent.ContactPhone);
                    CtrlHelper.SetText(txtQQAccount, agent.QQAccount);
                }
            }
        }

        /// <summary>
        /// 保存修改
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //获取修改数据
            string phone = CtrlHelper.GetText(txtPhone);
            string address = CtrlHelper.GetText(txtAddress);
            string qqaccount = CtrlHelper.GetText(txtQQAccount);

            //数据验证
            if(string.IsNullOrEmpty(phone))
            {
                ShowInfo("抱歉，手机号码不能为空");
                return;
            }
            if(string.IsNullOrEmpty(address))
            {
                ShowInfo("抱歉，联系地址不能为空");
                return;
            }
            if(string.IsNullOrEmpty(qqaccount))
            {
                ShowInfo("抱歉，QQ账号不能为空");
                return;
            }

            //执行修改
            AccountsAgentInfo agent = new AccountsAgentInfo();
            agent.ContactAddress = address;
            agent.ContactPhone = phone;
            agent.QQAccount = qqaccount;
            agent.UserID = userTicket.UserID;

            int result = FacadeManage.aideAccountsFacade.UpdateAgentInfo(agent);
            if(result > 0)
            {
                Response.Redirect("/Card/Success.aspx?t=1002");
            }
            else
            {
                ShowInfo("抱歉，修改失败，请稍后重试");
            }
        }
    }
}