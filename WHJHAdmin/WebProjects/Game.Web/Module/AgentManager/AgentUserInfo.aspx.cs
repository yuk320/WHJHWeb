using Game.Entity.Accounts;
using Game.Entity.Enum;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using Game.Web.UI;
using System;

namespace Game.Web.Module.AgentManager
{
    public partial class AgentUserInfo : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 数据保存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            AuthUserOperationPermission(Permission.Add);
            string agentNote = CtrlHelper.GetText(txtAgentNote);
            string compellation = CtrlHelper.GetText(txtCompellation);
            string address = CtrlHelper.GetText(txtContactAddress);
            string phone = CtrlHelper.GetText(txtContactPhone);
            string domain = CtrlHelper.GetText(txtDomain);
            int gameid = CtrlHelper.GetInt(txtGameID, 0);
            int pgameid = CtrlHelper.GetInt(txtParentGameID, 0);
            string qqaccount = CtrlHelper.GetText(txtQQAccount);
            string wxnickname = CtrlHelper.GetText(txtWCNickName);
            int level = Convert.ToInt32(ddlLevel.SelectedValue);

            //判断用户是否存在
            AccountsInfo info = FacadeManage.aideAccountsFacade.GetAccountInfoByGameId(gameid);
            if(info==null || info.UserID <= 0)
            {
                MessageBox("游戏ID无效，请重新输入");
                return;
            }
            if(!string.IsNullOrEmpty(info.Compellation)&&!info.Compellation.Equals(compellation))
            {
                MessageBox("真实姓名与实名认证资料不符");
                return;
            }
            AccountsAgentInfo agent = new AccountsAgentInfo
            {
                AgentDomain = domain,
                AgentLevel = Convert.ToByte(level),
                AgentNote = agentNote,
                Compellation = compellation,
                ContactAddress = address,
                ContactPhone = phone,
                QQAccount = qqaccount,
                WCNickName = wxnickname,
                UserID = info.UserID
            };

            Message msg = FacadeManage.aideAccountsFacade.InsertAgentUser(agent, pgameid);
            if(msg.Success)
            {
                MessageBoxCloseRef(msg.Content);
            }
            else
            {
                MessageBox(msg.Content);
            }
        }

        /// <summary>
        /// 切换代理等级
        /// </summary>
        protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            int level = Convert.ToInt32(ddlLevel.SelectedValue);
            parent.Visible = level > 1;
            if (level > 1) return;
            txtParentGameID.Text = "";
            txtParentNickName.Text = "";
        }
    }
}