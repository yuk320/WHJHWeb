using System;
using System.Collections.Generic;
using System.Data;
using Game.Kernel;
using Game.IData;
using System.Data.Common;

namespace Game.Data
{
    /// <summary>
    /// 记录库数据访问层
    /// </summary>
    public sealed class RecordDataProvider : BaseDataProvider, IRecordDataProvider
    {
        #region Fields

        #endregion

        #region 构造方法

        public RecordDataProvider(string connString)
            : base(connString)
        {
        }

        #endregion

        #region 代理信息

        /// <summary>
        /// 获取今日赠送钻石
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetTodayAgentPresentDiamond(int userId)
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            const string sql =
                "SELECT ISNULL(SUM(PresentDiamond),0) FROM RecordPresentCurrency WITH(NOLOCK) WHERE SourceUserID=@SourceUserID AND CollectDate BETWEEN @StartTime AND @EndTime";
            List<DbParameter> prams =
                new List<DbParameter>
                {
                    Database.MakeInParam("SourceUserID", userId),
                    Database.MakeInParam("StartTime", Convert.ToDateTime(today + " 00:00:00")),
                    Database.MakeInParam("EndTime", Convert.ToDateTime(today + " 23:59:59"))
                };
            object obj = Database.ExecuteScalar(CommandType.Text, sql, prams.ToArray());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }

        /// <summary>
        /// 代理赠送钻石记录
        /// </summary>
        /// <param name="whereQuery">查询条件</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public PagerSet GetAgentPresentDiamondRecord(string whereQuery, int pageIndex, int pageSize)
        {
            const string orderQuery = "ORDER By CollectDate DESC";
            string[] returnField = {"SourceDiamond", "TargetUserID", "PresentDiamond", "CollectDate", "CollectNote"};
            PagerParameters pager = new PagerParameters("RecordPresentCurrency", orderQuery, whereQuery, pageIndex,
                pageSize, returnField) {CacherSize = 2};
            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 代理兑换游戏币记录
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetAgentExchangeDiamondRecord(string whereQuery, int pageIndex, int pageSize)
        {
            const string orderQuery = "ORDER By CollectDate DESC";
            string[] returnField = {"CurDiamond", "PresentGold", "ExchDiamond", "CollectDate"};
            PagerParameters pager = new PagerParameters("RecordCurrencyExch", orderQuery, whereQuery, pageIndex,
                pageSize, returnField) {CacherSize = 2};
            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 获取我的玩家分页列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetAgentBelowUserPresentDiamondRecord(string whereQuery, int pageIndex, int pageSize)
        {
            const string orderQuery = "ORDER By SUM(PresentDiamond) DESC";
            string[] returnField = {"TargetUserID", "SUM(PresentDiamond)"};
            string[] returnFieldAlias = {"UserID", "SumDiamond"};
            PagerParameters pager = new PagerParameters("RecordPresentCurrency", orderQuery, whereQuery, pageIndex,
                pageSize, returnField, returnFieldAlias) {CacherSize = 2};
            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 获取我的代理分页列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetAgentBelowAgentPresentDiamondRecord(string whereQuery, int pageIndex, int pageSize)
        {
            const string orderQuery = "ORDER By SUM(PresentDiamond) DESC";
            string[] returnField = {"SourceUserID", "SUM(PresentDiamond)"};
            string[] returnFieldAlias = {"UserID", "SumDiamond"};
            PagerParameters pager = new PagerParameters("RecordPresentCurrency", orderQuery, whereQuery, pageIndex,
                pageSize, returnField, returnFieldAlias) {CacherSize = 2};
            PagerSet ps = GetPagerSet2(pager);
            return ps ?? new PagerSet();
        }

        #endregion

        #region 推广系统

        /// <summary>
        /// 玩家领取推广返利
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="num"></param>
        /// <param name="clientIp"></param>
        /// <returns></returns>
        public Message UserSpreadReceive(int userid, int num,string clientIp)
        {
            var param = new List<DbParameter>()
            {
                Database.MakeInParam("dwUserID", userid),
                Database.MakeInParam("dwNum",num),
                Database.MakeInParam("strClientIP",clientIp),
                Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
            };

            return MessageHelper.GetMessage(Database, "NET_PJ_ReceiveSpreadReturn", param);
        }

        #endregion

        #region 钻石记录

        /// <summary>
        /// 钻石流水记录
        /// </summary>
        /// <param name="whereQuery">查询条件</param>
        /// <param name="pageIndex">页下标</param>
        /// <param name="pageSize">页显示数</param>
        /// <returns></returns>
        public PagerSet GetDiamondStreamList(string whereQuery, int pageIndex, int pageSize)
        {
            string[] returnField = {"SerialNumber", "TypeID", "CurDiamond", "ChangeDiamond", "CollectDate"};
            PagerParameters pager = new PagerParameters("RecordDiamondSerial", "ORDER BY CollectDate DESC", whereQuery,
                pageIndex, pageSize, returnField) {CacherSize = 2};
            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 获取某两人之间的转账总数（赠送标识或接收标识不可均为0）
        /// </summary>
        /// <param name="sourceUserId">赠送标识（为空则为接收人所有接收）</param>
        /// <param name="targetUserId">接收标识（为空则为赠送人所有赠送）</param>
        /// <param name="sqlTime">时间参数（可传其他条件）</param>
        /// <returns></returns>
        public long GetTotalPresentCount(int sourceUserId, int targetUserId, string sqlTime)
        {
            string sql = " SELECT SUM(PresentDiamond) AS SumDiamond FROM RecordPresentCurrency(NOLOCK) WHERE 1=1 ";

            if (targetUserId == 0 && sourceUserId == 0)
            {
                //赠送、接收均为不指定则直接返回0
                return 0;
            }

            if (sourceUserId > 0)
            {
                sql += $" AND SourceUserId = {sourceUserId} ";
            }
            if (targetUserId > 0)
            {
                sql += $" AND TargetUserId = {targetUserId} ";
            }
            sql += sqlTime + " GROUP BY SourceUserId,TargetUserId ";
            object result = Database.ExecuteScalar(CommandType.Text, sql);
            return result == null ? 0 : Convert.ToInt64(result);
        }

        #endregion

        #region 金币信息

        /// <summary>
        /// 金币流水记录
        /// </summary>
        /// <param name="whereQuery">查询条件</param>
        /// <param name="pageIndex">页下标</param>
        /// <param name="pageSize">页显示数</param>
        /// <returns></returns>
        public PagerSet GetGoldStreamList(string whereQuery, int pageIndex, int pageSize)
        {
            string[] returnField =
                {"SerialNumber", "TypeID", "CurScore", "CurInsureScore", "ChangeScore", "CollectDate"};
            PagerParameters pager = new PagerParameters("RecordTreasureSerial", "ORDER BY CollectDate DESC", whereQuery,
                pageIndex, pageSize, returnField) {CacherSize = 2};

            return GetPagerSet2(pager);
        }

        #endregion

        #region 公共方法

        public T GetEntity<T>(string tableName, string sqlWhere)
        {
            return GetTableProvider(tableName).GetObject<T>(sqlWhere);
        }

        public IList<T> GetList<T>(string tableName, string sqlWhere)
        {
            return GetTableProvider(tableName).GetObjectList<T>(sqlWhere);
        }

        public DataRow GetOne(string tableName, string sqlWhere)
        {
            return GetTableProvider(tableName).GetOne(sqlWhere);
        }

        public DataRow GetOne(string sql)
        {
            DataSet ds = Database.ExecuteDataset(CommandType.Text, sql);
            return ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0] : null;
        }

        public DataSet GetDataSet(string sql)
        {
            return Database.ExecuteDataset(CommandType.Text, sql);
        }

        public int GetRecordCount(string tableName, string sqlWhere)
        {
            return GetTableProvider(tableName).GetRecordsCount(sqlWhere);
        }

        public int GetRecordCount(string sql)
        {
            return Database.ExecuteScalar(CommandType.Text, sql) != null
                ? Convert.ToInt32(Database.ExecuteScalar(CommandType.Text, sql))
                : 0;
        }

        #endregion
    }
}