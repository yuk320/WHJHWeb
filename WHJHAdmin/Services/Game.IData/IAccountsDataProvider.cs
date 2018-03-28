using System.Data;
using Game.Entity.Accounts;
using Game.Kernel;
using System.Collections.Generic;

namespace Game.IData
{
    /// <summary>
    /// 帐号库数据层接口
    /// </summary>
    public interface IAccountsDataProvider
    {
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
        #endregion 公共

        #region 用户信息
        /// <summary>
        /// 根据用户标识获取用户信息
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <returns></returns>
        AccountsInfo GetAccountInfoByUserId(int userId);
        /// <summary>
        /// 根据游戏ID获取用户信息
        /// </summary>
        /// <param name="gameId">游戏ID</param>
        /// <returns></returns>
        AccountsInfo GetAccountInfoByGameId(int gameId);
        /// <summary>
        /// 冻结解冻账号
        /// </summary>
        /// <param name="userlist">用户列表</param>
        /// <param name="nullity">状态值（0 正常 1 冻结）</param>
        /// <returns></returns>
        int NullityAccountInfo(string userlist, int nullity);
        /// <summary>
        /// 设置取消机器人
        /// </summary>
        /// <param name="userlist">用户列表</param>
        /// <param name="isAndroid">状态值（0 正常 1 机器人）</param>
        /// <returns></returns>
        int AndroidAccountInfo(string userlist, int isAndroid);
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="accountsInfo">用户信息</param>
        /// <returns></returns>
        int UpdateAccountsBaseInfo(AccountsInfo accountsInfo);
        /// <summary>
        /// 随机获取10个靓号
        /// </summary>
        /// <returns></returns>
        DataSet GetReserveIdentifierList();
        /// <summary>
        /// 获取用户头像地址
        /// </summary>
        /// <param name="customid">头像标识</param>
        /// <returns></returns>
        string GetAccountsFaceById(int customid);

        /// <summary>
        /// 批量设置取消转账权限
        /// </summary>
        /// <param name="userlist">用户列表</param>
        /// <param name="userRight">权限值</param>
        /// <param name="calc"></param>
        /// <returns></returns>
        int TransferPowerAccounts(string userlist, int userRight, string calc = "|");

        /// <summary>
        /// 全部设置取消转账权限
        /// </summary>
        /// <param name="userRight">权限值</param>
        /// <param name="calc"></param>
        /// <returns></returns>
        int TransferPowerAccounts(int userRight, string calc = "|");

        /// <summary>
        /// 添加超端管理员
        /// </summary>
        /// <param name="user">用户信息（用户名、登录密码）</param>
        /// <returns></returns>
        Message InsertSuperUser(AccountsInfo user);
        #endregion

        #region 限制管理
        /// <summary>
        /// 获取限制地址信息
        /// </summary>
        /// <param name="strAddress">限制地址</param>
        /// <returns></returns>
        ConfineAddress GetConfineAddressByAddress(string strAddress);
        /// <summary>
        /// 新增限制地址信息
        /// </summary>
        /// <param name="address">地址信息</param>
        /// <returns></returns>
        int InsertConfineAddress(ConfineAddress address);
        /// <summary>
        ///  更新限制地址信息
        /// </summary>
        /// <param name="address">地址信息</param>
        /// <returns></returns>
        int UpdateConfineAddress(ConfineAddress address);
        /// <summary>
        /// 删除限制地址信息
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        int DeleteConfineAddress(string where);
        /// <summary>
        /// 获取IP注册前100名
        /// </summary>
        /// <returns></returns>
        DataSet GetIpRegisterTop100();
        /// <summary>
        /// 批量插入限制IP
        /// </summary>
        /// <param name="ipList">ip列表</param>
        /// <returns></returns>
        void BatchInsertConfineAddress(string ipList);
        /// <summary>
        /// 获取限制机器码信息
        /// </summary>
        /// <param name="strSerial">限制机器码</param>
        /// <returns></returns>
        ConfineMachine GetConfineMachineBySerial(string strSerial);
        /// <summary>
        /// 新增限制机器码信息
        /// </summary>
        /// <param name="machine">机器码信息</param>
        /// <returns></returns>
        int InsertConfineMachine(ConfineMachine machine);
        /// <summary>
        /// 修改限制机器码信息
        /// </summary>
        /// <param name="machine">机器码信息</param>
        /// <returns></returns>
        int UpdateConfineMachine(ConfineMachine machine);
        /// <summary>
        /// 删除限制机器码信息
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        int DeleteConfineMachine(string where);
        /// <summary>
        /// 机器码注册前100名
        /// </summary>
        /// <returns></returns>
        DataSet GetMachineRegisterTop100();
        /// <summary>
        /// 批量插入限制机器码
        /// </summary>
        /// <param name="machineList">机器码列表</param>
        /// <returns></returns>
        void BatchInsertConfineMachine(string machineList);
        #endregion

        #region 系统配置
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="statusName">配置键值</param>
        /// <returns></returns>
        SystemStatusInfo GetSystemStatusInfo(string statusName);
        /// <summary>
        /// 修改配置信息
        /// </summary>
        /// <param name="statusinfo">配置信息</param>
        /// <returns></returns>
        int UpdateSystemStatusInfo(SystemStatusInfo statusinfo);
        #endregion

        #region 代理系统
        /// <summary>
        /// 获取代理信息
        /// </summary>
        /// <param name="agentId">代理标识</param>
        /// <returns></returns>
        AccountsAgentInfo GetAccountsAgentInfo(int agentId);
        /// <summary>
        /// 添加代理信息
        /// </summary>
        /// <param name="agent">代理信息</param>
        /// <param name="pGameId">父级代理游戏ID</param>
        /// <returns></returns>
        Message InsertAgentUser(AccountsAgentInfo agent, int pGameId);
        /// <summary>
        /// 更新代理基本信息
        /// </summary>
        /// <param name="agent">代理信息</param>
        /// <returns></returns>
        int UpdateAgentUser(AccountsAgentInfo agent);
        /// <summary>
        /// 冻结解冻代理
        /// </summary>
        /// <param name="strList">代理标识列表</param>
        /// <param name="nullity">代理状态</param>
        /// <returns></returns>
        int NullityAgentUser(string strList, int nullity);
        /// <summary>
        /// 获取注册下线
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <returns></returns>
        DataSet GetAgentUserUnderRegister(int userid);
        #endregion

        #region 消息推送
        /// <summary>
        /// 获取推送消息用户列表
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        IList<AccountsUmeng> GetAccountsUmengList(string where);
        /// <summary>
        /// 获取推送消息对象
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <returns></returns>
        AccountsUmeng GetAccountsUmeng(int userid);
        #endregion

        #region 用户统计
        /// <summary>
        /// 获取用户注册每日统计
        /// </summary>
        /// <param name="sDateId">起始时间标识</param>
        /// <param name="eDateId">结束时间标识</param>
        /// <returns></returns>
        IList<SystemStreamInfo> GetRecordEveryDayRegister(string sDateId, string eDateId);
        #endregion
    }
}