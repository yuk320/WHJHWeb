using Game.Data.Factory;
using Game.Entity.NativeWeb;
using Game.IData;
using Game.Kernel;

// ReSharper disable once CheckNamespace
namespace Game.Facade
{
    /// <summary>
    /// 后台外观
    /// </summary>
    public class NativeWebFacade //: BaseFacadeProvider
    {
        #region Fields

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private INativeWebDataProvider _aideNativeWebData;

        #endregion Fields

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public NativeWebFacade()
        {
            _aideNativeWebData = ClassFactory.GetINativeWebDataProvider();
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
            return _aideNativeWebData.GetList(tableName, pageIndex, pageSize, condition, orderby);
        }
        #endregion

        #region 站点配置
        /// <summary>
        /// 获取站点配置
        /// </summary>
        /// <param name="id">配置标识</param>
        /// <returns></returns>
        public ConfigInfo GetConfigInfo(int id)
        {
            return _aideNativeWebData.GetConfigInfo(id);
        }
        /// <summary>
        /// 获取站点配置
        /// </summary>
        /// <param name="configKey">配置Key</param>
        /// <returns></returns>
        public ConfigInfo GetConfigInfo(string configKey)
        {
            return _aideNativeWebData.GetConfigInfo(configKey);
        }
        /// <summary>
        /// 修改站点配置
        /// </summary>
        /// <param name="config">配置信息</param>
        public int UpdateConfigInfo(ConfigInfo config)
        {
            return _aideNativeWebData.UpdateConfigInfo(config);
        }
        #endregion 站点配置

        #region 广告管理
        /// <summary>
        /// 获取广告实体
        /// </summary>
        /// <param name="id">广告标识</param>
        /// <returns>广告实体</returns>
        public Ads GetAds(int id)
        {
            return _aideNativeWebData.GetAds(id);
        }
        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="idlist">标识列表</param>
        public int DeleteAds(string idlist)
        {
            return _aideNativeWebData.DeleteAds(idlist);
        }
        /// <summary>
        /// 新增广告
        /// </summary>
        /// <param name="ads">广告信息</param>
        public int InsertAds(Ads ads)
        {
            return _aideNativeWebData.InsertAds(ads);
        }
        /// <summary>
        /// 修改广告
        /// </summary>
        /// <param name="ads">广告实体</param>
        public int UpdateAds(Ads ads)
        {
            return _aideNativeWebData.UpdateAds(ads);
        }
        #endregion 广告管理

        #region 新闻公告
        /// <summary>
        /// 获取新闻公告信息
        /// </summary>
        /// <param name="id">公告标识</param>
        /// <returns></returns>
        public SystemNotice GetSystemNoticeInfo(int id)
        {
            return _aideNativeWebData.GetSystemNoticeInfo(id);
        }
        /// <summary>
        /// 新增新闻公告
        /// </summary>
        /// <param name="notice">公告信息</param>
        /// <returns></returns>
        public int InsertSystemNotice(SystemNotice notice)
        {
            return _aideNativeWebData.InsertSystemNotice(notice);
        }
        /// <summary>
        /// 修改公告信息
        /// </summary>
        /// <param name="notice">公告信息</param>
        /// <returns></returns>
        public int UpdateSystemNotice(SystemNotice notice)
        {
            return _aideNativeWebData.UpdateSystemNotice(notice);
        }
        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="idlist">标识列表</param>
        /// <returns></returns>
        public int DeleteSystemNotice(string idlist)
        {
            return _aideNativeWebData.DeleteSystemNotice(idlist);
        }
        #endregion

        #region 排行榜
        /// <summary>
        /// 根据标识获取排行榜配置
        /// </summary>
        /// <param name="configid">配置标识</param>
        /// <returns></returns>
        public RankingConfig GetRankingConfigById(int configid)
        {
            return _aideNativeWebData.GetRankingConfigById(configid);
        }
        /// <summary>
        /// 判断排行榜配置是否存在
        /// </summary>
        /// <param name="typeid">排行榜类型</param>
        /// <param name="rankid">排行榜排名</param>
        /// <returns></returns>
        public bool ExistRankingConfig(int typeid, int rankid)
        {
            return _aideNativeWebData.ExistRankingConfig(typeid, rankid);
        }
        /// <summary>
        /// 新增排行榜配置
        /// </summary>
        /// <param name="config">配置信息</param>
        /// <returns></returns>
        public int InsertRankingConfig(RankingConfig config)
        {
            return _aideNativeWebData.InsertRankingConfig(config);
        }
        /// <summary>
        /// 修改排行榜配置
        /// </summary>
        /// <param name="config">配置信息</param>
        /// <returns></returns>
        public int UpdateRankingConfig(RankingConfig config)
        {
            return _aideNativeWebData.UpdateRankingConfig(config);
        }
        /// <summary>
        /// 删除奖励配置
        /// </summary>
        /// <param name="configid">配置标识</param>
        /// <returns></returns>
        public int DeleteRankingConfig(int configid)
        {
            return _aideNativeWebData.DeleteRankingConfig(configid);
        }
        #endregion

        #region 游戏规则
        /// <summary>
        /// 获取游戏规则
        /// </summary>
        /// <param name="kindid">游戏标识</param>
        /// <returns></returns>
        public GameRule GetGameRuleInfo(int kindid)
        {
            return _aideNativeWebData.GetGameRuleInfo(kindid);
        }

        /// <summary>
        /// 新增游戏规则
        /// </summary>
        /// <param name="rule">游戏规则</param>
        /// <returns></returns>
        public int InsertGameRule(GameRule rule)
        {
            return _aideNativeWebData.InsertGameRule(rule);
        }
        /// <summary>
        /// 修改游戏规则
        /// </summary>
        /// <param name="rule">游戏规则</param>
        /// <returns></returns>
        public int UpdateGameRule(GameRule rule)
        {
            return _aideNativeWebData.UpdateGameRule(rule);
        }
        /// <summary>
        /// 删除游戏规则
        /// </summary>
        /// <param name="idlist">标识列表</param>
        /// <returns></returns>
        public int DeleteGameRule(string idlist)
        {
            return _aideNativeWebData.DeleteGameRule(idlist);
        }
        /// <summary>
        /// 禁用启用游戏规则
        /// </summary>
        /// <param name="idlist">标识列表</param>
        /// <param name="nullity">禁用启用标识</param>
        /// <returns></returns>
        public int NullityGameRule(string idlist, int nullity)
        {
            return _aideNativeWebData.NullityGameRule(idlist, nullity);
        }
        #endregion

        #region 常见问题

        /// <summary>
        /// 获取常见问题实体 by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="Question"/></returns>
        public Question GetQuestionInfo(int id)
        {
            return _aideNativeWebData.GetQuestionInfo(id);
        }

        /// <summary>
        /// 保存常见问题（新增、修改）通用
        /// </summary>
        /// <param name="quest"></param>
        /// <returns><see cref="int"/></returns>
        public int SaveQuestionInfo(Question quest)
        {
            return _aideNativeWebData.SaveQuestionInfo(quest);
        }

        /// <summary>
        /// 批量删除常见问题
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteQuestionInfo(string ids)
        {
            return _aideNativeWebData.DeleteQuestionInfo(ids);
        }

        #endregion
    }
}