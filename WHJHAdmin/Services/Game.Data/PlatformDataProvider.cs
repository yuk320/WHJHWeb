using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

using Game.Kernel;
using Game.IData;
using Game.Entity.Platform;
using Game.Entity.Record;

namespace Game.Data
{
    /// <summary>
    /// 平台库数据层
    /// </summary>
    public class PlatformDataProvider : BaseDataProvider, IPlatformDataProvider
    {
        #region 构造方法
        public PlatformDataProvider( string connString )
            : base( connString )
        {
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
            GameGameItem game = GetGameGameItemInfo(kindId);
            if(game != null)
            {
                DataBaseInfo database = GetDataBaseInfo(game.DataBaseAddr);
                if(database != null)
                {
                    string userId = Utils.CWHEncryptNet.XorCrevasse(database.DBUser);
                    string password = Utils.CWHEncryptNet.XorCrevasse(database.DBPassword);
                    return
                        $"Data Source={game.DataBaseAddr + (string.IsNullOrEmpty(database.DBPort.ToString()) ? "" : ("," + database.DBPort.ToString()))}; Initial Catalog={game.DataBaseName}; User ID={userId}; Password={password}; Pooling=true";
                }
            }
            return "";
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
            PagerParameters pagerPrams = new PagerParameters(tableName, orderby, condition, pageIndex, pageSize);
            return GetPagerSet2(pagerPrams);
        }
        #endregion

        #region 游戏模块
        /// <summary>
        /// 获取游戏模块
        /// </summary>
        /// <param name="gameId">游戏模块标识</param>
        /// <returns></returns>
        public GameGameItem GetGameGameItemInfo( int gameId )
        {
            string sqlQuery = $"SELECT * FROM GameGameItem WITH(NOLOCK) WHERE GameID={gameId}";
            return Database.ExecuteObject<GameGameItem>(sqlQuery);
        }
        /// <summary>
        /// 新增游戏模块
        /// </summary>
        /// <param name="gameGameItem">游戏模块</param>
        public int InsertGameGameItem( GameGameItem gameGameItem )
        {
            string sqlQuery = @"INSERT INTO GameGameItem VALUES(@GameID,@GameName,@SuportType,@DataBaseAddr,
                            @DataBaseName,@ServerVersion,@ClientVersion,@ServerDLLName,@ClientExeName)";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("GameID", gameGameItem.GameID),
                Database.MakeInParam("GameName", gameGameItem.GameName),
                Database.MakeInParam("SuportType", gameGameItem.SuportType),
                Database.MakeInParam("DataBaseAddr", gameGameItem.DataBaseAddr),
                Database.MakeInParam("DataBaseName", gameGameItem.DataBaseName),
                Database.MakeInParam("ServerVersion", gameGameItem.ServerVersion),
                Database.MakeInParam("ClientVersion", gameGameItem.ClientVersion),
                Database.MakeInParam("ServerDLLName", gameGameItem.ServerDLLName),
                Database.MakeInParam("ClientExeName", gameGameItem.ClientExeName)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }
        /// <summary>
        /// 修改游戏模块
        /// </summary>
        /// <param name="gameGameItem">游戏模块</param>
        public int UpdateGameGameItem( GameGameItem gameGameItem )
        {
            string sqlQuery = @"UPDATE GameGameItem SET GameName=@GameName,SuportType=@SuporType,DataBaseAddr=@DataBaseAddr,
                        DataBaseName=@DataBaseName,ServerVersion=@ServerVersion,ClientVersion=@ClientVersion,
                        ServerDLLName=@ServerDLLName,ClientExeName=@ClientExeName WHERE GameID=@GameID";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("GameName", gameGameItem.GameName),
                Database.MakeInParam("SuporType", gameGameItem.SuportType),
                Database.MakeInParam("DataBaseAddr", gameGameItem.DataBaseAddr),
                Database.MakeInParam("DataBaseName", gameGameItem.DataBaseName),
                Database.MakeInParam("ServerVersion", gameGameItem.ServerVersion),
                Database.MakeInParam("ClientVersion", gameGameItem.ClientVersion),
                Database.MakeInParam("ServerDLLName", gameGameItem.ServerDLLName),
                Database.MakeInParam("ClientExeName", gameGameItem.ClientExeName),
                Database.MakeInParam("GameID", gameGameItem.GameID)
            };

            return Database.ExecuteNonQuery( CommandType.Text, sqlQuery, prams.ToArray() );
        }
        /// <summary>
        /// 删除游戏模块
        /// </summary>
        /// <param name="gamelist">游戏标识列表</param>
        public int DeleteGameGameItem( string gamelist)
        {
            string sqlQuery = $"DELETE GameGameItem WHERE GameID IN({gamelist})";
            return Database.ExecuteNonQuery(sqlQuery);
        }
        /// <summary>
        /// 删除游戏信息
        /// </summary>
        /// <param name="kindlist">游戏标识列表</param>
        public int DeleteMobileKindItem(string kindlist)
        {
            string sql = $"DELETE MobileKindItem WHERE KindID IN ({kindlist})";
            return Database.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 获取游戏信息
        /// </summary>
        /// <param name="kindid">游戏标识</param>
        /// <returns></returns>
        public MobileKindItem GetMobileKindItemInfo(int kindid)
        {
            string sql = $"SELECT * FROM MobileKindItem WITH(NOLOCK) WHERE KindID={kindid}";
            return Database.ExecuteObject<MobileKindItem>(sql);
        }
        /// <summary>
        /// 获取启用游戏列表
        /// </summary>
        /// <returns></returns>
        public IList<MobileKindItem> GetMobileKindItemList()
        {
            const string sql = "SELECT * FROM MobileKindItem WITH(NOLOCK) WHERE Nullity=0 ";
            return Database.ExecuteObjectList<MobileKindItem>(sql);
        }
        /// <summary>
        /// 新增游戏信息
        /// </summary>
        /// <param name="item">游戏信息</param>
        /// <returns></returns>
        public int InsertMobileKindItem(MobileKindItem item)
        {
            string sql = @"INSERT INTO MobileKindItem(KindID,KindName,TypeID,ModuleName,ClientVersion,ResVersion,SortID,KindMark,Nullity) 
                        VALUES(@KindID,@KindName,@TypeID,@ModuleName,@ClientVersion,@ResVersion,@SortID,@KindMark,@Nullity)";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("KindID", item.KindID),
                Database.MakeInParam("KindName", item.KindName),
                Database.MakeInParam("TypeID", item.TypeID),
                Database.MakeInParam("ModuleName", item.ModuleName),
                Database.MakeInParam("ClientVersion", item.ClientVersion),
                Database.MakeInParam("ResVersion", item.ResVersion),
                Database.MakeInParam("SortID", item.SortID),
                Database.MakeInParam("KindMark", item.KindMark),
                Database.MakeInParam("Nullity", item.Nullity)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sql, prams.ToArray());
        }
        /// <summary>
        /// 修改游戏信息
        /// </summary>
        /// <param name="item">游戏信息</param>
        /// <returns></returns>
        public int UpdateMobileKindItem(MobileKindItem item)
        {
            string sql = @"UPDATE MobileKindItem SET TypeID=@TypeID,ModuleName=@ModuleName,ClientVersion=@ClientVersion,
                            ResVersion=@ResVersion,SortID=@SortID,KindMark=@KindMark,Nullity=@Nullity WHERE KindID=@KindID";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("KindID", item.KindID),
                Database.MakeInParam("TypeID", item.TypeID),
                Database.MakeInParam("ModuleName", item.ModuleName),
                Database.MakeInParam("ClientVersion", item.ClientVersion),
                Database.MakeInParam("ResVersion", item.ResVersion),
                Database.MakeInParam("SortID", item.SortID),
                Database.MakeInParam("KindMark", item.KindMark),
                Database.MakeInParam("Nullity", item.Nullity)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sql, prams.ToArray());
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
            string sqlQuery = $"SELECT * FROM DataBaseInfo WITH(NOLOCK) WHERE DBInfoID= {dBInfoId}";
            return Database.ExecuteObject<DataBaseInfo>(sqlQuery);
        }
        /// <summary>
        /// 获取机器信息
        /// </summary>
        /// <param name="dBAddr">机器地址</param>
        /// <returns></returns>
        public DataBaseInfo GetDataBaseInfo(string dBAddr)
        {
            string sqlQuery = "SELECT * FROM DataBaseInfo WITH(NOLOCK) WHERE DBAddr= @DBAddr";

            var prams = new List<DbParameter> {Database.MakeInParam("DBAddr", dBAddr)};

            return Database.ExecuteObject<DataBaseInfo>(sqlQuery, prams);
        }
        /// <summary>
        /// 新增机器信息
        /// </summary>
        /// <param name="dataBase">机器信息</param>
        /// <returns></returns>
        public int InsertDataBase(DataBaseInfo dataBase)
        {
            string sqlQuery = @"INSERT INTO DataBaseInfo(DBAddr,DBPort,DBUser,DBPassword,MachineID,Information) VALUES(@DBAddr,
                    @DBPort,@DBUser,@DBPassword,@MachineID,@Information)";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("DBAddr", dataBase.DBAddr),
                Database.MakeInParam("DBPort", dataBase.DBPort),
                Database.MakeInParam("DBUser", dataBase.DBUser),
                Database.MakeInParam("DBPassword", dataBase.DBPassword),
                Database.MakeInParam("MachineID", dataBase.MachineID),
                Database.MakeInParam("Information", dataBase.Information)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }
        /// <summary>
        ///  修改机器信息
        /// </summary>
        /// <param name="dataBase">机器信息</param>
        /// <returns></returns>
        public int UpdateDataBase(DataBaseInfo dataBase)
        {
            string sqlQuery = @"UPDATE DataBaseInfo SET DBAddr=@DBAddr,DBPort=@DBPort,DBUser=@DBUser,
            DBPassword=@DBPassword,MachineID=@MachineID,Information=@Information WHERE DBInfoID=@DBInfoID";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("DBAddr", dataBase.DBAddr),
                Database.MakeInParam("DBPort", dataBase.DBPort),
                Database.MakeInParam("DBUser", dataBase.DBUser),
                Database.MakeInParam("DBPassword", dataBase.DBPassword),
                Database.MakeInParam("MachineID", dataBase.MachineID),
                Database.MakeInParam("Information", dataBase.Information),
                Database.MakeInParam("DBInfoID", dataBase.DBInfoID)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }
        /// <summary>
        /// 删除机器信息
        /// </summary>
        /// <param name="idlist">标识列表</param>
        /// <returns></returns>
        public int DeleteDataBase(string idlist)
        {
            string sqlQuery = $"DELETE DataBaseInfo WHERE DBInfoID IN({idlist})";
            return Database.ExecuteNonQuery(sqlQuery);
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
            string sqlQuery = $"SELECT * FROM GameRoomInfo WITH(NOLOCK) WHERE ServerID= {serverId}";
            return Database.ExecuteObject<GameRoomInfo>(sqlQuery);
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
            string sqlQuery = $"SELECT * FROM SystemMessage WITH(NOLOCK) WHERE ID= {id}";
            return Database.ExecuteObject<SystemMessage>(sqlQuery);
        }
        /// <summary>
        /// 新增系统消息
        /// </summary>
        /// <param name="systemMessage">系统消息</param>
        /// <returns></returns>
        public int InsertSystemMessage(SystemMessage systemMessage)
        {
            string sqlQuery = @"INSERT INTO SystemMessage(MessageType,ServerRange,MessageString,StartTime,
            ConcludeTime,TimeRate,Nullity,CreateDate,CreateMasterID,UpdateDate,UpdateMasterID,UpdateCount,CollectNote) 
            VALUES(@MessageType,@ServerRange,@MessageString,@StartTime,@ConcludeTime,@TimeRate,@Nullity,@CreateDate,
                @CreateMasterID,@UpdateDate,@UpdateMasterID,@UpdateCount,@CollectNote)";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("MessageType", systemMessage.MessageType),
                Database.MakeInParam("ServerRange", systemMessage.ServerRange),
                Database.MakeInParam("MessageString", systemMessage.MessageString),
                Database.MakeInParam("StartTime", systemMessage.StartTime),
                Database.MakeInParam("ConcludeTime", systemMessage.ConcludeTime),
                Database.MakeInParam("TimeRate", systemMessage.TimeRate),
                Database.MakeInParam("Nullity", systemMessage.Nullity),
                Database.MakeInParam("CreateDate", systemMessage.CreateDate),
                Database.MakeInParam("CreateMasterID", systemMessage.CreateMasterID),
                Database.MakeInParam("UpdateDate", systemMessage.UpdateDate),
                Database.MakeInParam("UpdateMasterID", systemMessage.UpdateMasterID),
                Database.MakeInParam("UpdateCount", systemMessage.UpdateCount),
                Database.MakeInParam("CollectNote", systemMessage.CollectNote)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }
        /// <summary>
        /// 修改系统消息
        /// </summary>
        /// <param name="systemMessage">系统消息</param>
        /// <returns></returns>
        public int UpdateSystemMessage(SystemMessage systemMessage)
        {
            string sqlQuery = @"UPDATE SystemMessage SET MessageType=@MessageType,ServerRange=@ServerRange,
                    MessageString=@MessageString,StartTime=@StartTime,ConcludeTime=@ConcludeTime,TimeRate=@TimeRate,
                    Nullity=@Nullity,UpdateDate=@UpdateDate,UpdateMasterID=@UpdateMasterID,UpdateCount=@UpdateCount,
                    CollectNote=@CollectNote WHERE ID=@ID";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("MessageType", systemMessage.MessageType),
                Database.MakeInParam("ServerRange", systemMessage.ServerRange),
                Database.MakeInParam("MessageString", systemMessage.MessageString),
                Database.MakeInParam("StartTime", systemMessage.StartTime),
                Database.MakeInParam("ConcludeTime", systemMessage.ConcludeTime),
                Database.MakeInParam("TimeRate", systemMessage.TimeRate),
                Database.MakeInParam("Nullity", systemMessage.Nullity),
                Database.MakeInParam("UpdateDate", systemMessage.UpdateDate),
                Database.MakeInParam("UpdateMasterID", systemMessage.UpdateMasterID),
                Database.MakeInParam("UpdateCount", systemMessage.UpdateCount),
                Database.MakeInParam("CollectNote", systemMessage.CollectNote),
                Database.MakeInParam("ID", systemMessage.ID)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }
        /// <summary>
        /// 删除系统消息
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public int DeleteSystemMessage(string where)
        {
            string sqlQuery = $"DELETE SystemMessage {where}";
            return Database.ExecuteNonQuery(sqlQuery);
        }
        /// <summary>
        /// 冻结解冻系统消息
        /// </summary>
        /// <param name="idList">标识字符集</param>
        /// <param name="nullity">状态值</param>
        /// <returns></returns>
        public int NullitySystemMessage(string idList, int nullity)
        {
            string sql = $"UPDATE SystemMessage SET Nullity ={nullity} WHERE ID IN({idList})";
            return Database.ExecuteNonQuery(sql);
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
            string sql =
                $"SELECT COUNT(UserID) AS UCont,SUM(CreateTableFee) AS CTotal FROM StreamCreateTableFeeInfo WITH(NOLOCK) WHERE PayMode=0 AND UserID={userid}";
            return Database.ExecuteDataset(CommandType.Text, sql);
        }
        /// <summary>
        /// 获取约战房间开房前50名
        /// </summary>
        /// <returns></returns>
        public DataSet GetCreateRoomRank()
        {
            string sql = "SELECT TOP 50 UserID,COUNT(UserID) AS UCont FROM StreamCreateTableFeeInfo WITH(NOLOCK) WHERE PayMode=0 AND RoomScoreInfo IS NOT NULL GROUP BY UserID ORDER BY COUNT(UserID) DESC";
            return Database.ExecuteDataset(CommandType.Text, sql);
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
            string sqlQuery =
                $"SELECT ISNULL(SUM(CreateTableFee),0) AS Diamond FROM StreamCreateTableFeeInfo WITH(NOLOCK) {where}";
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery);
            return obj != null ? Convert.ToInt64(obj) : 0;
        }
        /// <summary>
        /// 获取创建房间数量
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public long GetTotalCreateRoomTable(string where)
        {
            string sqlQuery = $"SELECT COUNT(RecordID) AS [Table] FROM StreamCreateTableFeeInfo WITH(NOLOCK) {where}";
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery);
            return obj != null ? Convert.ToInt64(obj) : 0;
        }
        /// <summary>
        /// 获取AA创建房间钻石统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public long GetAaTotalCreateRoomDiamond(string where)
        {
            string sqlQuery =
                $"SELECT ISNULL(SUM(Diamond),0) FROM WHJHRecordDB.dbo.RecordGameDiamond WITH(NOLOCK) WHERE RoomID IN(SELECT RoomID FROM StreamCreateTableFeeInfo WITH(NOLOCK) {where})";
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery);
            return obj != null ? Convert.ToInt64(obj) : 0;
        }
        /// <summary>
        /// 获取在线人数统计
        /// </summary>
        /// <param name="sTime">起始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns></returns>
        public IList<StatisticsOnline> GetUserOnlineStatistics(string sTime, string eTime)
        {
            string sqlQuery = @"SELECT InsertDateTime AS DTime,OnLineCountSum AS RUser,AndroidCountSum AS AUser FROM OnLineStreamInfo WITH(NOLOCK) WHERE InsertDateTime>=@sTime AND InsertDateTime<=@eTime ORDER BY InsertDateTime ASC";
            var prams = new List<DbParameter>
            {
                Database.MakeInParam("sTime", sTime),
                Database.MakeInParam("eTime", eTime)
            };

            return Database.ExecuteObjectList<StatisticsOnline>(sqlQuery, prams);
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
            return Database.ExecuteObject<GameProperty>($"SELECT * FROM GameProperty(NOLOCK) WHERE ID = {id}");
        }

        /// <summary>
        /// 设置更新道具
        /// </summary>
        /// <param name="sql"></param>
        public void SetProperty(string sql)
        {
            Database.ExecuteNonQuery("UPDATE GameProperty " + sql);
        }

        #endregion
    }
}
