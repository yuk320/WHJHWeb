using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Game.Entity.Record;
using Game.IData;
using Game.Kernel;
using System.Data.SqlClient;

namespace Game.Data
{
    /// <summary>
    /// 记录库数据层
    /// </summary>
    public class RecordDataProvider : BaseDataProvider, IRecordDataProvider
    {
        #region 构造方法

        public RecordDataProvider(string connString)
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

        #region 查询钻石

        /// <summary>
        /// 获取代理钻石信息
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <returns></returns>
        public DataSet GetQueryAgentDiamond(int userid)
        {
            var prams = new List<DbParameter> {Database.MakeInParam("dwUserID", userid)};

            return Database.ExecuteDataset(CommandType.StoredProcedure, "NET_PW_QueryAgentDiamond", prams.ToArray());
        }

        /// <summary>
        /// 获取转出钻石前50名排行
        /// </summary>
        /// <returns></returns>
        public DataSet GetTradeOutDiamondRank()
        {
            string sqlQuery =
                "SELECT TOP 50 SourceUserID AS UserID,SUM(PresentDiamond) AS PresentDiamond FROM RecordPresentCurrency WITH(NOLOCK) GROUP BY SourceUserID ORDER BY SUM(PresentDiamond) DESC";
            return Database.ExecuteDataset(CommandType.Text, sqlQuery);
        }

        /// <summary>
        /// 获取购买道具花费钻石前50名排行
        /// </summary>
        /// <returns></returns>
        public DataSet GetBuyPropDiamondRank()
        {
            string sqlQuery =
                "SELECT UserID,SUM(Diamond) AS Diamond FROM RecordBuyNewProperty WITH(NOLOCK) GROUP BY UserID ORDER BY SUM(Diamond) DESC";
            return Database.ExecuteDataset(CommandType.Text, sqlQuery);
        }

        /// <summary>
        /// 获取统计数据
        /// </summary>
        /// <param name="sDateId">起始时间</param>
        /// <param name="eDateId">结束时间</param>
        /// <returns></returns>
        public IList<RecordEveryDayCurrency> GetRecordEveryDayCurrency(string sDateId, string eDateId)
        {
            string sqlQuery =
                $"SELECT * FROM RecordEveryDayCurrency WITH(NOLOCK) WHERE DateID>={sDateId} AND DateID<={eDateId} ORDER BY DateID ASC";
            return Database.ExecuteObjectList<RecordEveryDayCurrency>(sqlQuery);
        }

        /// <summary>
        /// 获取钻石兑换金币前50名排行
        /// </summary>
        /// <returns></returns>
        public DataSet GetDiamondExchangeGoldRank()
        {
            string sqlQuery =
                "SELECT TOP 50 UserID,SUM(PresentGold) AS Gold,SUM(ExchDiamond) AS Diamond FROM RecordCurrencyExch WITH(NOLOCK) GROUP BY UserID ORDER BY SUM(ExchDiamond) DESC";
            return Database.ExecuteDataset(CommandType.Text, sqlQuery);
        }

        #endregion

        #region 推送记录

        /// <summary>
        /// 批量新增推送记录
        /// </summary>
        /// <param name="table">新增数据集</param>
        /// <param name="connStr">数据库连接字符串</param>
        /// <returns></returns>
        public int AddRecordAccountsUmeng(DataTable table, string connStr)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlBulkCopy bulkCopy = new SqlBulkCopy(conn)
                    {
                        DestinationTableName = "RecordAccountsUmeng",
                        BatchSize = table.Rows.Count
                    };
                    conn.Open();
                    if (table.Rows.Count != 0)
                    {
                        bulkCopy.WriteToServer(table);
                    }
                    conn.Close();
                }
                return table.Rows.Count;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 新增单条推送记录
        /// </summary>
        /// <param name="umeng">推送信息</param>
        /// <returns></returns>
        public int AddRecordAccountsUmeng(RecordAccountsUmeng umeng)
        {
            string sqlQuery = @"INSERT INTO RecordAccountsUmeng(MasterID,UserID,PushType,PushContent,PushTime,PushIP) 
                            VALUES(@MasterID,@UserID,@PushType,@PushContent,@PushTime,@PushIP)";
            var prams = new List<DbParameter>
            {
                Database.MakeInParam("MasterID", umeng.MasterID),
                Database.MakeInParam("UserID", umeng.UserID),
                Database.MakeInParam("PushType", umeng.PushType),
                Database.MakeInParam("PushContent", umeng.PushContent),
                Database.MakeInParam("PushTime", umeng.PushTime),
                Database.MakeInParam("PushIP", umeng.PushIP)
            };
            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }

        #endregion

        #region 统计总数

        /// <summary>
        /// 获取钻石变化统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public long GetTotalDiamondChange(string where)
        {
            string sqlQuery =
                $"SELECT ISNULL(SUM(ChangeDiamond),0) AS Diamond FROM RecordDiamondSerial WITH(NOLOCK) {where}";
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery);
            return obj != null ? Convert.ToInt64(obj) : 0;
        }

        /// <summary>
        /// 获取后台赠送钻石统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public long GetTotalBackPresentDiamond(string where)
        {
            string sqlQuery =
                $"SELECT ISNULL(SUM(AddDiamond),0) AS Diamond FROM RecordGrantDiamond WITH(NOLOCK) {where}";
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery);
            return obj != null ? Convert.ToInt64(obj) : 0;
        }

        /// <summary>
        /// 获取代理赠送钻石统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public long GetTotalAgentPresentDiamond(string where)
        {
            string sqlQuery =
                $"SELECT ISNULL(SUM(PresentDiamond),0) AS Diamond FROM RecordPresentCurrency WITH(NOLOCK) {where}";
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery);
            return obj != null ? Convert.ToInt64(obj) : 0;
        }

        /// <summary>
        /// 获取购买喇叭花费钻石统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public long GetTotalBuyHornDiamond(string where)
        {
            string sqlQuery =
                $"SELECT ISNULL(SUM(Diamond),0) AS Diamond FROM RecordBuyNewProperty WITH(NOLOCK) {where}";
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery);
            return obj != null ? Convert.ToInt64(obj) : 0;
        }

        /// <summary>
        /// 获取AA制钻石消耗
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public long GetTotalAaGameDiamond(string where)
        {
            string sqlQuery =
                $"SELECT ISNULL(SUM(Diamond),0) AS Diamond FROM RecordGameDiamond WITH(NOLOCK) {where}";
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery);
            return obj != null ? Convert.ToInt64(obj) : 0;
        }

        /// <summary>
        /// 获取钻石兑换金币统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public TotalDiamondExch GetTotalDiamondExchGold(string where)
        {
            string sqlQuery =
                $"SELECT ISNULL(SUM(ExchDiamond),0) AS ExchDiamond, ISNULL(SUM(PresentGold),0) AS PresentGold FROM RecordCurrencyExch WITH(NOLOCK) {where}";
            return Database.ExecuteObject<TotalDiamondExch>(sqlQuery);
        }

        /// <summary>
        /// 获取购买喇叭花费金币统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public long GetTotalBuyHornGold(string where)
        {
            string sqlQuery =
                $"SELECT ISNULL(SUM(CostGold),0) AS CostGold FROM RecordGoldBuyProperty WITH(NOLOCK) {where}";
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery);
            return obj != null ? Convert.ToInt64(obj) : 0;
        }

        /// <summary>
        /// 获取金币变化统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public long GetTotalTreasureChange(string where)
        {
            string sqlQuery =
                $"SELECT ISNULL(SUM(ChangeScore),0) AS Diamond FROM RecordTreasureSerial WITH(NOLOCK) {where}";
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery);
            return obj != null ? Convert.ToInt64(obj) : 0;
        }

        /// <summary>
        /// 获取用户钻石兑换金币总数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public long[] GetTotalDiamondExch(string where)
        {
            string sqlQuery =
                $"SELECT ISNULL(SUM(PresentGold),0) AS Gold,ISNULL(SUM(ExchDiamond),0) AS Diamond FROM RecordCurrencyExch WITH(NOLOCK) {where}";
            DataSet obj = Database.ExecuteDataset(CommandType.Text, sqlQuery);
            return new[] {Convert.ToInt64(obj.Tables[0].Rows[0]["Gold"]), Convert.ToInt64(obj.Tables[0].Rows[0]["Diamond"])};
        }

        #endregion

        #region 赠送靓号

        /// <summary>
        /// 赠送靓号
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <param name="gameId">游戏ID</param>
        /// <param name="masterId">管理员标识</param>
        /// <param name="strReason">赠送原因</param>
        /// <param name="strIp">赠送ip</param>
        /// <returns></returns>
        public Message GrantGameId(int userId, int gameId, int masterId, string strReason, string strIp)
        {
            var prams = new List<DbParameter>
            {
                Database.MakeInParam("UserID", userId),
                Database.MakeInParam("ReGameID", gameId),
                Database.MakeInParam("MasterID", masterId),
                Database.MakeInParam("Reason", strReason),
                Database.MakeInParam("ClientIP", strIp),
                Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
            };

            return MessageHelper.GetMessage(Database, "WSP_PM_GrantGameID", prams);
        }

        #endregion 赠送靓号
    }
}