using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Data.Factory;
using Game.IData;
using Game.Kernel;
using Game.Utils;
using System.Data;
using Game.Entity.Record;

namespace Game.Facade
{
    /// <summary>
    /// 记录库外观
    /// </summary>
    public class RecordFacade
    {
        #region Fields

        private IRecordDataProvider recordData;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RecordFacade()
        {
            recordData = ClassFactory.GetIRecordDataProvider();
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
            return recordData.GetTodayAgentPresentDiamond(userId);
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
            return recordData.GetAgentPresentDiamondRecord(whereQuery, pageIndex, pageSize);
        }

        /// <summary>
        /// 代理赠送钻石记录
        /// </summary>
        /// <param name="whereQuery">查询条件</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public PagerSet GetAgentExchangeDiamondRecord(string whereQuery, int pageIndex, int pageSize)
        {
            return recordData.GetAgentExchangeDiamondRecord(whereQuery, pageIndex, pageSize);
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
            return recordData.GetAgentBelowUserPresentDiamondRecord(whereQuery, pageIndex, pageSize);
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
            return recordData.GetAgentBelowAgentPresentDiamondRecord(whereQuery, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取代理我的玩家
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <returns></returns>
        public long GetAgentBelowAccountsCount(int userid)
        {
            DataRow dr =recordData.GetOne($"SELECT COUNT(1) AS [Count] FROM (SELECT TargetUserID FROM RecordPresentCurrency(NOLOCK) WHERE SourceUserID={userid} AND TargetUserID NOT IN (SELECT UserID FROM WHJHAccountsDBLink.WHJHAccountsDB.dbo.AccountsAgentInfo) GROUP BY TargetUserID) AS a ");
            return dr != null ? Convert.ToInt64(dr["Count"]) : 0;
        }

        public long GetAgentPresentInCount(int userid, string sqlTime = "")
        {
            return GetAgentPresentCount(userid, "TargetUserID", sqlTime);
        }

        public long GetAgentPresentOutCount(int userid, string sqlTime = "")
        {
            return GetAgentPresentCount(userid, "SourceUserID", sqlTime);
        }

        private long GetAgentPresentCount(int userid, string key, string sqlTime = "")
        {
            string sql =
                $" SELECT SUM(PresentDiamond) AS Diamond FROM RecordPresentCurrency(NOLOCK) WHERE {key}={userid} {sqlTime} GROUP BY SourceUserID ";
            return recordData.GetOne(sql) != null ? Convert.ToInt64(recordData.GetOne(sql)["Diamond"]) : 0;
        }

        #endregion

        #region 推广系统

        /// <summary>
        /// 玩家领取推广返利
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public Message UserSpreadReceive(int userid, int num)
        {
            return recordData.UserSpreadReceive(userid, num, GameRequest.GetUserIP());
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
            return recordData.GetDiamondStreamList(whereQuery, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取某两人之间的转账总数（赠送标识或接收标识不可均为0）
        /// </summary>
        /// <param name="sourceUserId">赠送标识（为空则为接收人所有接收）</param>
        /// <param name="targetUserId">接收标识（为空则为赠送人所有赠送）</param>
        /// <param name="sqlTime">时间参数（可传其他条件）</param>
        /// <returns></returns>
        public long GetTotalPresentCount(int sourceUserId, int targetUserId = 0, string sqlTime = "")
        {
            return recordData.GetTotalPresentCount(sourceUserId, targetUserId, sqlTime);
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
            return recordData.GetGoldStreamList(whereQuery, pageIndex, pageSize);
        }

        #endregion
    }
}