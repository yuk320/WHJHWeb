using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Game.Kernel;
using Game.IData;
using Game.Entity.Treasure;
using Game.Entity.Record;

namespace Game.Data
{
    /// <summary>
    /// 金币库数据层
    /// </summary>
    public class TreasureDataProvider : BaseDataProvider, ITreasureDataProvider
    {
        #region 构造方法

        public TreasureDataProvider(string connString)
            : base(connString)
        {
        }

        #endregion

        #region 公共分页

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

        #region 充值配置

        /// <summary>
        /// 获取充值产品
        /// </summary>
        /// <param name="configId">充值产品标识</param>
        /// <returns></returns>
        public AppPayConfig GetAppPayConfig(int configId)
        {
            string sqlQuery = $"SELECT * FROM AppPayConfig WITH(NOLOCK) WHERE ConfigID = '{configId}'";
            return Database.ExecuteObject<AppPayConfig>(sqlQuery);
        }

        /// <summary>
        /// 删除充值产品
        /// </summary>
        /// <param name="idlist">标识列表</param>
        public int DeleteAppPayConfig(string idlist)
        {
            string sqlQuery = $"DELETE AppPayConfig WHERE ConfigID IN({idlist})";
            return Database.ExecuteNonQuery(sqlQuery);
        }

        /// <summary>
        /// 判断充值产品是否存在
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public bool IsExistAppPayConfig(string where)
        {
            string sqlQuery = $"SELECT ConfigID FROM AppPayConfig WITH(NOLOCK) {where} ";
            return Database.ExecuteScalar(CommandType.Text, sqlQuery) != null;
        }

        /// <summary>
        /// 新增充值产品
        /// </summary>
        /// <param name="config">充值产品</param>
        /// <returns></returns>
        public int InsertAppPayConfig(AppPayConfig config)
        {
            string sqlQuery =
                @"INSERT INTO AppPayConfig(AppleID,PayName,PayType,PayPrice,PayIdentity,ImageType,SortID,Diamond,PresentDiamond,ConfigTime) 
                                        VALUES(@AppleID,@PayName,@PayType,@PayPrice,@PayIdentity,@ImageType,@SortID,@Diamond,@PresentDiamond,@ConfigTime)";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("AppleID", config.AppleID),
                Database.MakeInParam("PayName", config.PayName),
                Database.MakeInParam("PayType", config.PayType),
                Database.MakeInParam("PayPrice", config.PayPrice),
                Database.MakeInParam("PayIdentity", config.PayIdentity),
                Database.MakeInParam("ImageType", config.ImageType),
                Database.MakeInParam("SortID", config.SortID),
                Database.MakeInParam("Diamond", config.Diamond),
                Database.MakeInParam("PresentDiamond", config.PresentDiamond),
                Database.MakeInParam("ConfigTime", config.ConfigTime)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }

        /// <summary>
        /// 修改充值产品
        /// </summary>
        /// <param name="config">充值产品</param>
        /// <returns></returns>
        public int UpdateAppPayConfig(AppPayConfig config)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE AppPayConfig SET ")
                .Append("AppleID=@AppleID, ")
                .Append("PayName=@PayName, ")
                .Append("PayType=@PayType, ")
                .Append("PayPrice=@PayPrice, ")
                .Append("PayIdentity=@PayIdentity, ")
                .Append("ImageType=@ImageType, ")
                .Append("Diamond=@Diamond, ")
                .Append("PresentDiamond=@PresentDiamond, ")
                .Append("SortID=@SortID ")
                .Append("WHERE ConfigID=@ConfigID");

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("AppleID", config.AppleID),
                Database.MakeInParam("PayName", config.PayName),
                Database.MakeInParam("PayType", config.PayType),
                Database.MakeInParam("PayPrice", config.PayPrice),
                Database.MakeInParam("PayIdentity", config.PayIdentity),
                Database.MakeInParam("ImageType", config.ImageType),
                Database.MakeInParam("Diamond", config.Diamond),
                Database.MakeInParam("PresentDiamond", config.PresentDiamond),
                Database.MakeInParam("SortID", config.SortID),
                Database.MakeInParam("ConfigID", config.ConfigID)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        #endregion

        #region 推广配置

        /// <summary>
        /// 获取推广配置
        /// </summary>
        /// <param name="configId">推广配置id</param>
        /// <returns></returns>
        public SpreadConfig GetSpreadConfig(int configId)
        {
            string sqlQuery = $"SELECT * FROM SpreadConfig WITH(NOLOCK) WHERE ConfigID = {configId}";
            return Database.ExecuteObject<SpreadConfig>(sqlQuery);
        }

        /// <summary>
        /// 推广配置数量
        /// </summary>
        /// <returns></returns>
        public int SpreadConfigCount()
        {
            string sqlQuery = "SELECT COUNT(ConfigID) FROM SpreadConfig WITH(NOLOCK)";
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery);
            return obj == null ? 0 : Convert.ToInt32(obj);
        }

        /// <summary>
        /// 删除推广配置
        /// </summary>
        /// <param name="configId">推广配置id</param>
        /// <returns></returns>
        public int DeleteSpreadConfig(int configId)
        {
            string sqlQuery = $"DELETE SpreadConfig WHERE ConfigID = {configId}";
            return Database.ExecuteNonQuery(sqlQuery);
        }

        /// <summary>
        /// 新增推广信息
        /// </summary>
        /// <param name="config">推广配置</param>
        /// <returns></returns>
        public int InsertSpreadConfig(SpreadConfig config)
        {
            string sqlQuery =
                @"INSERT INTO SpreadConfig(SpreadNum,PresentDiamond,PresentPropID,PresentPropName,PresentPropNum,UpdateTime) 
                            VALUES(@SpreadNum,@PresentDiamond,@PresentPropID,@PresentPropName,@PresentPropNum,@UpdateTime)";
            var prams = new List<DbParameter>
            {
                Database.MakeInParam("SpreadNum", config.SpreadNum),
                Database.MakeInParam("PresentDiamond", config.PresentDiamond),
                Database.MakeInParam("PresentPropID", config.PresentPropID),
                Database.MakeInParam("PresentPropName", config.PresentPropName),
                Database.MakeInParam("PresentPropNum", config.PresentPropNum),
                Database.MakeInParam("UpdateTime", config.UpdateTime)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }

        /// <summary>
        /// 修改推广信息
        /// </summary>
        /// <param name="config">推广配置</param>
        /// <returns></returns>
        public int UpdateSpreadConfig(SpreadConfig config)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE SpreadConfig SET ")
                .Append("SpreadNum=@SpreadNum, ")
                .Append("PresentDiamond=@PresentDiamond, ")
                .Append("PresentPropID=@PresentPropID, ")
                .Append("PresentPropName=@PresentPropName, ")
                .Append("PresentPropNum=@PresentPropNum ")
                .Append("WHERE ConfigID=@ConfigID");

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("SpreadNum", config.SpreadNum),
                Database.MakeInParam("PresentDiamond", config.PresentDiamond),
                Database.MakeInParam("PresentPropID", config.PresentPropID),
                Database.MakeInParam("PresentPropName", config.PresentPropName),
                Database.MakeInParam("PresentPropNum", config.PresentPropNum),
                Database.MakeInParam("ConfigID", config.ConfigID)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        /// <summary>
        /// 获取推广返利配置
        /// </summary>
        /// <param name="configId"></param>
        /// <returns></returns>
        public SpreadReturnConfig GetSpreadReturnConfig(int configId)
        {
            string sqlQuery = $"SELECT * FROM SpreadReturnConfig WITH(NOLOCK) WHERE ConfigID = {configId}";
            return Database.ExecuteObject<SpreadReturnConfig>(sqlQuery);
        }

        /// <summary>
        /// 删除推广返利配置
        /// </summary>
        /// <param name="configId"></param>
        /// <returns></returns>
        public int DeleteSpreadReturnConfig(int configId)
        {
            string sqlQuery = $"DELETE SpreadReturnConfig WHERE ConfigID = {configId}";
            return Database.ExecuteNonQuery(sqlQuery);
        }


        /// <summary>
        /// 保存推广返利配置（新增、更新）
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public int SaveSpreadReturnConfig(SpreadReturnConfig config)
        {
            string sql;
            var prams = new List<DbParameter>
            {
                Database.MakeInParam("SpreadLevel", config.SpreadLevel),
                Database.MakeInParam("PresentScale", config.PresentScale),
                Database.MakeInParam("Nullity", config.Nullity),
                Database.MakeInParam("UpdateTime", config.UpdateTime)
            };
            if (config.ConfigID > 0)
            {
                prams.Add(Database.MakeInParam("ConfigID",config.ConfigID));
                sql =
                    "UPDATE SpreadReturnConfig SET SpreadLevel=@SpreadLevel,PresentScale=@PresentScale,Nullity=@Nullity,UpdateTime=@UpdateTime WHERE ConfigID=@ConfigID";
            }
            else
            {
                sql = @"
                            IF NOT EXISTS (SELECT 1 FROM SpreadReturnConfig WHERE SpreadLevel = @SpreadLevel)
                            BEGIN
                                INSERT INTO SpreadReturnConfig(SpreadLevel,PresentScale,Nullity,UpdateTime) 
                                VALUES(@SpreadLevel,@PresentScale,@Nullity,@UpdateTime)
                            END";
            }
            return Database.ExecuteNonQuery(CommandType.Text, sql, prams.ToArray());
        }

        #endregion

        #region 赠送钻石

        /// <summary>
        /// 赠送钻石
        /// </summary>
        /// <param name="diamond">赠送钻石信息</param>
        /// <returns></returns>
        public Message GrantDiamond(RecordGrantDiamond diamond)
        {
            var prams = new List<DbParameter>
            {
                Database.MakeInParam("MasterID", diamond.MasterID),
                Database.MakeInParam("ClientIP", diamond.ClientIP),
                Database.MakeInParam("UserID", diamond.UserID),
                Database.MakeInParam("AddDiamond", diamond.AddDiamond),
                Database.MakeInParam("TypeID", diamond.TypeID),
                Database.MakeInParam("CollectNote", diamond.CollectNote)
            };

            Message msg = MessageHelper.GetMessage(Database, "WSP_PM_GrantDiamond", prams);
            return msg;
        }

        /// <summary>
        /// 获取钻石前50名排行
        /// </summary>
        /// <returns></returns>
        public DataSet GetDiamondRank()
        {
            string sqlQuery = "SELECT TOP 50 UserID,Diamond FROM UserCurrency ORDER BY Diamond DESC,UserID ASC";
            return Database.ExecuteDataset(CommandType.Text, sqlQuery);
        }

        #endregion

        #region 统计总数

        /// <summary>
        /// 系统统计
        /// </summary>
        /// <returns></returns>
        public DataSet GetStatInfo()
        {
            return Database.ExecuteDataset(CommandType.StoredProcedure, "NET_PM_StatInfo");
        }

        /// <summary>
        /// 按条件获取已支付总数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public decimal GetTotalPayAmount(string where)
        {
            string sql = $"SELECT ISNULL(SUM(Amount),0) AS Amount FROM OnLinePayOrder {where} AND OrderStatus=1";
            return Convert.ToDecimal(Database.ExecuteScalar(CommandType.Text, sql));
        }

        /// <summary>
        /// 按条件获取已支付订单数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public long GetTotalPayOrderCount(string where)
        {
            string sql = $"SELECT COUNT(1) AS [COUNT] FROM OnLinePayOrder {where} AND OrderStatus=1";
            return Convert.ToInt64(Database.ExecuteScalar(CommandType.Text, sql));
        }

        /// <summary>
        /// 获取钻石统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public long GetTotalUserDiamond(string where)
        {
            string sqlQuery = $"SELECT ISNULL(SUM(Diamond),0) AS Diamond FROM UserCurrency {where}";
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery);
            return obj != null ? Convert.ToInt64(obj) : 0;
        }

        /// <summary>
        /// 统计银行税收
        /// </summary>
        /// <param name="sTime">起始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns></returns>
        public IList<StatisticsRevenue> GetDayInsureRevenue(string sTime, string eTime)
        {
            string sqlQuery =
                @"SELECT CONVERT(VARCHAR(10),CollectDate,120) AS TimeDate,ISNULL(SUM(Revenue),0) AS Revenue FROM 
                        (SELECT CollectDate,Revenue FROM RecordInsure WITH(NOLOCK) WHERE Revenue>0 AND 
                            CollectDate>=@sTime AND CollectDate<=@eTime) AS T GROUP BY CONVERT(VARCHAR(10),CollectDate,120) ORDER BY TimeDate";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("sTime", sTime),
                Database.MakeInParam("eTime", eTime)
            };
            return Database.ExecuteObjectList<StatisticsRevenue>(sqlQuery, prams);
        }

        /// <summary>
        /// 统计游戏税收
        /// </summary>
        /// <param name="sTime">起始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns></returns>
        public IList<StatisticsRevenue> GetDayGameRevenue(string sTime, string eTime)
        {
            string sqlQuery =
                @"SELECT CONVERT(VARCHAR(10),ConcludeTime,120) AS TimeDate,ISNULL(SUM(Revenue),0) AS Revenue FROM 
                        (SELECT ConcludeTime,Revenue FROM RecordDrawInfo WITH(NOLOCK) WHERE Revenue>0 AND 
                            ConcludeTime>=@sTime AND ConcludeTime<=@eTime) AS T GROUP BY CONVERT(VARCHAR(10),ConcludeTime,120) ORDER BY TimeDate";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("sTime", sTime),
                Database.MakeInParam("eTime", eTime)
            };
            return Database.ExecuteObjectList<StatisticsRevenue>(sqlQuery, prams);
        }

        /// <summary>
        /// 统计游戏损耗
        /// </summary>
        /// <param name="sTime">起始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <param name="extra"></param>
        /// <returns></returns>
        public IList<StatisticsWaste> GetDayWaste(string sTime, string eTime, string extra = "")
        {
            string extra0;
            string extra1;
            if (extra.IndexOf("|", StringComparison.Ordinal) > 0)
            {
                extra0 = extra.Split('|')[0];
                extra1 = extra.Split('|')[1];
            }
            else
            {
                extra0 = extra1 = "";
            }
            string sqlQuery =
                $"SELECT CONVERT(VARCHAR(10),ConcludeTime,120) AS TimeDate,ISNULL(SUM(Waste),0) AS Waste {extra1} FROM (SELECT ConcludeTime,Waste,KindID,ServerID FROM RecordDrawInfo WITH(NOLOCK) WHERE Waste>0 {extra0} AND  ConcludeTime>='{sTime}' AND ConcludeTime<='{eTime}') AS T GROUP BY CONVERT(VARCHAR(10),ConcludeTime,120) {extra1} ORDER BY TimeDate";
            return Database.ExecuteObjectList<StatisticsWaste>(sqlQuery);
        }

        /// <summary>
        /// 获取金币分布统计
        /// </summary>
        /// <returns></returns>
        public DataSet GetGoldDistribute()
        {
            return Database.ExecuteDataset(CommandType.StoredProcedure, "NET_PW_GetGoldDistribute");
        }

        /// <summary>
        /// 获取钻石分布统计
        /// </summary>
        /// <returns></returns>
        public DataSet GetDiamondDistribute()
        {
            return Database.ExecuteDataset(CommandType.StoredProcedure, "NET_PW_GetDiamondDistribute");
        }

        #endregion

        #region 金币信息

        /// <summary>
        /// 赠送金币
        /// </summary>
        /// <param name="rgt">赠送金币信息</param>
        /// <returns></returns>
        public Message GrantTreasure(RecordGrantTreasure rgt)
        {
            var prams = new List<DbParameter>
            {
                Database.MakeInParam("MasterID", rgt.MasterID),
                Database.MakeInParam("ClientIP", rgt.ClientIP),
                Database.MakeInParam("UserID", rgt.UserID),
                Database.MakeInParam("AddGold", rgt.AddGold),
                Database.MakeInParam("Reason", rgt.Reason)
            };

            Message msg = MessageHelper.GetMessage(Database, "WSP_PM_GrantTreasure", prams);
            return msg;
        }

        /// <summary>
        /// 获取兑换金币配置
        /// </summary>
        /// <param name="configId">配置标识</param>
        /// <returns></returns>
        public CurrencyExchConfig GetCurrencyExch(int configId)
        {
            string sqlQuery = $"SELECT * FROM CurrencyExchConfig WITH(NOLOCK) WHERE ConfigID = '{configId}'";
            return Database.ExecuteObject<CurrencyExchConfig>(sqlQuery);
        }

        /// <summary>
        /// 删除兑换金币配置
        /// </summary>
        /// <param name="idlist">标识列表</param>
        public int DeleteCurrencyExch(string idlist)
        {
            string sqlQuery = $"DELETE CurrencyExchConfig WHERE ConfigID IN({idlist})";
            return Database.ExecuteNonQuery(sqlQuery);
        }

        /// <summary>
        /// 判断兑换金币配置是否存在相同钻石
        /// </summary>
        /// <param name="diamond">兑换钻石数</param>
        /// <returns></returns>
        public bool IsExistCurrencyExch(int diamond)
        {
            string sqlQuery = $"SELECT ConfigID FROM CurrencyExchConfig WITH(NOLOCK) WHERE Diamond = {diamond} ";
            return Database.ExecuteScalar(CommandType.Text, sqlQuery) != null;
        }

        /// <summary>
        /// 新增兑换金币配置
        /// </summary>
        /// <param name="config">兑换金币配置</param>
        /// <returns></returns>
        public int InsertCurrencyExch(CurrencyExchConfig config)
        {
            string sqlQuery =
                @"INSERT INTO CurrencyExchConfig(ConfigName,Diamond,ExchGold,ImageType,SortID) VALUES(@ConfigName,@Diamond,@ExchGold,@ImageType,@SortID)";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("ConfigName", config.ConfigName),
                Database.MakeInParam("Diamond", config.Diamond),
                Database.MakeInParam("ExchGold", config.ExchGold),
                Database.MakeInParam("ImageType", config.ImageType),
                Database.MakeInParam("SortID", config.SortID)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }

        /// <summary>
        /// 修改兑换金币配置
        /// </summary>
        /// <param name="config">兑换金币配置</param>
        /// <returns></returns>
        public int UpdateCurrencyExch(CurrencyExchConfig config)
        {
            string sqlQuery =
                @"UPDATE CurrencyExchConfig SET ConfigName=@ConfigName,Diamond=@Diamond,ExchGold=@ExchGold,ImageType=@ImageType,SortID=@SortID WHERE ConfigID=@ConfigID";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("ConfigName", config.ConfigName),
                Database.MakeInParam("Diamond", config.Diamond),
                Database.MakeInParam("ExchGold", config.ExchGold),
                Database.MakeInParam("ImageType", config.ImageType),
                Database.MakeInParam("SortID", config.SortID),
                Database.MakeInParam("ConfigID", config.ConfigID)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }

        #endregion

        #region 卡线管理

        /// <summary>
        /// 批量清除玩家卡线
        /// </summary>
        /// <param name="userlist"></param>
        /// <returns></returns>
        public int CleanGameScoreLocker(string userlist)
        {
            string sqlQuery = $"DELETE GameScoreLocker  WHERE UserID IN({userlist})";
            return Database.ExecuteNonQuery(sqlQuery);
        }

        #endregion
    }
}