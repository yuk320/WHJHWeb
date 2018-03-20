using System.Collections.Generic;
using Game.Kernel;
using Game.Entity.Treasure;
using System.Data;
using Game.Entity.Record;

namespace Game.IData
{
    /// <summary>
    /// 金币库数据层接口
    /// </summary>
    public interface ITreasureDataProvider //: IProvider
    {
        #region 公共分页
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

        #region 充值配置
        /// <summary>
        /// 获取充值产品
        /// </summary>
        /// <param name="configId">充值产品标识</param>
        /// <returns></returns>
        AppPayConfig GetAppPayConfig(int configId);
        /// <summary>
        /// 删除充值产品
        /// </summary>
        /// <param name="idlist">标识列表</param>
        int DeleteAppPayConfig(string idlist);
        /// <summary>
        /// 判断充值产品是否存在
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        bool IsExistAppPayConfig(string where);
        /// <summary>
        /// 新增充值产品
        /// </summary>
        /// <param name="config">充值产品</param>
        /// <returns></returns>
        int InsertAppPayConfig(AppPayConfig config);
        /// <summary>
        /// 修改充值产品
        /// </summary>
        /// <param name="config">充值产品</param>
        /// <returns></returns>
        int UpdateAppPayConfig(AppPayConfig config);
        #endregion

        #region 推广配置
        /// <summary>
        /// 获取推广配置
        /// </summary>
        /// <param name="configId">推广配置id</param>
        /// <returns></returns>
        SpreadConfig GetSpreadConfig(int configId);
        /// <summary>
        /// 推广配置数量
        /// </summary>
        /// <returns></returns>
        int SpreadConfigCount();
        /// <summary>
        /// 删除推广配置
        /// </summary>
        /// <param name="configId">推广配置id</param>
        /// <returns></returns>
        int DeleteSpreadConfig(int configId);
        /// <summary>
        /// 新增推广信息
        /// </summary>
        /// <param name="config">推广配置</param>
        /// <returns></returns>
        int InsertSpreadConfig(SpreadConfig config);
        /// <summary>
        /// 修改推广信息
        /// </summary>
        /// <param name="config">推广配置</param>
        /// <returns></returns>
        int UpdateSpreadConfig(SpreadConfig config);

        /// <summary>
        /// 获取推广返利配置
        /// </summary>
        /// <param name="configId"></param>
        /// <returns></returns>
        SpreadReturnConfig GetSpreadReturnConfig(int configId);

        /// <summary>
        /// 删除推广返利配置
        /// </summary>
        /// <param name="configId"></param>
        /// <returns></returns>
        int DeleteSpreadReturnConfig(int configId);

        /// <summary>
        /// 保存推广返利配置（新增、更新）
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        int SaveSpreadReturnConfig(SpreadReturnConfig config);
        #endregion

        #region 赠送钻石
        /// <summary>
        /// 赠送钻石
        /// </summary>
        /// <param name="diamond">赠送钻石信息</param>
        /// <returns></returns>
        Message GrantDiamond(RecordGrantDiamond diamond);
        /// <summary>
        /// 获取钻石前50名排行
        /// </summary>
        /// <returns></returns>
        DataSet GetDiamondRank();
        #endregion

        #region 统计总数
        /// <summary>
        /// 系统统计
        /// </summary>
        /// <returns></returns>
        DataSet GetStatInfo();
        /// <summary>
        /// 按条件获取已支付总数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        decimal GetTotalPayAmount(string where);
        /// <summary>
        /// 按条件获取已支付订单数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        long GetTotalPayOrderCount(string where);
        /// <summary>
        /// 获取钻石统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        long GetTotalUserDiamond(string where);
        /// <summary>
        /// 统计银行税收
        /// </summary>
        /// <param name="sTime">起始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns></returns>
        IList<StatisticsRevenue> GetDayInsureRevenue(string sTime, string eTime);
        /// <summary>
        /// 统计游戏税收
        /// </summary>
        /// <param name="sTime">起始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns></returns>
        IList<StatisticsRevenue> GetDayGameRevenue(string sTime, string eTime);

        /// <summary>
        /// 统计游戏损耗
        /// </summary>
        /// <param name="sTime">起始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <param name="extra"></param>
        /// <returns></returns>
        IList<StatisticsWaste> GetDayWaste(string sTime, string eTime, string extra);
        /// <summary>
        /// 获取金币分布统计
        /// </summary>
        /// <returns></returns>
        DataSet GetGoldDistribute();
        /// <summary>
        /// 获取钻石分布统计
        /// </summary>
        /// <returns></returns>
        DataSet GetDiamondDistribute();
        #endregion

        #region 金币信息
        /// <summary>
        /// 赠送金币
        /// </summary>
        /// <param name="rgt">赠送金币信息</param>
        /// <returns></returns>
        Message GrantTreasure(RecordGrantTreasure rgt);
        /// <summary>
        /// 获取兑换金币配置
        /// </summary>
        /// <param name="configId">配置标识</param>
        /// <returns></returns>
        CurrencyExchConfig GetCurrencyExch(int configId);
        /// <summary>
        /// 删除兑换金币配置
        /// </summary>
        /// <param name="idlist">标识列表</param>
        int DeleteCurrencyExch(string idlist);
        /// <summary>
        /// 判断兑换金币配置是否存在相同钻石
        /// </summary>
        /// <param name="diamond">兑换钻石数</param>
        /// <returns></returns>
        bool IsExistCurrencyExch(int diamond);
        /// <summary>
        /// 新增兑换金币配置
        /// </summary>
        /// <param name="config">兑换金币配置</param>
        /// <returns></returns>
        int InsertCurrencyExch(CurrencyExchConfig config);
        /// <summary>
        /// 修改兑换金币配置
        /// </summary>
        /// <param name="config">兑换金币配置</param>
        /// <returns></returns>
        int UpdateCurrencyExch(CurrencyExchConfig config);
        #endregion

        #region 卡线管理

        /// <summary>
        /// 批量清除玩家卡线
        /// </summary>
        /// <param name="userlist"></param>
        /// <returns></returns>
        int CleanGameScoreLocker(string userlist);

        #endregion
    }
}
