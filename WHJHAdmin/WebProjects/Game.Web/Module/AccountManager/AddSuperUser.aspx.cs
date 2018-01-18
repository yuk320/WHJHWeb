using Game.Entity.Accounts;
using Game.Entity.Enum;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using Game.Web.UI;
using System;

namespace Game.Web.Module.AccountManager
{
    public partial class AddSuperUser : AdminPage
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
            AccountsInfo info = new AccountsInfo()
            {
                Accounts = CtrlHelper.GetText(txtAccounts),
                LogonPass = Utility.MD5(CtrlHelper.GetText(txtPassword)),
                UserRight = CtrlHelper.GetInt(txtGrantGold,0)
            };

            Message msg = FacadeManage.aideAccountsFacade.InsertSuperUser(info);
            if(msg.Success)
            {
                MessageBoxCloseRef(msg.Content);
            }
            else
            {
                MessageBox(msg.Content);
            }
        }
    }
}