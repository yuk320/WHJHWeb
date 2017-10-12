using System;
using System.Web.UI;
using Game.Entity;
using Game.Entity.NativeWeb;
using Game.Entity.PlatformManager;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using Game.Utils.Cache;

namespace Game.Web
{
    public partial class Login : System.Web.UI.Page
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                
            }
        }

        /// <summary>
        /// 登录操作
        /// </summary>
        protected void btnLogin_Click(object sender, ImageClickEventArgs e)
        {
            string verifyCode = CtrlHelper.GetText(txtVerifyCode);
            string accounts = CtrlHelper.GetTextAndFilter(txtLoginName);
            string password = CtrlHelper.GetText(txtLoginPass);

            if(string.IsNullOrEmpty(accounts) || string.IsNullOrEmpty(password))
            {
                Page.ClientScript.RegisterStartupScript(typeof(Page), "", "alert('账号信息错误');", true);
                return;
            }
            if(string.IsNullOrEmpty(verifyCode)||!Fetch.ValidVerifyCodeVer2(verifyCode))
            {
                Page.ClientScript.RegisterStartupScript(typeof(Page), "", "alert('验证码信息错误');", true);
                return;
            }

            Base_Users user = new Base_Users();
            user.Username = accounts;
            user.Password = Utility.MD5(password);
            user.LastLoginIP = GameRequest.GetUserIP();

            Message msg = FacadeManage.aidePlatformManagerFacade.UserLogon(user);
            if(msg.Success)
            {
                LoginUser login = msg.EntityList[0] as LoginUser;
                //缓存账号信息
                Fetch.SaveLoginUser(login);
                //缓存资源信息
                Fetch.SaveLoginResources(login.UserID);
                //页面跳转
                Fetch.Redirect("/Index.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(typeof(Page), "", string.Format("alert('{0}');", msg.Content), true);
                return;
            }
        }
    }
}