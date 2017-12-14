using System;
using Game.Entity.NativeWeb;
using Game.Facade;
using Game.Utils;
using Game.Web.Helper;

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
            if (IsPostBack) return;

            if (AppConfig.Mode == AppConfig.CodeMode.Dev)
            {
                #region 内部测试

                string unionid = "59C6CEA7FBAB5A19C3177E1216BBACF6";
                string nickname = "网站47033";
                string headimgurl =
                    "http://wx.qlogo.cn/mmopen/ajNVdqHZLLAMfyaTEB7juwPCNXBEC5SNBkgUgLuQjeu8bgcsiaEM77Y1F83qb05w0UjGJkVCqqgrs1EWic1Pmn5LjYYKJxSgRwwXz7iaxia6to0/0";

                string wxParam = $"<{unionid}>,<{unionid}>,<{nickname}>,<{1}>,<{headimgurl}>";

                string url = "http://172.16.0.211:6566/develop/majiang/index.html" + "?w=" +
                             Server.UrlEncode(Fetch.AESEncrypt(wxParam, AppConfig.WxH5Key, AppConfig.WxH5Key));

                Response.Redirect(url);

                #endregion
            }
            else if (AppConfig.Mode == AppConfig.CodeMode.Production)
            {
                #region 客户版本

                WxAuthorize jsApiDown = new WxAuthorize(this);
                try
                {
                    jsApiDown.GetOpenidAndAccessToken();
                    jsApiDown.GetUserInfo();

                    string openid = jsApiDown.Openid;
                    string unionid = jsApiDown.Unionid;
                    string nickname = jsApiDown.Nickname;
                    int sex = jsApiDown.Sex;
                    string headimgurl = jsApiDown.Headimgurl;

                    string wxParam = $"<{openid}>,<{unionid}>,<{nickname}>,<{sex}>,<{headimgurl}>";
                    ConfigInfo config = Fetch.GetWebSiteConfig();
                    string url = (config != null ? config.Field4 : "/h5/") + "?w=" +
                                 Server.UrlEncode(Fetch.AESEncrypt(wxParam, AppConfig.WxH5Key,
                                     AppConfig.WxH5Key));

                    Response.Redirect(url);
                }
                catch (Exception)
                {
                    Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面加载出错，请重试" + "</span>");
                }

                #endregion
            }
            else if (AppConfig.Mode == AppConfig.CodeMode.Demo)
            {
                #region 演示版本

                Response.Redirect("http://ry.foxuc.net/JJHAuthorize.aspx?url=" + (LinkUrl.Equals("")?"http://jh.foxuc.net/h5/hall/":LinkUrl));

                #endregion
            }
        }
    }
}
