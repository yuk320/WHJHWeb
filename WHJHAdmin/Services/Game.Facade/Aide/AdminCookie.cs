using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

using Game.Utils;
using System.Web.Security;
using Game.Entity.PlatformManager;

// ReSharper disable once CheckNamespace
namespace Game.Facade
{
    /// <summary>
    /// 用户Cookie数据
    /// </summary>
    public class AdminCookie
    {

        /// <summary>
        /// 设置用户 Cookie
        /// </summary>
        /// <param name="user"></param>
        public static void SetUserCookie( Base_Users user )
        {
            Dictionary<string, object> dic = new Dictionary<string, object>( );
            dic.Add( Base_Users._UserID, user.UserID );
            dic.Add( Base_Users._Username, user.Username );
            dic.Add( Base_Users._RoleID, user.RoleID );
            dic.Add( Base_Users._IsBand, user.IsBand );
            Add( dic , 30 );
        }

        /// <summary>
        /// 获取用户对象 by Cookie
        /// </summary>
        /// <returns></returns>
        public static Base_Users GetUserFromCookie( )
        {
            HttpContext context = HttpContext.Current;
            if ( context == null )
                return null;

            Base_Users user = new Base_Users( );
            object objUserId = GetValue( Base_Users._UserID );
            object objAccount = GetValue( Base_Users._Username );
            object objRoleId = GetValue( Base_Users._RoleID );
            object objIsBand = GetValue( Base_Users._IsBand );
            if ( objUserId == null || objAccount == null || objRoleId == null || objIsBand == null )
                return null;
            user.UserID = int.Parse( objUserId.ToString( ) );
            user.Username = objAccount.ToString( );
            user.RoleID = int.Parse( objRoleId.ToString( ) );
            user.IsBand = int.Parse( objIsBand.ToString( ) );
            SetUserCookie( user );
            return user;
        }

        /// <summary>
        /// 清理用户Cookie
        /// </summary>
        public static void ClearUserCookie( )
        {
            if ( HttpContext.Current == null )
                return;

            HttpCookie cookie = HttpContext.Current.Request.Cookies[Fetch.GetCookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears( -1 );
                HttpContext.Current.Response.Cookies.Add( cookie );
            }
        }

        /// <summary>
        /// 检查用户登录状态
        /// </summary>
        /// <returns></returns>
        public static bool CheckedUserLogon( )
        {
            Base_Users user = GetUserFromCookie( );
            if ( user == null || user.UserID <= 0 || user.RoleID <= 0 )
                return false;
            else
                SetUserCookie( user );

            return true;
        }

        //根据键值获取Cookie值

        #region Cookie操作
        private static string ValidateKey = "{2EF1D4CB-16BA-471D-9DFC-12C1E4D15253}";
        private static string ValidateName = "VS";
        private static string ExpireTimeStr = "_ETS";
        /// <summary>
        /// Cookie名称
        /// </summary>
        protected static string CookiesName
        {
            get
            {
                string strName = Utility.GetAppSetting( "CookiesName" );
                if ( !string.IsNullOrEmpty( strName ) )
                {
                    return strName;
                }
                else if ( !string.IsNullOrEmpty( Fetch.GetCookieName ) )
                {
                    return Fetch.GetCookieName;
                }
                return "Default";
            }
        }
        /// <summary>
        /// Cookie过期时间(分钟)
        /// </summary>
        protected static DateTime CookiesExpire
        {
            get
            {
                int num;
                if ( !int.TryParse( Utility.GetAppSetting( "CookiesExpireMinutes" ), out num ) )
                {
                    num = 30;
                }
                return DateTime.Now.AddMinutes( num );
            }
        }
        /// <summary>
        /// Cookie路径
        /// </summary>
        protected static string CookiesPath
        {
            get
            {
                string strPath = Utility.GetAppSetting( "CookiesPath" );
                if ( !string.IsNullOrEmpty( strPath ) )
                {
                    return strPath;
                }
                return "/";
            }
        }
        /// <summary>
        /// Cookie域
        /// </summary>
        protected static string CookiesDomain
        {
            get
            {
                string strDomain = Utility.GetAppSetting( "CookiesDomain" );
                if ( !string.IsNullOrEmpty( strDomain ) )
                {
                    return strDomain;
                }
                return "";
            }
        }
        /// <summary>
        /// 设置用户Cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="timeout">过期时间(分钟)</param>
        public static void Add( string key, object value, int? timeout )
        {
            HttpCookie ck = HttpContext.Current.Request.Cookies[CookiesName];
            if ( ck == null )
            {
                ck = new HttpCookie( CookiesName );
            }
            ck.Expires = DateTime.Now.AddYears( 50 );
            ck.Domain = CookiesDomain;
            ck.Values[key] = HttpUtility.UrlEncode( value.ToString( ) );
            ck.Values[key + ExpireTimeStr] = !timeout.HasValue ? HttpUtility.UrlEncode( CookiesExpire.ToString( "yyyy-MM-dd HH:mm:ss" ) ) : HttpUtility.UrlEncode( DateTime.Now.AddMinutes( timeout.Value ).ToString( "yyyy-MM-dd HH:mm:ss" ) );
            ck.Values[ValidateName] = GetValidateStr( ck );
            HttpContext.Current.Response.Cookies.Add( ck );
        }
        /// <summary>
        /// 设置用户Cookie
        /// </summary>
        /// <param name="dic">键值集合</param>
        /// <param name="timeout">过期时间(分钟)</param>
        public static void Add( Dictionary<string, object> dic, int? timeout )
        {
            HttpCookie ck = HttpContext.Current.Request.Cookies[CookiesName];
            if ( ck == null )
            {
                ck = new HttpCookie( CookiesName );
            }
            ck.Expires = DateTime.Now.AddYears( 50 );
            ck.Domain = CookiesDomain;
            foreach ( KeyValuePair<string, object> pair in dic )
            {
                ck.Values[pair.Key] = HttpUtility.UrlEncode( pair.Value.ToString( ) );
                ck.Values[pair.Key + ExpireTimeStr] = !timeout.HasValue ? HttpUtility.UrlEncode( CookiesExpire.ToString( "yyyy-MM-dd HH:mm:ss" ) ) : HttpUtility.UrlEncode( DateTime.Now.AddMinutes( timeout.Value ).ToString( "yyyy-MM-dd HH:mm:ss" ) );
            }
            ck.Values[ValidateName] = GetValidateStr( ck );
            HttpContext.Current.Response.Cookies.Add( ck );
        }
        /// <summary>
        /// 根据键名获取Cookie值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static object GetValue( string key )
        {
            if ( ( key != null ) && ( key != "" ) )
            {
                DateTime time;
                HttpCookie ck = HttpContext.Current.Request.Cookies[CookiesName];
                if ( ck == null )
                {
                    return null;
                }
                ck.Expires = DateTime.Now.AddYears( 50 );
                ck.Domain = CookiesDomain;
                if ( !ValidateCookies( ck ) )
                {
                    ck.Expires = DateTime.Now.AddYears( -1 );
                    HttpContext.Current.Response.Cookies.Add( ck );
                    return null;
                }
                string str = ck.Values[key + ExpireTimeStr];
                if ( string.IsNullOrEmpty( str ) || !DateTime.TryParse( HttpUtility.UrlDecode( str ), out time ) )
                {
                    ck.Values[key] = "";
                    ck.Values[key + ExpireTimeStr] = "";
                    ck.Values[ValidateName] = GetValidateStr( ck );
                    HttpContext.Current.Response.Cookies.Add( ck );
                    return null;
                }
                DateTime now = DateTime.Now;
                if ( time > now )
                {
                    return HttpUtility.UrlDecode( ck.Values[key] );
                }
                ck.Values[key] = "";
                ck.Values[key + ExpireTimeStr] = "";
                ck.Values[ValidateName] = GetValidateStr( ck );
                HttpContext.Current.Response.Cookies.Add( ck );
            }
            return null;
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="ck"></param>
        /// <returns></returns>
        private static string GetValidateStr( HttpCookie ck )
        {
            StringBuilder builder = new StringBuilder( );
            foreach ( string str in ck.Values.AllKeys )
            {
                if ( str != ValidateName )
                {
                    builder.Append( ck.Values[str] );
                }
            }
            builder.Append( ValidateKey );
            builder.Append( HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"] );
            builder.Append( HttpContext.Current.Request.ServerVariables["INSTANCE_ID"] );
            builder.Append( HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"] );
            var hashPasswordForStoringInConfigFile = FormsAuthentication.HashPasswordForStoringInConfigFile(builder.ToString(), "md5");
            if (hashPasswordForStoringInConfigFile != null)
                return hashPasswordForStoringInConfigFile.ToLower()
                    .Substring(8, 0x10);
            return "";
        }
        /// <summary>
        ///  验证
        /// </summary>
        /// <param name="ck"></param>
        /// <returns></returns>
        private static bool ValidateCookies( HttpCookie ck )
        {
            string strSourse = ck.Values[ValidateName];
            StringBuilder builder = new StringBuilder( );
            foreach ( string str in ck.Values.AllKeys )
            {
                if ( str != ValidateName )
                {
                    builder.Append( ck.Values[str] );
                }
            }
            builder.Append( ValidateKey );
            builder.Append( HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"] );
            builder.Append( HttpContext.Current.Request.ServerVariables["INSTANCE_ID"] );
            builder.Append( HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"] );
            string strValidate = FormsAuthentication.HashPasswordForStoringInConfigFile( builder.ToString( ), "md5" ).ToLower( ).Substring( 8, 0x10 );
            return strSourse.Equals( strValidate );
        }
        #endregion
    }
}
