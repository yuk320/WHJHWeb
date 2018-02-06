using Game.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Game.Web.Helper
{
    public class WxAuthorize
    {
        //公众账号ID
        public static string Appid => ApplicationSettings.Get("WXNATIVEAPPID");

        //公众帐号secert
        public static string Appsecret => ApplicationSettings.Get("WXNATIVESECRET");

        /// <summary>
        /// 保存页面对象，因为要在类的方法中使用Page的Request对象
        /// </summary>
        private Page Page { get; }

        /// <summary>
        /// openid用于调用统一下单接口
        /// </summary>
        public string Openid { get; set; }

        /// <summary>
        /// unionid用户应用帐号互通
        /// </summary>
        public string Unionid { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 微信头像地址
        /// </summary>
        public string Headimgurl { get; set; }

        /// <summary>
        /// access_token用于获取收货地址js函数入口参数
        /// </summary>
        public string AccessToken { get; set; }

        public WxAuthorize(Page page)
        {
            Page = page;
        }

        /**
        * 
        * 网页授权获取用户基本信息的全部过程
        * 详情请参看网页授权获取用户基本信息：http://mp.weixin.qq.com/wiki/17/c0f37d5704f0b64713d5d2c37b468d75.html
        * 第一步：利用url跳转获取code
        * 第二步：利用code去获取openid和access_token
        * 
        */
        public void GetOpenidAndAccessToken()
        {
            if(!string.IsNullOrEmpty(Page.Request.QueryString["code"]))
            {
                //获取code码，以获取openid和access_token
                string code = Page.Request.QueryString["code"];
                GetOpenidAndAccessTokenFromCode(code);
            }
            else
            {
                //构造网页授权获取code的URL
                string host = Page.Request.Url.Host;
                string path = Page.Request.Path;
                int gameId = GameRequest.GetQueryInt("g", 0);
                string link = GameRequest.GetQueryString("url");
                string redirectUri = HttpUtility.UrlEncode("http://" + host + path + "?g=" + gameId.ToString() + "&url=" + Page.Server.UrlEncode(link));
                SortedDictionary<string, object> dic =
                    new SortedDictionary<string, object>
                    {
                        {"appid", Appid},
                        {"redirect_uri", redirectUri},
                        {"response_type", "code"},
                        {"scope", "snsapi_userinfo"},
                        {"state", "STATE" + "#wechat_redirect"}
                    };
                string url = "https://open.weixin.qq.com/connect/oauth2/authorize?" + ToUrl(dic);
                try
                {
                    //触发微信返回code码         
                    Page.Response.Redirect(url);//Redirect函数会抛出ThreadAbortException异常，不用处理这个异常
                }
                catch
                {
                    // ignored
                }
            }
        }

        /**
	    * 
	    * 通过code换取网页授权access_token和openid的返回数据，正确时返回的JSON数据包如下：
	    * {
	    *  "access_token":"ACCESS_TOKEN",
	    *  "expires_in":7200,
	    *  "refresh_token":"REFRESH_TOKEN",
	    *  "openid":"OPENID",
	    *  "scope":"SCOPE",
	    *  "unionid": "o6_bmasdasdsad6_2sgVt7hMZOPfL"
	    * }
	    * 其中access_token可用于获取共享收货地址
	    * openid是微信支付jsapi支付接口统一下单时必须的参数
        * 更详细的说明请参考网页授权获取用户基本信息：http://mp.weixin.qq.com/wiki/17/c0f37d5704f0b64713d5d2c37b468d75.html
        * @失败时抛异常WxPayException
	    */
        public void GetOpenidAndAccessTokenFromCode(string code)
        {
            //构造获取openid及access_token的url
            SortedDictionary<string, object> dic =
                new SortedDictionary<string, object>
                {
                    {"appid", Appid},
                    {"secret", Appsecret},
                    {"code", code},
                    {"grant_type", "authorization_code"}
                };
            string url = "https://api.weixin.qq.com/sns/oauth2/access_token?" + ToUrl(dic);

            //请求url以获取数据
            string result = WxHttpService.Get(url);

            //保存access_token，用于收货地址获取
            JObject jd = (JObject)JsonConvert.DeserializeObject(result);
            AccessToken = (string)jd["access_token"];

            //获取用户openid
            Openid = (string)jd["openid"];
            //获取用户unionid
            if(jd["unionid"] == null)
            {
                Unionid = "";
            }
            else
            {
                Unionid = (string)jd["unionid"];
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        public void GetUserInfo()
        {
            //构造获取用户信息的url
            SortedDictionary<string, object> dic =
                new SortedDictionary<string, object>
                {
                    {"access_token", AccessToken},
                    {"openid", Openid},
                    {"lang", "zh_CN"}
                };
            string url = "https://api.weixin.qq.com/sns/userinfo?" + ToUrl(dic);

            //请求url以获取数据
            string result = WxHttpService.Get(url);

            JObject jd = (JObject)JsonConvert.DeserializeObject(result);
            Nickname = (string)jd["nickname"];
            Sex = (int)jd["sex"]>0?(int)jd["sex"]-1:(int)jd["sex"];
            Headimgurl = (string)jd["headimgurl"];
        }

        /**
        * @Dictionary格式转化成url参数格式
        * @ return url格式串, 该串不包含sign字段值
        */
        public static string ToUrl(SortedDictionary<string, object> mValues)
        {
            string buff = mValues.Where(pair => pair.Value != null && pair.Key != "sign" && pair.Value.ToString() != "").Aggregate("", (current, pair) => current + (pair.Key + "=" + pair.Value + "&"));
            buff = buff.Trim('&');
            return buff;
        }
    }
}