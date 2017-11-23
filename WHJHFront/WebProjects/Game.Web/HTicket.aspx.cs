using Game.Utils;
using System;
using System.Web.Script.Serialization;
using Game.Facade;
using Game.Facade.DataStruct;
using Game.Facade.Enum;
using Game.Utils.Cache;
using Game.Web.Helper;

namespace Game.Web
{
    public partial class HTicket : System.Web.UI.Page
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (AppConfig.Mode == AppConfig.CodeMode.Production)
                {
                    #region 客户版本

                    Response.AddHeader("Access-Control-Allow-Origin", AppConfig.MoblieInterfaceDomain);
                    string time = GameRequest.GetQueryString("time");
                    string sign = GameRequest.GetQueryString("sign");
                    //签名验证
                    AjaxJsonValid ajv = Fetch.VerifySignData((AppConfig.MoblieInterfaceKey + time), sign);
                    if(ajv.code == (int)ApiCode.VertySignErrorCode)
                    {
                        Response.Write(ajv.SerializeToJson());
                        return;
                    }
                    object obj = WHCache.Default.Get<AspNetCache>(AppConfig.WxTicket);
                    TicketCache tc = obj as TicketCache;
                    if(tc == null)
                    {
                        try
                        {
                            string url =
                                $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={WxAuthorize.Appid}&secret={WxAuthorize.Appsecret}";
                            string result = WxHttpService.Get(url);
                            JavaScriptSerializer js = new JavaScriptSerializer();
                            AccessToken at = js.Deserialize<AccessToken>(result);
                            if(at.errcode > 0)
                            {
                                ajv.msg = at.errmsg;
                                Response.Write(ajv.SerializeToJson());
                                return;
                            }

                            url =
                                $"https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={at.access_token}&type=jsapi";
                            result = WxHttpService.Get(url);
                            JsapiTicket jt = js.Deserialize<JsapiTicket>(result);
                            if(jt.errcode > 0)
                            {
                                ajv.msg = at.errmsg;
                                Response.Write(ajv.SerializeToJson());
                                return;
                            }

                            tc = new TicketCache();
                            tc.access_token = at.access_token;
                            tc.ticket = jt.ticket;
                            int timeout = (at.expires_in / 60) - 3;
                            WHCache.Default.Save<AspNetCache>(AppConfig.WxTicket, tc, timeout);
                        }
                        catch(Exception)
                        {
                            Response.Write(ajv.SerializeToJson());
                            return;
                        }
                    }
                    ajv.SetValidDataValue(true);
                    ajv.SetDataItem("access_token", tc.access_token);
                    ajv.SetDataItem("ticket", tc.ticket);
                    Response.Write(ajv.SerializeToJson());

                    #endregion
                }
                else
                {
                    #region 演示版本

                    string time = GameRequest.GetQueryString("time");
                    string sign = GameRequest.GetQueryString("sign");
                    Response.Redirect("http://ry.foxuc.net/JJTicket.aspx?time=" + time + "&sign=" + sign);

                    #endregion
                }
            }
        }
    }
}