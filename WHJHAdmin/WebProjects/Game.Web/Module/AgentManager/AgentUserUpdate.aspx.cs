using Game.Entity.Accounts;
using Game.Entity.NativeWeb;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using Game.Web.UI;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Entity.Enum;

namespace Game.Web.Module.AgentManager
{
    public partial class AgentUserUpdate : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(IntParam > 0)
                {
                    AccountsAgentInfo info = FacadeManage.aideAccountsFacade.GetAccountsAgentInfo(IntParam);
                    if(info != null)
                    {
                        AccountsInfo accounts = FacadeManage.aideAccountsFacade.GetAccountInfoByUserId(info.UserID);

                        CtrlHelper.SetText(txtAgentNote, info.AgentNote);
                        CtrlHelper.SetText(txtCompellation, info.Compellation);
                        CtrlHelper.SetText(txtContactAddress, info.ContactAddress);
                        CtrlHelper.SetText(txtContactPhone, info.ContactPhone);
                        CtrlHelper.SetText(txtDomain, info.AgentDomain);
                        CtrlHelper.SetText(txtQQAccount, info.QQAccount);
                        CtrlHelper.SetText(txtWCNickName, accounts.NickName);
                        ddlLevel.SelectedValue = info.AgentLevel.ToString();
                    }
                }
            }
        }
        /// <summary>
        /// 数据保存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            AuthUserOperationPermission(Permission.Edit);
            if(IntParam > 0)
            {
                AccountsAgentInfo info = FacadeManage.aideAccountsFacade.GetAccountsAgentInfo(IntParam);
                if(info != null)
                {
                    info.AgentNote = CtrlHelper.GetText(txtAgentNote);
                    info.Compellation = CtrlHelper.GetText(txtCompellation);
                    info.ContactAddress = CtrlHelper.GetText(txtContactAddress);
                    info.ContactPhone = CtrlHelper.GetText(txtContactPhone);
                    info.AgentDomain = CtrlHelper.GetText(txtDomain);
                    info.QQAccount = CtrlHelper.GetText(txtQQAccount);
                    info.WCNickName = CtrlHelper.GetText(txtWCNickName);
                    info.AgentLevel = Convert.ToByte(ddlLevel.SelectedValue);

                    AccountsInfo accounts = FacadeManage.aideAccountsFacade.GetAccountInfoByUserId(info.UserID);
                    if(accounts == null || accounts.UserID <= 0)
                    {
                        MessageBox("代理信息异常，请联系管理员");
                        return;
                    }
                    if(!string.IsNullOrEmpty(accounts.Compellation) && !accounts.Compellation.Equals(info.Compellation))
                    {
                        MessageBox("真实姓名与实名认证资料不符");
                        return;
                    }
                    if(!string.IsNullOrEmpty(accounts.NickName) && !accounts.NickName.Equals(info.WCNickName))
                    {
                        MessageBox("微信昵称与真实昵称不符");
                        return;
                    }

                    int result = FacadeManage.aideAccountsFacade.UpdateAgentUser(info);
                    if(result > 0)
                    {
                        MessageBoxCloseRef("修改成功");
                    }
                    else
                    {
                        MessageBox("修改失败");
                    }
                }
            }
        }
    }
}