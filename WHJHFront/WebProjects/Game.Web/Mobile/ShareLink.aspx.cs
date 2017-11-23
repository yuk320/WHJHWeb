using System;
using Game.Utils;
using Game.Facade;
using Game.Entity.Accounts;
using System.Text.RegularExpressions;

namespace Game.Web.Mobile
{
    public partial class ShareLink : System.Web.UI.Page
    {
        //公用属性
        protected string Nickname = string.Empty;

        protected string Spread = string.Empty;
        protected string Diamond = "0";
        protected string FaceUrl = "/image/face.png";
        protected string PlatformDownloadUrl = string.Empty;

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            string state = GameRequest.GetQueryString("s");
            string type = GameRequest.GetQueryString("y");

            if (state == "already")
            {
                int gameid = GameRequest.GetQueryInt("g", 0);
                if (gameid <= 0)
                {
                    return;
                }

                AccountsInfo info = FacadeManage.aideAccountsFacade.GetAccountsInfoByGameID(gameid);
                if (info == null)
                {
                    return;
                }
                Nickname = info.NickName;
                Spread = gameid.ToString();
                AccountsFace face = FacadeManage.aideAccountsFacade.GetAccountsFace(info.CustomID);
                FaceUrl = face != null ? face.FaceUrl : FaceUrl;

                SystemStatusInfo status =
                    FacadeManage.aideAccountsFacade.GetSystemStatusInfo(AppConfig.ConfigInfoKey.JJBindSpreadPresent
                        .ToString());
                if (status != null)
                {
                    Diamond = status.StatusValue.ToString();
                }
                PlatformDownloadUrl = Fetch.GetDownLoadUrl(Page.Request);
                Title = "推广邀请 - " + AppConfig.PageTitle;
            }
            else
            {
                string domain = Request.Url.Authority;
                int gameid = FacadeManage.aideAccountsFacade.GetGameIDByAgentDomain(domain);
                if (gameid <= 0)
                {
                    string[] domainStr = domain.Split('.');
                    if (domainStr.Length == 3 && Regex.IsMatch(domainStr[0], "^[0-9]*$"))
                    {
                        gameid = Convert.ToInt32(domainStr[0]);
                    }
                    else
                    {
                        gameid = GameRequest.GetQueryInt("g", 0);
                    }
                }
                Response.Redirect("/Mobile/WxRegister.aspx?t=81&g=" + gameid+(!string.IsNullOrEmpty(type)?"&y="+type:""));
            }
        }
    }
}
