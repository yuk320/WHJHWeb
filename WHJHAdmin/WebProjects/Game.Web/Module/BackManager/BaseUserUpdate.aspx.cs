using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

using Game.Web.UI;
using Game.Utils;
using Game.Kernel;
using Game.Entity.PlatformManager;
using Game.Entity.Enum;
using Game.Facade;


namespace Game.Web.Module.BackManager
{
    public partial class BaseUserUpdate : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                UserDataBind();
            }
        }
        /// <summary>
        /// 数据保存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string oldLogonPass = Utility.MD5(CtrlHelper.GetText(txtOldLogonPass));
            string oldConfirmLogonPass = CtrlHelper.GetText(hdfOldLogonPass);
            string logonPass = Utility.MD5(CtrlHelper.GetText(txtLogonPass));
            string confirmlogonPass = Utility.MD5(CtrlHelper.GetText(txtConfirmLogonPass));
            if(oldLogonPass != oldConfirmLogonPass)
            {
                MessageBox("原始密码输入错误！");
                return;
            }
            if(string.IsNullOrEmpty(logonPass))
            {
                MessageBox("新密码不能为空！");
                return;
            }
            if(logonPass != confirmlogonPass)
            {
                MessageBox("两次输入的密码不一样");
                return;
            }
            int result = FacadeManage.aidePlatformManagerFacade.ModifyUserLogonPass(IntParam, logonPass);
            if(result > 0)
            {
                MessageBoxClose("密码修改成功");
            }
            else
            {
                MessageBox("密码修改失败");
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void UserDataBind()
        {
            if(IntParam<=0|| userExt==null || userExt.UserID != IntParam)
            {
                Redirect("/Login.aspx");
            }

            //获取用户信息
            Base_Users user = FacadeManage.aidePlatformManagerFacade.GetUserByUserId(IntParam);
            if(user != null)
            {
                CtrlHelper.SetText(lblAccounts, user.Username);
                CtrlHelper.SetText(lblRoleName, GetRoleName(user.UserID) );
                CtrlHelper.SetText(hdfOldLogonPass, user.Password);
            }
        } 
    }
}
