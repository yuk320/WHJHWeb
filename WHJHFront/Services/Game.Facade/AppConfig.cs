using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Utils;
using Game.Entity.NativeWeb;
using Game.Utils.Cache;
// ReSharper disable InconsistentNaming

namespace Game.Facade
{
    public class AppConfig
    {
        #region 枚举
        /// <summary>
        /// 站点配置KEY
        /// </summary>
        public enum SiteConfigKey
        {
            /// <summary>
            /// 大厅下载配置
            /// </summary>
            SiteConfig,
            /// <summary>
            /// 大厅整包配置
            /// </summary>
            GameFullPackageConfig,
            /// <summary>
            /// 大厅简包配置
            /// </summary>
            GameJanePackageConfig,
            /// <summary>
            /// 安卓大厅配置
            /// </summary>
//            GameAndroidConfig,
            /// <summary>
            /// 苹果大厅配置
            /// </summary>
//            GameIosConfig,
            /// <summary>
            /// 移动版大厅配置
            /// </summary>
            MobilePlatformVersion,
            /// <summary>
            /// 网站站点配置
            /// </summary>
            WebSiteConfig,
            /// <summary>
            /// 网站服务条款
            /// </summary>
            WebAgreement,
            /// <summary>
            /// 网站站点地图
            /// </summary>
            WebSitemap,
            /// <summary>
            /// 系统客服配置
            /// </summary>
            SysCustomerService
        }
        /// <summary>
        /// 系统配置KEY
        /// </summary>
        public enum ConfigInfoKey
        {
            /// <summary>
            /// 是否开启手机商城
            /// </summary>
            JJOpenMobileMall,
            /// <summary>
            /// 充值是否需绑定推广人
            /// </summary>
            JJPayBindSpread,
            /// <summary>
            /// 绑定推广人赠送钻石
            /// </summary>
            JJBindSpreadPresent,
            /// <summary>
            /// 显示排行榜类型
            /// </summary>
            JJRankingListType,
            /// <summary>
            /// 支持充值模式
            /// </summary>
            JJPayChannel,
            /// <summary>
            /// 钻石购买道具价
            /// </summary>
            JJDiamondBuyProp,
            /// <summary>
            /// 金币购买喇叭价
            /// </summary>
//            JJGoldBuyProp,
            /// <summary>
            /// 实名认证赠送钻石
            /// </summary>
            JJRealNameAuthentPresent,
            /// <summary>
            /// 有效好友游戏局数
            /// </summary>
            JJEffectiveFriendGame,
//                        /// <summary>
//                       /// 苹果第三方充值
//                        /// </summary>
//                        IOSNotStorePaySwitch,
            /// <summary>
            /// 全局推广返利类型（0：金币、1：钻石）
            /// </summary>
            SpreadReturnType,
            /// <summary>
            /// 代理后台版本
            /// </summary>
            AgentHomeVersion
        }

        public enum CodeMode
        {
            /// <summary>
            /// 开发模式（内部测试）
            /// </summary>
            Dev,
            /// <summary>
            /// 演示模式（演示平台）
            /// </summary>
            Demo,
            /// <summary>
            /// 生产模式（客户版本）
            /// </summary>
            Production
        }
        #endregion

        #region WebConfig配置
        /// <summary>
        /// 代码模式 Mode
        /// </summary>
        public static CodeMode Mode
        {
            get
            {
                try {
                    string mode = ApplicationSettings.Get("Mode");
                    if (CodeMode.Demo.ToString().Equals(mode)) return CodeMode.Demo;
                    return CodeMode.Production.ToString().Equals(mode) ? CodeMode.Production : CodeMode.Dev;
                }
                catch {
                    return CodeMode.Dev;
                }
            }
        }

        /// <summary>
        /// 用户登录缓存key
        /// </summary>
        public static string UserLoginCacheKey
        {
            get
            {
                return ApplicationSettings.Get("UserLoginCacheKey");
            }
        }
        /// <summary>
        /// 用户登录缓存过期时间 单位分钟
        /// </summary>
        public static int UserLoginTimeOut
        {
            get
            {
                return Convert.ToInt32(ApplicationSettings.Get("UserLoginCacheTimeOut"));
            }
        }
        /// <summary>
        /// 网站接口对外签名验证密钥
        /// </summary>
        public static string MoblieInterfaceKey
        {
            get
            {
                return ApplicationSettings.Get("MobileInterfaceKey");
            }
        }
        /// <summary>
        /// 网站授权请求站点域名
        /// </summary>
        public static string MoblieInterfaceDomain
        {
            get
            {
                return ApplicationSettings.Get("MoblieInterfaceDomain");
            }
        }
        /// <summary>
        /// 网站前台域名
        /// </summary>
        public static string FrontSiteDomain
        {
            get
            {
                return ApplicationSettings.Get("FrontSiteDomain");
            }
        }
        /// <summary>
        /// 页面标题
        /// </summary>
        public static string PageTitle
        {
            get
            {
                return ApplicationSettings.Get("title");
            }
        }
        /// <summary>
        /// 页面关键字
        /// </summary>
        public static string PageKey
        {
            get
            {
                return ApplicationSettings.Get("keywords");
            }
        }
        /// <summary>
        /// 页面描述
        /// </summary>
        public static string PageDescript
        {
            get
            {
                return ApplicationSettings.Get("description");
            }
        }

        public static long ApiVersion
        {
            get
            {
                long version = 20171010;
                try
                {
                    object cfgVersion = ApplicationSettings.Get("apiVersion");
                    if (cfgVersion != null) version = Convert.ToInt64(cfgVersion);
                }
                catch
                {
                    // ignored
                }
                return version;
            }
        }
        #endregion

        #region 默认常量
        /// <summary>
        /// 验证码Session的KEY值
        /// </summary>
        public const string VerifyCodeKey = "VerifyCodeKey";
        /// <summary>
        /// 广告图数据缓存
        /// </summary>
        public const string AdsConfigCache = "AdsConfigCache";
        /// <summary>
        /// 网站配置缓存
        /// </summary>
        public const string WebSiteConfigCache = "WebSiteConfigCache";
        /// <summary>
        /// 网站配置缓存时间
        /// </summary>
        public const int ResourceTimeOut = 30;
        /// <summary>
        /// 微信授权页面地址(默认用 /Authorize.aspx )
        /// </summary>
        public static string AuthorizeURL = (string.IsNullOrEmpty(FrontSiteDomain) ? "":"http://"+FrontSiteDomain) + "/Authorize.aspx";
        public static string HAuthorizeURL = (string.IsNullOrEmpty(FrontSiteDomain) ? "":"http://"+FrontSiteDomain) + "/HAuthorize.aspx";
        /// <summary>
        /// 微信注册参数加密key（必须8位）
        /// </summary>
        public const string WxUrlKey = "wxwukjgs";
        /// <summary>
        /// H5游戏授权参数加密key（必须16位）
        /// </summary>
        public const string WxH5Key = "szwhkjyxgsqipaim";
        /// <summary>
        /// 微信票据缓存key
        /// </summary>
        public const string WxTicket = "WxTicket";
        #endregion

        /// <summary>
        /// 公共方法获取Web.Config Setting
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSetting(string key)
        {
            try
            {
                var value = ApplicationSettings.Get(key);
                return value;
            }
            catch
            {
                return "";
            }
        }
    }
}
