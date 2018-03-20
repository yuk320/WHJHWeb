using System.Collections.Generic;
using Game.IData;
using Game.Data.Factory;
using Game.Kernel;
using Game.Entity.Treasure;
using System.Data;
using Game.Entity.Record;

// ReSharper disable once CheckNamespace
namespace Game.Facade
{
    /// <summary>
    /// 金币库外观
    /// </summary>
    public class TreasureFacade
    {
        #region Fields

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private ITreasureDataProvider aideTreasureData;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public TreasureFacade()
        {
            aideTreasureData = ClassFactory.GetITreasureDataProvider();
        }
        #endregion

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
        public PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby)
        {
            return aideTreasureData.GetList(tableName, pageIndex, pageSize, condition, orderby);
        }
        #endregion

        #region 充值配置
        /// <summary>
        /// 获取充值产品
        /// </summary>
        /// <param name="configId">充值产品标识</param>
        /// <returns></returns>
        public AppPayConfig GetAppPayConfig(int configId)
        {
            return aideTreasureData.GetAppPayConfig(configId);
        }
        /// <summary>
        /// 删除充值产品
        /// </summary>
        /// <param name="idlist">标识列表</param>
        public int DeleteAppPayConfig(string idlist)
        {
            return aideTreasureData.DeleteAppPayConfig(idlist);
        }
        /// <summary>
        /// 判断充值产品是否存在
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public bool IsExistAppPayConfig(string where)
        {
            return aideTreasureData.IsExistAppPayConfig(where);
        }
        /// <summary>
        /// 新增充值产品
        /// </summary>
        /// <param name="config">充值产品</param>
        /// <returns></returns>
        public int InsertAppPayConfig(AppPayConfig config)
        {
            return aideTreasureData.InsertAppPayConfig(config);
        }
        /// <summary>
        /// 修改充值产品
        /// </summary>
        /// <param name="config">充值产品</param>
        /// <returns></returns>
        public int UpdateAppPayConfig(AppPayConfig config)
        {
            return aideTreasureData.UpdateAppPayConfig(config);
        }
        #endregion

        #region 推广配置
        /// <summary>
        /// 获取推广配置
        /// </summary>
        /// <param name="configId">推广配置id</param>
        /// <returns></returns>
        public SpreadConfig GetSpreadConfig(int configId)
        {
            return aideTreasureData.GetSpreadConfig(configId);
        }
        /// <summary>
        /// 推广配置数量
        /// </summary>
        /// <returns></returns>
        public int SpreadConfigCount()
        {
            return aideTreasureData.SpreadConfigCount();
        }
        /// <summary>
        /// 删除推广配置
        /// </summary>
        /// <param name="configId">推广配置id</param>
        /// <returns></returns>
        public int DeleteSpreadConfig(int configId)
        {
            return aideTreasureData.DeleteSpreadConfig(configId);
        }
        /// <summary>
        /// 新增推广信息
        /// </summary>
        /// <param name="config">推广配置</param>
        /// <returns></returns>
        public int InsertSpreadConfig(SpreadConfig config)
        {
            return aideTreasureData.InsertSpreadConfig(config);
        }
        /// <summary>
        /// 修改推广信息
        /// </summary>
        /// <param name="config">推广配置</param>
        /// <returns></returns>
        public int UpdateSpreadConfig(SpreadConfig config)
        {
            return aideTreasureData.UpdateSpreadConfig(config);
        }

        /// <summary>
        /// 获取推广返利配置
        /// </summary>
        /// <param name="configId"></param>
        /// <returns></returns>
        public SpreadReturnConfig GetSpreadReturnConfig(int configId)
        {
            return aideTreasureData.GetSpreadReturnConfig(configId);
        }

        /// <summary>
        /// 删除推广返利配置
        /// </summary>
        /// <param name="configId"></param>
        /// <returns></returns>
        public int DeleteSpreadReturnConfig(int configId)
        {
            return aideTreasureData.DeleteSpreadReturnConfig(configId);
        }

        /// <summary>
        /// 保存推广返利配置（新增、更新）
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public int SaveSpreadReturnConfig(SpreadReturnConfig config)
        {
            return aideTreasureData.SaveSpreadReturnConfig(config);
        }
        #endregion

        #region 赠送钻石
        /// <summary>
        /// 赠送钻石
        /// </summary>
        /// <param name="diamond">赠送钻石信息</param>
        /// <returns></returns>
        public Message GrantDiamond(RecordGrantDiamond diamond)
        {
            return aideTreasureData.GrantDiamond(diamond);
        }
        /// <summary>
        /// 获取钻石前50名排行
        /// </summary>
        /// <returns></returns>
        public DataSet GetDiamondRank()
        {
            return aideTreasureData.GetDiamondRank();
        }
        #endregion

        #region 统计总数

        /// <summary>
        /// 系统统计
        /// </summary>
        /// <returns></returns>
        public DataSet GetStatInfo()
        {
            return aideTreasureData.GetStatInfo();
        }
        /// <summary>
        /// 按条件获取已支付总数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public decimal GetTotalPayAmount(string where)
        {
            return aideTreasureData.GetTotalPayAmount(where);
        }
        /// <summary>
        /// 按条件获取已支付订单数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public long GetTotalPayOrderCount(string where)
        {
            return aideTreasureData.GetTotalPayOrderCount(where);
        }
        /// <summary>
        /// 获取钻石统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public long GetTotalUserDiamond(string where)
        {
            return aideTreasureData.GetTotalUserDiamond(where);
        }
        /// <summary>
        /// 统计银行税收
        /// </summary>
        /// <param name="sTime">起始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns></returns>
        public IList<StatisticsRevenue> GetDayInsureRevenue(string sTime, string eTime)
        {
            return aideTreasureData.GetDayInsureRevenue(sTime, eTime);
        }
        /// <summary>
        /// 统计游戏税收
        /// </summary>
        /// <param name="sTime">起始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns></returns>
        public IList<StatisticsRevenue> GetDayGameRevenue(string sTime, string eTime)
        {
            return aideTreasureData.GetDayGameRevenue(sTime, eTime);
        }
        /// <summary>
        /// 统计游戏损耗
        /// </summary>
        /// <param name="sTime">起始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <param name="extra"></param>
        /// <returns></returns>
        public IList<StatisticsWaste> GetDayWaste(string sTime, string eTime, string extra)
        {
            return aideTreasureData.GetDayWaste(sTime, eTime, extra);
        }
        /// <summary>
        /// 获取金币分布统计
        /// </summary>
        /// <returns></returns>
        public DataSet GetGoldDistribute()
        {
            return aideTreasureData.GetGoldDistribute();
        }
        /// <summary>
        /// 获取钻石分布统计
        /// </summary>
        /// <returns></returns>
        public DataSet GetDiamondDistribute()
        {
            return aideTreasureData.GetDiamondDistribute();
        }
        #endregion

        #region 金币信息
        /// <summary>
        /// 赠送金币
        /// </summary>
        /// <param name="rgt">赠送金币信息</param>
        /// <returns></returns>
        public Message GrantTreasure(RecordGrantTreasure rgt)
        {
            return aideTreasureData.GrantTreasure(rgt);
        }
        /// <summary>
        /// 获取兑换金币配置
        /// </summary>
        /// <param name="configId">配置标识</param>
        /// <returns></returns>
        public CurrencyExchConfig GetCurrencyExch(int configId)
        {
            return aideTreasureData.GetCurrencyExch(configId);
        }
        /// <summary>
        /// 删除兑换金币配置
        /// </summary>
        /// <param name="idlist">标识列表</param>
        public int DeleteCurrencyExch(string idlist)
        {
            return aideTreasureData.DeleteCurrencyExch(idlist);
        }
        /// <summary>
        /// 判断兑换金币配置是否存在相同钻石
        /// </summary>
        /// <param name="diamond">兑换钻石数</param>
        /// <returns></returns>
        public bool IsExistCurrencyExch(int diamond)
        {
            return aideTreasureData.IsExistCurrencyExch(diamond);
        }
        /// <summary>
        /// 新增兑换金币配置
        /// </summary>
        /// <param name="config">兑换金币配置</param>
        /// <returns></returns>
        public int InsertCurrencyExch(CurrencyExchConfig config)
        {
            return aideTreasureData.InsertCurrencyExch(config);
        }
        /// <summary>
        /// 修改兑换金币配置
        /// </summary>
        /// <param name="config">兑换金币配置</param>
        /// <returns></returns>
        public int UpdateCurrencyExch(CurrencyExchConfig config)
        {
            return aideTreasureData.UpdateCurrencyExch(config);
        }
        #endregion

        #region 卡线管理

        /// <summary>
        /// 批量清除玩家卡线
        /// </summary>
        /// <param name="userlist"></param>
        /// <returns></returns>
        public int CleanGameScoreLocker(string userlist)
        {
            return aideTreasureData.CleanGameScoreLocker(userlist);
        }

        #endregion
    }
}
