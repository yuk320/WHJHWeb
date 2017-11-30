using System;
using System.Collections.Generic;
using Game.Kernel;
using Game.IData;
using System.Data.Common;
using System.Data;
using Game.Entity.Treasure;

namespace Game.Data
{
    /// <summary>
    /// 金币数据访问层
    /// </summary>
    public class TreasureDataProvider : BaseDataProvider, ITreasureDataProvider
    {
        #region 构造方法

        public TreasureDataProvider(string connString)
            : base(connString)
        {
        }

        #endregion

        #region 充值产品

        /// <summary>
        /// 获取充值产品列表
        /// </summary>
        /// <param name="typeId">充值产品类型</param>
        /// <returns></returns>
        public IList<AppPayConfig> GetAppPayConfigList(int typeId)
        {
            const string sqlQuery =
                @"SELECT * FROM AppPayConfig WITH(NOLOCK) WHERE PayType = @PayType ORDER BY PayIdentity DESC,SortID DESC";
            List<DbParameter> parms = new List<DbParameter> {Database.MakeInParam("PayType", typeId)};
            return Database.ExecuteObjectList<AppPayConfig>(sqlQuery, parms);
        }

        /// <summary>
        /// 获取充值产品列表
        /// </summary>
        /// <param name="typeId">充值产品类型</param>
        /// <param name="userid">首充用户</param>
        /// <returns></returns>
        public DataSet GetAppPayConfigList(int typeId, int userid)
        {
            List<DbParameter> parms =
                new List<DbParameter>
                {
                    Database.MakeInParam("dwUserID", userid),
                    Database.MakeInParam("PayType", typeId)
                };

            return Database.ExecuteDataset(CommandType.StoredProcedure, "NET_PW_GetMobilePayConfig", parms.ToArray());
        }

        #endregion

        #region 订单信息

        /// <summary>
        /// 钻石充值下单
        /// </summary>
        /// <param name="order">订单信息</param>
        /// <returns></returns>
        public Message CreatePayOrderInfo(OnLinePayOrder order, string device)
        {
            List<DbParameter> prams = new List<DbParameter>
            {
                Database.MakeInParam("dwUserID", order.UserID),
                Database.MakeInParam("dwShareID", order.ShareID),
                Database.MakeInParam("dwConfigID", order.ConfigID),
                Database.MakeInParam("strOrderID", order.OrderID),
                Database.MakeInParam("strIPAddress", order.OrderAddress),
                Database.MakeInParam("strDevice", device),
                Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
            };
            return MessageHelper.GetMessageForObject<OnLinePayOrder>(Database, "NET_PW_CreateOnLineOrder", prams);
        }

        /// <summary>
        /// 在线充值
        /// </summary>
        /// <param name="order">订单信息</param>
        /// <returns></returns>
        public Message FinishOnLineOrder(OnLinePayOrder order)
        {
            var parms = new List<DbParameter>
            {
                Database.MakeInParam("strOrdersID", order.OrderID),
                Database.MakeInParam("PayAmount", order.Amount),
                Database.MakeInParam("strIPAddress", order.PayAddress),
                Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
            };

            return MessageHelper.GetMessage(Database, "NET_PW_FinishOnLineOrder", parms);
        }

        /// <summary>
        /// 在线充值（苹果）
        /// </summary>
        /// <param name="order">订单信息</param>
        /// <param name="appleid">苹果产品标识</param>
        /// <returns></returns>
        public Message FinishOnLineOrderIOS(OnLinePayOrder order, string appleid)
        {
            var parms = new List<DbParameter>
            {
                Database.MakeInParam("strOrdersID", order.OrderID),
                Database.MakeInParam("PayAmount", order.Amount),
                Database.MakeInParam("dwUserID", order.UserID),
                Database.MakeInParam("strAppleID", appleid),
                Database.MakeInParam("strIPAddress", order.PayAddress),
                Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
            };

            return MessageHelper.GetMessage(Database, "NET_PW_FinishOnLineOrderIOS", parms);
        }

        #endregion

        #region 充值记录

        /// <summary>
        /// 是否今日已充值过
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <returns></returns>
        public bool IsTodayHasPay(int userid)
        {
            string sqlQuery =
                @"SELECT OnLineID FROM OnLinePayOrder WITH(NOLOCK) WHERE UserID=@UserID AND OrderStatus=1 AND OrderDate BETWEEN @StartTime AND @EndTime";
            string nowTime = DateTime.Now.ToString("yyyy-MM-dd");
            List<DbParameter> parms =
                new List<DbParameter>
                {
                    Database.MakeInParam("UserID", userid),
                    Database.MakeInParam("StartTime", nowTime + " 00:00:00"),
                    Database.MakeInParam("EndTime", nowTime + " 23:59:59")
                };

            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery, parms.ToArray());
            return obj != null;
        }

        /// <summary>
        /// 充值记录
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="returnField"></param>
        /// <returns></returns>
        public PagerSet GetPayDiamondRecord(string whereQuery, int pageIndex, int pageSize, string[] returnField = null)
        {
            const string orderQuery = "ORDER By OrderDate DESC";
            returnField = returnField ?? new[] {"Amount", "Diamond", "OtherPresent", "BeforeDiamond", "PayDate"};
            PagerParameters pager = new PagerParameters("OnLinePayOrder", orderQuery, whereQuery, pageIndex, pageSize,
                returnField) {CacherSize = 2};

            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 根据订单号获取充值订单
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public OnLinePayOrder GetPayOnLinePayOrder(string orderid)
        {
            string sqlQuery =
                @"SELECT OrderID,OrderStatus,Amount,(Diamond+OtherPresent) AS Diamond FROM OnLinePayOrder WITH(NOLOCK) WHERE OrderID=@OrderID";
            List<DbParameter> parms =
                new List<DbParameter>
                {
                    Database.MakeInParam("OrderID", orderid)
                };

            return  Database.ExecuteObject<OnLinePayOrder>(sqlQuery, parms);
        }
        #endregion

        #region 推广信息

        /// <summary>
        /// 领取推广人有效好友奖励
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <param name="configid">推广配置标识</param>
        /// <param name="ip">领取地址</param>
        /// <returns></returns>
        public Message ReceiveSpreadAward(int userid, int configid, string ip)
        {
            List<DbParameter> parms =
                new List<DbParameter>
                {
                    Database.MakeInParam("UserID", userid),
                    Database.MakeInParam("ConfigID", configid),
                    Database.MakeInParam("strClientIP", ip),
                    Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
                };

            return MessageHelper.GetMessage(Database, "NET_PJ_ReceiveSpreadAward", parms);
        }

        #endregion

        #region 钻石信息

        /// <summary>
        /// 获取用户财富
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <returns></returns>
        public UserWealth GetUserWealth(int userId)
        {
            string sqlQuery = string.Format(
                @"SELECT ISNULL(G.Score,0) AS Score,ISNULL(G.InsureScore,0) AS InsureScore,ISNULL(U.Diamond,0) AS Diamond
                            FROM (SELECT UserID,Score,InsureScore FROM GameScoreInfo WHERE UserID={0}) AS G FULL JOIN
                            (SELECT UserID,Diamond FROM UserCurrency WHERE UserID={0}) AS U ON G.UserID=U.UserID",
                userId);
            return Database.ExecuteObject<UserWealth>(sqlQuery);
        }

        /// <summary>
        /// 获取钻石信息
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <returns></returns>
        public UserCurrency GetUserCurrency(int userId)
        {
            const string sqlQuery = "SELECT * FROM UserCurrency WHERE UserID=@UserID";
            List<DbParameter> prams = new List<DbParameter> {Database.MakeInParam("UserID", userId)};
            return Database.ExecuteObject<UserCurrency>(sqlQuery, prams);
        }

        /// <summary>
        /// 代理钻石赠送
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="presentCount"></param>
        /// <param name="gameId"></param>
        /// <param name="clientIp"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public Message AgentPresentDiamond(int userId, string password, int presentCount, int gameId, string clientIp,
            string note)
        {
            List<DbParameter> prams = new List<DbParameter>
            {
                Database.MakeInParam("dwUserID", userId),
                Database.MakeInParam("dwPresentCount", presentCount),
                Database.MakeInParam("dwGameID", gameId),
                Database.MakeInParam("strPassword", password),
                Database.MakeInParam("strNote", note),
                Database.MakeInParam("strClientIP", clientIp),
                Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
            };

            return MessageHelper.GetMessage(Database, "NET_PW_AgentPresentDiamond", prams);
        }

        /// <summary>
        /// 钻石兑换金币 数据层
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="configid"></param>
        /// <param name="typeid"></param>
        /// <param name="clientIp"></param>
        /// <returns></returns>
        public Message DiamondExchangeGold(int userid, int configid, int typeid, string clientIp)
        {
            List<DbParameter> prams = new List<DbParameter>
            {
                Database.MakeInParam("dwUserID", userid),
                Database.MakeInParam("dwConfigID", configid),
                Database.MakeInParam("dwTypeID", typeid),
                Database.MakeInParam("strClientIP", clientIp),
                Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
            };

            return MessageHelper.GetMessageForDataSet(Database, "NET_PW_DiamondExchangeGold", prams);
        }

        #endregion

        #region 游戏回放

        /// <summary>
        /// 获取录像存盘数据
        /// </summary>
        /// <param name="number">录像编号</param>
        /// <returns></returns>
        public byte[] GetVideoDataByVNumber(string number)
        {
            const string sqlQuery =
                "SELECT VideoData FROM RecordVideoInfo WITH(NOLOCK) WHERE VideoNumber = @VideoNumber";
            List<DbParameter> param = new List<DbParameter> {Database.MakeInParam("VideoNumber", number)};

            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery, param.ToArray());
            return obj as byte[];
        }

        #endregion

        #region 注册奖励

        /// <summary>
        /// 领取注册奖励
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <param name="ip">领取地址</param>
        /// <returns></returns>
        public Message ReceiveRegisterAward(int userId, string ip)
        {
            var prams = new List<DbParameter>
            {
                Database.MakeInParam("dwUserID", userId),
                Database.MakeInParam("strClientIP", ip),
                Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
            };

            return MessageHelper.GetMessageForObject<UserWealth>(Database, "NET_PJ_RecevieRegisterGrant", prams);
        }

        #endregion
    }
}
