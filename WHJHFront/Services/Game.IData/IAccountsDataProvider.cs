using System.Collections.Generic;

using Game.Kernel;
using System.Data;
using Game.Entity.Accounts;
// ReSharper disable InconsistentNaming

namespace Game.IData
{
    /// <summary>
    ///  用户库数据层接口
    /// </summary>
    public interface IAccountsDataProvider//:IProvider
    {
        #region 系统配置
        /// <summary>
        /// 获取系统配置信息
        /// </summary>
        /// <param name="key">配置键值</param>
        /// <returns></returns>
        SystemStatusInfo GetSystemStatusInfo(string key);
        #endregion

        #region 用户信息
        /// <summary>
        /// 代理用户登录（微信）
        /// </summary>
        /// <param name="unionid">微信标识</param>
        /// <param name="ip">登录ip</param>
        /// <returns></returns>
        Message WXLogin(string unionid, string ip);
        /// <summary>
        /// 代理用户安全密码登录
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="pass"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        Message AgentMobileLogin(string mobile, string pass, string ip);
        /// <summary>
        /// 根据用户id获取用户信息
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <returns></returns>
        AccountsInfo GetAccountsInfoByUserID(int userid);
        /// <summary>
        /// 根据游戏id获取用户信息
        /// </summary>
        /// <param name="gameid">游戏id</param>
        /// <returns></returns>
        AccountsInfo GetAccountsInfoByGameID(int gameid);
        /// <summary>
        /// 根据微信标识获取用户信息
        /// </summary>
        /// <param name="useruin">微信标识</param>
        /// <returns></returns>
        AccountsInfo GetAccountsInfoByUserUin(string useruin);

        /// <summary>
        /// 用户注册（微信）
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="registerType">注册类型</param>
        /// <param name="faceUrl"></param>
        /// <returns></returns>
        Message RegisterWX(UserInfo user, int registerType, string faceUrl);

        /// <summary>
        /// 获取用户最后登录地址
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <returns>LastLogonIP</returns>
        string GetUserIP(int userid);
        #endregion

        #region 代理信息
        /// <summary>
        /// 根据代理域名获取游戏ID
        /// </summary>
        /// <param name="domain">代理域名</param>
        /// <returns></returns>
        int GetGameIDByAgentDomain(string domain);
        /// <summary>
        /// 根据代理标识获取代理信息
        /// </summary>
        /// <param name="agentid">代理标识</param>
        /// <returns></returns>
        AccountsAgentInfo GetAccountsAgentInfoByAgentID(int agentid);
        /// <summary>
        /// 添加代理信息
        /// </summary>
        /// <param name="userid">父级代理用户标识</param>
        /// <param name="agent">代理信息</param>
        /// <param name="gameID">添加用户的游戏id</param>
        /// <returns></returns>
        Message InsertAgentUser(int userid, AccountsAgentInfo agent, int gameID);
        /// <summary>
        /// 设置代理安全密码
        /// </summary>
        /// <param name="agentid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        int SetAgentSafePassword(int agentid, string password);
        /// <summary>
        /// 修改代理信息
        /// </summary>
        /// <returns></returns>
        int UpdateAgentInfo(AccountsAgentInfo agent);
        /// <summary>
        /// 获取代理推广人列表
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        DataSet GetAgentSpreadList(int userid, int pageIndex, int pageSize);
        /// <summary>
        /// 获取代理推广人总数
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        long GetAgentSpreadCount(int userid);
        /// <summary>
        /// 新增代理下线
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <param name="gameid">游戏id</param>
        /// <returns></returns>
        Message InsertAgentSpread(int userid, int gameid);

        /// <summary>
        /// 返回代理直属下线总数
        /// </summary>
        /// <param name="userId">代理UserID</param>
        /// <returns></returns>
        int GetAgentBelowAgentCount(int userId);

        /// <summary>
        /// 返回代理直属下属列表
        /// </summary>
        /// <param name="userId">代理UserID</param>
        /// <returns></returns>
        IList<AccountsAgentInfo> GetAgentBelowAgentList(int userId);
        #endregion

        #region 推广中心

        /// <summary>
        /// 推广中心首页数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        DataSet GetUserSpreadHomeDataSet(int userId);

        #endregion

        #region 手机登录信息
        /// <summary>
        /// 获取手机登录信息
        /// </summary>
        /// <param name="userid">登录用户标识</param>
        /// <returns></returns>
        DataSet GetMobileLoginLaterData(int userid);
        #endregion

        #region 自定义头像
        /// <summary>
        /// 获取自定义头像
        /// </summary>
        /// <param name="customId">自定义头像标识</param>
        /// <returns></returns>
        AccountsFace GetAccountsFace(int customId);
        #endregion
    }
}
