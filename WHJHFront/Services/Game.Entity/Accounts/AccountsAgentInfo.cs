/*
 * 版本： 4.0
 * 日期：2017/6/12 14:56:33
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Accounts
{
    /// <summary>
    /// 实体类 AccountsAgentInfo  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class AccountsAgentInfo
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "AccountsAgentInfo";

        #endregion 

        #region 私有变量

        private int p_agentid;
        private int P_parentagent;
        private int p_userid;
        private string p_password;
        private string p_compellation;
        private string p_qqaccount;
        private string p_wcnickname;
        private string p_contactphone;
        private string p_contactaddress;
        private string p_agentdomain;
        private byte p_agentlevel;
        private string p_agentnote;
        private byte p_nullity;
        private DateTime p_collectdate;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化AccountsAgentInfo
        /// </summary>
        public AccountsAgentInfo()
        {
            p_agentid = 0;
            P_parentagent = 0;
            p_userid = 0;
            p_password = string.Empty;
            p_compellation = string.Empty;
            p_qqaccount = string.Empty;
            p_wcnickname = string.Empty;
            p_contactphone = string.Empty;
            p_contactaddress = string.Empty;
            p_agentdomain = string.Empty;
            p_agentlevel = 0;
            p_agentnote = string.Empty;
            p_nullity = 0;
            p_collectdate = DateTime.Now;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// AgentID
        /// </summary>
        public int AgentID
        {
            set
            {
                p_agentid = value;
            }
            get
            {
                return p_agentid;
            }
        }

        /// <summary>
        /// ParentAgent
        /// </summary>
        public int ParentAgent
        {
            set
            {
                P_parentagent = value;
            }
            get
            {
                return P_parentagent;
            }
        }

        /// <summary>
        /// UserID
        /// </summary>
        public int UserID
        {
            set
            {
                p_userid = value;
            }
            get
            {
                return p_userid;
            }
        }

        /// <summary>
        /// Password
        /// </summary>
        public string Password
        {
            set
            {
                p_password = value;
            }
            get
            {
                return p_password;
            }
        }

        /// <summary>
        /// Compellation
        /// </summary>
        public string Compellation
        {
            set
            {
                p_compellation = value;
            }
            get
            {
                return p_compellation;
            }
        }

        /// <summary>
        /// QQAccount
        /// </summary>
        public string QQAccount
        {
            set
            {
                p_qqaccount = value;
            }
            get
            {
                return p_qqaccount;
            }
        }

        /// <summary>
        /// WCNickName
        /// </summary>
        public string WCNickName
        {
            set
            {
                p_wcnickname = value;
            }
            get
            {
                return p_wcnickname;
            }
        }

        /// <summary>
        /// ContactPhone
        /// </summary>
        public string ContactPhone
        {
            set
            {
                p_contactphone = value;
            }
            get
            {
                return p_contactphone;
            }
        }

        /// <summary>
        /// ContactAddress
        /// </summary>
        public string ContactAddress
        {
            set
            {
                p_contactaddress = value;
            }
            get
            {
                return p_contactaddress;
            }
        }

        /// <summary>
        /// AgentDomain
        /// </summary>
        public string AgentDomain
        {
            set
            {
                p_agentdomain = value;
            }
            get
            {
                return p_agentdomain;
            }
        }

        /// <summary>
        /// AgentLevel
        /// </summary>
        public byte AgentLevel
        {
            set
            {
                p_agentlevel = value;
            }
            get
            {
                return p_agentlevel;
            }
        }

        /// <summary>
        /// AgentNote
        /// </summary>
        public string AgentNote
        {
            set
            {
                p_agentnote = value;
            }
            get
            {
                return p_agentnote;
            }
        }

        /// <summary>
        /// Nullity
        /// </summary>
        public byte Nullity
        {
            set
            {
                p_nullity = value;
            }
            get
            {
                return p_nullity;
            }
        }

        /// <summary>
        /// CollectDate
        /// </summary>
        public DateTime CollectDate
        {
            set
            {
                p_collectdate = value;
            }
            get
            {
                return p_collectdate;
            }
        }

        #endregion
    }
}

