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
    public partial class BaseUserInfo : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                GetUserRoleList();
                GameUserDataBind();
            }
        }
        /// <summary>
        /// 数据保存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Base_Users user = new Base_Users();
            if(IntParam > 0)
            {
                AuthUserOperationPermission(Permission.Edit);
                user = FacadeManage.aidePlatformManagerFacade.GetUserByUserId(IntParam);
            }
            else
            {
                AuthUserOperationPermission(Permission.Add);
            }
            user.Username = CtrlHelper.GetText(txtAccounts);
            if(hidfLogonPass.Value.Trim() == "********")
            {
                user.Password = Utility.MD5(txtLogonPass.Text.Trim());
            }
            else
            {
                user.Password = hidfLogonPass.Value.Trim();
            }
            user.RoleID = Convert.ToInt32(ddlRole.SelectedValue.Trim());

            int result = IntParam>0? FacadeManage.aidePlatformManagerFacade.ModifyUserInfo(user): FacadeManage.aidePlatformManagerFacade.RegisterUser(user);
            if(result > 0)
            {
                ShowInfo("用户信息操作成功", "BaseUserList.aspx", 1200);
            }
            else
            {
                ShowError("用户信息操作失败");
            }
        }
        /// <summary>
        /// 角色列表绑定
        /// </summary>
        private void GetUserRoleList()
        {
            PagerSet pagerSet = FacadeManage.aidePlatformManagerFacade.GetList(Base_Roles.Tablename, 1, 100, "", "ORDER BY RoleID ASC");
            ddlRole.DataSource = pagerSet.PageSet;
            ddlRole.DataTextField = "RoleName";
            ddlRole.DataValueField = "RoleID";
            ddlRole.DataBind();
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void GameUserDataBind()
        {
            if(IntParam > 0)
            {
                Base_Users user = FacadeManage.aidePlatformManagerFacade.GetUserByUserId(IntParam);
                if(user != null)
                {
                    CtrlHelper.SetText(txtAccounts, user.Username);
                    txtLogonPass.Attributes.Add("value", "********");
                    txtConfirmPass.Attributes.Add("value", "********");
                    CtrlHelper.SetText(hidfLogonPass, user.Password);
                    ddlRole.SelectedValue = user.RoleID.ToString().Trim();
                }
            }
        }
    }
}
