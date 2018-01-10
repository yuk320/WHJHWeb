using System.Collections.Generic;
using System.Data;

using Game.Kernel;
using Game.Entity.Platform;
using Game.Entity.Record;

namespace Game.IData
{
    /// <summary>
    /// 平台库数据层接口
    /// </summary>
    public interface IPlatformDataProvider //: IProvider
    {
        #region 数据库连接字符
        /// <summary>
        /// 获取积分库的连接串
        /// </summary>
        /// <param name="kindId">游戏标识</param>
        /// <returns></returns>
        string GetConn(int kindId);
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
        PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby);
        #endregion

        #region 游戏模块
        /// <summary>
        /// 获取游戏模块
        /// </summary>
        /// <param name="gameId">游戏模块标识</param>
        /// <returns></returns>
        GameGameItem GetGameGameItemInfo(int gameId);
        /// <summary>
        /// 新增游戏模块
        /// </summary>
        /// <param name="gameGameItem">游戏模块</param>
        int InsertGameGameItem(GameGameItem gameGameItem);
        /// <summary>
        /// 修改游戏模块
        /// </summary>
        /// <param name="gameGameItem">游戏模块</param>
        int UpdateGameGameItem(GameGameItem gameGameItem);
        /// <summary>
        /// 删除游戏模块
        /// </summary>
        /// <param name="gamelist">游戏标识列表</param>
        int DeleteGameGameItem(string gamelist);
        /// <summary>
        /// 删除游戏信息
        /// </summary>
        /// <param name="kindlist">游戏标识列表</param>
        int DeleteMobileKindItem(string kindlist);
        /// <summary>
        /// 获取游戏信息
        /// </summary>
        /// <param name="kindid">游戏标识</param>
        /// <returns></returns>
        MobileKindItem GetMobileKindItemInfo(int kindid);
        /// <summary>
        /// 获取启用游戏列表
        /// </summary>
        /// <returns></returns>
        IList<MobileKindItem> GetMobileKindItemList();
        /// <summary>
        /// 新增游戏信息
        /// </summary>
        /// <param name="item">游戏信息</param>
        /// <returns></returns>
        int InsertMobileKindItem(MobileKindItem item);
        /// <summary>
        /// 修改游戏信息
        /// </summary>
        /// <param name="item">游戏信息</param>
        /// <returns></returns>
        int UpdateMobileKindItem(MobileKindItem item);

        /// <summary>
        /// 批量修改时游戏可用性
        /// </summary>
        /// <param name="idList">idlist（0,x,x,x）</param>
        /// <param name="nullity">0：启用 1：禁用</param>
        /// <returns></returns>
        int ChangeMobileKindNullity(string idList, int nullity);
        #endregion

        #region 机器管理
        /// <summary>
        /// 获取机器信息
        /// </summary>
        /// <param name="dBInfoId">标识</param>
        /// <returns></returns>
        DataBaseInfo GetDataBaseInfo(int dBInfoId);
        /// <summary>
        /// 获取机器信息
        /// </summary>
        /// <param name="dBAddr">机器地址</param>
        /// <returns></returns>
        DataBaseInfo GetDataBaseInfo(string dBAddr);
        /// <summary>
        /// 新增机器信息
        /// </summary>
        /// <param name="dataBase">机器信息</param>
        /// <returns></returns>
        int InsertDataBase(DataBaseInfo dataBase);
        /// <summary>
        ///  修改机器信息
        /// </summary>
        /// <param name="dataBase">机器信息</param>
        /// <returns></returns>
        int UpdateDataBase(DataBaseInfo dataBase);
        /// <summary>
        /// 删除机器信息
        /// </summary>
        /// <param name="idlist">标识列表</param>
        /// <returns></returns>
        int DeleteDataBase(string idlist);
        #endregion

        #region 游戏房间
        /// <summary>
        /// 获取游戏房间
        /// </summary>
        /// <param name="serverId">房间标识</param>
        /// <returns></returns>
        GameRoomInfo GetGameRoomInfoInfo(int serverId);
        #endregion

        #region 系统消息
        /// <summary>
        /// 获取系统消息
        /// </summary>
        /// <param name="id">消息标识</param>
        /// <returns></returns>
        SystemMessage GetSystemMessageInfo(int id);
        /// <summary>
        /// 新增系统消息
        /// </summary>
        /// <param name="systemMessage">系统消息</param>
        /// <returns></returns>
        int InsertSystemMessage(SystemMessage systemMessage);
        /// <summary>
        /// 修改系统消息
        /// </summary>
        /// <param name="systemMessage">系统消息</param>
        /// <returns></returns>
        int UpdateSystemMessage(SystemMessage systemMessage);
        /// <summary>
        /// 删除系统消息
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        int DeleteSystemMessage(string where);
        /// <summary>
        /// 冻结解冻系统消息
        /// </summary>
        /// <param name="idList">标识字符集</param>
        /// <param name="nullity">状态值</param>
        /// <returns></returns>
        int NullitySystemMessage(string idList, int nullity);
        #endregion

        #region 约战房间
        /// <summary>
        /// 获取约战房间信息
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <returns></returns>
        DataSet GetCreateRoomInfo(int userid);
        /// <summary>
        /// 获取约战房间开房前50名
        /// </summary>
        /// <returns></returns>
        DataSet GetCreateRoomRank();
        #endregion

        #region 统计总数
        /// <summary>
        /// 获取创建房间钻石统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        long GetTotalCreateRoomDiamond(string where);
        /// <summary>
        /// 获取创建房间数量
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        long GetTotalCreateRoomTable(string where);
        /// <summary>
        /// 获取AA创建房间钻石统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        long GetAaTotalCreateRoomDiamond(string where);
        /// <summary>
        /// 获取在线人数统计
        /// </summary>
        /// <param name="sTime">起始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns></returns>
        IList<StatisticsOnline> GetUserOnlineStatistics(string sTime, string eTime);
        #endregion

        #region 道具管理

        /// <summary>
        /// 获取道具信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GameProperty GetPropertyInfo(int id);

        /// <summary>
        /// 设置更新道具
        /// </summary>
        /// <param name="sql"></param>
        void SetProperty(string sql);

        #endregion
    }
}
