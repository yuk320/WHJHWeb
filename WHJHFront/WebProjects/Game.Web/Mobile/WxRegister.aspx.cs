using Game.Entity.Accounts;
using Game.Facade;
using Game.Utils;
using System;
using Game.Entity.NativeWeb;

namespace Game.Web.Mobile
{
    public partial class WxRegister : System.Web.UI.Page
    {
        //页面参数
        protected int Typeid = GameRequest.GetQueryInt("t", 0);

        protected int Gameid = GameRequest.GetQueryInt("g", 0);
        protected int Roomid = GameRequest.GetQueryInt("r", 0);
        protected int Kindid = GameRequest.GetQueryInt("k", 0);
        protected int Action = GameRequest.GetQueryInt("a", 0);
        protected string Password = GameRequest.GetQueryString("p");
        protected string Wxparam = GameRequest.GetQueryString("w");
        protected string PlatformType = GameRequest.GetQueryString("y");

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if ((Typeid != 81 && Typeid != 82) || Gameid <= 0)
            {
                Response.Redirect("/404.html");
                return;
            }
            if (string.IsNullOrEmpty(Wxparam))
            {
                string param = Typeid == 82
                    ? $"/Mobile/WxRegister.aspx?t=82&g={Gameid}&r={Roomid}&k={Kindid}&a={Action}&p={Password}"
                    : "/Mobile/WxRegister.aspx?t=81&g=" + Gameid;
                if (!string.IsNullOrEmpty(PlatformType))
                {
                    param += $"&y={PlatformType}";
//                    TextLogger.Write($"HAuthorizeUrl：{AppConfig.HAuthorizeURL}?url={Server.UrlEncode(param)}");
//                    Response.Redirect($"{AppConfig.HAuthorizeURL}?url={Server.UrlEncode(param)}");
//                    return;
                }
                Response.Redirect($"{AppConfig.AuthorizeURL}?url={Server.UrlEncode(param)}");
            }
            else
            {
                WxUser wu = Fetch.GetWxUser(Wxparam);
                if (wu == null)
                {
                    Response.Redirect("/404.html");
                    return;
                }
                AccountsInfo info = FacadeManage.aideAccountsFacade.GetAccountsInfoByUserUin(wu.unionid);
                if (info == null)
                {
                    //注册帐号
                    UserInfo user = new UserInfo
                    {
                        NickName = wu.nickname,
                        Gender = wu.sex,
                        RegisterIP = GameRequest.GetUserIP(),
                        GameID = Gameid,
                        UserUin = wu.unionid
                    };
                    FacadeManage.aideAccountsFacade.RegisterWX(user, Typeid, wu.headimgurl);
                }
                string redirect;
                if (PlatformType == "h5")
                {
                    string wxParam = $"<{wu.openid}>,<{wu.unionid}>,<{wu.nickname}>,<{wu.sex}>,<{wu.headimgurl}>";
                    ConfigInfo webCfg = Fetch.GetWebSiteConfig();
                    string w = Server.UrlEncode(Fetch.AESEncrypt(wxParam, AppConfig.WxH5Key,
                        AppConfig.WxH5Key));
                    string baseUrl = "/h5/";
                    if (Typeid == 82)
                    {
                        switch (Kindid)
                        {
                            case 200:
                                baseUrl += "land";
                                break;
                            case 50:
                                baseUrl += "oxsixx";
                                break;
                            default:
                                baseUrl += "hall";
                                break;
                        }
                    }
                    else
                    {
                        baseUrl += "hall";
                    }
                    redirect = baseUrl + (Typeid == 82
                                   ? $"/?w={w}&r={Roomid}&a={Action}&p={Password}"
                                   : $"/?w={w}");
                }
                else
                {
                    redirect = Typeid == 82
                        ? "/Mobile/RoomLink.aspx?" +
                          $"s=already&g={Gameid}&r={Roomid}&k={Kindid}&a={Action}&p={Password}"
                        : $"/Mobile/ShareLink.aspx?s=already&g={Gameid}";
                }
                Response.Redirect(redirect);
            }
        }
    }
}
