using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Data.Factory;
using Game.IData;
using Game.Kernel;
using Game.Utils;
using System.Data;
using Game.Data;
using System.Data.Common;
using System.Web;
using Game.Entity.Treasure;

namespace Game.Facade
{
    /// <summary>
    /// 金币库外观
    /// </summary>
    public class TreasureFacade
    {
        #region Fields

        private ITreasureDataProvider treasureData;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public TreasureFacade()
        {
            treasureData = ClassFactory.GetITreasureDataProvider();
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
            return treasureData.GetAppPayConfigList(typeId);
        }
        /// <summary>
        /// 获取充值产品列表
        /// </summary>
        /// <param name="typeId">充值产品类型</param>
        /// <param name="userid">首充用户</param>
        /// <returns></returns>
        public DataSet GetAppPayConfigList(int typeId, int userid)
        {
            return treasureData.GetAppPayConfigList(typeId, userid);
        }
        #endregion

        #region 订单信息
        /// <summary>
        /// 钻石充值下单
        /// </summary>
        /// <param name="order">订单信息</param>
        /// <returns></returns>
        public Message CreatePayOrderInfo(OnLinePayOrder order, string device="")
        {
            return treasureData.CreatePayOrderInfo(order, device);
        }
        /// <summary>
        /// 在线充值
        /// </summary>
        /// <param name="order">订单信息</param>
        /// <returns></returns>
        public Message FinishOnLineOrder(OnLinePayOrder order)
        {
            return treasureData.FinishOnLineOrder(order);
        }
        /// <summary>
        /// 在线充值（苹果）
        /// </summary>
        /// <param name="order">订单信息</param>
        /// <param name="appleid">苹果产品标识</param>
        /// <returns></returns>
        public Message FinishOnLineOrderIOS(OnLinePayOrder order, string appleid)
        {
            return treasureData.FinishOnLineOrderIOS(order, appleid);
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
            return treasureData.IsTodayHasPay(userid);
        }
        /// <summary>
        /// 充值记录
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetPayDiamondRecord(string whereQuery, int pageIndex, int pageSize, string[] returnField = null)
        {
            return treasureData.GetPayDiamondRecord(whereQuery, pageIndex, pageSize, returnField);
        }

        /// <summary>
        /// 根据订单号获取充值订单
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public OnLinePayOrder GetPayOnLinePayOrder(string orderid)
        {
            return treasureData.GetPayOnLinePayOrder(orderid);
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
            return treasureData.ReceiveSpreadAward(userid, configid, ip);
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
            return treasureData.GetUserWealth(userId);
        }
        /// <summary>
        /// 获取钻石信息
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <returns></returns>
        public UserCurrency GetUserCurrency(int userId)
        {
            return treasureData.GetUserCurrency(userId);
        }
        /// <summary>
        /// 代理钻石赠送
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="presentCount"></param>
        /// <param name="gameId"></param>
        /// <param name="clientIP"></param>
        /// <returns></returns>
        public Message AgentPresentDiamond(int userId, string password, int presentCount, int gameId, string clientIP, string note="")
        {
            return treasureData.AgentPresentDiamond(userId, password, presentCount, gameId, clientIP, note);
        }

        /// <summary>
        /// 钻石兑换金币 外壳层
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="configid"></param>
        /// <param name="typeid"></param>
        /// <param name="clientIp"></param>
        /// <returns></returns>
        public Message DiamondExchangeGold(int userid, int configid, int typeid, string clientIp)
        {
            return treasureData.DiamondExchangeGold(userid, configid, typeid, clientIp);
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
            return treasureData.GetVideoDataByVNumber(number);
        }
        #endregion

        #region 注册奖励
        /// <summary>
        /// 领取注册奖励
        /// </summary>
        /// <param name="UserID">用户标识</param>
        /// <param name="IP">领取地址</param>
        /// <returns></returns>
        public Message ReceiveRegisterAward(int UserID, string IP)
        {
            return treasureData.ReceiveRegisterAward(UserID, IP);
        }
        #endregion
    }
}
