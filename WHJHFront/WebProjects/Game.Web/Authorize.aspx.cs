using Game.Facade;
using Game.Web.Helper;
using System;
using System.Web.UI;
using Game.Utils;

namespace Game.Web
{
    public partial class Authorize : Page
    {
        //页面参数
        protected string LinkUrl = GameRequest.GetQueryString("url");

        /// <summary>
        /// 页面加载（微信授权-网站使用）
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (AppConfig.Mode == AppConfig.CodeMode.Dev)
            {
                #region 内部测试

                Random rd = new Random();
                string unionid = Utility.MD5(rd.Next(1000000, 9999999).ToString());
                string nickname = "网站" + rd.Next(10000, 99999).ToString();
                string headimgurl =
                    "http://wx.qlogo.cn/mmopen/ajNVdqHZLLAMfyaTEB7juwPCNXBEC5SNBkgUgLuQjeu8bgcsiaEM77Y1F83qb05w0UjGJkVCqqgrs1EWic1Pmn5LjYYKJxSgRwwXz7iaxia6to0/0";
                if (LinkUrl.IndexOf("Card/Index", StringComparison.Ordinal) > 0)
                {
                    unionid = "o9ZMqwltMdZANFwT5P8FAGS2ZDkk";
                }
                string wxParam = $"<{unionid}>,<{unionid}>,<{nickname}>,<{1}>,<{headimgurl}>";
                LinkUrl = LinkUrl + "&w=" + Server.UrlEncode(Fetch.DESEncrypt(wxParam, AppConfig.WxUrlKey));

                Response.Redirect(LinkUrl);

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

                    if (string.IsNullOrEmpty(LinkUrl))
                    {
                        Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面加载出错，请重试" + "</span>");
                        return;
                    }
                    string wxParam = $"<{openid}>,<{unionid}>,<{nickname}>,<{sex}>,<{headimgurl}>";
                    LinkUrl = LinkUrl + "&w=" + Server.UrlEncode(Fetch.DESEncrypt(wxParam, AppConfig.WxUrlKey));

                    Response.Redirect(LinkUrl);
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

                 Response.Redirect("http://ry.foxuc.net/JJAuthorize.aspx?url=" + Server.UrlEncode(LinkUrl));

                #endregion
            }
        }
    }
}
