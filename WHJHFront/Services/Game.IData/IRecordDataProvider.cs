using System.Collections.Generic;
using Game.Kernel;
using System.Data;

namespace Game.IData
{
    /// <summary>
    /// 记录库数据层接口
    /// </summary>
    public interface IRecordDataProvider //: IProvider
    {
        #region 代理信息

        /// <summary>
        /// 获取今日赠送钻石
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        int GetTodayAgentPresentDiamond(int userId);

        /// <summary>
        /// 代理赠送钻石记录
        /// </summary>
        /// <param name="whereQuery">查询条件</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        PagerSet GetAgentPresentDiamondRecord(string whereQuery, int pageIndex, int pageSize);

        /// <summary>
        /// 代理兑换游戏币记录
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagerSet GetAgentExchangeDiamondRecord(string whereQuery, int pageIndex, int pageSize);

        /// <summary>
        /// 获取我的玩家分页列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagerSet GetAgentBelowUserPresentDiamondRecord(string whereQuery, int pageIndex, int pageSize);

        /// <summary>
        /// 获取我的代理分页列表
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagerSet GetAgentBelowAgentPresentDiamondRecord(string whereQuery, int pageIndex, int pageSize);

        #endregion

        #region 推广系统

        /// <summary>
        /// 玩家领取推广返利
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="num"></param>
        /// <param name="clientIp"></param>
        /// <returns></returns>
        Message UserSpreadReceive(int userid, int num, string clientIp);

        #endregion

        #region 钻石记录

        /// <summary>
        /// 钻石流水记录
        /// </summary>
        /// <param name="whereQuery">查询条件</param>
        /// <param name="pageIndex">页下标</param>
        /// <param name="pageSize">页显示数</param>
        /// <returns></returns>
        PagerSet GetDiamondStreamList(string whereQuery, int pageIndex, int pageSize);

        /// <summary>
        /// 获取某两人之间的转账总数（赠送标识或接收标识不可均为0）
        /// </summary>
        /// <param name="sourceUserId">赠送标识（为空则为接收人所有接收）</param>
        /// <param name="targetUserId">接收标识（为空则为赠送人所有赠送）</param>
        /// <param name="sqlTime">时间参数（可传其他条件）</param>
        /// <returns></returns>
        long GetTotalPresentCount(int sourceUserId, int targetUserId, string sqlTime);

        #endregion

        #region 金币信息

        /// <summary>
        /// 金币流水记录
        /// </summary>
        /// <param name="whereQuery">查询条件</param>
        /// <param name="pageIndex">页下标</param>
        /// <param name="pageSize">页显示数</param>
        /// <returns></returns>
        PagerSet GetGoldStreamList(string whereQuery, int pageIndex, int pageSize);

        #endregion

        #region 公共方法

        T GetEntity<T>(string tableName, string sqlWhere);

        IList<T> GetList<T>(string tableName, string sqlWhere);

        DataRow GetOne(string tableName, string sqlWhere);

        DataRow GetOne(string sql);

        DataSet GetDataSet(string sql);

        int GetRecordCount(string tableName, string sqlWhere);

        int GetRecordCount(string sql);

        #endregion
    }
}
