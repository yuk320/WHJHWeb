// ReSharper disable once CheckNamespace
namespace Game.Facade
{
    public class AppConfig
    {
        #region 常量
        /// <summary>
        /// 验证码Session的KEY值
        /// </summary>
        public const string VerifyCodeKey = "VCKCache";
        /// <summary>
        /// 登录资源缓存KEY值
        /// </summary>
        public const string LoginResources = "LRESCache";
        /// <summary>
        /// 用户登录cookie缓存KEY值
        /// </summary>
        public const string UserCookieKey = "UCKCache";
        /// <summary>
        /// 用户登录cookie缓存时长
        /// </summary>
        public const int UserCookieTimeOut = 60;
        /// <summary>
        /// 用户登录session缓存KEY值
        /// </summary>
        public const string UserSessionKey = "USKCache";
        /// <summary>
        /// 用户登录session缓存时长
        /// </summary>
        public const int UserSessionTimeOut = 30;
        /// <summary>
        /// ip地址库缓存
        /// </summary>
        public const string IpSessionKey = "IPSKCache";
        #endregion 常量
    }
}