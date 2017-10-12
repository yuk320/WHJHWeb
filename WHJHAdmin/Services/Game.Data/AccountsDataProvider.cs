using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Game.Entity.Accounts;
using Game.IData;
using Game.Kernel;
using Game.Entity.Record;

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
        /// <param name="userID">用户标识</param>
        /// <returns></returns>
        public AccountsInfo GetAccountInfoByUserID(int userID)
        {
            string sqlQuery = string.Format("SELECT * FROM AccountsInfo WITH(NOLOCK) WHERE UserID={0}", userID);
            return Database.ExecuteObject<AccountsInfo>(sqlQuery);
        }
        /// <summary>
        /// 根据游戏ID获取用户信息
        /// </summary>
        /// <param name="gameID">游戏ID</param>
        /// <returns></returns>
        public AccountsInfo GetAccountInfoByGameID(int gameID)
        {
            string sqlQuery = string.Format("SELECT * FROM AccountsInfo WITH(NOLOCK) WHERE GameID={0}", gameID);
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
            string sqlQuery = string.Format("UPDATE AccountsInfo SET Nullity={0} WHERE UserID IN({1})", nullity, userlist);
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
            string sqlQuery = string.Format("UPDATE AccountsInfo SET IsAndroid={0} WHERE UserID IN({1})", isAndroid, userlist);
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

            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("UnderWrite", accountsInfo.UnderWrite));
            prams.Add(Database.MakeInParam("PassPortID", accountsInfo.PassPortID));
            prams.Add(Database.MakeInParam("Compellation", accountsInfo.Compellation));
            prams.Add(Database.MakeInParam("Nullity", accountsInfo.Nullity));
            prams.Add(Database.MakeInParam("StunDown", accountsInfo.StunDown));
            prams.Add(Database.MakeInParam("MoorMachine", accountsInfo.MoorMachine));
            prams.Add(Database.MakeInParam("IsAndroid", accountsInfo.IsAndroid));
            prams.Add(Database.MakeInParam("UserID", accountsInfo.UserID));

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }
        /// <summary>
        /// 随机获取10个靓号
        /// </summary>
        /// <returns></returns>
        public DataSet GetReserveIdentifierList()
        {
            string sqlQuery = @"SELECT TOP 10 GameID FROM ReserveIdentifier WITH(NOLOCK) WHERE Distribute=0 ORDER BY NEWID()";
            return Database.ExecuteDataset(CommandType.Text, sqlQuery);
        }
        /// <summary>
        /// 获取用户头像地址
        /// </summary>
        /// <param name="customid">头像标识</param>
        /// <returns></returns>
        public string GetAccountsFaceByID(int customid)
        {
            string sqlQuery = string.Format("SELECT FaceUrl FROM AccountsFace WITH(NOLOCK) WHERE ID={0}", customid);
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery);
            return obj != null ? obj.ToString() : "";
        }
        /// <summary>
        /// 批量设置取消转账权限
        /// </summary>
        /// <param name="userlist">用户列表</param>
        /// <param name="userRight">权限值</param>
        /// <returns></returns>
        public int TransferPowerAccounts(string userlist, int userRight)
        {
            string sqlQuery = string.Format("UPDATE AccountsInfo SET UserRight={0} WHERE UserID IN({1})", userRight, userlist);
            return Database.ExecuteNonQuery(sqlQuery);
        }
        /// <summary>
        /// 全部设置取消转账权限
        /// </summary>
        /// <param name="userRight">权限值</param>
        /// <returns></returns>
        public int TransferPowerAccounts(int userRight)
        {
            string sqlQuery = string.Format("UPDATE AccountsInfo SET UserRight={0}", userRight);
            return Database.ExecuteNonQuery(sqlQuery);
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
            string sqlQuery = string.Format("SELECT * FROM ConfineAddress WITH(NOLOCK) WHERE AddrString=@AddrString");
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("AddrString", strAddress));
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
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("AddrString", address.AddrString));
            prams.Add(Database.MakeInParam("EnjoinLogon", address.EnjoinLogon));
            prams.Add(Database.MakeInParam("EnjoinRegister", address.EnjoinRegister));
            prams.Add(Database.MakeInParam("EnjoinOverDate", address.EnjoinOverDate.Equals(Convert.ToDateTime("1900-01-01")) ? null : address.EnjoinOverDate.ToString()));
            prams.Add(Database.MakeInParam("CollectDate", DateTime.Now));
            prams.Add(Database.MakeInParam("CollectNote", address.CollectNote));
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

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("EnjoinLogon", address.EnjoinLogon));
            prams.Add(Database.MakeInParam("EnjoinRegister", address.EnjoinRegister));
            prams.Add(Database.MakeInParam("EnjoinOverDate", address.EnjoinOverDate.Equals(Convert.ToDateTime("1900-01-01")) ? null : address.EnjoinOverDate.ToString()));
            prams.Add(Database.MakeInParam("CollectNote", address.CollectNote));
            prams.Add(Database.MakeInParam("AddrString", address.AddrString));

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }
        /// <summary>
        /// 删除限制地址信息
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public int DeleteConfineAddress(string where)
        {
            string sqlQuery = string.Format("DELETE ConfineAddress {0}", where);
            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString());
        }
        /// <summary>
        /// 获取IP注册前100名
        /// </summary>
        /// <returns></returns>
        public DataSet GetIPRegisterTop100()
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
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strIpList", ipList));
            Database.ExecuteNonQuery(CommandType.StoredProcedure, "NET_PM_BatchInsertConfineAddress", prams.ToArray());
        }
        /// <summary>
        /// 获取限制机器码信息
        /// </summary>
        /// <param name="strSerial">限制机器码</param>
        /// <returns></returns>
        public ConfineMachine GetConfineMachineBySerial(string strSerial)
        {
            string sqlQuery = string.Format("SELECT * FROM ConfineMachine WITH(NOLOCK) WHERE MachineSerial=@MachineSerial");
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("MachineSerial", strSerial));
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
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("MachineSerial", machine.MachineSerial));
            prams.Add(Database.MakeInParam("EnjoinLogon", machine.EnjoinLogon));
            prams.Add(Database.MakeInParam("EnjoinRegister", machine.EnjoinRegister));
            prams.Add(Database.MakeInParam("EnjoinOverDate", machine.EnjoinOverDate.Equals(Convert.ToDateTime("1900-01-01")) ? null : machine.EnjoinOverDate.ToString()));
            prams.Add(Database.MakeInParam("CollectDate", DateTime.Now));
            prams.Add(Database.MakeInParam("CollectNote", machine.CollectNote));
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

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("EnjoinLogon", machine.EnjoinLogon));
            prams.Add(Database.MakeInParam("EnjoinRegister", machine.EnjoinRegister));
            prams.Add(Database.MakeInParam("EnjoinOverDate", machine.EnjoinOverDate.Equals(Convert.ToDateTime("1900-01-01")) ? null : machine.EnjoinOverDate.ToString()));
            prams.Add(Database.MakeInParam("CollectNote", machine.CollectNote));
            prams.Add(Database.MakeInParam("MachineSerial", machine.MachineSerial));

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }
        /// <summary>
        /// 删除限制机器码信息
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public int DeleteConfineMachine(string where)
        {
            string sqlQuery = string.Format("DELETE ConfineMachine {0}", where);
            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString());
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
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strMachineList", machineList));
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
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("StatusName", statusName));
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

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("StatusValue", statusinfo.StatusValue));
            prams.Add(Database.MakeInParam("StatusString", statusinfo.StatusString));
            prams.Add(Database.MakeInParam("StatusTip", statusinfo.StatusTip));
            prams.Add(Database.MakeInParam("StatusDescription", statusinfo.StatusDescription));
            prams.Add(Database.MakeInParam("StatusName", statusinfo.StatusName));

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }
        #endregion

        #region 代理系统
        /// <summary>
        /// 获取代理信息
        /// </summary>
        /// <param name="agentID">代理标识</param>
        /// <returns></returns>
        public AccountsAgentInfo GetAccountsAgentInfo(int agentID)
        {
            string sql = string.Format("SELECT * FROM AccountsAgentInfo WITH(NOLOCK) WHERE AgentID = {0}", agentID);
            return Database.ExecuteObject<AccountsAgentInfo>(sql);
        }
        /// <summary>
        /// 添加代理信息
        /// </summary>
        /// <param name="agent">代理信息</param>
        /// <param name="pGameID">父级代理游戏ID</param>
        /// <returns></returns>
        public Message InsertAgentUser(AccountsAgentInfo agent, int pGameID)
        {
            var prams = new List<DbParameter>();

            prams.Add(Database.MakeInParam("strAgentDomain", agent.AgentDomain));
            prams.Add(Database.MakeInParam("dwAgentLevel", agent.AgentLevel));
            prams.Add(Database.MakeInParam("strAgentNote", agent.AgentNote));
            prams.Add(Database.MakeInParam("strCompellation", agent.Compellation));
            prams.Add(Database.MakeInParam("strContactAddress", agent.ContactAddress));
            prams.Add(Database.MakeInParam("strContactPhone", agent.ContactPhone));
            prams.Add(Database.MakeInParam("dwUserID", agent.UserID));
            prams.Add(Database.MakeInParam("strQQAccount", agent.QQAccount));
            prams.Add(Database.MakeInParam("strWCNickName", agent.WCNickName));
            prams.Add(Database.MakeInParam("dwParentGameID", pGameID));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

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

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("Compellation", agent.Compellation));
            prams.Add(Database.MakeInParam("QQAccount", agent.QQAccount));
            prams.Add(Database.MakeInParam("WCNickName", agent.WCNickName));
            prams.Add(Database.MakeInParam("ContactPhone", agent.ContactPhone));
            prams.Add(Database.MakeInParam("ContactAddress", agent.ContactAddress));
            prams.Add(Database.MakeInParam("AgentDomain", agent.AgentDomain));
            prams.Add(Database.MakeInParam("AgentNote", agent.AgentNote));
            prams.Add(Database.MakeInParam("AgentID", agent.AgentID));

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }
        /// <summary>
        /// 冻结解冻代理
        /// </summary>
        /// <param name="strList">代理标识列表</param>
        /// <param name="nullity">代理状态</param>
        /// <returns></returns>
        public int NullityAgentUser(string strList, int nullity)
        {
            string sql = string.Format("UPDATE AccountsAgentInfo SET Nullity={0} WHERE AgentID IN({1})", nullity, strList);
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
            string sql = string.Format("SELECT * FROM AccountsUmeng WITH(NOLOCK) {0}", where);
            return Database.ExecuteObjectList<AccountsUmeng>(sql);
        }
        /// <summary>
        /// 获取推送消息对象
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <returns></returns>
        public AccountsUmeng GetAccountsUmeng(int userid)
        {
            string sql = string.Format("SELECT * FROM AccountsUmeng WITH(NOLOCK) WHERE UserID={0}", userid);
            return Database.ExecuteObject<AccountsUmeng>(sql);
        }
        #endregion

        #region 用户统计
        /// <summary>
        /// 获取用户注册每日统计
        /// </summary>
        /// <param name="sDateID">起始时间标识</param>
        /// <param name="eDateID">结束时间标识</param>
        /// <returns></returns>
        public IList<SystemStreamInfo> GetRecordEveryDayRegister(string sDateID, string eDateID)
        {
            string sqlQuery = @"SELECT WebRegisterSuccess,GameRegisterSuccess,CollectDate FROM SystemStreamInfo WITH(NOLOCK) 
                                    WHERE DateID>=@sDateID AND DateID <=@eDateID ORDER BY DateID ASC";
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("sDateID", sDateID));
            prams.Add(Database.MakeInParam("eDateID", eDateID));

            return Database.ExecuteObjectList<SystemStreamInfo>(sqlQuery, prams);
        }
        #endregion
    }
}