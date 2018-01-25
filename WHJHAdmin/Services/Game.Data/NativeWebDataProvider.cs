using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Game.Entity.NativeWeb;
using Game.IData;
using Game.Kernel;

namespace Game.Data
{
    /// <summary>
    /// 网站数据层
    /// </summary>
    public class NativeWebDataProvider : BaseDataProvider, INativeWebDataProvider
    {
        #region 构造方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public NativeWebDataProvider(string connString)
            : base(connString)
        {
        }

        #endregion 构造方法

        #region 公用分页

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pageIndex">页下标</param>
        /// <param name="pageSize">页显示数</param>
        /// <param name="condition">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        public PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(tableName, orderby, condition, pageIndex, pageSize);
            return GetPagerSet2(pagerPrams);
        }

        #endregion

        #region 站点配置

        /// <summary>
        /// 获取站点配置
        /// </summary>
        /// <param name="id">配置标识</param>
        /// <returns></returns>
        public ConfigInfo GetConfigInfo(int id)
        {
            string sql = $"SELECT * FROM ConfigInfo WITH(NOLOCK) WHERE ConfigID={id}";
            return Database.ExecuteObject<ConfigInfo>(sql);
        }

        /// <summary>
        /// 获取站点配置
        /// </summary>
        /// <param name="configKey">配置Key</param>
        /// <returns></returns>
        public ConfigInfo GetConfigInfo(string configKey)
        {
            string sql = "SELECT * FROM ConfigInfo WITH(NOLOCK) WHERE ConfigKey=@ConfigKey";
            var prams = new List<DbParameter> {Database.MakeInParam("ConfigKey", configKey)};
            return Database.ExecuteObject<ConfigInfo>(sql, prams);
        }

        /// <summary>
        /// 修改站点配置
        /// </summary>
        /// <param name="config">配置信息</param>
        public int UpdateConfigInfo(ConfigInfo config)
        {
            string sqlQuery =
                @"UPDATE ConfigInfo SET Field1=@Field1,Field2=@Field2,Field3=@Field3,Field4=@Field4,Field5=@Field5,
                                Field6=@Field6,Field7=@Field7,Field8=@Field8,ConfigString=@ConfigString WHERE ConfigID=@ConfigID";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("Field1", config.Field1),
                Database.MakeInParam("Field2", config.Field2),
                Database.MakeInParam("Field3", config.Field3),
                Database.MakeInParam("Field4", config.Field4),
                Database.MakeInParam("Field5", config.Field5),
                Database.MakeInParam("Field6", config.Field6),
                Database.MakeInParam("Field7", config.Field7),
                Database.MakeInParam("Field8", config.Field8),
                Database.MakeInParam("ConfigString", config.ConfigString),
                Database.MakeInParam("ConfigID", config.ConfigID)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }

        #endregion 站点配置

        #region 广告管理

        /// <summary>
        /// 获取广告实体
        /// </summary>
        /// <param name="id">广告标识</param>
        /// <returns>广告实体</returns>
        public Ads GetAds(int id)
        {
            string sqlQuery = $"SELECT * FROM Ads WITH(NOLOCK) WHERE ID={id}";
            return Database.ExecuteObject<Ads>(sqlQuery);
        }

        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="idlist">标识列表</param>
        public int DeleteAds(string idlist)
        {
            string sqlQuery = $"DELETE Ads WHERE ID IN({idlist})";
            return Database.ExecuteNonQuery(sqlQuery);
        }

        /// <summary>
        /// 新增广告
        /// </summary>
        /// <param name="ads">广告信息</param>
        public int InsertAds(Ads ads)
        {
            string sqlQuery =
                @"INSERT Ads(Title,ResourceURL,LinkURL,Type,SortID,Remark) VALUES(@Title,@ResourceURL,@LinkURL,@Type,@SortID,@Remark)";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("Title", ads.Title),
                Database.MakeInParam("ResourceURL", ads.ResourceURL),
                Database.MakeInParam("LinkURL", ads.LinkURL),
                Database.MakeInParam("Type", ads.Type),
                Database.MakeInParam("SortID", ads.SortID),
                Database.MakeInParam("Remark", ads.Remark)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }

        /// <summary>
        /// 修改广告
        /// </summary>
        /// <param name="ads">广告实体</param>
        public int UpdateAds(Ads ads)
        {
            string sqlQuery = @"UPDATE Ads SET Title=@Title,ResourceURL=@ResourceURL,LinkUrl= @LinkUrl,
                        Type=@Type,SortID=@SortID,Remark=@Remark WHERE ID= @ID";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("ID", ads.ID),
                Database.MakeInParam("Title", ads.Title),
                Database.MakeInParam("ResourceURL", ads.ResourceURL),
                Database.MakeInParam("LinkURL", ads.LinkURL),
                Database.MakeInParam("Type", ads.Type),
                Database.MakeInParam("SortID", ads.SortID),
                Database.MakeInParam("Remark", ads.Remark)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }

        #endregion 广告管理

        #region 新闻公告

        /// <summary>
        /// 获取新闻公告信息
        /// </summary>
        /// <param name="id">公告标识</param>
        /// <returns></returns>
        public SystemNotice GetSystemNoticeInfo(int id)
        {
            string sql = $"SELECT * FROM SystemNotice WITH(NOLOCK) WHERE NoticeID = {id}";
            return Database.ExecuteObject<SystemNotice>(sql);
        }

        /// <summary>
        /// 新增新闻公告
        /// </summary>
        /// <param name="notice">公告信息</param>
        /// <returns></returns>
        public int InsertSystemNotice(SystemNotice notice)
        {
            string sql =
                @"INSERT INTO SystemNotice(NoticeTitle,MoblieContent,WebContent,SortID,Publisher,PublisherTime,IsHot,IsTop,Nullity)
                            VALUES(@NoticeTitle,@MoblieContent,@WebContent,@SortID,@Publisher,@PublisherTime,@IsHot,@IsTop,@Nullity)";
            var prams = new List<DbParameter>
            {
                Database.MakeInParam("NoticeTitle", notice.NoticeTitle),
                Database.MakeInParam("MoblieContent", notice.MoblieContent),
                Database.MakeInParam("WebContent", notice.WebContent),
                Database.MakeInParam("SortID", notice.SortID),
                Database.MakeInParam("Publisher", notice.Publisher),
                Database.MakeInParam("PublisherTime", notice.PublisherTime),
                Database.MakeInParam("IsHot", notice.IsHot),
                Database.MakeInParam("IsTop", notice.IsTop),
                Database.MakeInParam("Nullity", notice.Nullity)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sql, prams.ToArray());
        }

        /// <summary>
        /// 修改公告信息
        /// </summary>
        /// <param name="notice">公告信息</param>
        /// <returns></returns>
        public int UpdateSystemNotice(SystemNotice notice)
        {
            string sqlQuery =
                @"UPDATE SystemNotice SET NoticeTitle=@NoticeTitle,MoblieContent=@MoblieContent,WebContent=@WebContent,SortID= @SortID,
                    Publisher= @Publisher,PublisherTime= @PublisherTime,IsHot=@IsHot,IsTop=@IsTop,Nullity=@Nullity WHERE NoticeID= @NoticeID";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("NoticeTitle", notice.NoticeTitle),
                Database.MakeInParam("MoblieContent", notice.MoblieContent),
                Database.MakeInParam("WebContent", notice.WebContent),
                Database.MakeInParam("SortID", notice.SortID),
                Database.MakeInParam("Publisher", notice.Publisher),
                Database.MakeInParam("PublisherTime", notice.PublisherTime),
                Database.MakeInParam("IsHot", notice.IsHot),
                Database.MakeInParam("IsTop", notice.IsTop),
                Database.MakeInParam("Nullity", notice.Nullity),
                Database.MakeInParam("NoticeID", notice.NoticeID)
            };
            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="idlist">标识列表</param>
        /// <returns></returns>
        public int DeleteSystemNotice(string idlist)
        {
            string sqlQuery = $"DELETE SystemNotice WHERE NoticeID IN({idlist})";
            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery);
        }

        #endregion

        #region 排行榜

        /// <summary>
        /// 根据标识获取排行榜配置
        /// </summary>
        /// <param name="configid">配置标识</param>
        /// <returns></returns>
        public RankingConfig GetRankingConfigById(int configid)
        {
            string sql = $"SELECT * FROM RankingConfig WITH(NOLOCK) WHERE ConfigID ={configid}";
            return Database.ExecuteObject<RankingConfig>(sql);
        }

        /// <summary>
        /// 判断排行榜配置是否存在
        /// </summary>
        /// <param name="typeid">排行榜类型</param>
        /// <param name="rankid">排行榜排名</param>
        /// <returns></returns>
        public bool ExistRankingConfig(int typeid, int rankid)
        {
            string sql = $"SELECT ConfigID FROM RankingConfig WITH(NOLOCK) WHERE TypeID ={typeid} AND RankID ={rankid}";
            object obj = Database.ExecuteScalar(CommandType.Text, sql);
            return obj != null;
        }

        /// <summary>
        /// 新增排行榜配置
        /// </summary>
        /// <param name="config">配置信息</param>
        /// <returns></returns>
        public int InsertRankingConfig(RankingConfig config)
        {
            string sql = @"INSERT INTO RankingConfig(TypeID,RankID,Diamond,ValidityTime,UpdateTime)
                            VALUES(@TypeID,@RankID,@Diamond,@ValidityTime,@UpdateTime)";
            var prams = new List<DbParameter>
            {
                Database.MakeInParam("TypeID", config.TypeID),
                Database.MakeInParam("RankID", config.RankID),
                Database.MakeInParam("Diamond", config.Diamond),
                Database.MakeInParam("ValidityTime", config.ValidityTime),
                Database.MakeInParam("UpdateTime", config.UpdateTime)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sql, prams.ToArray());
        }

        /// <summary>
        /// 修改排行榜配置
        /// </summary>
        /// <param name="config">配置信息</param>
        /// <returns></returns>
        public int UpdateRankingConfig(RankingConfig config)
        {
            string sql = @"UPDATE RankingConfig SET TypeID=@TypeID,RankID=@RankID,Diamond=@Diamond,
                    ValidityTime=@ValidityTime,UpdateTime=@UpdateTime WHERE ConfigID=@ConfigID";
            var prams = new List<DbParameter>
            {
                Database.MakeInParam("TypeID", config.TypeID),
                Database.MakeInParam("RankID", config.RankID),
                Database.MakeInParam("Diamond", config.Diamond),
                Database.MakeInParam("ValidityTime", config.ValidityTime),
                Database.MakeInParam("UpdateTime", config.UpdateTime),
                Database.MakeInParam("ConfigID", config.ConfigID)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sql, prams.ToArray());
        }

        /// <summary>
        /// 删除奖励配置
        /// </summary>
        /// <param name="configid">配置标识</param>
        /// <returns></returns>
        public int DeleteRankingConfig(int configid)
        {
            string sql = $"DELETE RankingConfig WHERE ConfigID={configid}";
            return Database.ExecuteNonQuery(sql);
        }

        #endregion

        #region 游戏规则

        /// <summary>
        /// 获取游戏规则
        /// </summary>
        /// <param name="kindid">游戏标识</param>
        /// <returns></returns>
        public GameRule GetGameRuleInfo(int kindid)
        {
            string sql = $"SELECT * FROM GameRule WITH(NOLOCK) WHERE KindID = {kindid} ORDER BY SortID ASC,KindID DESC";
            return Database.ExecuteObject<GameRule>(sql);
        }

        /// <summary>
        /// 新增游戏规则
        /// </summary>
        /// <param name="rule">游戏规则</param>
        /// <returns></returns>
        public int InsertGameRule(GameRule rule)
        {
            string sql =
                @"INSERT INTO GameRule(KindID,KindName,KindIcon,KindIntro,KindRule,Nullity,CollectDate,SortID) VALUES(@KindID,@KindName,@KindIcon,@KindIntro,@KindRule,@Nullity,@CollectDate,@SortID)";
            var prams = new List<DbParameter>
            {
                Database.MakeInParam("KindID", rule.KindID),
                Database.MakeInParam("KindName", rule.KindName),
                Database.MakeInParam("KindIcon", rule.KindIcon),
                Database.MakeInParam("KindIntro", rule.KindIntro),
                Database.MakeInParam("KindRule", rule.KindRule),
                Database.MakeInParam("Nullity", rule.Nullity),
                Database.MakeInParam("SortID", rule.SortID),
                Database.MakeInParam("CollectDate", rule.CollectDate)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sql, prams.ToArray());
        }

        /// <summary>
        /// 修改游戏规则
        /// </summary>
        /// <param name="rule">游戏规则</param>
        /// <returns></returns>
        public int UpdateGameRule(GameRule rule)
        {
            string sqlQuery =
                @"UPDATE GameRule SET KindIcon=@KindIcon,KindIntro=@KindIntro,KindRule=@KindRule,Nullity= @Nullity,SortID=@SortID WHERE KindID= @KindID";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("KindIcon", rule.KindIcon),
                Database.MakeInParam("KindIntro", rule.KindIntro),
                Database.MakeInParam("KindRule", rule.KindRule),
                Database.MakeInParam("Nullity", rule.Nullity),
                Database.MakeInParam("KindID", rule.KindID),
                Database.MakeInParam("SortID", rule.SortID)
            };
            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }

        /// <summary>
        /// 删除游戏规则
        /// </summary>
        /// <param name="idlist">标识列表</param>
        /// <returns></returns>
        public int DeleteGameRule(string idlist)
        {
            string sqlQuery = $"DELETE GameRule WHERE KindID IN({idlist})";
            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery);
        }

        /// <summary>
        /// 禁用启用游戏规则
        /// </summary>
        /// <param name="idlist">标识列表</param>
        /// <param name="nullity">禁用启用标识</param>
        /// <returns></returns>
        public int NullityGameRule(string idlist, int nullity)
        {
            string sqlQuery = $"UPDATE GameRule SET Nullity={nullity} WHERE KindID IN({idlist})";
            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery);
        }

        #endregion

        #region 常见问题

        /// <summary>
        /// 获取常见问题实体 by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Question GetQuestionInfo(int id)
        {
            return Database.ExecuteObject<Question>($"SELECT * FROM DBO.[Question] WHERE ID={id}");
        }

        /// <summary>
        /// 保存常见问题（新增、修改）通用
        /// </summary>
        /// <param name="quest"></param>
        /// <returns></returns>
        public int SaveQuestionInfo(Question quest)
        {
            var parm = new List<DbParameter>()
            {
                Database.MakeInParam("QuestionTitle", quest.QuestionTitle),
                Database.MakeInParam("Answer", quest.Answer),
                Database.MakeInParam("SortID", quest.SortID)
            };
            if (quest.ID > 0) parm.Add(Database.MakeInParam("ID", quest.ID));

            const string sqlInsert =
                " INSERT DBO.[Question] (QuestionTitle,Answer,SortID) VALUES (@QuestionTitle,@Answer,@SortID) ";
            const string sqlUpdate =
                " UPDATE DBO.[Question] SET QuestionTitle=@QuestionTitle,Answer=@Answer,SortID=@SortID WHERE ID=@ID ";

            return Database.ExecuteNonQuery(CommandType.Text, quest.ID > 0 ? sqlUpdate : sqlInsert, parm.ToArray());
        }

        /// <summary>
        /// 批量删除常见问题
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteQuestionInfo(string ids)
        {
            return Database.ExecuteNonQuery($"DELETE DBO.[Question] WHERE ID IN ({ids})");
        }

        #endregion
    }
}
