using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Game.Entity.Accounts;
using Game.IData;
using Game.Kernel;

namespace Game.Data
{
    /// <summary>
    /// 帐号库数据层
    /// </summary>
    public class AccountsDataProvider : BaseDataProvider, IAccountsDataProvider
    {
        #region 构造方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public AccountsDataProvider(string connString)
            : base(connString)
        {

        }

        #endregion 构造方法

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
        #endregion 公用分页

        #region 用户信息
        /// <summary>
        /// 根据用户标识获取用户信息
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <returns></returns>
        public AccountsInfo GetAccountInfoByUserId(int userId)
        {
            string sqlQuery = $"SELECT * FROM AccountsInfo WITH(NOLOCK) WHERE UserID={userId}";
            return Database.ExecuteObject<AccountsInfo>(sqlQuery);
        }
        /// <summary>
        /// 根据游戏ID获取用户信息
        /// </summary>
        /// <param name="gameId">游戏ID</param>
        /// <returns></returns>
        public AccountsInfo GetAccountInfoByGameId(int gameId)
        {
            string sqlQuery = $"SELECT * FROM AccountsInfo WITH(NOLOCK) WHERE GameID={gameId}";
            return Database.ExecuteObject<AccountsInfo>(sqlQuery);
        }
        /// <summary>
        /// 冻结解冻账号
        /// </summary>
        /// <param name="userlist">用户列表</param>
        /// <param name="nullity">状态值（0 正常 1 冻结）</param>
        /// <returns></returns>
        public int NullityAccountInfo(string userlist, int nullity)
        {
            string sqlQuery = $"UPDATE AccountsInfo SET Nullity={nullity} WHERE UserID IN({userlist})";
            return Database.ExecuteNonQuery(sqlQuery);
        }
        /// <summary>
        /// 设置取消机器人
        /// </summary>
        /// <param name="userlist">用户列表</param>
        /// <param name="isAndroid">状态值（0 正常 1 机器人）</param>
        /// <returns></returns>
        public int AndroidAccountInfo(string userlist, int isAndroid)
        {
            string sqlQuery = $"UPDATE AccountsInfo SET IsAndroid={isAndroid} WHERE UserID IN({userlist})";
            return Database.ExecuteNonQuery(sqlQuery);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="accountsInfo">用户信息</param>
        /// <returns></returns>
        public int UpdateAccountsBaseInfo(AccountsInfo accountsInfo)
        {
            string sqlQuery = @"UPDATE AccountsInfo SET UnderWrite=@UnderWrite,PassPortID=@PassPortID,
                Compellation=@Compellation,Nullity=@Nullity,StunDown=@StunDown,MoorMachine=@MoorMachine,
                IsAndroid=@IsAndroid WHERE UserID=@UserID";

            List<DbParameter> prams = new List<DbParameter>
            {
                Database.MakeInParam("UnderWrite", accountsInfo.UnderWrite),
                Database.MakeInParam("PassPortID", accountsInfo.PassPortID),
                Database.MakeInParam("Compellation", accountsInfo.Compellation),
                Database.MakeInParam("Nullity", accountsInfo.Nullity),
                Database.MakeInParam("StunDown", accountsInfo.StunDown),
                Database.MakeInParam("MoorMachine", accountsInfo.MoorMachine),
                Database.MakeInParam("IsAndroid", accountsInfo.IsAndroid),
                Database.MakeInParam("UserID", accountsInfo.UserID)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }
        /// <summary>
        /// 随机获取10个靓号
        /// </summary>
        /// <returns></returns>
        public DataSet GetReserveIdentifierList()
        {
            string sqlQuery = @"SELECT TOP 1 GameID FROM ReserveIdentifier WITH(NOLOCK) WHERE Distribute=0 ORDER BY NEWID()";
            return Database.ExecuteDataset(CommandType.Text, sqlQuery);
        }
        /// <summary>
        /// 获取用户头像地址
        /// </summary>
        /// <param name="customid">头像标识</param>
        /// <returns></returns>
        public string GetAccountsFaceById(int customid)
        {
            string sqlQuery = $"SELECT FaceUrl FROM AccountsFace WITH(NOLOCK) WHERE ID={customid}";
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery);
            return obj?.ToString() ?? "";
        }
        /// <summary>
        /// 批量设置取消转账权限
        /// </summary>
        /// <param name="userlist">用户列表</param>
        /// <param name="userRight">权限值</param>
        /// <param name="calc"></param>
        /// <returns></returns>
        public int TransferPowerAccounts(string userlist, int userRight, string calc = "|")
        {
            string sqlQuery = $"UPDATE AccountsInfo SET UserRight= UserRight {calc} {userRight} WHERE UserID IN({userlist})";
            return Database.ExecuteNonQuery(sqlQuery);
        }

        /// <summary>
        /// 全部设置取消转账权限
        /// </summary>
        /// <param name="userRight">权限值</param>
        /// <param name="calc"></param>
        /// <returns></returns>
        public int TransferPowerAccounts(int userRight,string calc = "|")
        {
            string sqlQuery = $"UPDATE AccountsInfo SET UserRight=UserRight {calc} {userRight}";
            return Database.ExecuteNonQuery(sqlQuery);
        }

        /// <summary>
        /// 添加超端管理员
        /// </summary>
        /// <param name="user">用户信息（用户名、登录密码）</param>
        /// <returns></returns>
        public Message InsertSuperUser(AccountsInfo user)
        {
            var prams = new List<DbParameter>
            {
                Database.MakeInParam("strAccounts", user.Accounts),
                Database.MakeInParam("strLogonPass", user.LogonPass),
                Database.MakeInParam("dwGrantGold",user.UserRight),
                Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
            };

            return MessageHelper.GetMessage(Database, "NET_PM_AddSuperUser", prams);
        }
        #endregion

        #region 限制管理
        /// <summary>
        /// 获取限制地址信息
        /// </summary>
        /// <param name="strAddress">限制地址</param>
        /// <returns></returns>
        public ConfineAddress GetConfineAddressByAddress(string strAddress)
        {
            string sqlQuery = "SELECT * FROM ConfineAddress WITH(NOLOCK) WHERE AddrString=@AddrString";
            var prams = new List<DbParameter> {Database.MakeInParam("AddrString", strAddress)};
            return Database.ExecuteObject<ConfineAddress>(sqlQuery, prams);
        }
        /// <summary>
        /// 新增限制地址信息
        /// </summary>
        /// <param name="address">地址信息</param>
        /// <returns></returns>
        public int InsertConfineAddress(ConfineAddress address)
        {
            string sqlQuery = "INSERT INTO ConfineAddress VALUES(@AddrString,@EnjoinLogon,@EnjoinRegister,@EnjoinOverDate,@CollectDate,@CollectNote)";
            var prams = new List<DbParameter>
            {
                Database.MakeInParam("AddrString", address.AddrString),
                Database.MakeInParam("EnjoinLogon", address.EnjoinLogon),
                Database.MakeInParam("EnjoinRegister", address.EnjoinRegister),
                Database.MakeInParam("EnjoinOverDate",
                    address.EnjoinOverDate.Equals(Convert.ToDateTime("1900-01-01"))
                        ? null
                        : address.EnjoinOverDate.ToString()),
                Database.MakeInParam("CollectDate", DateTime.Now),
                Database.MakeInParam("CollectNote", address.CollectNote)
            };
            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }
        /// <summary>
        ///  更新限制地址信息
        /// </summary>
        /// <param name="address">地址信息</param>
        /// <returns></returns>
        public int UpdateConfineAddress(ConfineAddress address)
        {
            string sqlQuery = @"UPDATE ConfineAddress SET EnjoinLogon=@EnjoinLogon,EnjoinRegister=@EnjoinRegister,
                    EnjoinOverDate=@EnjoinOverDate,CollectNote=@CollectNote WHERE AddrString=@AddrString";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("EnjoinLogon", address.EnjoinLogon),
                Database.MakeInParam("EnjoinRegister", address.EnjoinRegister),
                Database.MakeInParam("EnjoinOverDate",
                    address.EnjoinOverDate.Equals(Convert.ToDateTime("1900-01-01"))
                        ? null
                        : address.EnjoinOverDate.ToString()),
                Database.MakeInParam("CollectNote", address.CollectNote),
                Database.MakeInParam("AddrString", address.AddrString)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }
        /// <summary>
        /// 删除限制地址信息
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public int DeleteConfineAddress(string where)
        {
            string sqlQuery = $"DELETE ConfineAddress {where}";
            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery);
        }
        /// <summary>
        /// 获取IP注册前100名
        /// </summary>
        /// <returns></returns>
        public DataSet GetIpRegisterTop100()
        {
            return Database.ExecuteDataset(CommandType.StoredProcedure, "WSP_PM_GetIPRegisterTop100");
        }
        /// <summary>
        /// 批量插入限制IP
        /// </summary>
        /// <param name="ipList">ip列表</param>
        /// <returns></returns>
        public void BatchInsertConfineAddress(string ipList)
        {
            var prams = new List<DbParameter> {Database.MakeInParam("strIpList", ipList)};
            Database.ExecuteNonQuery(CommandType.StoredProcedure, "NET_PM_BatchInsertConfineAddress", prams.ToArray());
        }
        /// <summary>
        /// 获取限制机器码信息
        /// </summary>
        /// <param name="strSerial">限制机器码</param>
        /// <returns></returns>
        public ConfineMachine GetConfineMachineBySerial(string strSerial)
        {
            string sqlQuery = "SELECT * FROM ConfineMachine WITH(NOLOCK) WHERE MachineSerial=@MachineSerial";
            var prams = new List<DbParameter> {Database.MakeInParam("MachineSerial", strSerial)};
            return Database.ExecuteObject<ConfineMachine>(sqlQuery, prams);
        }
        /// <summary>
        /// 新增限制机器码信息
        /// </summary>
        /// <param name="machine">机器码信息</param>
        /// <returns></returns>
        public int InsertConfineMachine(ConfineMachine machine)
        {
            string sqlQuery = "INSERT INTO ConfineMachine VALUES(@MachineSerial,@EnjoinLogon,@EnjoinRegister,@EnjoinOverDate,@CollectDate,@CollectNote)";
            var prams = new List<DbParameter>
            {
                Database.MakeInParam("MachineSerial", machine.MachineSerial),
                Database.MakeInParam("EnjoinLogon", machine.EnjoinLogon),
                Database.MakeInParam("EnjoinRegister", machine.EnjoinRegister),
                Database.MakeInParam("EnjoinOverDate",
                    machine.EnjoinOverDate.Equals(Convert.ToDateTime("1900-01-01"))
                        ? null
                        : machine.EnjoinOverDate.ToString()),
                Database.MakeInParam("CollectDate", DateTime.Now),
                Database.MakeInParam("CollectNote", machine.CollectNote)
            };
            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }
        /// <summary>
        /// 修改限制机器码信息
        /// </summary>
        /// <param name="machine">机器码信息</param>
        /// <returns></returns>
        public int UpdateConfineMachine(ConfineMachine machine)
        {
            string sqlQuery = @"UPDATE ConfineMachine SET EnjoinLogon=@EnjoinLogon,EnjoinRegister=@EnjoinRegister,
                    EnjoinOverDate=@EnjoinOverDate,CollectNote=@CollectNote WHERE MachineSerial=@MachineSerial";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("EnjoinLogon", machine.EnjoinLogon),
                Database.MakeInParam("EnjoinRegister", machine.EnjoinRegister),
                Database.MakeInParam("EnjoinOverDate",
                    machine.EnjoinOverDate.Equals(Convert.ToDateTime("1900-01-01"))
                        ? null
                        : machine.EnjoinOverDate.ToString()),
                Database.MakeInParam("CollectNote", machine.CollectNote),
                Database.MakeInParam("MachineSerial", machine.MachineSerial)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }
        /// <summary>
        /// 删除限制机器码信息
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public int DeleteConfineMachine(string where)
        {
            string sqlQuery = $"DELETE ConfineMachine {where}";
            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery);
        }
        /// <summary>
        /// 机器码注册前100名
        /// </summary>
        /// <returns></returns>
        public DataSet GetMachineRegisterTop100()
        {
            return Database.ExecuteDataset(CommandType.StoredProcedure, "WSP_PM_GetMachineRegisterTop100");
        }
        /// <summary>
        /// 批量插入限制机器码
        /// </summary>
        /// <param name="machineList">机器码列表</param>
        /// <returns></returns>
        public void BatchInsertConfineMachine(string machineList)
        {
            var prams = new List<DbParameter> {Database.MakeInParam("strMachineList", machineList)};
            Database.ExecuteNonQuery(CommandType.StoredProcedure, "NET_PM_BatchInsertConfineMachine", prams.ToArray());
        }
        #endregion

        #region 系统配置
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="statusName">配置键值</param>
        /// <returns></returns>
        public SystemStatusInfo GetSystemStatusInfo(string statusName)
        {
            string sqlQuery = @"SELECT * FROM SystemStatusInfo WITH(NOLOCK) WHERE StatusName=@StatusName";
            var prams = new List<DbParameter> {Database.MakeInParam("StatusName", statusName)};
            return Database.ExecuteObject<SystemStatusInfo>(sqlQuery, prams);
        }
        /// <summary>
        /// 修改配置信息
        /// </summary>
        /// <param name="statusinfo">配置信息</param>
        /// <returns></returns>
        public int UpdateSystemStatusInfo(SystemStatusInfo statusinfo)
        {
            string sqlQuery = @"UPDATE SystemStatusInfo SET StatusValue=@StatusValue,StatusString=@StatusString,
                    StatusTip=@StatusTip,StatusDescription=@StatusDescription WHERE StatusName=@StatusName";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("StatusValue", statusinfo.StatusValue),
                Database.MakeInParam("StatusString", statusinfo.StatusString),
                Database.MakeInParam("StatusTip", statusinfo.StatusTip),
                Database.MakeInParam("StatusDescription", statusinfo.StatusDescription),
                Database.MakeInParam("StatusName", statusinfo.StatusName)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }
        #endregion

        #region 代理系统
        /// <summary>
        /// 获取代理信息
        /// </summary>
        /// <param name="agentId">代理标识</param>
        /// <returns></returns>
        public AccountsAgentInfo GetAccountsAgentInfo(int agentId)
        {
            string sql = $"SELECT * FROM AccountsAgentInfo WITH(NOLOCK) WHERE AgentID = {agentId}";
            return Database.ExecuteObject<AccountsAgentInfo>(sql);
        }
        /// <summary>
        /// 添加代理信息
        /// </summary>
        /// <param name="agent">代理信息</param>
        /// <param name="pGameId">父级代理游戏ID</param>
        /// <returns></returns>
        public Message InsertAgentUser(AccountsAgentInfo agent, int pGameId)
        {
            var prams = new List<DbParameter>
            {
                Database.MakeInParam("strAgentDomain", agent.AgentDomain),
                Database.MakeInParam("dwAgentLevel", agent.AgentLevel),
                Database.MakeInParam("strAgentNote", agent.AgentNote),
                Database.MakeInParam("strCompellation", agent.Compellation),
                Database.MakeInParam("strContactAddress", agent.ContactAddress),
                Database.MakeInParam("strContactPhone", agent.ContactPhone),
                Database.MakeInParam("dwUserID", agent.UserID),
                Database.MakeInParam("strQQAccount", agent.QQAccount),
                Database.MakeInParam("strWCNickName", agent.WCNickName),
                Database.MakeInParam("dwParentGameID", pGameId),
                Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
            };


            return MessageHelper.GetMessage(Database, "NET_PM_AddAgentUser", prams);
        }
        /// <summary>
        /// 更新代理基本信息
        /// </summary>
        /// <param name="agent">代理信息</param>
        /// <returns></returns>
        public int UpdateAgentUser(AccountsAgentInfo agent)
        {
            string sqlQuery = @"UPDATE AccountsAgentInfo SET Compellation=@Compellation,QQAccount=@QQAccount,WCNickName=@WCNickName,
            ContactPhone=@ContactPhone,ContactAddress=@ContactAddress,AgentDomain=@AgentDomain,AgentNote=@AgentNote WHERE AgentID=@AgentID";

            var prams = new List<DbParameter>
            {
                Database.MakeInParam("Compellation", agent.Compellation),
                Database.MakeInParam("QQAccount", agent.QQAccount),
                Database.MakeInParam("WCNickName", agent.WCNickName),
                Database.MakeInParam("ContactPhone", agent.ContactPhone),
                Database.MakeInParam("ContactAddress", agent.ContactAddress),
                Database.MakeInParam("AgentDomain", agent.AgentDomain),
                Database.MakeInParam("AgentNote", agent.AgentNote),
                Database.MakeInParam("AgentID", agent.AgentID)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }
        /// <summary>
        /// 冻结解冻代理
        /// </summary>
        /// <param name="strList">代理标识列表</param>
        /// <param name="nullity">代理状态</param>
        /// <returns></returns>
        public int NullityAgentUser(string strList, int nullity)
        {
            string sql = $"UPDATE AccountsAgentInfo SET Nullity={nullity} WHERE AgentID IN({strList})";
            return Database.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 获取注册下线
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <returns></returns>
        public DataSet GetAgentUserUnderRegister(int userid)
        {
            string sql = string.Format("SELECT * FROM WF_GetAgentBelowAccountsRegister({0}) WHERE UserID!={0}", userid);
            return Database.ExecuteDataset(sql);
        }
        #endregion

        #region 消息推送
        /// <summary>
        /// 获取推送消息用户列表
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public IList<AccountsUmeng> GetAccountsUmengList(string where)
        {
            string sql = $"SELECT * FROM AccountsUmeng WITH(NOLOCK) {where}";
            return Database.ExecuteObjectList<AccountsUmeng>(sql);
        }
        /// <summary>
        /// 获取推送消息对象
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <returns></returns>
        public AccountsUmeng GetAccountsUmeng(int userid)
        {
            string sql = $"SELECT * FROM AccountsUmeng WITH(NOLOCK) WHERE UserID={userid}";
            return Database.ExecuteObject<AccountsUmeng>(sql);
        }
        #endregion

        #region 用户统计
        /// <summary>
        /// 获取用户注册每日统计
        /// </summary>
        /// <param name="sDateId">起始时间标识</param>
        /// <param name="eDateId">结束时间标识</param>
        /// <returns></returns>
        public IList<SystemStreamInfo> GetRecordEveryDayRegister(string sDateId, string eDateId)
        {
            string sqlQuery = @"SELECT WebRegisterSuccess,GameRegisterSuccess,CollectDate FROM SystemStreamInfo WITH(NOLOCK) 
                                    WHERE DateID>=@sDateID AND DateID <=@eDateID ORDER BY DateID ASC";
            var prams = new List<DbParameter>
            {
                Database.MakeInParam("sDateID", sDateId),
                Database.MakeInParam("eDateID", eDateId)
            };

            return Database.ExecuteObjectList<SystemStreamInfo>(sqlQuery, prams);
        }
        #endregion
    }
}