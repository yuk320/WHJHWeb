using System.Collections.Generic;

using Game.Kernel;
using System.Data;
using Game.Entity.Treasure;
// ReSharper disable InconsistentNaming

namespace Game.IData
{
    /// <summary>
    /// 金币库数据层接口
    /// </summary>
    public interface ITreasureDataProvider //: IProvider
    {
        #region 充值产品
        /// <summary>
        /// 获取充值产品列表
        /// </summary>
        /// <param name="typeId">充值产品类型</param>
        /// <returns></returns>
        IList<AppPayConfig> GetAppPayConfigList(int typeId);
        /// <summary>
        /// 获取充值产品列表
        /// </summary>
        /// <param name="typeId">充值产品类型</param>
        /// <param name="userid">首充用户</param>
        /// <returns></returns>
        DataSet GetAppPayConfigList(int typeId, int userid);
        #endregion

        #region 订单信息
        /// <summary>
        /// 钻石充值下单
        /// </summary>
        /// <param name="order">订单信息</param>
        /// <returns></returns>
        Message CreatePayOrderInfo(OnLinePayOrder order, string device);
        /// <summary>
        /// 在线充值
        /// </summary>
        /// <param name="order">订单信息</param>
        /// <returns></returns>
        Message FinishOnLineOrder(OnLinePayOrder order);
        /// <summary>
        /// 在线充值（苹果）
        /// </summary>
        /// <param name="order">订单信息</param>
        /// <param name="appleid">苹果产品标识</param>
        /// <returns></returns>
        Message FinishOnLineOrderIOS(OnLinePayOrder order, string appleid);
        #endregion

        #region 充值记录
        /// <summary>
        /// 是否今日已充值过
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <returns></returns>
        bool IsTodayHasPay(int userid);

        /// <summary>
        /// 充值记录
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="returnField"></param>
        /// <returns></returns>
        PagerSet GetPayDiamondRecord(string whereQuery, int pageIndex, int pageSize, string[] returnField = null);

        /// <summary>
        /// 根据订单号获取充值订单
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        OnLinePayOrder GetPayOnLinePayOrder(string orderid);
        #endregion

        #region 推广信息
        /// <summary>
        /// 领取推广人有效好友奖励
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <param name="configid">推广配置标识</param>
        /// <param name="ip">领取地址</param>
        /// <returns></returns>
        Message ReceiveSpreadAward(int userid, int configid, string ip);
        #endregion

        #region 钻石信息
        /// <summary>
        /// 获取用户财富
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <returns></returns>
        UserWealth GetUserWealth(int userId);
        /// <summary>
        /// 获取钻石信息
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <returns></returns>
        UserCurrency GetUserCurrency(int userId);

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
        Message AgentPresentDiamond(int userId, string password, int presentCount, int gameId, string clientIp, string note);

        /// <summary>
        /// 钻石兑换金币 接口层
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="configid"></param>
        /// <param name="typeid"></param>
        /// <param name="clientIp"></param>
        /// <returns></returns>
        Message DiamondExchangeGold(int userid, int configid, int typeid, string clientIp);
        #endregion

        #region 游戏回放
        /// <summary>
        /// 获取录像存盘数据
        /// </summary>
        /// <param name="number">录像编号</param>
        /// <returns></returns>
        byte[] GetVideoDataByVNumber(string number);
        #endregion

        #region 注册奖励
        /// <summary>
        /// 领取注册奖励
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <param name="ip">领取地址</param>
        /// <returns></returns>
        Message ReceiveRegisterAward(int userId, string ip);
        #endregion
    }
}
