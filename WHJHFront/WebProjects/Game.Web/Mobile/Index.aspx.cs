using System;
using System.Collections.Generic;
using Game.Facade;
using Game.Entity.NativeWeb;

namespace Game.Web.Mobile
{
    public partial class Index : System.Web.UI.Page
    {
        //公用属性
        protected string Tel = string.Empty;
        protected string Qq = string.Empty;
        protected string PlatformDownloadUrl = string.Empty;
        protected string Mobilebg = string.Empty;
        protected string Mobilelogo = string.Empty;
        protected string Mobiledown = string.Empty;

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                int terminalType = Fetch.GetTerminalType(Page.Request);
                string imgDomain = string.Empty;
                IList<ConfigInfo> list = Fetch.GetConfigInfoList();
                foreach(var item in list)
                {
                    if(terminalType == 1 && item.ConfigKey == AppConfig.SiteConfigKey.GameAndroidConfig.ToString())
                    {
                        PlatformDownloadUrl = item.Field1;
                    }
                    if(terminalType == 2 && item.ConfigKey == AppConfig.SiteConfigKey.GameIosConfig.ToString())
                    {
                        PlatformDownloadUrl = item.Field1;
                    }
                    if(item.ConfigKey == AppConfig.SiteConfigKey.SysCustomerService.ToString())
                    {
                        Qq = item.Field3;
                        Tel = item.Field1;
                    }
                    if(item.ConfigKey == AppConfig.SiteConfigKey.WebSiteConfig.ToString())
                    {
                        imgDomain = item.Field2;
                    }
                }
                Mobilebg = Fetch.GetUploadFileUrl(imgDomain, "/Site/MobileBg.png");
                Mobilelogo = Fetch.GetUploadFileUrl(imgDomain, "/Site/MobileLogo.png");
                Mobiledown = Fetch.GetUploadFileUrl(imgDomain, "/Site/MobileDownLad.png");
                Title = AppConfig.PageTitle;
            }
        }
    }
}