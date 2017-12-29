using System.Collections.Generic;

using Game.Kernel;
using System.Data;
using Game.Entity.Platform;
// ReSharper disable InconsistentNaming

namespace Game.IData
{
    /// <summary>
    /// 平台库数据层接口
    /// </summary>
    public interface IPlatformDataProvider //: IProvider
    {
        #region 开房信息
        /// <summary>
        /// 钻石消耗记录
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagerSet GetCreateRoomCost(string whereQuery, int pageIndex, int pageSize);
        /// <summary>
        /// 获取创建房间
        /// </summary>
        /// <param name="roomid">房间编号</param>
        /// <returns></returns>
        StreamCreateTableFeeInfo GetStreamCreateTableFeeInfo(int roomid);
        #endregion

        #region 游戏信息
        /// <summary>
        /// 根据游戏标识获取游戏
        /// </summary>
        /// <param name="kindid">游戏标识</param>
        /// <returns></returns>
        MobileKindItem GetGameKindItemByID(int kindid);
        /// <summary>
        /// 获取游戏列表
        /// </summary>
        /// <returns></returns>
        IList<MobileKindItem> GetMobileKindItemList();
        /// <summary>
        /// 获取游戏列表和版本配置
        /// </summary>
        /// <returns></returns>
        DataSet GetMobileGameAndVersion();
        #endregion

        #region 道具管理

        /// <summary>
        /// 获取道具信息by ID
        /// </summary>
        /// <returns></returns>
        GameProperty GetGameProperty(int id);

        #endregion
    }
}
