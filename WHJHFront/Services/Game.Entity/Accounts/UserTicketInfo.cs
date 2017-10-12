using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Game.Entity.Accounts
{
    /// <summary>
    /// 用户票证信息
    /// </summary>
    [Serializable]
    public class UserTicketInfo
    {
        #region Fields

        private int m_userID;									//用户标识
        private int m_gameID;                                   //游戏标识
        private int m_agentID;                                  //代理标识
        private string m_accounts;  		                    //登录帐号
        private string m_logonPass;				                //用户密码

        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化用户票证对象实例
        /// </summary>
        public UserTicketInfo()
        {
            m_userID = 0;
            m_gameID = 0;
            m_agentID = 0;
            m_accounts = "";
            m_logonPass = "";
        }

        /// <summary>
        /// 初始化用户票证对象实例
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="gameID"></param>
        /// <param name="agentID"></param>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        public UserTicketInfo(int userID, int gameID, int agentID, string accounts, string logonPass)
        {
            m_gameID = gameID;
            m_userID = userID;
            m_agentID = agentID;
            m_accounts = accounts;
            m_logonPass = logonPass;
        }
        #endregion

        #region 公开属性
        /// <summary>
        /// 用户标识
        /// </summary>		
        public int UserID
        {
            get { return m_userID; }
            set { m_userID = value; }
        }

        /// <summary>
        /// 游戏标识
        /// </summary>
        public int GameID
        {
            get { return m_gameID; }
            set { m_gameID = value; }
        }

        /// <summary>
        /// 代理标识
        /// </summary>
        public int AgentID
        {
            get { return m_agentID; }
            set { m_agentID = value; }
        }

        /// <summary>
        /// 登录帐号
        /// </summary>		
        public virtual string Accounts
        {
            get { return m_accounts; }
            set { m_accounts = value; }
        }

        /// <summary>
        /// 登录密码
        /// </summary>		
        public virtual string LogonPass
        {
            get { return m_logonPass; }
            set { m_logonPass = value; }
        }
        #endregion

        #region 公开方法

        /// <summary>
        /// 序列化为Json对象
        /// </summary>
        /// <returns></returns>
        public string SerializeText()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// 反序列化Json对象
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static UserTicketInfo DeserializeObject(string jsonText)
        {
            UserTicketInfo userTicket = JsonConvert.DeserializeObject<UserTicketInfo>(jsonText);
            return userTicket;
        }

        #endregion
    }
}
