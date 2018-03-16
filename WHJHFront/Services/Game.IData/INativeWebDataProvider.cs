using System.Collections.Generic;

using Game.Kernel;
using System.Data;
using Game.Entity.Accounts;
using Game.Entity.NativeWeb;

namespace Game.IData
{
    /// <summary>
    /// 网站库数据层接口
    /// </summary>
    public interface INativeWebDataProvider //: IProvider
    {
        #region 数据分页
        /// <summary>
        /// 游戏规则数据分页
        /// </summary>
        /// <param name="pageIndex">分页下标</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        PagerSet GetGamePageList(int pageIndex, int pageSize);
        /// <summary>
        /// 新闻公告数据分页
        /// </summary>
        /// <param name="pageIndex">分页下标</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        PagerSet GetNewPageList(int pageIndex, int pageSize);
        #endregion

        #region 获取配置信息
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="configKey">配置键值</param>
        /// <returns></returns>
        ConfigInfo GetConfigInfo(string configKey);
        /// <summary>
        /// 获取配置信息集合
        /// </summary>
        /// <returns></returns>
        IList<ConfigInfo> GetConfigInfoList();
        #endregion

        #region 手机登录信息
        /// <summary>
        /// 获取手机登录数据
        /// </summary>
        /// <returns></returns>
        DataSet GetMobileLoginInfo();
        #endregion

        #region 获取排行榜信息
        /// <summary>
        /// 获取排行榜数据
        /// </summary>
        /// <param name="typeid">排行榜类型</param>
        /// <returns></returns>
        DataSet GetDayRankingData(int typeid);
        /// <summary>
        /// 获取财富排行榜
        /// </summary>
        /// <returns></returns>
        IList<CacheWealthRank> GetCacheWealthRank(int dateId);
        /// <summary>
        /// 获取战绩排行榜
        /// </summary>
        /// <returns></returns>
        IList<CacheScoreRank> GetCacheScoreRank(int dateId);
        /// <summary>
        /// 获取消耗排行榜
        /// </summary>
        /// <returns></returns>
        IList<CacheConsumeRank> GetCacheConsumeRank(int dateId);
        /// <summary>
        /// 领取排行榜奖励
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <param name="dateId">时间标识</param>
        /// <param name="typeId">排行榜类型</param>
        /// <param name="ip">领取地址</param>
        /// <returns></returns>
        Message ReceiveRankingAward(int userId, int dateId, int typeId, string ip);
        #endregion

        #region 新闻信息
        /// <summary>
        /// 获取首页新闻公告
        /// </summary>
        /// <returns></returns>
        IList<SystemNotice> GetHomePageNews();
        /// <summary>
        /// 获取新闻详情
        /// </summary>
        /// <param name="id">新闻标识</param>
        /// <returns></returns>
        SystemNotice GetWebNewsInfo(int id);
        #endregion

        #region 游戏规则
        /// <summary>
        /// 获取游戏规则列表
        /// </summary>
        /// <returns></returns>
        IList<GameRule> GetGameRuleList();
        /// <summary>
        /// 获取游戏规则
        /// </summary>
        /// <param name="kindid">游戏规则</param>
        /// <returns></returns>
        GameRule GetGameRuleInfo(int kindid);
        #endregion

        #region 广告位图片
        /// <summary>
        /// 获取广告图列表
        /// </summary>
        /// <returns></returns>
        IList<Ads> GetAdsList();
        #endregion

        #region 常见问题

        /// <summary>
        /// 获取常见问题列表
        /// </summary>
        /// <returns></returns>
        IList<Question> GetQAList(int top = 0);

        #endregion

        #region 代理Token管理

        /// <summary>
        /// 检查代理登录凭证
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        AgentTokenInfo VerifyAgentToken(string token);

        /// <summary>
        /// 保存代理登录凭证
        /// </summary>
        /// <param name="info"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        int SaveAgentToken(UserInfo info, string token);

        #endregion
    }
}
