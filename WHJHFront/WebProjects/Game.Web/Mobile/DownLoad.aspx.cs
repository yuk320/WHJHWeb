using Game.Facade;
using System;

namespace Game.Web.Mobile
{
    public partial class DownLoad : System.Web.UI.Page
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            string platformDownloadUrl = Fetch.GetDownLoadUrl(Page.Request);
            if(!string.IsNullOrEmpty(platformDownloadUrl))
            {
                Response.Redirect(platformDownloadUrl);
            }
        }
    }
}