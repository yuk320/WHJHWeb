using System.Collections.Generic;
using System.Data;
using Game.Kernel;
using Game.IData;
using System.Data.Common;
using Game.Entity.Platform;

namespace Game.Data
{
    /// <summary>
    /// 平台数据访问层
    /// </summary>
    public class PlatformDataProvider : BaseDataProvider, IPlatformDataProvider
    {
        #region 构造方法

        public PlatformDataProvider(string connString)
            : base(connString)
        {
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
            const string orderQuery = "ORDER By CreateDate DESC";
            string[] returnField = {"CreateDate", "RoomID", "CreateTableFee", "DissumeDate"};
            PagerParameters pager = new PagerParameters("StreamCreateTableFeeInfo", orderQuery, whereQuery, pageIndex,
                pageSize)
            {
                Fields = returnField,
                CacherSize = 2
            };
            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 获取创建房间
        /// </summary>
        /// <param name="roomid">房间编号</param>
        /// <returns></returns>
        public StreamCreateTableFeeInfo GetStreamCreateTableFeeInfo(int roomid)
        {
            const string sql =
                "SELECT UserID,NickName,CreateTableFee,CreateDate,DissumeDate,PayMode,RoomStatus,NeedRoomCard FROM StreamCreateTableFeeInfo WITH(NOLOCK) WHERE RoomID = @RoomID";

            List<DbParameter> prams = new List<DbParameter> {Database.MakeInParam("RoomID", roomid)};

            return Database.ExecuteObject<StreamCreateTableFeeInfo>(sql, prams);
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
            const string sql = "SELECT * FROM MobileKindItem WITH(NOLOCK) WHERE KindID = @KindID";

            List<DbParameter> prams = new List<DbParameter> {Database.MakeInParam("KindID", kindid)};

            return Database.ExecuteObject<MobileKindItem>(sql, prams);
        }

        /// <summary>
        /// 获取游戏列表
        /// </summary>
        /// <returns></returns>
        public IList<MobileKindItem> GetMobileKindItemList()
        {
            string sql = "SELECT * FROM MobileKindItem WITH(NOLOCK) WHERE Nullity=0 ORDER BY SortID DESC";
            return Database.ExecuteObjectList<MobileKindItem>(sql);
        }

        /// <summary>
        /// 获取游戏列表和版本配置
        /// </summary>
        /// <returns></returns>
        public DataSet GetMobileGameAndVersion()
        {
            return Database.ExecuteDataset(CommandType.StoredProcedure, "NET_PW_GetMobileGameAndVersion");
        }

        #endregion

        #region 道具管理

        /// <summary>
        /// 获取道具信息by ID
        /// </summary>
        /// <returns></returns>
        public GameProperty GetGameProperty(int id)
        {
            string sql = $"SELECT * FROM GameProperty WITH(NOLOCK) WHERE ID={id} ";
            return Database.ExecuteObject<GameProperty>(sql);
        }

        #endregion
    }
}
