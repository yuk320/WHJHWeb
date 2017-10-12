using System;
using Game.Facade;
using Game.Utils;

namespace Game.Web
{
    public partial class HAuthorize : System.Web.UI.Page
    {
        protected string LinkUrl = GameRequest.GetQueryString("url");

        /// <summary>
        /// 页面加载（微信授权-H5使用）
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {

            if(!IsPostBack)
            {
#if DEBUG
                #region 内部测试
                Random rd = new Random();
                string unionid = "59C6CEA7FBAB5A19C3177E1216BBACF6";
                string nickname = "网站47033";
                string headimgurl = "http://wx.qlogo.cn/mmopen/ajNVdqHZLLAMfyaTEB7juwPCNXBEC5SNBkgUgLuQjeu8bgcsiaEM77Y1F83qb05w0UjGJkVCqqgrs1EWic1Pmn5LjYYKJxSgRwwXz7iaxia6to0/0";

                string wxParam = string.Format("<{0}>,<{1}>,<{2}>,<{3}>,<{4}>",
                    unionid, unionid, nickname, 1, headimgurl);

                string LinkUrl = "http://172.16.0.211:6566/develop/majiang/index.html" + "?w=" + Server.UrlEncode(Fetch.AESEncrypt(wxParam, AppConfig.WxH5Key, AppConfig.WxH5Key));

                Response.Redirect(LinkUrl);
                #endregion
#else
                #region 客户版本
//                            WxAuthorize jsApiDown = new WxAuthorize(this);
//                            try
//                            {
//                                jsApiDown.GetOpenidAndAccessToken();
//                                jsApiDown.GetUserInfo();
//
//                                string openid = jsApiDown.openid;
//                                string unionid = jsApiDown.unionid;
//                                string nickname = jsApiDown.nickname;
//                                int sex = jsApiDown.sex;
//                                string headimgurl = jsApiDown.headimgurl;
//
//                                string wxParam = string.Format("<{0}>,<{1}>,<{2}>,<{3}>,<{4}>",
//                                    openid, unionid, nickname, sex, headimgurl);
//                                ConfigInfo config = Fetch.GetWebSiteConfig();
//                                string LinkUrl = (config != null ? config.Field4 : "") + "?w=" + Server.UrlEncode(Fetch.AESEncrypt(wxParam, AppConfig.WxH5Key, AppConfig.WxH5Key));
//
//                                Response.Redirect(LinkUrl);
//                                return;
//                            }
//                            catch(Exception ex)
//                            {
//                                Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面加载出错，请重试" + "</span>");
//                            }
                #endregion

                #region 演示版本
                                Response.Redirect("http://ry.foxuc.net/JJHAuthorize.aspx?url="+LinkUrl);
                #endregion
#endif
            }
        }
    }
}