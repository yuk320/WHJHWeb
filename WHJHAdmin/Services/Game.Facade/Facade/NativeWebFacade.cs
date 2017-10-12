using System.Data;
using Game.Data.Factory;
using Game.Entity.NativeWeb;
using Game.IData;
using Game.Kernel;

namespace Game.Facade
{
    /// <summary>
    /// 后台外观
    /// </summary>
    public class NativeWebFacade //: BaseFacadeProvider
    {
        #region Fields

        private INativeWebDataProvider aideNativeWebData;

        #endregion Fields

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public NativeWebFacade()
        {
            aideNativeWebData = ClassFactory.GetINativeWebDataProvider();
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
            return aideNativeWebData.GetList(tableName, pageIndex, pageSize, condition, orderby);
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
            return aideNativeWebData.GetConfigInfo(id);
        }
        /// <summary>
        /// 获取站点配置
        /// </summary>
        /// <param name="configKey">配置Key</param>
        /// <returns></returns>
        public ConfigInfo GetConfigInfo(string configKey)
        {
            return aideNativeWebData.GetConfigInfo(configKey);
        }
        /// <summary>
        /// 修改站点配置
        /// </summary>
        /// <param name="config">配置信息</param>
        public int UpdateConfigInfo(ConfigInfo config)
        {
            return aideNativeWebData.UpdateConfigInfo(config);
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
            return aideNativeWebData.GetAds(id);
        }
        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="idlist">标识列表</param>
        public int DeleteAds(string idlist)
        {
            return aideNativeWebData.DeleteAds(idlist);
        }
        /// <summary>
        /// 新增广告
        /// </summary>
        /// <param name="ads">广告信息</param>
        public int InsertAds(Ads ads)
        {
            return aideNativeWebData.InsertAds(ads);
        }
        /// <summary>
        /// 修改广告
        /// </summary>
        /// <param name="ads">广告实体</param>
        public int UpdateAds(Ads ads)
        {
            return aideNativeWebData.UpdateAds(ads);
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
            return aideNativeWebData.GetSystemNoticeInfo(id);
        }
        /// <summary>
        /// 新增新闻公告
        /// </summary>
        /// <param name="notice">公告信息</param>
        /// <returns></returns>
        public int InsertSystemNotice(SystemNotice notice)
        {
            return aideNativeWebData.InsertSystemNotice(notice);
        }
        /// <summary>
        /// 修改公告信息
        /// </summary>
        /// <param name="notice">公告信息</param>
        /// <returns></returns>
        public int UpdateSystemNotice(SystemNotice notice)
        {
            return aideNativeWebData.UpdateSystemNotice(notice);
        }
        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="idlist">标识列表</param>
        /// <returns></returns>
        public int DeleteSystemNotice(string idlist)
        {
            return aideNativeWebData.DeleteSystemNotice(idlist);
        }
        #endregion

        #region 排行榜
        /// <summary>
        /// 根据标识获取排行榜配置
        /// </summary>
        /// <param name="configid">配置标识</param>
        /// <returns></returns>
        public RankingConfig GetRankingConfigByID(int configid)
        {
            return aideNativeWebData.GetRankingConfigByID(configid);
        }
        /// <summary>
        /// 判断排行榜配置是否存在
        /// </summary>
        /// <param name="typeid">排行榜类型</param>
        /// <param name="rankid">排行榜排名</param>
        /// <returns></returns>
        public bool ExistRankingConfig(int typeid, int rankid)
        {
            return aideNativeWebData.ExistRankingConfig(typeid, rankid);
        }
        /// <summary>
        /// 新增排行榜配置
        /// </summary>
        /// <param name="config">配置信息</param>
        /// <returns></returns>
        public int InsertRankingConfig(RankingConfig config)
        {
            return aideNativeWebData.InsertRankingConfig(config);
        }
        /// <summary>
        /// 修改排行榜配置
        /// </summary>
        /// <param name="config">配置信息</param>
        /// <returns></returns>
        public int UpdateRankingConfig(RankingConfig config)
        {
            return aideNativeWebData.UpdateRankingConfig(config);
        }
        /// <summary>
        /// 删除奖励配置
        /// </summary>
        /// <param name="configid">配置标识</param>
        /// <returns></returns>
        public int DeleteRankingConfig(int configid)
        {
            return aideNativeWebData.DeleteRankingConfig(configid);
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
            return aideNativeWebData.GetGameRuleInfo(kindid);
        }

        /// <summary>
        /// 新增游戏规则
        /// </summary>
        /// <param name="rule">游戏规则</param>
        /// <returns></returns>
        public int InsertGameRule(GameRule rule)
        {
            return aideNativeWebData.InsertGameRule(rule);
        }
        /// <summary>
        /// 修改游戏规则
        /// </summary>
        /// <param name="rule">游戏规则</param>
        /// <returns></returns>
        public int UpdateGameRule(GameRule rule)
        {
            return aideNativeWebData.UpdateGameRule(rule);
        }
        /// <summary>
        /// 删除游戏规则
        /// </summary>
        /// <param name="idlist">标识列表</param>
        /// <returns></returns>
        public int DeleteGameRule(string idlist)
        {
            return aideNativeWebData.DeleteGameRule(idlist);
        }
        /// <summary>
        /// 禁用启用游戏规则
        /// </summary>
        /// <param name="idlist">标识列表</param>
        /// <param name="nullity">禁用启用标识</param>
        /// <returns></returns>
        public int NullityGameRule(string idlist, int nullity)
        {
            return aideNativeWebData.NullityGameRule(idlist, nullity);
        }
        #endregion
    }
}