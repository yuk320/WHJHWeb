using System;
using System.Collections.Generic;
using System.Web.Configuration;
using Game.Facade;
using Game.Entity.NativeWeb;
using Game.Utils;

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
        protected string MobileQrcode = string.Empty;
        protected string action = string.Empty;
        protected string msg = string.Empty;
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
                    if(terminalType == 1 && item.ConfigKey == AppConfig.SiteConfigKey.MobilePlatformVersion.ToString())
                    {
                        PlatformDownloadUrl = item.Field6;
                    }
                    if(terminalType == 2 && item.ConfigKey == AppConfig.SiteConfigKey.MobilePlatformVersion.ToString())
                    {
                        PlatformDownloadUrl = item.Field5;
                    }
                    if(item.ConfigKey == AppConfig.SiteConfigKey.SysCustomerService.ToString())
                    {
                        Qq = item.Field3;
                        Tel = item.Field1;
                    }
                    if(item.ConfigKey == AppConfig.SiteConfigKey.WebSiteConfig.ToString())
                    {
                        imgDomain = item.Field2;
                        MobileQrcode = Fetch.GetQrCode(item.Field1, 200);
                    }
                }
                Mobilebg = Fetch.GetUploadFileUrl(imgDomain, "/Site/MobileBg.png");
                Mobilelogo = Fetch.GetUploadFileUrl(imgDomain, "/Site/MobileLogo.png");
                Mobiledown = Fetch.GetUploadFileUrl(imgDomain, "/Site/MobileDownLad.png");
                Title = AppConfig.PageTitle;

                action = GameRequest.GetQueryString("action");
                msg = GameRequest.GetQueryString("msg");
            }
        }
    }
}
