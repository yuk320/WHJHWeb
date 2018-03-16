using System;
using System.Collections.Generic;
using Game.Kernel;
using Game.IData;
using System.Data.Common;
using System.Data;
using Game.Entity.Accounts;

namespace Game.Data
{
    /// <summary>
    /// 用户数据访问层
    /// </summary>
    public sealed class AccountsDataProvider : BaseDataProvider, IAccountsDataProvider
    {
        #region Fields

        #endregion

        #region 构造方法

        public AccountsDataProvider(string connString)
            : base(connString)
        {
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
            const string sqlQuery =
                @"SELECT StatusValue FROM SystemStatusInfo WITH(NOLOCK) WHERE StatusName = @StatusName";
            List<DbParameter> parms = new List<DbParameter> {Database.MakeInParam("StatusName", key)};
            return Database.ExecuteObject<SystemStatusInfo>(sqlQuery, parms);
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
            List<DbParameter> prams =
                new List<DbParameter>
                {
                    Database.MakeInParam("strUserUin", unionid),
                    Database.MakeInParam("strClientIP", ip),
                    Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
                };

            return MessageHelper.GetMessageForObject<UserInfo>(Database, "NET_PW_AgentAccountsLogin", prams);
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
            List<DbParameter> prams =
                new List<DbParameter>
                {
                    Database.MakeInParam("strMobile", mobile),
                    Database.MakeInParam("strPassword", pass),
                    Database.MakeInParam("strClientIP", ip),
                    Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
                };

            return MessageHelper.GetMessageForObject<UserInfo>(Database, "NET_PW_AgentAccountsLogin_MP", prams);
        }

        /// <summary>
        /// 根据用户id获取用户信息
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <returns></returns>
        public AccountsInfo GetAccountsInfoByUserID(int userid)
        {
            const string sqlQuery =
                @"SELECT UserID,GameID,SpreaderID,NickName,PassPortID,Compellation,FaceID,CustomID,RegisterOrigin,AgentID,RegisterIP,LastLogonIP,UnderWrite,PlaceName,UserUin,AgentID FROM AccountsInfo WITH(NOLOCK) WHERE UserID = @UserID";
            List<DbParameter> parms = new List<DbParameter> {Database.MakeInParam("UserID", userid)};
            return Database.ExecuteObject<AccountsInfo>(sqlQuery, parms);
        }

        /// <summary>
        /// 根据游戏id获取用户信息
        /// </summary>
        /// <param name="gameid">游戏id</param>
        /// <returns></returns>
        public AccountsInfo GetAccountsInfoByGameID(int gameid)
        {
            const string sqlQuery =
                @"SELECT UserID FROM AccountsInfo WITH(NOLOCK) WHERE GameID = @GameID";
            List<DbParameter> parms = new List<DbParameter> {Database.MakeInParam("GameID", gameid)};
            object result = Database.ExecuteScalar(CommandType.Text, sqlQuery, parms.ToArray());
            return result != null ? GetAccountsInfoByUserID(Convert.ToInt32(result)) : null;
        }

        /// <summary>
        /// 根据微信标识获取用户信息
        /// </summary>
        /// <param name="useruin">微信标识</param>
        /// <returns></returns>
        public AccountsInfo GetAccountsInfoByUserUin(string useruin)
        {
            const string sqlQuery =
                @"SELECT UserID FROM AccountsInfo WITH(NOLOCK) WHERE UserUin = @UserUin";
            List<DbParameter> parms = new List<DbParameter> {Database.MakeInParam("UserUin", useruin)};
            object result = Database.ExecuteScalar(CommandType.Text, sqlQuery, parms.ToArray());
            return result != null ? GetAccountsInfoByUserID(Convert.ToInt32(result)) : null;
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
            List<DbParameter> prams = new List<DbParameter>
            {
                Database.MakeInParam("strUserUin", user.UserUin),
                Database.MakeInParam("strNickName", user.NickName),
                Database.MakeInParam("cbGender", user.Gender),
                Database.MakeInParam("strFaceUrl", faceUrl),
                Database.MakeInParam("strSpreader", user.GameID),
                Database.MakeInParam("strClientIP", user.RegisterIP),
                Database.MakeInParam("dwRegisterOrigin", registerType),
                Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
            };

            return MessageHelper.GetMessage(Database, "NET_PW_RegisterAccountsWX", prams);
        }

        /// <summary>
        /// 获取用户最后登录地址
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <returns>LastLogonIP</returns>
        public string GetUserIP(int userid)
        {
            AccountsInfo userInfo = GetAccountsInfoByUserID(userid);
            return userInfo?.LastLogonIP ?? "";
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
            const string sqlQuery =
                @"SELECT GameID FROM AccountsInfo WITH(NOLOCK) WHERE UserID IN(SELECT UserID FROM AccountsAgentInfo WITH(NOLOCK) WHERE AgentDomain=@AgentDomain)";
            List<DbParameter> parms = new List<DbParameter> {Database.MakeInParam("AgentDomain", domain)};
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery, parms.ToArray());
            return obj != null ? Convert.ToInt32(obj) : 0;
        }

        /// <summary>
        /// 根据代理标识获取代理信息
        /// </summary>
        /// <param name="agentid">代理标识</param>
        /// <returns></returns>
        public AccountsAgentInfo GetAccountsAgentInfoByAgentID(int agentid)
        {
            const string sqlQuery = @"SELECT * FROM AccountsAgentInfo WITH(NOLOCK) WHERE AgentID = @AgentID";
            List<DbParameter> parms = new List<DbParameter> {Database.MakeInParam("AgentID", agentid)};
            return Database.ExecuteObject<AccountsAgentInfo>(sqlQuery, parms);
        }

        /// <summary>
        /// 添加代理信息
        /// </summary>
        /// <param name="userid">父级代理用户标识</param>
        /// <param name="agent">代理信息</param>
        /// <param name="gameId">添加用户的游戏id</param>
        /// <returns></returns>
        public Message InsertAgentUser(int userid, AccountsAgentInfo agent, int gameId)
        {
            var prams = new List<DbParameter>
            {
                Database.MakeInParam("dwUserID", userid),
                Database.MakeInParam("dwGameID", gameId),
                Database.MakeInParam("strCompellation", agent.Compellation),
                Database.MakeInParam("strAgentDomain", agent.AgentDomain),
                Database.MakeInParam("strQQAccount", agent.QQAccount),
                Database.MakeInParam("strWCNickName", agent.WCNickName),
                Database.MakeInParam("strContactPhone", agent.ContactPhone),
                Database.MakeInParam("strContactAddress", agent.ContactAddress),
                Database.MakeInParam("strAgentNote", agent.AgentNote),
                Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
            };

            Message msg = MessageHelper.GetMessage(Database, "NET_PM_AddAgentUserByAgent", prams);
            return msg;
        }

        /// <summary>
        /// 设置代理安全密码
        /// </summary>
        /// <param name="agentid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int SetAgentSafePassword(int agentid, string password)
        {
            string sql = "UPDATE AccountsAgentInfo SET [Password]=@Password WHERE AgentID=@AgentID";
            List<DbParameter> prams =
                new List<DbParameter>
                {
                    Database.MakeInParam("Password", password),
                    Database.MakeInParam("AgentID", agentid)
                };
            return Database.ExecuteNonQuery(CommandType.Text, sql, prams.ToArray());
        }

        /// <summary>
        /// 修改代理信息
        /// </summary>
        /// <returns></returns>
        public int UpdateAgentInfo(AccountsAgentInfo agent)
        {
            string sql =
                "UPDATE AccountsAgentInfo SET QQAccount=@QQAccount,ContactPhone=@ContactPhone,ContactAddress=@ContactAddress WHERE UserID=@UserID";

            DbParameter[] param =
            {
                Database.MakeInParam("QQAccount", agent.QQAccount),
                Database.MakeInParam("ContactPhone", agent.ContactPhone),
                Database.MakeInParam("ContactAddress", agent.ContactAddress),
                Database.MakeInParam("UserID", agent.UserID)
            };

            return Database.ExecuteNonQuery(CommandType.Text, sql, param);
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
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            string sql =
                $"SELECT * FROM (SELECT ROW_NUMBER() OVER( ORDER BY RegisterDate DESC) AS RowNumber,* FROM [WF_GetAgentBelowAccountsRegister]({userid}) WHERE UserID<>{userid}) AS A WHERE RowNumber>={start} AND RowNumber<={end}";

            return Database.ExecuteDataset(sql);
        }

        /// <summary>
        /// 获取代理推广人总数
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public long GetAgentSpreadCount(int userid)
        {
            string sql =
                $"SELECT COUNT(UserID) FROM [WF_GetAgentBelowAccountsRegister]({userid}) WHERE UserID!={userid}";
            object obj = Database.ExecuteScalar(CommandType.Text, sql);
            return obj != null ? Convert.ToInt64(obj) : 0;
        }

        /// <summary>
        /// 新增代理下线
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <param name="gameid">游戏id</param>
        /// <returns></returns>
        public Message InsertAgentSpread(int userid, int gameid)
        {
            var prams = new List<DbParameter>
            {
                Database.MakeInParam("dwUserID", userid),
                Database.MakeInParam("dwGameID", gameid),
                Database.MakeOutParam("strErrorDescribe", typeof(string), 127)
            };

            return MessageHelper.GetMessage(Database, "NET_PM_AddAgentSpreadUser", prams);
        }

        /// <summary>
        /// 返回代理直属下线总数
        /// </summary>
        /// <param name="userId">代理UserID</param>
        /// <returns></returns>
        public int GetAgentBelowAgentCount(int userId)
        {
            return GetAgentBelowAgentList(userId)?.Count ?? 0;
        }

        /// <summary>
        /// 返回代理直属下属列表
        /// </summary>
        /// <param name="userId">代理UserID</param>
        /// <returns></returns>
        public IList<AccountsAgentInfo> GetAgentBelowAgentList(int userId)
        {
            return Database.ExecuteObjectList<AccountsAgentInfo>(
                "SELECT * FROM AccountsAgentInfo WHERE ParentAgent IN (SELECT AgentID FROM AccountsAgentInfo(NOLOCK) WHERE UserID = @UserID)",
                new List<DbParameter> {Database.MakeInParam("UserID", userId)});
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
            var param = new List<DbParameter>()
            {
                Database.MakeInParam("dwUserID", userId)
            };
            return Database.ExecuteDataset(CommandType.StoredProcedure, "NET_PW_UserSpreadHome", param.ToArray());
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
            List<DbParameter> prams = new List<DbParameter> {Database.MakeInParam("dwUserID", userid)};

            return Database.ExecuteDataset(CommandType.StoredProcedure, "NET_PW_GetMobileLoginLater", prams.ToArray());
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
            const string sqlQuery = "SELECT UserID,FaceUrl FROM AccountsFace WITH(NOLOCK) WHERE ID=@ID";

            List<DbParameter> prams = new List<DbParameter> {Database.MakeInParam("ID", customId)};

            return Database.ExecuteObject<AccountsFace>(sqlQuery, prams);
        }

        #endregion
    }
}
