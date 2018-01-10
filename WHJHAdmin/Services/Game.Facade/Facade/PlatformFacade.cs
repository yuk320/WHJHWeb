using System.Collections.Generic;
using System.Data;

using Game.IData;
using Game.Data.Factory;
using Game.Entity.Platform;
using Game.Kernel;
using Game.Entity.Record;

// ReSharper disable once CheckNamespace
namespace Game.Facade
{
    /// <summary>
    /// 平台库外观
    /// </summary>
    public class PlatformFacade
    {
        #region Fields

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private IPlatformDataProvider _aidePlatformData;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PlatformFacade()
        {
            _aidePlatformData = ClassFactory.GetIPlatformDataProvider();
        }
        #endregion

        #region 数据库连接字符
        /// <summary>
        /// 获取积分库的连接串
        /// </summary>
        /// <param name="kindId">游戏标识</param>
        /// <returns></returns>
        public string GetConn(int kindId)
        {
            return _aidePlatformData.GetConn(kindId);
        }
        #endregion

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
            return _aidePlatformData.GetList(tableName, pageIndex, pageSize, condition, orderby);
        }
        #endregion

        #region 游戏模块
        /// <summary>
        /// 获取游戏模块
        /// </summary>
        /// <param name="gameId">游戏模块标识</param>
        /// <returns></returns>
        public GameGameItem GetGameGameItemInfo(int gameId)
        {
            return _aidePlatformData.GetGameGameItemInfo(gameId);
        }
        /// <summary>
        /// 新增游戏模块
        /// </summary>
        /// <param name="gameGameItem">游戏模块</param>
        public int InsertGameGameItem(GameGameItem gameGameItem)
        {
            return _aidePlatformData.InsertGameGameItem(gameGameItem);
        }
        /// <summary>
        /// 修改游戏模块
        /// </summary>
        /// <param name="gameGameItem">游戏模块</param>
        public int UpdateGameGameItem(GameGameItem gameGameItem)
        {
            return _aidePlatformData.UpdateGameGameItem(gameGameItem);
        }
        /// <summary>
        /// 删除游戏模块
        /// </summary>
        /// <param name="gamelist">游戏标识列表</param>
        public int DeleteGameGameItem(string gamelist)
        {
            return _aidePlatformData.DeleteGameGameItem(gamelist);
        }

        /// <summary>
        /// 删除游戏信息
        /// </summary>
        /// <param name="kindList">游戏标识列表</param>
        public int DeleteMobileKindItem(string kindList)
        {
            return _aidePlatformData.DeleteMobileKindItem(kindList);
        }
        /// <summary>
        /// 获取游戏信息
        /// </summary>
        /// <param name="kindid">游戏标识</param>
        /// <returns></returns>
        public MobileKindItem GetMobileKindItemInfo(int kindid)
        {
            return _aidePlatformData.GetMobileKindItemInfo(kindid);
        }
        /// <summary>
        /// 获取启用游戏列表
        /// </summary>
        /// <returns></returns>
        public IList<MobileKindItem> GetMobileKindItemList()
        {
            return _aidePlatformData.GetMobileKindItemList();
        }
        /// <summary>
        /// 新增游戏信息
        /// </summary>
        /// <param name="item">游戏信息</param>
        /// <returns></returns>
        public int InsertMobileKindItem(MobileKindItem item)
        {
            return _aidePlatformData.InsertMobileKindItem(item);
        }
        /// <summary>
        /// 修改游戏信息
        /// </summary>
        /// <param name="item">游戏信息</param>
        /// <returns></returns>
        public int UpdateMobileKindItem(MobileKindItem item)
        {
            return _aidePlatformData.UpdateMobileKindItem(item);
        }

        /// <summary>
        /// 批量修改时游戏可用性
        /// </summary>
        /// <param name="idList">idlist（0,x,x,x）</param>
        /// <param name="nullity">0：启用 1：禁用</param>
        /// <returns></returns>
        public int ChangeMobileKindNullity(string idList, int nullity)
        {
            return _aidePlatformData.ChangeMobileKindNullity(idList, nullity);
        }
        #endregion

        #region 机器管理
        /// <summary>
        /// 获取机器信息
        /// </summary>
        /// <param name="dBInfoId">标识</param>
        /// <returns></returns>
        public DataBaseInfo GetDataBaseInfo(int dBInfoId)
        {
            return _aidePlatformData.GetDataBaseInfo(dBInfoId);
        }
        /// <summary>
        /// 获取机器信息
        /// </summary>
        /// <param name="dBAddr">机器地址</param>
        /// <returns></returns>
        public DataBaseInfo GetDataBaseInfo(string dBAddr)
        {
            return _aidePlatformData.GetDataBaseInfo(dBAddr);
        }
        /// <summary>
        /// 新增机器信息
        /// </summary>
        /// <param name="dataBase">机器信息</param>
        /// <returns></returns>
        public int InsertDataBase(DataBaseInfo dataBase)
        {
            return _aidePlatformData.InsertDataBase(dataBase);
        }
        /// <summary>
        ///  修改机器信息
        /// </summary>
        /// <param name="dataBase">机器信息</param>
        /// <returns></returns>
        public int UpdateDataBase(DataBaseInfo dataBase)
        {
            return _aidePlatformData.UpdateDataBase(dataBase);
        }
        /// <summary>
        /// 删除机器信息
        /// </summary>
        /// <param name="idlist">标识列表</param>
        /// <returns></returns>
        public int DeleteDataBase(string idlist)
        {
            return _aidePlatformData.DeleteDataBase(idlist);
        }
        #endregion

        #region 游戏房间
        /// <summary>
        /// 获取游戏房间
        /// </summary>
        /// <param name="serverId">房间标识</param>
        /// <returns></returns>
        public GameRoomInfo GetGameRoomInfoInfo(int serverId)
        {
            return _aidePlatformData.GetGameRoomInfoInfo(serverId);
        }
        #endregion

        #region 系统消息
        /// <summary>
        /// 获取系统消息
        /// </summary>
        /// <param name="id">消息标识</param>
        /// <returns></returns>
        public SystemMessage GetSystemMessageInfo(int id)
        {
            return _aidePlatformData.GetSystemMessageInfo(id);
        }
        /// <summary>
        /// 新增系统消息
        /// </summary>
        /// <param name="systemMessage">系统消息</param>
        /// <returns></returns>
        public int InsertSystemMessage(SystemMessage systemMessage)
        {
            return _aidePlatformData.InsertSystemMessage(systemMessage);
        }
        /// <summary>
        /// 修改系统消息
        /// </summary>
        /// <param name="systemMessage">系统消息</param>
        /// <returns></returns>
        public int UpdateSystemMessage(SystemMessage systemMessage)
        {
            return _aidePlatformData.UpdateSystemMessage(systemMessage);
        }
        /// <summary>
        /// 删除系统消息
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public int DeleteSystemMessage(string where)
        {
            return _aidePlatformData.DeleteSystemMessage(where);
        }
        /// <summary>
        /// 冻结解冻系统消息
        /// </summary>
        /// <param name="idList">标识字符集</param>
        /// <param name="nullity">状态值</param>
        /// <returns></returns>
        public int NullitySystemMessage(string idList, int nullity)
        {
            return _aidePlatformData.NullitySystemMessage(idList, nullity);
        }
        #endregion

        #region 约战房间
        /// <summary>
        /// 获取约战房间信息
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <returns></returns>
        public DataSet GetCreateRoomInfo(int userid)
        {
            return _aidePlatformData.GetCreateRoomInfo(userid);
        }
        /// <summary>
        /// 获取约战房间开房前50名
        /// </summary>
        /// <returns></returns>
        public DataSet GetCreateRoomRank()
        {
            return _aidePlatformData.GetCreateRoomRank();
        }
        #endregion

        #region 统计总数
        /// <summary>
        /// 获取创建房间钻石统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public long GetTotalCreateRoomDiamond(string where)
        {
            return _aidePlatformData.GetTotalCreateRoomDiamond(where);
        }
        /// <summary>
        /// 获取创建房间数量
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public long GetTotalCreateRoomTable(string where)
        {
            return _aidePlatformData.GetTotalCreateRoomTable(where);
        }
        /// <summary>
        /// 获取AA创建房间钻石统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public long GetAaTotalCreateRoomDiamond(string where)
        {
            return _aidePlatformData.GetAaTotalCreateRoomDiamond(where);
        }
        /// <summary>
        /// 获取在线人数统计
        /// </summary>
        /// <param name="sTime">起始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns></returns>
        public IList<StatisticsOnline> GetUserOnlineStatistics(string sTime, string eTime)
        {
            return _aidePlatformData.GetUserOnlineStatistics(sTime, eTime);
        }
        #endregion

        #region 道具管理

        /// <summary>
        /// 获取道具信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GameProperty GetPropertyInfo(int id)
        {
            return _aidePlatformData.GetPropertyInfo(id);
        }

        /// <summary>
        /// 设置更新道具
        /// </summary>
        /// <param name="sql"></param>
        public void SetProperty(string sql)
        {
            _aidePlatformData.SetProperty(sql);
        }

        /// <summary>
        /// 编辑道具
        /// </summary>
        /// <param name="gameProperty"></param>
        public void UpdatePropertyInfo(GameProperty gameProperty)
        {
            string sql = " SET " +
                         $" [ExchangeRatio] = {gameProperty.ExchangeRatio}," +
                        //  $" [ExchangeGoldRatio] = {gameProperty.ExchangeGoldRatio}, " +
                         $" [BuyResultsGold] = {gameProperty.BuyResultsGold}, " +
                         $" [UseResultsGold] = {gameProperty.UseResultsGold}, " +
                         $" [RegulationsInfo] = N'{gameProperty.RegulationsInfo}', " +
                         $" [Recommend] = {gameProperty.Recommend}, " +
                         $" [SortID] = {gameProperty.SortID}, " +
                         $" [Nullity] = {gameProperty.Nullity} " +
                         $" WHERE ID={gameProperty.ID} ";
            _aidePlatformData.SetProperty(sql);
        }

        #endregion
    }
}
