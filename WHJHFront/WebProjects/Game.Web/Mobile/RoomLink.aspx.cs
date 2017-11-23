using Game.Entity.Accounts;
using Game.Entity.NativeWeb;
using Game.Entity.Platform;
using Game.Facade;
using Game.Utils;
using System;
using System.Collections.Generic;

namespace Game.Web.Mobile
{
    public partial class RoomLink : System.Web.UI.Page
    {
        //公用属性
        protected string Nickname = string.Empty;
        protected string PlatformDownloadUrl = string.Empty;
        protected string KindRule = string.Empty;
        protected string Mobilelogo = string.Empty;
        protected bool Finish = true;

        //页面参数
        protected int Gameid = GameRequest.GetQueryInt("g", 0);

        protected int Roomid = GameRequest.GetQueryInt("r", 0);
        protected int Kindid = GameRequest.GetQueryInt("k", 0);
        protected int Action = GameRequest.GetQueryInt("a", 0);
        protected string Password = GameRequest.GetQueryString("p");
        protected string Type = GameRequest.GetQueryString("y");

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string state = GameRequest.GetQueryString("s");
                if (state == "already")
                {
                    if (Gameid <= 0 || Roomid <= 0 || Kindid <= 0 || Action <= 0)
                    {
                        return;
                    }
                    AccountsInfo info = FacadeManage.aideAccountsFacade.GetAccountsInfoByGameID(Gameid);
                    if (info == null)
                    {
                        return;
                    }
                    Nickname = info.NickName.Length > 9 ? (info.NickName.Substring(0, 6) + "...") : info.NickName;
                    GameRule rule = FacadeManage.aideNativeWebFacade.GetGameRuleInfo(Kindid);
                    if (rule != null)
                    {
                        KindRule = rule.KindIntro;
                    }
                    StreamCreateTableFeeInfo table =
                        FacadeManage.aidePlatformFacade.GetStreamCreateTableFeeInfo(Roomid);
                    if (table == null || table.DissumeDate != null || table.RoomStatus == 2)
                    {
                        Finish = false;
                    }
                    int terminalType = Fetch.GetTerminalType(Page.Request);
                    string imgDomain = string.Empty;
                    IList<ConfigInfo> list = Fetch.GetConfigInfoList();
                    foreach (var item in list)
                    {
                        if (terminalType == 1 && item.ConfigKey ==
                            AppConfig.SiteConfigKey.MobilePlatformVersion.ToString())
                        {
                            PlatformDownloadUrl = item.Field6;
                        }
                        if (terminalType == 2 && item.ConfigKey ==
                            AppConfig.SiteConfigKey.MobilePlatformVersion.ToString())
                        {
                            PlatformDownloadUrl = item.Field5;
                        }
                        if (item.ConfigKey == AppConfig.SiteConfigKey.WebSiteConfig.ToString())
                        {
                            imgDomain = item.Field2;
                        }
                    }
                    Mobilelogo = Fetch.GetUploadFileUrl(imgDomain, "/Site/MobileLogo.png");
                    Title = "约战邀请 - " + AppConfig.PageTitle;
                }
                else
                {
                    if (Gameid <= 0 || Roomid <= 0 || Kindid <= 0 || Action <= 0)
                    {
                        return;
                    }

                    string url =
                        $"/Mobile/WxRegister.aspx?t=82&g={Gameid}&r={Roomid}&k={Kindid}&a={Action}&p={Password}";
                    if (!string.IsNullOrEmpty(Type)) url += "&y=" + Type;
                    Response.Redirect(url);
                }
            }
        }
    }
}
