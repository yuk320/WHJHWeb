using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using Game.Entity.Accounts;

namespace Game.Web.Card
{
    public partial class AddAgent : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Card.Site.Menu = 0;
        }

        /// <summary>
        /// 保存修改
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int gameid = CtrlHelper.GetInt(txtGameID, 0);
            string Compellation = CtrlHelper.GetText(txtCompellation);
            string QQAccount = CtrlHelper.GetText(txtQQAccount);
            string WCNickName = CtrlHelper.GetText(txtWCNickName);
            string ContactPhone = CtrlHelper.GetText(txtContactPhone);
            string ContactAddress = CtrlHelper.GetText(txtContactAddress);
            string AgentDomain = CtrlHelper.GetText(txtAgentDomain);
            string AgentNote = CtrlHelper.GetText(txtAgentNote);

            if(gameid <= 0)
            {
                ShowInfo("抱歉，添加代理游戏ID不能为空");
                return;
            }
            if(string.IsNullOrEmpty(Compellation))
            {
                ShowInfo("抱歉，真实姓名不能为空");
                return;
            }
            if(string.IsNullOrEmpty(QQAccount))
            {
                ShowInfo("抱歉，QQ账号不能为空");
                return;
            }
            if(string.IsNullOrEmpty(WCNickName))
            {
                ShowInfo("抱歉，微信昵称不能为空");
                return;
            }
            if(string.IsNullOrEmpty(ContactPhone))
            {
                ShowInfo("抱歉，联系电话不能为空");
                return;
            }
            if(!Utils.Validate.IsMobileCode(ContactPhone))
            {
                ShowInfo("抱歉，联系电话格式不正确");
                return;
            }
            if(string.IsNullOrEmpty(ContactAddress))
            {
                ShowInfo("抱歉，联系地址不能为空");
                return;
            }
            if(string.IsNullOrEmpty(AgentDomain))
            {
                ShowInfo("抱歉，代理域名不能为空");
                return;
            }

            AccountsInfo account = FacadeManage.aideAccountsFacade.GetAccountsInfoByGameID(gameid);
            if(account == null)
            {
                ShowInfo("抱歉，添加代理异常，请稍后重试");
                return;
            }
            if(!string.IsNullOrEmpty(account.Compellation)&&!account.Compellation.Equals(Compellation))
            {
                ShowInfo("抱歉，真实姓名与实名认证不一致");
                return;
            }
            if(!account.NickName.Equals(WCNickName))
            {
                ShowInfo("抱歉，微信昵称与真实昵称不一致");
                return;
            }

            AccountsAgentInfo info = new AccountsAgentInfo();
            info.AgentDomain = AgentDomain;
            info.AgentNote = AgentNote;
            info.Compellation = Compellation;
            info.ContactAddress = ContactAddress;
            info.ContactPhone = ContactPhone;
            info.QQAccount = QQAccount;
            info.WCNickName = WCNickName;

            Message msg = FacadeManage.aideAccountsFacade.InsertAgentUser(userTicket.UserID, info, gameid);
            if(msg.Success)
            {
                Response.Redirect("/Card/Success.aspx?t=1001");
            }
            else
            {
                ShowInfo(msg.Content);
            }
        }
    }
}