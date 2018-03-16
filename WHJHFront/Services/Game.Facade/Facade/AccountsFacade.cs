using System.Collections.Generic;
using Game.Data.Factory;
using Game.IData;
using Game.Kernel;
using System.Data;
using Game.Entity.Accounts;
// ReSharper disable InconsistentNaming

// ReSharper disable once CheckNamespace
namespace Game.Facade
{
    /// <summary>
    /// 用户外观
    /// </summary>
    public class AccountsFacade
    {
        #region Fields

        private readonly IAccountsDataProvider accountsData;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AccountsFacade()
        {
            accountsData = ClassFactory.GetIAccountsDataProvider();
        }
        #endregion

        #region 系统配置
        /// <summary>
        /// 获取系统配置信息
        /// </summary>
        /// <param name="key">配置键值</param>
        /// <returns></returns>
        public SystemStatusInfo GetSystemStatusInfo(string key)
        {
            return accountsData.GetSystemStatusInfo(key);
        }
        #endregion

        #region 用户信息
        /// <summary>
        /// 代理用户登录（微信）
        /// </summary>
        /// <param name="unionid">微信标识</param>
        /// <param name="ip">登录ip</param>
        /// <returns></returns>
        public Message WXLogin(string unionid, string ip)
        {
            return accountsData.WXLogin(unionid, ip);
        }
        /// <summary>
        /// 代理用户安全密码登录
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="pass"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public Message AgentMobileLogin(string mobile, string pass, string ip)
        {
            return accountsData.AgentMobileLogin(mobile, pass, ip);
        }
        /// <summary>
        /// 根据用户id获取用户信息
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <returns></returns>
        public AccountsInfo GetAccountsInfoByUserID(int userid)
        {
            return accountsData.GetAccountsInfoByUserID(userid);
        }
        /// <summary>
        /// 根据用户id获取游戏id
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int GetGameIDByUserID(int userid)
        {
            return GetAccountsInfoByUserID(userid)?.GameID??0;
        }
        /// <summary>
        /// 根据用户id获取用户昵称
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetNickNameByUserID(int userid)
        {
            return GetAccountsInfoByUserID(userid)?.NickName ?? "";
        }
        /// <summary>
        /// 根据游戏id获取用户信息
        /// </summary>
        /// <param name="gameid">游戏id</param>
        /// <returns></returns>
        public AccountsInfo GetAccountsInfoByGameID(int gameid)
        {
            return accountsData.GetAccountsInfoByGameID(gameid);
        }
        /// <summary>
        /// 根据微信标识获取用户信息
        /// </summary>
        /// <param name="useruin">微信标识</param>
        /// <returns></returns>
        public AccountsInfo GetAccountsInfoByUserUin(string useruin)
        {
            return accountsData.GetAccountsInfoByUserUin(useruin);
        }

        /// <summary>
        /// 用户注册（微信）
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="registerType">注册类型</param>
        /// <param name="faceUrl"></param>
        /// <returns></returns>
        public Message RegisterWX(UserInfo user, int registerType, string faceUrl)
        {
            return accountsData.RegisterWX(user, registerType, faceUrl);
        }

        /// <summary>
        /// 获取用户最后登录地址
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <returns>LastLogonIP</returns>
        public string GetUserIP(int userid)
        {
            return accountsData.GetUserIP(userid);
        }
        #endregion

        #region 代理信息
        /// <summary>
        /// 根据代理域名获取游戏ID
        /// </summary>
        /// <param name="domain">代理域名</param>
        /// <returns></returns>
        public int GetGameIDByAgentDomain(string domain)
        {
            return accountsData.GetGameIDByAgentDomain(domain);
        }
        /// <summary>
        /// 根据代理标识获取代理信息
        /// </summary>
        /// <param name="agentid">代理标识</param>
        /// <returns></returns>
        public AccountsAgentInfo GetAccountsAgentInfoByAgentID(int agentid)
        {
            return accountsData.GetAccountsAgentInfoByAgentID(agentid);
        }
        /// <summary>
        /// 添加代理信息
        /// </summary>
        /// <param name="userid">父级代理用户标识</param>
        /// <param name="agent">代理信息</param>
        /// <param name="gameID">添加用户的游戏id</param>
        /// <returns></returns>
        public Message InsertAgentUser(int userid, AccountsAgentInfo agent, int gameID)
        {
            return accountsData.InsertAgentUser(userid, agent, gameID);
        }
        /// <summary>
        /// 设置代理安全密码
        /// </summary>
        /// <param name="agentid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int SetAgentSafePassword(int agentid, string password)
        {
            return accountsData.SetAgentSafePassword(agentid, password);
        }
        /// <summary>
        /// 修改代理信息
        /// </summary>
        /// <returns></returns>
        public int UpdateAgentInfo(AccountsAgentInfo agent)
        {
            return accountsData.UpdateAgentInfo(agent);
        }
        /// <summary>
        /// 获取代理推广人列表
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataSet GetAgentSpreadList(int userid, int pageIndex, int pageSize)
        {
            return accountsData.GetAgentSpreadList(userid, pageIndex, pageSize);
        }
        /// <summary>
        /// 获取代理推广人总数
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public long GetAgentSpreadCount(int userid)
        {
            return accountsData.GetAgentSpreadCount(userid);
        }
        /// <summary>
        /// 新增代理下线
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <param name="gameid">游戏id</param>
        /// <returns></returns>
        public Message InsertAgentSpread(int userid, int gameid)
        {
            return accountsData.InsertAgentSpread(userid, gameid);
        }

        /// <summary>
        /// 返回代理直属下线总数
        /// </summary>
        /// <param name="userId">代理UserID</param>
        /// <returns></returns>
        public int GetAgentBelowAgentCount(int userId)
        {
            return accountsData.GetAgentBelowAgentCount(userId);
        }

        /// <summary>
        /// 返回代理直属下属列表
        /// </summary>
        /// <param name="userId">代理UserID</param>
        /// <returns></returns>
        public IList<AccountsAgentInfo> GetAgentBelowAgentList(int userId)
        {
            return accountsData.GetAgentBelowAgentList(userId);
        }
        #endregion

        #region 推广中心

        /// <summary>
        /// 推广中心首页数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataSet GetUserSpreadHomeDataSet(int userId)
        {
            return accountsData.GetUserSpreadHomeDataSet(userId);
        }

        #endregion

        #region 手机登录信息
        /// <summary>
        /// 获取手机登录信息
        /// </summary>
        /// <param name="userid">登录用户标识</param>
        /// <returns></returns>
        public DataSet GetMobileLoginLaterData(int userid)
        {
            return accountsData.GetMobileLoginLaterData(userid);
        }
        #endregion

        #region 自定义头像
        /// <summary>
        /// 获取自定义头像
        /// </summary>
        /// <param name="customId">自定义头像标识</param>
        /// <returns></returns>
        public AccountsFace GetAccountsFace(int customId)
        {
            return accountsData.GetAccountsFace(customId);
        }
        #endregion
    }
}
