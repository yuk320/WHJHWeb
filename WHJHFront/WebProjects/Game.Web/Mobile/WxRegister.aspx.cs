using Game.Entity.Accounts;
using Game.Facade;
using Game.Utils;
using System;

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

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if((Typeid!= 81&&Typeid!=82) || Gameid <= 0)
            {
                Response.Redirect("/404.html");
                return;
            }
            if(string.IsNullOrEmpty(Wxparam))
            {
                string param = Typeid == 82 ? $"/Mobile/WxRegister.aspx?t=82&g={Gameid}&r={Roomid}&k={Kindid}&a={Action}&p={Password}"
                    : "/Mobile/WxRegister.aspx?t=81&g=" + Gameid;

                Response.Redirect(AppConfig.AuthorizeURL + "?url=" + Server.UrlEncode(param));
            }
            else
            {
                WxUser wu = Fetch.GetWxUser(Wxparam);
                if(wu == null)
                {
                    Response.Redirect("/404.html");
                    return;
                }
                AccountsInfo info = FacadeManage.aideAccountsFacade.GetAccountsInfoByUserUin(wu.unionid);
                if(info == null)
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

                string redirect = Typeid == 82 ? "/Mobile/RoomLink.aspx?" +
                                                 $"s=already&g={Gameid}&r={Roomid}&k={Kindid}&a={Action}&p={Password}" : "/Mobile/ShareLink.aspx?s=already&g=" + Gameid.ToString();
                Response.Redirect(redirect);
            }
        }
    }
}