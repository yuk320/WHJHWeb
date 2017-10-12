using System;

using Game.Facade;
using Game.Entity.PlatformManager;
using Game.Utils;

namespace Game.Web
{
    public partial class Index : System.Web.UI.Page
    {
        public string SiteTitle;
        /// <summary>
        /// 登录账号
        /// </summary>
        protected LoginUser userExt;
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
#if DEBUG
            SiteTitle = "本地精华版";
#else
            try {
                SiteTitle = ApplicationSettings.Get("SiteTitle");
            }
            catch {
                SiteTitle = "演示平台精华版";
            }
#endif

            userExt = Fetch.GetLoginUser();
            if(userExt==null || userExt.UserID <= 0)
            {
                Response.Redirect("/Login.aspx");
            }
        }
    }
}