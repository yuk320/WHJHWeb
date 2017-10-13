using Game.Entity.Accounts;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Card
{
    public partial class Index : System.Web.UI.Page
    {
        protected string wxparam = GameRequest.GetQueryString("w");

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
#if !DEBUG
                //演示和通用平台
//                if(string.IsNullOrEmpty(wxparam))
//                {
//                    string domain = "http://"+ (string.IsNullOrEmpty(AppConfig.FrontSiteDomain)
//                    ? GameRequest.GetCurrentFullHost()
//                    : AppConfig.FrontSiteDomain);
//                    Response.Redirect(domain+AppConfig.AuthorizeURL + "?url=/Card/Index.aspx?code=1001");
//                    return;
//                }
//                else
//                {
//                    WxUser wu = Fetch.GetWxUser(wxparam);
//                    if(wu == null)
//                    {
//                        Response.Write("<div style=\"font-size:1.2rem; color:red; text-align:center; margin-top:3rem;\">参数异常，请稍后尝试。</div>");
//                        return;
//                    }
//                    Message msg = FacadeManage.aideAccountsFacade.WXLogin(wu.unionid, GameRequest.GetUserIP());
//                    if(msg.Success)
//                    {
//                        UserInfo ui = msg.EntityList[0] as UserInfo;
//                        if(ui != null)
//                        {
//                            Fetch.SetUserCookie(ui.ToUserTicketInfo());
//                            Response.Redirect("/Card/AgentInfo.aspx");
//                        }
//                        else
//                        {
//                            Response.Write("<div style=\"font-size:1.2rem; color:red; text-align:center; margin-top:3rem;\">登录失败，请稍后尝试</div>");
//                            return;
//                        }
//                    }
//                    else
//                    {
//                        Response.Write("<div style=\"font-size:1.2rem; color:red; text-align:center; margin-top:3rem;\">" + msg.Content + "</div>");
//                        return;
//                    }
//                }
#endif
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            AccountsInfo info = FacadeManage.aideAccountsFacade.GetAccountsInfoByGameID(Convert.ToInt32(TextBox1.Text));
            Message msg = FacadeManage.aideAccountsFacade.WXLogin(info != null ? info.UserUin : "yryr", GameRequest.GetUserIP());
            if(msg.Success)
            {
                UserInfo ui = msg.EntityList[0] as UserInfo;
                if(ui != null)
                {
                    Fetch.SetUserCookie(ui.ToUserTicketInfo());
                    Response.Redirect("/Card/AgentInfo.aspx");
                }
                else
                {
                    Response.Write("<div style=\"font-size:1.2rem; color:red; text-align:center; margin-top:3rem;\">登录失败，请稍后尝试</div>");
                    return;
                }
            }
            else
            {
                Response.Write("<div style=\"font-size:1.2rem; color:red; text-align:center; margin-top:3rem;\">" + msg.Content + "</div>");
                return;
            }
        }
    }
}