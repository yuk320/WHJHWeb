using System.Collections.Generic;
using System.Data;
using Game.Entity.Record;
using Game.Kernel;

namespace Game.IData
{
    /// <summary>
    /// 记录库数据层接口
    /// </summary>
    public interface IRecordDataProvider //: IProvider
    {
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
        PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby);
        #endregion

        #region 查询钻石
        /// <summary>
        /// 获取代理钻石信息
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <returns></returns>
        DataSet GetQueryAgentDiamond(int userid);
        /// <summary>
        /// 获取转出钻石前50名排行
        /// </summary>
        /// <returns></returns>
        DataSet GetTradeOutDiamondRank();
        /// <summary>
        /// 获取购买道具花费钻石前50名排行
        /// </summary>
        /// <returns></returns>
        DataSet GetBuyPropDiamondRank();
        /// <summary>
        /// 获取统计数据
        /// </summary>
        /// <param name="sDateID">起始时间</param>
        /// <param name="eDateID">结束时间</param>
        /// <returns></returns>
        IList<RecordEveryDayCurrency> GetRecordEveryDayCurrency(string sDateID, string eDateID);

        /// <summary>
        /// 获取钻石兑换金币前50名排行
        /// </summary>
        /// <returns></returns>
        DataSet GetDiamondExchangeGoldRank();
        #endregion

        #region 推送记录
        /// <summary>
        /// 批量新增推送记录
        /// </summary>
        /// <param name="table">新增数据集</param>
        /// <param name="connStr">数据库连接字符串</param>
        /// <returns></returns>
        int AddRecordAccountsUmeng(DataTable table, string connStr);
        /// <summary>
        /// 新增单条推送记录
        /// </summary>
        /// <param name="umeng">推送信息</param>
        /// <returns></returns>
        int AddRecordAccountsUmeng(RecordAccountsUmeng umeng);
        #endregion

        #region 统计总数
        /// <summary>
        /// 获取钻石变化统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        long GetTotalDiamondChange(string where);
        /// <summary>
        /// 获取后台赠送钻石统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        long GetTotalBackPresentDiamond(string where);
        /// <summary>
        /// 获取代理赠送钻石统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        long GetTotalAgentPresentDiamond(string where);
        /// <summary>
        /// 获取购买喇叭花费钻石统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        long GetTotalBuyHornDiamond(string where);
        /// <summary>
        /// 获取AA制钻石消耗
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        long GetTotalAaGameDiamond(string where);
        /// <summary>
        /// 获取钻石兑换金币统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        TotalDiamondExch GetTotalDiamondExchGold(string where);
        /// <summary>
        /// 获取购买喇叭花费金币统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        long GetTotalBuyHornGold(string where);
        /// <summary>
        /// 获取金币变化统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        long GetTotalTreasureChange(string where);

        /// <summary>
        /// 获取用户钻石兑换金币总数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        long[] GetTotalDiamondExch(string where);
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
        Message GrantGameId(int userId, int gameId, int masterId, string strReason, string strIp);
        #endregion 赠送靓号
    }
}