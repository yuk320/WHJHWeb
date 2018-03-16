using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Data.Factory;
using Game.IData;
using Game.Kernel;
using Game.Utils;
using System.Data;
using Game.Entity.Accounts;
using Game.Entity.NativeWeb;

namespace Game.Facade
{
    /// <summary>
    /// 网站外观
    /// </summary>
    public class NativeWebFacade
    {
        #region Fields

        private INativeWebDataProvider webData;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public NativeWebFacade()
        {
            webData = ClassFactory.GetINativeWebDataProvider();
        }
        #endregion

        #region 数据分页
        /// <summary>
        /// 游戏规则数据分页
        /// </summary>
        /// <param name="pageIndex">分页下标</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public PagerSet GetGamePageList(int pageIndex, int pageSize)
        {
            return webData.GetGamePageList(pageIndex, pageSize);
        }
        /// <summary>
        /// 新闻公告数据分页
        /// </summary>
        /// <param name="pageIndex">分页下标</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public PagerSet GetNewPageList(int pageIndex, int pageSize)
        {
            return webData.GetNewPageList(pageIndex, pageSize);
        }
        #endregion

        #region 获取配置信息
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="configKey">配置键值</param>
        /// <returns></returns>
        public ConfigInfo GetConfigInfo(string configKey)
        {
            return webData.GetConfigInfo(configKey);
        }
        /// <summary>
        /// 获取配置信息集合
        /// </summary>
        /// <returns></returns>
        public IList<ConfigInfo> GetConfigInfoList()
        {
            return webData.GetConfigInfoList();
        }
        #endregion

        #region 手机登录信息
        /// <summary>
        /// 获取手机登录数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetMobileLoginInfo()
        {
            return webData.GetMobileLoginInfo();
        }
        #endregion

        #region 获取排行榜信息
        /// <summary>
        /// 获取排行榜数据
        /// </summary>
        /// <param name="typeid">排行榜类型</param>
        /// <returns></returns>
        public DataSet GetDayRankingData(int typeid)
        {
            return webData.GetDayRankingData(typeid);
        }
        /// <summary>
        /// 获取财富排行榜
        /// </summary>
        /// <returns></returns>
        public IList<CacheWealthRank> GetCacheWealthRank(int DateID)
        {
            return webData.GetCacheWealthRank(DateID);
        }
        /// <summary>
        /// 获取战绩排行榜
        /// </summary>
        /// <returns></returns>
        public IList<CacheScoreRank> GetCacheScoreRank(int DateID)
        {
            return webData.GetCacheScoreRank(DateID);
        }
        /// <summary>
        /// 获取消耗排行榜
        /// </summary>
        /// <returns></returns>
        public IList<CacheConsumeRank> GetCacheConsumeRank(int DateID)
        {
            return webData.GetCacheConsumeRank(DateID);
        }
        /// <summary>
        /// 领取排行榜奖励
        /// </summary>
        /// <param name="UserID">用户标识</param>
        /// <param name="DateID">时间标识</param>
        /// <param name="TypeID">排行榜类型</param>
        /// <param name="IP">领取地址</param>
        /// <returns></returns>
        public Message ReceiveRankingAward(int UserID, int DateID, int TypeID, string IP)
        {
            return webData.ReceiveRankingAward(UserID, DateID, TypeID, IP);
        }
        #endregion

        #region 新闻信息
        /// <summary>
        /// 获取首页新闻公告
        /// </summary>
        /// <returns></returns>
        public IList<SystemNotice> GetHomePageNews()
        {
            return webData.GetHomePageNews();
        }
        /// <summary>
        /// 获取新闻详情
        /// </summary>
        /// <param name="id">新闻标识</param>
        /// <returns></returns>
        public SystemNotice GetWebNewsInfo(int id)
        {
            return webData.GetWebNewsInfo(id);
        }
        #endregion

        #region 游戏规则
        /// <summary>
        /// 获取游戏规则列表
        /// </summary>
        /// <returns></returns>
        public IList<GameRule> GetGameRuleList()
        {
            return webData.GetGameRuleList();
        }
        /// <summary>
        /// 获取游戏规则
        /// </summary>
        /// <param name="kindid">游戏规则</param>
        /// <returns></returns>
        public GameRule GetGameRuleInfo(int kindid)
        {
            return webData.GetGameRuleInfo(kindid);
        }
        #endregion

        #region 广告位图片
        /// <summary>
        /// 获取广告图列表
        /// </summary>
        /// <returns></returns>
        public IList<Ads> GetAdsList()
        {
            return webData.GetAdsList();
        }
        #endregion

        #region 常见问题

        /// <summary>
        /// 获取常见问题列表
        /// </summary>
        /// <returns></returns>
        public IList<Question> GetQAList(int top = 0)
        {
            return webData.GetQAList(top);
        }

        #endregion

        #region 代理Token管理

        /// <summary>
        /// 检查代理登录凭证
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public AgentTokenInfo VerifyAgentToken(string token)
        {
            return webData.VerifyAgentToken(token);
        }

        /// <summary>
        /// 保存代理登录凭证
        /// </summary>
        /// <param name="info"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public int SaveAgentToken(UserInfo info, string token)
        {
            return webData.SaveAgentToken(info, token);
        }
        #endregion
    }
}
