using System.Data;
using Game.Data.Factory;
using Game.Entity.Record;
using Game.IData;
using Game.Kernel;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Game.Facade
{
    /// <summary>
    /// 记录库外观
    /// </summary>
    public class RecordFacade
    {
        #region Fields

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        // ReSharper disable once InconsistentNaming
        private IRecordDataProvider aideRecordData;

        #endregion Fields

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RecordFacade()
        {
            aideRecordData = ClassFactory.GetIRecordDataProvider();
        }

        #endregion 构造函数

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
            return aideRecordData.GetList(tableName, pageIndex, pageSize, condition, orderby);
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
            return aideRecordData.GetQueryAgentDiamond(userid);
        }
        /// <summary>
        /// 获取转出钻石前50名排行
        /// </summary>
        /// <returns></returns>
        public DataSet GetTradeOutDiamondRank()
        {
            return aideRecordData.GetTradeOutDiamondRank();
        }
        /// <summary>
        /// 获取购买道具花费钻石前50名排行
        /// </summary>
        /// <returns></returns>
        public DataSet GetBuyPropDiamondRank()
        {
            return aideRecordData.GetBuyPropDiamondRank();
        }
        /// <summary>
        /// 获取统计数据
        /// </summary>
        /// <param name="sDateId">起始时间</param>
        /// <param name="eDateId">结束时间</param>
        /// <returns></returns>
        public IList<RecordEveryDayCurrency> GetRecordEveryDayCurrency(string sDateId, string eDateId)
        {
            return aideRecordData.GetRecordEveryDayCurrency(sDateId, eDateId);
        }

        /// <summary>
        /// 获取钻石兑换金币前50名排行
        /// </summary>
        /// <returns></returns>
        public DataSet GetDiamondExchangeGoldRank()
        {
            return aideRecordData.GetDiamondExchangeGoldRank();
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
            return aideRecordData.AddRecordAccountsUmeng(table, connStr);
        }
        /// <summary>
        /// 新增单条推送记录
        /// </summary>
        /// <param name="umeng">推送信息</param>
        /// <returns></returns>
        public int AddRecordAccountsUmeng(RecordAccountsUmeng umeng)
        {
            return aideRecordData.AddRecordAccountsUmeng(umeng);
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
            return aideRecordData.GetTotalDiamondChange(where);
        }
        /// <summary>
        /// 获取后台赠送钻石统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public long GetTotalBackPresentDiamond(string where)
        {
            return aideRecordData.GetTotalBackPresentDiamond(where);
        }
        /// <summary>
        /// 获取代理赠送钻石统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public long GetTotalAgentPresentDiamond(string where)
        {
            return aideRecordData.GetTotalAgentPresentDiamond(where);
        }
        /// <summary>
        /// 获取购买喇叭花费钻石统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public long GetTotalBuyHornDiamond(string where)
        {
            return aideRecordData.GetTotalBuyHornDiamond(where);
        }
        /// <summary>
        /// 获取AA制钻石消耗
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public long GetTotalAaGameDiamond(string where)
        {
            return aideRecordData.GetTotalAaGameDiamond(where);
        }
        /// <summary>
        /// 获取钻石兑换金币统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public TotalDiamondExch GetTotalDiamondExchGold(string where)
        {
            return aideRecordData.GetTotalDiamondExchGold(where);
        }
        /// <summary>
        /// 获取购买喇叭花费金币统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public long GetTotalBuyHornGold(string where)
        {
            return aideRecordData.GetTotalBuyHornGold(where);
        }
        /// <summary>
        /// 获取金币变化统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public long GetTotalTreasureChange(string where)
        {
            return aideRecordData.GetTotalTreasureChange(where);
        }

        /// <summary>
        /// 获取用户钻石兑换金币总数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public long[] GetTotalDiamondExch(string where)
        {
            return aideRecordData.GetTotalDiamondExch(where);
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
            return aideRecordData.GrantGameId(userId, gameId, masterId, strReason, strIp);
        }
        #endregion 赠送靓号

    }
}