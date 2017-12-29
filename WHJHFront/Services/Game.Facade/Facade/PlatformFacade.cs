using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Data.Factory;
using Game.IData;
using Game.Kernel;
using Game.Utils;
using System.Data;
using Game.Entity.Platform;

namespace Game.Facade
{
    /// <summary>
    /// 平台外观
    /// </summary>
    public class PlatformFacade
    {
        #region Fields

        private IPlatformDataProvider platformData;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PlatformFacade()
        {
            platformData = ClassFactory.GetIPlatformDataProvider();
        }
        #endregion

        #region 开房信息
        /// <summary>
        /// 钻石消耗记录
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetCreateRoomCost(string whereQuery, int pageIndex, int pageSize)
        {
            return platformData.GetCreateRoomCost(whereQuery, pageIndex, pageSize);
        }
        /// <summary>
        /// 获取创建房间
        /// </summary>
        /// <param name="roomid">房间编号</param>
        /// <returns></returns>
        public StreamCreateTableFeeInfo GetStreamCreateTableFeeInfo(int roomid)
        {
            return platformData.GetStreamCreateTableFeeInfo(roomid);
        }
        #endregion

        #region 游戏信息
        /// <summary>
        /// 根据游戏标识获取游戏
        /// </summary>
        /// <param name="kindid">游戏标识</param>
        /// <returns></returns>
        public MobileKindItem GetGameKindItemByID(int kindid)
        {
            return platformData.GetGameKindItemByID(kindid);
        }
        /// <summary>
        /// 获取游戏列表
        /// </summary>
        /// <returns></returns>
        public IList<MobileKindItem> GetMobileKindItemList()
        {
            return platformData.GetMobileKindItemList();
        }
        /// <summary>
        /// 获取游戏列表和版本配置
        /// </summary>
        /// <returns></returns>
        public DataSet GetMobileGameAndVersion()
        {
            return platformData.GetMobileGameAndVersion();
        }
        #endregion

        #region 道具管理

        /// <summary>
        /// 获取道具信息by ID
        /// </summary>
        /// <returns></returns>
        public GameProperty GetGameProperty(int id)
        {
            return platformData.GetGameProperty(id);
        }

        #endregion
    }
}
