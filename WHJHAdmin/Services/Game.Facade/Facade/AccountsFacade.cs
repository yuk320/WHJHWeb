using System.Data;
using Game.Data.Factory;
using Game.Entity.Accounts;
using Game.IData;
using Game.Kernel;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Game.Facade
{
    /// <summary>
    /// 帐号库外观
    /// </summary>
    public class AccountsFacade
    {
        #region Fields

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        // ReSharper disable once InconsistentNaming
        private IAccountsDataProvider aideAccountsData;

        #endregion Fields

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AccountsFacade()
        {
            aideAccountsData = ClassFactory.GetAccountsDataProvider();
        }

        #endregion 构造函数

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
            return aideAccountsData.GetList(tableName, pageIndex, pageSize, condition, orderby);
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
            return aideAccountsData.GetAccountInfoByUserId(userId);
        }
        /// <summary>
        /// 根据游戏ID获取用户信息
        /// </summary>
        /// <param name="gameId">游戏ID</param>
        /// <returns></returns>
        public AccountsInfo GetAccountInfoByGameId(int gameId)
        {
            return aideAccountsData.GetAccountInfoByGameId(gameId);
        }
        /// <summary>
        /// 冻结解冻账号
        /// </summary>
        /// <param name="userlist">用户列表</param>
        /// <param name="nullity">状态值（0 正常 1 冻结）</param>
        /// <returns></returns>
        public int NullityAccountInfo(string userlist, int nullity)
        {
            return aideAccountsData.NullityAccountInfo(userlist, nullity);
        }
        /// <summary>
        /// 设置取消机器人
        /// </summary>
        /// <param name="userlist">用户列表</param>
        /// <param name="isAndroid">状态值（0 正常 1 机器人）</param>
        /// <returns></returns>
        public int AndroidAccountInfo(string userlist, int isAndroid)
        {
            return aideAccountsData.AndroidAccountInfo(userlist, isAndroid);
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="accountsInfo">用户信息</param>
        /// <returns></returns>
        public int UpdateAccountsBaseInfo(AccountsInfo accountsInfo)
        {
            return aideAccountsData.UpdateAccountsBaseInfo(accountsInfo);
        }
        /// <summary>
        /// 随机获取10个靓号
        /// </summary>
        /// <returns></returns>
        public DataSet GetReserveIdentifierList()
        {
            return aideAccountsData.GetReserveIdentifierList();
        }

        /// <summary>
        /// 批量设置取消转账权限
        /// </summary>
        /// <param name="userlist">用户列表</param>
        /// <param name="userRight">权限值</param>
        /// <param name="calc"></param>
        /// <returns></returns>
        public int TransferPowerAccounts(string userlist, int userRight,string calc="|")
        {
            return aideAccountsData.TransferPowerAccounts(userlist, userRight, calc);
        }

        /// <summary>
        /// 全部设置取消转账权限
        /// </summary>
        /// <param name="userRight">权限值</param>
        /// <param name="calc"></param>
        /// <returns></returns>
        public int TransferPowerAccounts(int userRight, string calc = "|")
        {
            return aideAccountsData.TransferPowerAccounts(userRight, calc);
        }

        /// <summary>
        /// 添加超端管理员
        /// </summary>
        /// <param name="user">用户信息（用户名、登录密码）</param>
        /// <returns></returns>
        public Message InsertSuperUser(AccountsInfo user)
        {
            return aideAccountsData.InsertSuperUser(user);
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
            return aideAccountsData.GetConfineAddressByAddress(strAddress);
        }
        /// <summary>
        /// 新增限制地址信息
        /// </summary>
        /// <param name="address">地址信息</param>
        /// <returns></returns>
        public int InsertConfineAddress(ConfineAddress address)
        {
            return aideAccountsData.InsertConfineAddress(address);
        }
        /// <summary>
        ///  更新限制地址信息
        /// </summary>
        /// <param name="address">地址信息</param>
        /// <returns></returns>
        public int UpdateConfineAddress(ConfineAddress address)
        {
            return aideAccountsData.UpdateConfineAddress(address);
        }
        /// <summary>
        /// 删除限制地址信息
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public int DeleteConfineAddress(string where)
        {
            return aideAccountsData.DeleteConfineAddress(where);
        }
        /// <summary>
        /// 获取IP注册前100名
        /// </summary>
        /// <returns></returns>
        public DataSet GetIpRegisterTop100()
        {
            return aideAccountsData.GetIpRegisterTop100();
        }
        /// <summary>
        /// 批量插入限制IP
        /// </summary>
        /// <param name="ipList">ip列表</param>
        /// <returns></returns>
        public void BatchInsertConfineAddress(string ipList)
        {
            aideAccountsData.BatchInsertConfineAddress(ipList);
        }
        /// <summary>
        /// 获取限制机器码信息
        /// </summary>
        /// <param name="strSerial">限制机器码</param>
        /// <returns></returns>
        public ConfineMachine GetConfineMachineBySerial(string strSerial)
        {
            return aideAccountsData.GetConfineMachineBySerial(strSerial);
        }
        /// <summary>
        /// 新增限制机器码信息
        /// </summary>
        /// <param name="machine">机器码信息</param>
        /// <returns></returns>
        public int InsertConfineMachine(ConfineMachine machine)
        {
            return aideAccountsData.InsertConfineMachine(machine);
        }
        /// <summary>
        /// 修改限制机器码信息
        /// </summary>
        /// <param name="machine">机器码信息</param>
        /// <returns></returns>
        public int UpdateConfineMachine(ConfineMachine machine)
        {
            return aideAccountsData.UpdateConfineMachine(machine);
        }
        /// <summary>
        /// 删除限制机器码信息
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public int DeleteConfineMachine(string where)
        {
            return aideAccountsData.DeleteConfineMachine(where);
        }
        /// <summary>
        /// 机器码注册前100名
        /// </summary>
        /// <returns></returns>
        public DataSet GetMachineRegisterTop100()
        {
            return aideAccountsData.GetMachineRegisterTop100();
        }
        /// <summary>
        /// 批量插入限制机器码
        /// </summary>
        /// <param name="machineList">机器码列表</param>
        /// <returns></returns>
        public void BatchInsertConfineMachine(string machineList)
        {
            aideAccountsData.BatchInsertConfineMachine(machineList);
        }
        /// <summary>
        /// 获取用户头像地址
        /// </summary>
        /// <param name="customid">头像标识</param>
        /// <returns></returns>
        public string GetAccountsFaceById(int customid)
        {
            return aideAccountsData.GetAccountsFaceById(customid);
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
            return aideAccountsData.GetSystemStatusInfo(statusName);
        }
        /// <summary>
        /// 修改配置信息
        /// </summary>
        /// <param name="statusinfo">配置信息</param>
        /// <returns></returns>
        public int UpdateSystemStatusInfo(SystemStatusInfo statusinfo)
        {
            return aideAccountsData.UpdateSystemStatusInfo(statusinfo);
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
            return aideAccountsData.GetAccountsAgentInfo(agentId);
        }
        /// <summary>
        /// 添加代理信息
        /// </summary>
        /// <param name="agent">代理信息</param>
        /// <param name="pGameId">父级代理游戏ID</param>
        /// <returns></returns>
        public Message InsertAgentUser(AccountsAgentInfo agent, int pGameId)
        {
            return aideAccountsData.InsertAgentUser(agent, pGameId);
        }
        /// <summary>
        /// 更新代理基本信息
        /// </summary>
        /// <param name="agent">代理信息</param>
        /// <returns></returns>
        public int UpdateAgentUser(AccountsAgentInfo agent)
        {
            return aideAccountsData.UpdateAgentUser(agent);
        }
        /// <summary>
        /// 冻结解冻代理
        /// </summary>
        /// <param name="strList">代理标识列表</param>
        /// <param name="nullity">代理状态</param>
        /// <returns></returns>
        public int NullityAgentUser(string strList, int nullity)
        {
            return aideAccountsData.NullityAgentUser(strList, nullity);
        }
        /// <summary>
        /// 获取注册下线
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <returns></returns>
        public DataSet GetAgentUserUnderRegister(int userid)
        {
            return aideAccountsData.GetAgentUserUnderRegister(userid);
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
            return aideAccountsData.GetAccountsUmengList(where);
        }
        /// <summary>
        /// 获取推送消息对象
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <returns></returns>
        public AccountsUmeng GetAccountsUmeng(int userid)
        {
            return aideAccountsData.GetAccountsUmeng(userid);
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
            return aideAccountsData.GetRecordEveryDayRegister(sDateId, eDateId);
        }
        #endregion
    }
}