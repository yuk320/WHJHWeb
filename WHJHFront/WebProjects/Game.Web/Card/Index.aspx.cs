using Game.Entity.Accounts;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using System;

namespace Game.Web.Card
{
    public partial class Index : System.Web.UI.Page
    {
        protected string wxparam = GameRequest.GetQueryString("w");

        protected int version = FacadeManage.aideAccountsFacade
                                    .GetSystemStatusInfo(AppConfig.ConfigInfoKey.AgentHomeVersion.ToString())
                                    ?.StatusValue ?? 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (AppConfig.Mode != AppConfig.CodeMode.Dev)
                {
                    if (Fetch.isWeChat(Request))
                    {
                        //演示和通用平台
                        if (string.IsNullOrEmpty(wxparam))
                        {
//                        string domain = "http://" + (string.IsNullOrEmpty(AppConfig.FrontSiteDomain)
//                                            ? GameRequest.GetCurrentFullHost()
//                                            : AppConfig.FrontSiteDomain);
                            Response.Redirect(AppConfig.AuthorizeURL + "?url=http://" +
                                              GameRequest.GetCurrentFullHost() + "/Card/Index.aspx?code=1001");
                        }
                        else
                        {
                            WxUser wu = Fetch.GetWxUser(wxparam);
                            if (wu == null)
                            {
                                Response.Write(
                                    "<div style=\"font-size:1.2rem; color:red; text-align:center; margin-top:3rem;\">参数异常，请稍后尝试。</div>");
                                return;
                            }
                            Message msg = FacadeManage.aideAccountsFacade.WXLogin(wu.unionid, GameRequest.GetUserIP());
                            if (msg.Success)
                            {
                                UserInfo ui = msg.EntityList[0] as UserInfo;
                                if (ui != null)
                                {
                                    if (version == 1)
                                    {
                                        // for Version 1.0 跳转
                                        Fetch.SetUserCookie(ui.ToUserTicketInfo());
                                        Response.Redirect("Card/AgentInfo.aspx");
                                    }
                                    else if (version == 2)
                                    {
                                        //for Version 2.0 跳转
                                        string token = Fetch.SHA256Encrypt(
                                            $"<{ui.UserID}>,<{ui.AgentID}>,<{ui.GameID}>,<{Fetch.ConvertDateTimeToUnix(DateTime.Now)}>");
                                        FacadeManage.aideNativeWebFacade.SaveAgentToken(ui, token);
                                        Response.Redirect($"v2/#/?token={token}");
                                    }
                                }
                                else
                                {
                                    Response.Write(
                                        "<div style=\"font-size:1.2rem; color:red; text-align:center; margin-top:3rem;\">登录失败，请稍后尝试</div>");
                                }
                            }
                            else
                            {
                                Response.Write(
                                    "<div style=\"font-size:1.2rem; color:red; text-align:center; margin-top:3rem;\">" +
                                    wu.nickname + "，" +
                                    msg.Content + "</div>");
                            }
                        }
                    }
                    else
                    {
                        if (version == 1)
                            // for Version 1.0 非微信提示
                            Response.Write(
                                "<div style=\"font-size:1.2rem; color:red; text-align:center; margin-top:3rem;\">请在微信内打开</div>");
                        else if (version == 2)
                            // for Version 2.0 跳转到手机+安全密码登录页面
                            Response.Redirect("v2/#/Login");
                    }
                }
            }
        }

        protected void btnAuth_OnClick(object sender, EventArgs e)
        {
            if (AppConfig.Mode == AppConfig.CodeMode.Dev)
            {
                if (version == 1)
                {
                    #region Version 1.0 Dev

                    AccountsInfo info =
                        FacadeManage.aideAccountsFacade.GetAccountsInfoByGameID(Convert.ToInt32(txtGameID.Text));
                    Message msg =
                        FacadeManage.aideAccountsFacade.WXLogin(info != null ? info.UserUin : "yryr",
                            GameRequest.GetUserIP());
                    if (msg.Success)
                    {
                        UserInfo ui = msg.EntityList[0] as UserInfo;
                        if (ui != null)
                        {
                            Fetch.SetUserCookie(ui.ToUserTicketInfo());
                            Response.Redirect("/Card/AgentInfo.aspx");
                        }
                        else
                        {
                            Response.Write(
                                "<div style=\"font-size:1.2rem; color:red; text-align:center; margin-top:3rem;\">登录失败，请稍后尝试</div>");
                        }
                    }
                    else
                    {
                        Response.Write(
                            "<div style=\"font-size:1.2rem; color:red; text-align:center; margin-top:3rem;\">" +
                            msg.Content + "</div>");
                    }

                    #endregion
                }
                else if (version == 2)
                {
                    #region Version 2.0 Dev

                    string mobile = CtrlHelper.GetText(txtPhone);
                    string pass = Utility.MD5(CtrlHelper.GetText(txtPassword));
                    Message msg =
                        FacadeManage.aideAccountsFacade.AgentMobileLogin(mobile, pass, GameRequest.GetUserIP());
                    if (msg.Success)
                    {
                        UserInfo info = msg.EntityList[0] as UserInfo;
                        if (info != null)
                        {
                            string token =
                                Fetch.SHA256Encrypt(
                                    $"<{info.UserID}>,<{info.AgentID}>,<{info.GameID}>,<{Fetch.ConvertDateTimeToUnix(DateTime.Now)}>");

                            FacadeManage.aideNativeWebFacade.SaveAgentToken(info, token);
                            Response.Redirect($"v2/#/?token={token}");
                            return;
                        }
                    }
                    else
                    {
                        Response.Write(
                            $"<div style=\"font-size:1.2rem; color:red; text-align:center; margin-top:3rem;\">{msg.Content}</div>");
                        return;
                    }

                    Response.Write(
                        "<div style=\"font-size:1.2rem; color:red; text-align:center; margin-top:3rem;\">账号或密码错误。</div>");

                    #endregion
                }
            }
        }
    }
}
