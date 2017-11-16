using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading;
using Game.Utils.Cache;
using Game.Entity.PlatformManager;
using Game.Utils;
using System.Data;

namespace Game.Facade
{
    public class Fetch
    {
        #region 页面重定向

        /// <summary>
        /// 页面重定向
        /// </summary>
        /// <param name="url"></param>
        public static void Redirect( string url )
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.StatusCode = 301;
            HttpContext.Current.Response.AppendHeader( "location", url );
            HttpContext.Current.Response.End();
        }
        #endregion

        #region 获取验证码

        /// <summary>
        /// 获取验证码
        /// </summary>
        public static string GetVerifyCodeVer2
        {
            get
            {
                object obj = WHCache.Default.Get<SessionCache>( AppConfig.VerifyCodeKey );
                if( obj != null )
                {
                    return obj.ToString();
                }
                return "";
            }
        }

        /// <summary>
        /// 校验验证码
        /// </summary>
        /// <param name="verifyCode"></param>
        /// <returns></returns>
        public static bool ValidVerifyCodeVer2( string verifyCode )
        {
            object obj = WHCache.Default.Get<SessionCache>( AppConfig.VerifyCodeKey );
            if( obj != null )
            {
                if( obj.ToString() == verifyCode )
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region 当前Cookie名称

        /// <summary>
        /// 获取当前Cookie名称
        /// </summary>
        public static string GetCookieName
        {
            get
            {
                return "6603Admin";
            }
        }

        #endregion

        #region 日期处理

        /// <summary>
        /// 获取开始时间
        /// </summary>
        /// <param name="bgDate"></param>
        /// <returns></returns>
        public static string GetStartTime( DateTime bgDate )
        {
            DateTime bgTime = new DateTime( bgDate.Year, bgDate.Month, bgDate.Day, 0, 0, 0 );
            return Convert.ToString( bgTime );
        }

        /// <summary>
        /// 获取结束时间
        /// </summary>
        /// <param name="enDate"></param>
        /// <returns></returns>
        public static string GetEndTime( DateTime enDate )
        {
            DateTime enTime = new DateTime( enDate.Year, enDate.Month, enDate.Day, 23, 59, 59 );
            return Convert.ToString( enTime );
        }

        /// <summary>
        /// 获取指定日期的开始时间和结束时间(日期格式：开始时间$结束时间)
        /// </summary>
        /// <param name="bgDate"></param>
        /// <param name="enDate"></param>
        /// <returns></returns>
        public static string GetTimeByDate( DateTime bgDate, DateTime enDate )
        {
            DateTime bgTime = new DateTime( bgDate.Year, bgDate.Month, bgDate.Day, 0, 0, 0 );
            DateTime enTime = new DateTime( enDate.Year, enDate.Month, enDate.Day, 23, 59, 59 );
            return Convert.ToString( bgTime ) + "$" + Convert.ToString( enTime );
        }


        /// <summary>
        /// 获取当天的开始时间和结束时间
        /// </summary>
        public static string GetTodayTime()
        {
            DateTime dt = DateTime.Now;
            return GetTimeByDate( dt, dt );
        }

        /// <summary>
        /// 获取昨天的开始时间和结束时间
        /// </summary>
        /// <returns></returns>
        public static string GetYesterdayTime()
        {
            DateTime dt = DateTime.Now.AddDays( -1 );
            return GetTimeByDate( dt, dt );
        }

        /// <summary>
        /// 获取本周的开始时间和结束时间
        /// </summary>
        public static string GetWeekTime()
        {
            DateTime dt = DateTime.Now;
            DateTime startWeek = dt.AddDays( 0 - Convert.ToInt32( dt.DayOfWeek.ToString( "d" ) ) );  //本周周日
            DateTime endWeek = startWeek.AddDays( 6 );  //本周周六
            return GetTimeByDate( startWeek, endWeek );
        }

        /// <summary>
        /// 获取上周的开始时间和结束时间
        /// </summary>
        public static string GetLastWeekTime()
        {
            DateTime dt = DateTime.Now;
            DateTime startWeek = dt.AddDays( 0 - 7 - Convert.ToInt32( dt.DayOfWeek.ToString( "d" ) ) );  //本周周日
            DateTime endWeek = startWeek.AddDays( 6 );  //本周周日六
            return GetTimeByDate( startWeek, endWeek );
        }

        /// <summary>
        /// 获取本月的开始时间和结束时间
        /// </summary>
        /// <returns></returns>
        public static string GetMonthTime()
        {
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays( 1 - dt.Day );  //本月月初
            DateTime endMonth = startMonth.AddMonths( 1 ).AddDays( -1 );  //本月月末
            return GetTimeByDate( startMonth, endMonth );
        }

        /// <summary>
        /// 获取上月的开始时间和结束时间
        /// </summary>
        /// <returns></returns>
        public static string GetLastMonthTime()
        {
            DateTime dt = DateTime.Now.AddMonths(-1);
            DateTime startMonth = dt.AddDays(1 - dt.Day);
            DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);
            return GetTimeByDate(startMonth, endMonth);
        }

        /// <summary>
        /// 获取本年的开始时间和结束时间
        /// </summary>
        /// <returns></returns>
        public static string GetYearTime()
        {
            DateTime dt = DateTime.Now;
            DateTime startYear = dt.AddDays(1 - dt.DayOfYear);//本年年初
            DateTime endYear = startYear.AddYears(1).AddDays(-1);  //本年年末
            return GetTimeByDate(startYear, endYear);
        }

        /// <summary>
        /// 获取给定日期距离1900-01-01的天数
        /// </summary>
        /// <param name="DateTime"></param>
        /// <returns></returns>
        public static string GetDateID( DateTime DateTime )
        {
            TimeSpan ts1 = new TimeSpan( DateTime.Ticks );
            TimeSpan ts2 = new TimeSpan( Convert.ToDateTime( "1900-01-01" ).Ticks );
            TimeSpan ts = ts1.Subtract( ts2 ).Duration();
            return ts.Days.ToString();
        }
        /// <summary>
        ///  返回指定天数所对应的日期
        /// </summary>
        /// <param name="dateID"></param>
        /// <returns></returns>
        public static string ShowDate( int dateID )
        {
            return Convert.ToDateTime( "1900-01-01" ).AddDays( dateID ).ToString( "yyyy-MM-dd" );
        }
        /// <summary>
        /// 获取时间间隔
        /// </summary>
        /// <param name="dtStartDate">开始时间</param>
        /// <param name="dtEndDate">结束时间</param>
        /// <returns></returns>
        public static string GetTimeSpan( DateTime dtStartDate, DateTime dtEndDate )
        {
            StringBuilder sb = new StringBuilder();
            TimeSpan ts = dtEndDate.Subtract( dtStartDate );
            if( ts.Days != 0 )
                sb.AppendFormat( "{0}天", ts.Days );
            if( ts.Hours != 0 )
                sb.AppendFormat( "{0}小时", ts.Hours );
            if( ts.Minutes != 0 )
                sb.AppendFormat( "{0}分钟", ts.Minutes );
            if( ts.Seconds != 0 )
                sb.AppendFormat( "{0}秒", ts.Seconds );
            if( string.IsNullOrEmpty( sb.ToString() ) )
                return "0秒";
            return sb.ToString();
        }
        /// <summary>
        /// 秒数转换成 0天0小时0分钟0秒
        /// </summary>
        /// <param name="seconds">秒</param>
        /// <returns></returns>
        public static string ConverTimeToDHMS( long seconds )
        {
            StringBuilder sb = new StringBuilder();
            if( seconds <= 0 )
                return "0秒";
            long day = seconds / 0x15180;
            long hour = ( seconds % 0x15180 ) / 0xe10;
            long minute = ( seconds % 0xe10 ) / 60;
            long second = seconds % 60;
            if( day > 0 )
                sb.AppendFormat( "{0}天", day );
            if( hour > 0 )
                sb.AppendFormat( "{0}小时", hour );
            if( minute > 0 )
                sb.AppendFormat( "{0}分钟", minute );
            if( second > 0 )
                sb.AppendFormat( "{0}秒", second );
            if( string.IsNullOrEmpty( sb.ToString() ) )
                return "0秒";
            return sb.ToString();

        }
        #endregion

        #region 字符串
        /// <summary>
        /// 随机生成指定长度的数字串
        /// </summary>
        /// <param name="length">指定长度</param>
        /// <returns></returns>
        public static string GetRandomNumeric( int length )
        {
            if( length <= 0 )
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            for( int i = length; i > 0; i-- )
            {
                builder.Append( GetRandomSingleDigit().ToString() );
            }
            return builder.ToString();
        }
        /// <summary>
        /// 随机生成指定长度的数字串
        /// </summary>
        /// <param name="length">指定长度</param>
        /// <returns></returns>
        public static string GetRandomNumeric( int length, Random rand )
        {
            if( length <= 0 )
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            for( int i = length; i > 0; i-- )
            {
                builder.Append( rand.Next( 0, 10 ).ToString() );
            }
            return builder.ToString();
        }
        /// <summary>
        /// 随机生成指定长度的数字英文串
        /// </summary>
        /// <param name="length">指定长度</param>
        /// <returns></returns>
        public static string GetRandomNumericAndEn( int length, Random random )
        {
            if( length <= 0 )
            {
                return "";
            }
            string str = "nMe7lcIPKpQ1oAtuGCzL2qf8NO9X4mdFSaHbsOj3DvJrwV6ghiUYZWx5kETRyB";
            int num = length;
            StringBuilder builder = new StringBuilder();
            List<char> list = new List<char>();
            while( num > 0 )
            {
                char item = str[random.Next( str.Length )];
                if( !list.Contains( item ) )
                {
                    list.Add( item );
                    num--;
                }
            }
            foreach( char ch2 in list )
            {
                builder.Append( ch2.ToString() );
            }
            return builder.ToString();
        }
        /// <summary>
        /// 随机生成单个数字
        /// </summary>
        /// <returns></returns>
        public static int GetRandomSingleDigit()
        {
            Random random = new Random();
            Thread.Sleep( 10 );
            return random.Next( 10 );
        }
        /// <summary>
        /// 随机生成指定长度的英文字符串
        /// </summary>
        /// <param name="length">指定长度</param>
        /// <param name="notRepeat">是否允许字母重复 true:不允许 false:允许</param>
        /// <returns></returns>
        public static string GetRandomStr( int length, bool notRepeat, Random random )
        {
            if( length <= 0 )
            {
                return "";
            }
            string str = "cohxMLdeabzEFGNmQZPAstpvwTHOkKnlWqrSYyijXufgRIJUVBCD";
            int num = length;
            StringBuilder builder = new StringBuilder();
            List<char> list = new List<char>();
            while( num > 0 )
            {
                char item = str[random.Next( str.Length )];
                if( notRepeat )
                {
                    if( length >= 0x1a )
                    {
                        throw new Exception( "指定了不允许字母重复，并且要生成的字符串长度大于等于26，将造成系统陷入死循环。" );
                    }
                    if( !list.Contains( item ) )
                    {
                        list.Add( item );
                    }
                }
                else
                {
                    list.Add( item );
                }
                num--;
            }
            foreach( char ch2 in list )
            {
                builder.Append( ch2.ToString() );
            }
            return builder.ToString();
        }

        #endregion

        #region 账号处理
        /// <summary>
        /// 缓存管理账号
        /// </summary>
        /// <param name="user">账号信息</param>
        /// <returns></returns>
        public static void SaveLoginUser(LoginUser user)
        {
            if(user != null)
            {
                WHCache.Default.Save<SessionCache>(AppConfig.UserSessionKey, user, AppConfig.UserSessionTimeOut);
                WHCache.Default.Save<CookiesCache>(AppConfig.UserCookieKey, user.UserID, AppConfig.UserCookieTimeOut);
            }
        }
        /// <summary>
        /// 缓存登录资源
        /// </summary>
        public static LoginCache SaveLoginResources(int userid)
        {
            if(userid > 0)
            {
                //缓存类
                LoginCache cache = new LoginCache();

                //页面授权
                List<ModulePage> pagePowerList = SerializationHelper.Deserialize(typeof(List<ModulePage>), TextUtility.GetRealPath("/config/power.config")) as List<ModulePage>;
                //用户权限
                Dictionary<string, long> userPermission = new Dictionary<string, long>();
                DataSet ds = FacadeManage.aidePlatformManagerFacade.GetPermissionByUserId(userid);
                if(ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        userPermission.Add(ds.Tables[0].Rows[i]["ModuleID"].ToString(), Convert.ToInt64(ds.Tables[0].Rows[i]["OperationPermission"]));
                    }
                }
                //缓存资源信息
                cache.userId = userid;
                cache.pagePowerList = pagePowerList;
                cache.userPower = userPermission;
                WHCache.Default.Save<AspNetCache>(AppConfig.LoginResources, cache, AppConfig.UserSessionTimeOut);
                return cache;
            }
            return null;
        }
        /// <summary>
        /// 获取登录资源
        /// </summary>
        /// <returns></returns>
        public static LoginCache GetLoginResources(int userid)
        {
            LoginCache cache = new LoginCache();
            object obj = WHCache.Default.Get<AspNetCache>(AppConfig.LoginResources);
            if(obj != null)
            {
                cache = obj as LoginCache;
                if(cache==null || cache.userId!= userid)
                {
                    cache = SaveLoginResources(userid);
                }
            }
            else
            {
                cache = SaveLoginResources(userid);
            }
            return cache;
        }
        /// <summary>
        /// 清除管理账号缓存
        /// </summary>
        public static void ClearLoginUser()
        {
            object session = WHCache.Default.Get<SessionCache>(AppConfig.UserSessionKey);
            if(session != null)
            {
                WHCache.Default.Delete<SessionCache>(AppConfig.UserSessionKey);
            }
            object cookie = WHCache.Default.Get<CookiesCache>(AppConfig.UserCookieKey);
            if(cookie != null)
            {
                WHCache.Default.Delete<CookiesCache>(AppConfig.UserCookieKey);
            }
            object userPower = WHCache.Default.Get<SessionCache>(AppConfig.LoginResources);
            if(userPower != null)
            {
                WHCache.Default.Delete<SessionCache>(AppConfig.LoginResources);
            }
        }
        /// <summary>
        /// 获取缓存管理账号
        /// </summary>
        /// <returns></returns>
        public static LoginUser GetLoginUser()
        {
            object session = WHCache.Default.Get<SessionCache>(AppConfig.UserSessionKey);
            if(session == null)
            {
                object cookie = WHCache.Default.Get<CookiesCache>(AppConfig.UserCookieKey);
                if(cookie != null && Validate.IsPositiveInt(cookie.ToString()))
                {
                    int userid = Convert.ToInt32(cookie);
                    Base_Users user = FacadeManage.aidePlatformManagerFacade.GetUserByUserId(userid);
                    if(user != null)
                    {
                        Base_Roles role = FacadeManage.aidePlatformManagerFacade.GetRoleInfo(user.RoleID);
                        LoginUser login = new LoginUser();
                        login.UserID = user.UserID;
                        login.LoginTimes = user.LoginTimes;
                        login.PreLoginIP = user.PreLoginIP;
                        login.PreLogintime = user.PreLogintime;
                        login.RoleID = user.RoleID;
                        login.RoleName = role.RoleName;
                        login.UserName = user.Username;
                        SaveLoginUser(login);
                        return login;
                    }
                }
                return null;
            }
            else
            {
                return session as LoginUser;
            }
        }
        #endregion
    }
}
