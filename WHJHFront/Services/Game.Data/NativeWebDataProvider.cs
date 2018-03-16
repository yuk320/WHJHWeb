using System.Collections.Generic;
using Game.Kernel;
using Game.IData;
using System.Data.Common;
using System.Data;
using Game.Entity.Accounts;
using Game.Entity.NativeWeb;
using Game.Entity.Treasure;

namespace Game.Data
{
    /// <summary>
    /// 网站数据访问层
    /// </summary>
    public class NativeWebDataProvider : BaseDataProvider, INativeWebDataProvider
    {
        #region 构造方法

        public NativeWebDataProvider(string connString)
            : base(connString)
        {
        }

        #endregion

        #region 数据分页

        /// <summary>
        /// 游戏规则数据分页
        /// </summary>
        /// <param name="pageIndex">分页下标</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public PagerSet GetGamePageList(int pageIndex, int pageSize)
        {
            PagerParameters pager = new PagerParameters(GameRule.Tablename, "ORDER BY KindID DESC", "WHERE Nullity=0",
                pageIndex, pageSize);
            string[] returnField = {"KindID", "KindName", "KindIcon", "KindIntro"};
            pager.Fields = returnField;
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 新闻公告数据分页
        /// </summary>
        /// <param name="pageIndex">分页下标</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public PagerSet GetNewPageList(int pageIndex, int pageSize)
        {
            PagerParameters pager = new PagerParameters(SystemNotice.Tablename,
                "ORDER BY IsTop DESC,IsHot DESC,SortID ASC,NoticeID DESC", "WHERE Nullity=0", pageIndex, pageSize);
            string[] returnField = {"NoticeID", "NoticeTitle", "MoblieContent", "PublisherTime"};
            pager.Fields = returnField;
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
        }

        #endregion

        #region 获取配置信息

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="configKey">配置键值</param>
        /// <returns></returns>
        public ConfigInfo GetConfigInfo(string configKey)
        {
            const string sqlQuery =
                @"SELECT ConfigKey,Field1,Field2,Field3,Field4,Field5,Field6,Field7,Field8 FROM ConfigInfo WITH(NOLOCK) WHERE ConfigKey = @ConfigKey";
            List<DbParameter> parms = new List<DbParameter> {Database.MakeInParam("ConfigKey", configKey)};
            return Database.ExecuteObject<ConfigInfo>(sqlQuery, parms);
        }

        /// <summary>
        /// 获取配置信息集合
        /// </summary>
        /// <returns></returns>
        public IList<ConfigInfo> GetConfigInfoList()
        {
            string sqlQuery =
                @"SELECT ConfigKey,Field1,Field2,Field3,Field4,Field5,Field6,Field7,Field8 FROM ConfigInfo WITH(NOLOCK)";
            return Database.ExecuteObjectList<ConfigInfo>(sqlQuery);
        }

        #endregion

        #region 手机登录信息

        /// <summary>
        /// 获取手机登录数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetMobileLoginInfo()
        {
            return Database.ExecuteDataset(CommandType.StoredProcedure, "NET_PW_GetMobileLoginData");
        }

        #endregion

        #region 获取排行榜信息

        /// <summary>
        /// 获取排行榜数据
        /// </summary>
        /// <param name="typeid">排行榜类型</param>
        /// <returns></returns>
        public DataSet GetDayRankingData(int typeid)
        {
            var prams = new List<DbParameter> {Database.MakeInParam("TypeID", typeid)};
            return Database.ExecuteDataset(CommandType.StoredProcedure, "NET_PW_GetDayRankingData", prams.ToArray());
        }

        /// <summary>
        /// 获取财富排行榜
        /// </summary>
        /// <returns></returns>
        public IList<CacheWealthRank> GetCacheWealthRank(int dateId)
        {
            string sqlQuery =
                $"SELECT TOP 10 * FROM CacheWealthRank WITH(NOLOCK) WHERE DateID = {dateId} ORDER BY RankNum ASC";
            return Database.ExecuteObjectList<CacheWealthRank>(sqlQuery);
        }

        /// <summary>
        /// 获取战绩排行榜
        /// </summary>
        /// <returns></returns>
        public IList<CacheScoreRank> GetCacheScoreRank(int dateId)
        {
            const string sqlQuery =
                "SELECT TOP 10 * FROM CacheScoreRank WITH(NOLOCK) WHERE DateID = @DateID ORDER BY RankNum ASC";
            var prams = new List<DbParameter> {Database.MakeInParam("DateID", dateId)};

            return Database.ExecuteObjectList<CacheScoreRank>(sqlQuery, prams);
        }

        /// <summary>
        /// 获取消耗排行榜
        /// </summary>
        /// <returns></returns>
        public IList<CacheConsumeRank> GetCacheConsumeRank(int dateId)
        {
            const string sqlQuery =
                "SELECT TOP 10 * FROM CacheConsumeRank WITH(NOLOCK) WHERE DateID = @DateID ORDER BY RankNum ASC";
            var prams = new List<DbParameter> {Database.MakeInParam("DateID", dateId)};

            return Database.ExecuteObjectList<CacheConsumeRank>(sqlQuery, prams);
        }

        /// <summary>
        /// 领取排行榜奖励
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <param name="dateId">时间标识</param>
        /// <param name="typeId">排行榜类型</param>
        /// <param name="ip">领取地址</param>
        /// <returns></returns>
        public Message ReceiveRankingAward(int userId, int dateId, int typeId, string ip)
        {
            var prams = new List<DbParameter>
            {
                Database.MakeInParam("dwUserID", userId),
                Database.MakeInParam("dwDateID", dateId),
                Database.MakeInParam("dwTypeID", typeId),
                Database.MakeInParam("strClientIP", ip),
                Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
            };


            return MessageHelper.GetMessageForObject<UserCurrency>(Database, "NET_PJ_RecevieRankingAward", prams);
        }

        #endregion

        #region 新闻信息

        /// <summary>
        /// 获取首页新闻公告
        /// </summary>
        /// <returns></returns>
        public IList<SystemNotice> GetHomePageNews()
        {
            string sqlQuery =
                @"SELECT TOP 13 NoticeID,NoticeTitle,MoblieContent,PublisherTime,IsHot,IsTop FROM SystemNotice WITH(NOLOCK) WHERE Nullity=0 ORDER BY IsTop DESC,IsHot DESC,SortID ASC,NoticeID DESC";
            return Database.ExecuteObjectList<SystemNotice>(sqlQuery);
        }

        /// <summary>
        /// 获取新闻详情
        /// </summary>
        /// <param name="id">新闻标识</param>
        /// <returns></returns>
        public SystemNotice GetWebNewsInfo(int id)
        {
            const string sqlQuery =
                @"SELECT NoticeTitle,WebContent,Publisher,PublisherTime FROM SystemNotice WITH(NOLOCK) WHERE NoticeID = @NoticeID";
            List<DbParameter> parms = new List<DbParameter> {Database.MakeInParam("NoticeID", id)};
            return Database.ExecuteObject<SystemNotice>(sqlQuery, parms);
        }

        #endregion

        #region 游戏规则

        /// <summary>
        /// 获取游戏规则列表
        /// </summary>
        /// <returns></returns>
        public IList<GameRule> GetGameRuleList()
        {
            string sqlQuery =
                @"SELECT KindID,KindName,KindIcon,KindIntro FROM GameRule WITH(NOLOCK) WHERE Nullity=0 ORDER BY SortID ASC,KindID DESC";
            return Database.ExecuteObjectList<GameRule>(sqlQuery);
        }

        /// <summary>
        /// 获取游戏规则
        /// </summary>
        /// <param name="kindid">游戏规则</param>
        /// <returns></returns>
        public GameRule GetGameRuleInfo(int kindid)
        {
            const string sqlQuery =
                @"SELECT KindName,KindIntro,KindRule FROM GameRule WITH(NOLOCK) WHERE KindID = @KindID";
            List<DbParameter> parms = new List<DbParameter> {Database.MakeInParam("KindID", kindid)};
            return Database.ExecuteObject<GameRule>(sqlQuery, parms);
        }

        #endregion

        #region 广告位图片

        /// <summary>
        /// 获取广告图列表
        /// </summary>
        /// <returns></returns>
        public IList<Ads> GetAdsList()
        {
            string sqlQuery =
                @"SELECT ID,Title,ResourceURL,LinkURL,[Type],SortID FROM Ads WITH(NOLOCK) WHERE [Type]!=3 AND [Type]!=4 ORDER BY [Type] DESC,SortID ASC";
            return Database.ExecuteObjectList<Ads>(sqlQuery);
        }

        #endregion

        #region 常见问题

        /// <summary>
        /// 获取常见问题列表
        /// </summary>
        /// <returns></returns>
        public IList<Question> GetQAList(int top = 0)
        {
            string topSql = top > 0 ? $" TOP {top} " : "";
            IList<Question> list =
                Database.ExecuteObjectList<Question>(
                    $"SELECT {topSql} * FROM DBO.[Question] ORDER BY SortID ASC,ID DESC ");
            return list;
        }

        #endregion

        #region 代理Token管理

        /// <summary>
        /// 检查代理登录凭证
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public AgentTokenInfo VerifyAgentToken(string token)
        {
            Database.ExecuteNonQuery(CommandType.Text, "DELETE AgentTokenInfo WHERE ExpirtAt < GETDATE() ");
            return 
                Database.ExecuteObject<AgentTokenInfo>(
                    $"SELECT * FROM AgentTokenInfo(NOLOCK) WHERE Token = N'{token}' AND ExpirtAt > GETDATE() ");
        }

        /// <summary>
        /// 保存代理登录凭证
        /// </summary>
        /// <param name="info"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public int SaveAgentToken(UserInfo info, string token)
        {
            Database.ExecuteNonQuery(CommandType.Text, "DELETE AgentTokenInfo WHERE ExpirtAt < GETDATE() ");
            string sql =
                $" IF EXISTS (SELECT 1 FROM AgentTokenInfo WHERE UserID = {info.UserID} AND AgentID = {info.AgentID})  " +
                $" BEGIN  UPDATE AgentTokenInfo SET Token = N'{token}',ExpirtAt = GETDATE() + 1 WHERE UserID = {info.UserID} AND AgentID = {info.AgentID}  END" +
                $" ELSE BEGIN INSERT AgentTokenInfo (UserID,AgentID,Token) VALUES ({info.UserID},{info.AgentID},N'{token}') END ";
            return Database.ExecuteNonQuery(CommandType.Text, sql);
        }
        #endregion
    }
}
