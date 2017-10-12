using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// <param name="kindID">游戏标识</param>
        /// <returns></returns>
        public string GetConn(int kindID)
        {
            GameGameItem game = GetGameGameItemInfo(kindID);
            if(game != null)
            {
                DataBaseInfo database = GetDataBaseInfo(game.DataBaseAddr);
                if(database != null)
                {
                    string userID = Utils.CWHEncryptNet.XorCrevasse(database.DBUser);
                    string password = Utils.CWHEncryptNet.XorCrevasse(database.DBPassword);
                    return string.Format("Data Source={0}; Initial Catalog={1}; User ID={2}; Password={3}; Pooling=true",
                        game.DataBaseAddr + (string.IsNullOrEmpty(database.DBPort.ToString()) ? "" : ("," + database.DBPort.ToString())),
                        game.DataBaseName, userID, password);
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
        /// <param name="gameID">游戏模块标识</param>
        /// <returns></returns>
        public GameGameItem GetGameGameItemInfo( int gameID )
        {
            string sqlQuery = string.Format("SELECT * FROM GameGameItem WITH(NOLOCK) WHERE GameID={0}", gameID);
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

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("GameID", gameGameItem.GameID));
            prams.Add(Database.MakeInParam("GameName", gameGameItem.GameName));
            prams.Add(Database.MakeInParam("SuportType", gameGameItem.SuportType));
            prams.Add(Database.MakeInParam("DataBaseAddr", gameGameItem.DataBaseAddr));
            prams.Add(Database.MakeInParam("DataBaseName", gameGameItem.DataBaseName));
            prams.Add(Database.MakeInParam("ServerVersion", gameGameItem.ServerVersion));
            prams.Add(Database.MakeInParam("ClientVersion", gameGameItem.ClientVersion));
            prams.Add(Database.MakeInParam("ServerDLLName", gameGameItem.ServerDLLName));
            prams.Add(Database.MakeInParam("ClientExeName", gameGameItem.ClientExeName));

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

            var prams = new List<DbParameter>();
            prams.Add( Database.MakeInParam( "GameName", gameGameItem.GameName ) );
            prams.Add( Database.MakeInParam( "SuporType", gameGameItem.SuportType ) );
            prams.Add( Database.MakeInParam( "DataBaseAddr", gameGameItem.DataBaseAddr ) );
            prams.Add( Database.MakeInParam( "DataBaseName", gameGameItem.DataBaseName ) );
            prams.Add( Database.MakeInParam( "ServerVersion", gameGameItem.ServerVersion ) );
            prams.Add( Database.MakeInParam( "ClientVersion", gameGameItem.ClientVersion ) );
            prams.Add( Database.MakeInParam( "ServerDLLName", gameGameItem.ServerDLLName ) );
            prams.Add( Database.MakeInParam( "ClientExeName", gameGameItem.ClientExeName ) );
            prams.Add( Database.MakeInParam( "GameID", gameGameItem.GameID ) );

            return Database.ExecuteNonQuery( CommandType.Text, sqlQuery.ToString(), prams.ToArray() );
        }
        /// <summary>
        /// 删除游戏模块
        /// </summary>
        /// <param name="gamelist">游戏标识列表</param>
        public int DeleteGameGameItem( string gamelist)
        {
            string sqlQuery = string.Format("DELETE GameGameItem WHERE GameID IN({0})", gamelist);
            return Database.ExecuteNonQuery(sqlQuery);
        }
        /// <summary>
        /// 删除游戏信息
        /// </summary>
        /// <param name="kindlist">游戏标识列表</param>
        public int DeleteMobileKindItem(string kindlist)
        {
            string sql = string.Format("DELETE MobileKindItem WHERE KindID IN ({0})", kindlist);
            return Database.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 获取游戏信息
        /// </summary>
        /// <param name="kindid">游戏标识</param>
        /// <returns></returns>
        public MobileKindItem GetMobileKindItemInfo(int kindid)
        {
            string sql = string.Format("SELECT * FROM MobileKindItem WITH(NOLOCK) WHERE KindID={0}", kindid);
            return Database.ExecuteObject<MobileKindItem>(sql);
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

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("KindID", item.KindID));
            prams.Add(Database.MakeInParam("KindName", item.KindName));
            prams.Add(Database.MakeInParam("TypeID", item.TypeID));
            prams.Add(Database.MakeInParam("ModuleName", item.ModuleName));
            prams.Add(Database.MakeInParam("ClientVersion", item.ClientVersion));
            prams.Add(Database.MakeInParam("ResVersion", item.ResVersion));
            prams.Add(Database.MakeInParam("SortID", item.SortID));
            prams.Add(Database.MakeInParam("KindMark", item.KindMark));
            prams.Add(Database.MakeInParam("Nullity", item.Nullity));

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

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("KindID", item.KindID));
            prams.Add(Database.MakeInParam("TypeID", item.TypeID));
            prams.Add(Database.MakeInParam("ModuleName", item.ModuleName));
            prams.Add(Database.MakeInParam("ClientVersion", item.ClientVersion));
            prams.Add(Database.MakeInParam("ResVersion", item.ResVersion));
            prams.Add(Database.MakeInParam("SortID", item.SortID));
            prams.Add(Database.MakeInParam("KindMark", item.KindMark));
            prams.Add(Database.MakeInParam("Nullity", item.Nullity));

            return Database.ExecuteNonQuery(CommandType.Text, sql, prams.ToArray());
        }
        #endregion

        #region 机器管理
        /// <summary>
        /// 获取机器信息
        /// </summary>
        /// <param name="dBInfoID">标识</param>
        /// <returns></returns>
        public DataBaseInfo GetDataBaseInfo(int dBInfoID)
        {
            string sqlQuery = string.Format("SELECT * FROM DataBaseInfo WITH(NOLOCK) WHERE DBInfoID= {0}", dBInfoID);
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

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("DBAddr", dBAddr));

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

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("DBAddr", dataBase.DBAddr));
            prams.Add(Database.MakeInParam("DBPort", dataBase.DBPort));
            prams.Add(Database.MakeInParam("DBUser", dataBase.DBUser));
            prams.Add(Database.MakeInParam("DBPassword", dataBase.DBPassword));
            prams.Add(Database.MakeInParam("MachineID", dataBase.MachineID));
            prams.Add(Database.MakeInParam("Information", dataBase.Information));

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

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("DBAddr", dataBase.DBAddr));
            prams.Add(Database.MakeInParam("DBPort", dataBase.DBPort));
            prams.Add(Database.MakeInParam("DBUser", dataBase.DBUser));
            prams.Add(Database.MakeInParam("DBPassword", dataBase.DBPassword));
            prams.Add(Database.MakeInParam("MachineID", dataBase.MachineID));
            prams.Add(Database.MakeInParam("Information", dataBase.Information));
            prams.Add(Database.MakeInParam("DBInfoID", dataBase.DBInfoID));

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }
        /// <summary>
        /// 删除机器信息
        /// </summary>
        /// <param name="idlist">标识列表</param>
        /// <returns></returns>
        public int DeleteDataBase(string idlist)
        {
            string sqlQuery = string.Format("DELETE DataBaseInfo WHERE DBInfoID IN({0})", idlist);
            return Database.ExecuteNonQuery(sqlQuery);
        }
        #endregion

        #region 游戏房间
        /// <summary>
        /// 获取游戏房间
        /// </summary>
        /// <param name="serverID">房间标识</param>
        /// <returns></returns>
        public GameRoomInfo GetGameRoomInfoInfo(int serverID)
        {
            string sqlQuery = string.Format("SELECT * FROM GameRoomInfo WITH(NOLOCK) WHERE ServerID= {0}", serverID);
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
            string sqlQuery = string.Format("SELECT * FROM SystemMessage WITH(NOLOCK) WHERE ID= {0}", id);
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

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("MessageType", systemMessage.MessageType));
            prams.Add(Database.MakeInParam("ServerRange", systemMessage.ServerRange));
            prams.Add(Database.MakeInParam("MessageString", systemMessage.MessageString));
            prams.Add(Database.MakeInParam("StartTime", systemMessage.StartTime));
            prams.Add(Database.MakeInParam("ConcludeTime", systemMessage.ConcludeTime));
            prams.Add(Database.MakeInParam("TimeRate", systemMessage.TimeRate));
            prams.Add(Database.MakeInParam("Nullity", systemMessage.Nullity));
            prams.Add(Database.MakeInParam("CreateDate", systemMessage.CreateDate));
            prams.Add(Database.MakeInParam("CreateMasterID", systemMessage.CreateMasterID));
            prams.Add(Database.MakeInParam("UpdateDate", systemMessage.UpdateDate));
            prams.Add(Database.MakeInParam("UpdateMasterID", systemMessage.UpdateMasterID));
            prams.Add(Database.MakeInParam("UpdateCount", systemMessage.UpdateCount));
            prams.Add(Database.MakeInParam("CollectNote", systemMessage.CollectNote));

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

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("MessageType", systemMessage.MessageType));
            prams.Add(Database.MakeInParam("ServerRange", systemMessage.ServerRange));
            prams.Add(Database.MakeInParam("MessageString", systemMessage.MessageString));
            prams.Add(Database.MakeInParam("StartTime", systemMessage.StartTime));
            prams.Add(Database.MakeInParam("ConcludeTime", systemMessage.ConcludeTime));
            prams.Add(Database.MakeInParam("TimeRate", systemMessage.TimeRate));
            prams.Add(Database.MakeInParam("Nullity", systemMessage.Nullity));
            prams.Add(Database.MakeInParam("UpdateDate", systemMessage.UpdateDate));
            prams.Add(Database.MakeInParam("UpdateMasterID", systemMessage.UpdateMasterID));
            prams.Add(Database.MakeInParam("UpdateCount", systemMessage.UpdateCount));
            prams.Add(Database.MakeInParam("CollectNote", systemMessage.CollectNote));
            prams.Add(Database.MakeInParam("ID", systemMessage.ID));

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }
        /// <summary>
        /// 删除系统消息
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public int DeleteSystemMessage(string where)
        {
            string sqlQuery = string.Format("DELETE SystemMessage {0}", where);
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
            string sql = string.Format("UPDATE SystemMessage SET Nullity ={0} WHERE ID IN({1})", nullity, idList);
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
            string sql = string.Format("SELECT COUNT(UserID) AS UCont,SUM(CreateTableFee) AS CTotal FROM StreamCreateTableFeeInfo WITH(NOLOCK) WHERE PayMode=0 AND UserID={0}", userid);
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
            string sqlQuery = string.Format("SELECT ISNULL(SUM(CreateTableFee),0) AS Diamond FROM StreamCreateTableFeeInfo WITH(NOLOCK) {0}", where);
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
            string sqlQuery = string.Format("SELECT COUNT(RecordID) AS [Table] FROM StreamCreateTableFeeInfo WITH(NOLOCK) {0}", where);
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery);
            return obj != null ? Convert.ToInt64(obj) : 0;
        }
        /// <summary>
        /// 获取AA创建房间钻石统计
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public long GetAATotalCreateRoomDiamond(string where)
        {
            string sqlQuery = string.Format("SELECT ISNULL(SUM(Diamond),0) FROM WHJHRecordDB.dbo.RecordGameDiamond WITH(NOLOCK) WHERE RoomID IN(SELECT RoomID FROM StreamCreateTableFeeInfo WITH(NOLOCK) {0})", where);
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
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("sTime", sTime));
            prams.Add(Database.MakeInParam("eTime", eTime));

            return Database.ExecuteObjectList<StatisticsOnline>(sqlQuery, prams);
        }
        #endregion
    }
}
