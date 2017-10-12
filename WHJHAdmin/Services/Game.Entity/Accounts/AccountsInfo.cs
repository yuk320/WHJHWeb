/*
 * 版本： 4.0
 * 日期：2017/8/7 10:49:53
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Accounts
{
    /// <summary>
    /// 实体类 AccountsInfo  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class AccountsInfo
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "AccountsInfo";

        #endregion 

        #region 私有变量

        private int p_userid;
        private int p_gameid;
        private int p_spreaderid;
        private string p_accounts;
        private string p_nickname;
        private string p_regaccounts;
        private string p_underwrite;
        private string p_passportid;
        private string p_compellation;
        private string p_logonpass;
        private string p_insurepass;
        private string p_dynamicpass;
        private DateTime p_dynamicpasstime;
        private Int16 p_faceid;
        private int p_customid;
        private int p_userright;
        private int p_masterright;
        private int p_serviceright;
        private byte p_masterorder;
        private byte p_memberorder;
        private DateTime p_memberoverdate;
        private DateTime p_memberswitchdate;
        private byte p_customfacever;
        private byte p_gender;
        private byte p_nullity;
        private DateTime p_nullityoverdate;
        private byte p_stundown;
        private byte p_moormachine;
        private byte p_isandroid;
        private int p_weblogontimes;
        private int p_gamelogontimes;
        private int p_playtimecount;
        private int p_onlinetimecount;
        private string p_lastlogonip;
        private DateTime p_lastlogondate;
        private string p_lastlogonmobile;
        private string p_lastlogonmachine;
        private string p_registerip;
        private DateTime p_registerdate;
        private string p_registermobile;
        private string p_registermachine;
        private byte p_registerorigin;
        private Int16 p_platformid;
        private string p_useruin;
        private int p_rankid;
        private int p_agentid;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化AccountsInfo
        /// </summary>
        public AccountsInfo() 
        {
            p_userid = 0;
            p_gameid = 0;
            p_spreaderid = 0;
            p_accounts = string.Empty;
            p_nickname = string.Empty;
            p_regaccounts = string.Empty;
            p_underwrite = string.Empty;
            p_passportid = string.Empty;
            p_compellation = string.Empty;
            p_logonpass = string.Empty;
            p_insurepass = string.Empty;
            p_dynamicpass = string.Empty;
            p_dynamicpasstime = DateTime.Now;
            p_faceid = 0;
            p_customid = 0;
            p_userright = 0;
            p_masterright = 0;
            p_serviceright = 0;
            p_masterorder = 0;
            p_memberorder = 0;
            p_memberoverdate = DateTime.Now;
            p_memberswitchdate = DateTime.Now;
            p_customfacever = 0;
            p_gender = 0;
            p_nullity = 0;
            p_nullityoverdate = DateTime.Now;
            p_stundown = 0;
            p_moormachine = 0;
            p_isandroid = 0;
            p_weblogontimes = 0;
            p_gamelogontimes = 0;
            p_playtimecount = 0;
            p_onlinetimecount = 0;
            p_lastlogonip = string.Empty;
            p_lastlogondate = DateTime.Now;
            p_lastlogonmobile = string.Empty;
            p_lastlogonmachine = string.Empty;
            p_registerip = string.Empty;
            p_registerdate = DateTime.Now;
            p_registermobile = string.Empty;
            p_registermachine = string.Empty;
            p_registerorigin = 0;
            p_platformid = 0;
            p_useruin = string.Empty;
            p_rankid = 0;
            p_agentid = 0;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// UserID
        /// </summary>
        public int UserID
        {
            set
            {
                p_userid=value;
            }
            get
            {
                return p_userid;
            }
        }

        /// <summary>
        /// GameID
        /// </summary>
        public int GameID
        {
            set
            {
                p_gameid=value;
            }
            get
            {
                return p_gameid;
            }
        }

        /// <summary>
        /// SpreaderID
        /// </summary>
        public int SpreaderID
        {
            set
            {
                p_spreaderid=value;
            }
            get
            {
                return p_spreaderid;
            }
        }

        /// <summary>
        /// Accounts
        /// </summary>
        public string Accounts
        {
            set
            {
                p_accounts=value;
            }
            get
            {
                return p_accounts;
            }
        }

        /// <summary>
        /// NickName
        /// </summary>
        public string NickName
        {
            set
            {
                p_nickname=value;
            }
            get
            {
                return p_nickname;
            }
        }

        /// <summary>
        /// RegAccounts
        /// </summary>
        public string RegAccounts
        {
            set
            {
                p_regaccounts=value;
            }
            get
            {
                return p_regaccounts;
            }
        }

        /// <summary>
        /// UnderWrite
        /// </summary>
        public string UnderWrite
        {
            set
            {
                p_underwrite=value;
            }
            get
            {
                return p_underwrite;
            }
        }

        /// <summary>
        /// PassPortID
        /// </summary>
        public string PassPortID
        {
            set
            {
                p_passportid=value;
            }
            get
            {
                return p_passportid;
            }
        }

        /// <summary>
        /// Compellation
        /// </summary>
        public string Compellation
        {
            set
            {
                p_compellation=value;
            }
            get
            {
                return p_compellation;
            }
        }

        /// <summary>
        /// LogonPass
        /// </summary>
        public string LogonPass
        {
            set
            {
                p_logonpass=value;
            }
            get
            {
                return p_logonpass;
            }
        }

        /// <summary>
        /// InsurePass
        /// </summary>
        public string InsurePass
        {
            set
            {
                p_insurepass=value;
            }
            get
            {
                return p_insurepass;
            }
        }

        /// <summary>
        /// DynamicPass
        /// </summary>
        public string DynamicPass
        {
            set
            {
                p_dynamicpass=value;
            }
            get
            {
                return p_dynamicpass;
            }
        }

        /// <summary>
        /// DynamicPassTime
        /// </summary>
        public DateTime DynamicPassTime
        {
            set
            {
                p_dynamicpasstime=value;
            }
            get
            {
                return p_dynamicpasstime;
            }
        }

        /// <summary>
        /// FaceID
        /// </summary>
        public Int16 FaceID
        {
            set
            {
                p_faceid=value;
            }
            get
            {
                return p_faceid;
            }
        }

        /// <summary>
        /// CustomID
        /// </summary>
        public int CustomID
        {
            set
            {
                p_customid=value;
            }
            get
            {
                return p_customid;
            }
        }

        /// <summary>
        /// UserRight
        /// </summary>
        public int UserRight
        {
            set
            {
                p_userright=value;
            }
            get
            {
                return p_userright;
            }
        }

        /// <summary>
        /// MasterRight
        /// </summary>
        public int MasterRight
        {
            set
            {
                p_masterright=value;
            }
            get
            {
                return p_masterright;
            }
        }

        /// <summary>
        /// ServiceRight
        /// </summary>
        public int ServiceRight
        {
            set
            {
                p_serviceright=value;
            }
            get
            {
                return p_serviceright;
            }
        }

        /// <summary>
        /// MasterOrder
        /// </summary>
        public byte MasterOrder
        {
            set
            {
                p_masterorder=value;
            }
            get
            {
                return p_masterorder;
            }
        }

        /// <summary>
        /// MemberOrder
        /// </summary>
        public byte MemberOrder
        {
            set
            {
                p_memberorder=value;
            }
            get
            {
                return p_memberorder;
            }
        }

        /// <summary>
        /// MemberOverDate
        /// </summary>
        public DateTime MemberOverDate
        {
            set
            {
                p_memberoverdate=value;
            }
            get
            {
                return p_memberoverdate;
            }
        }

        /// <summary>
        /// MemberSwitchDate
        /// </summary>
        public DateTime MemberSwitchDate
        {
            set
            {
                p_memberswitchdate=value;
            }
            get
            {
                return p_memberswitchdate;
            }
        }

        /// <summary>
        /// CustomFaceVer
        /// </summary>
        public byte CustomFaceVer
        {
            set
            {
                p_customfacever=value;
            }
            get
            {
                return p_customfacever;
            }
        }

        /// <summary>
        /// Gender
        /// </summary>
        public byte Gender
        {
            set
            {
                p_gender=value;
            }
            get
            {
                return p_gender;
            }
        }

        /// <summary>
        /// Nullity
        /// </summary>
        public byte Nullity
        {
            set
            {
                p_nullity=value;
            }
            get
            {
                return p_nullity;
            }
        }

        /// <summary>
        /// NullityOverDate
        /// </summary>
        public DateTime NullityOverDate
        {
            set
            {
                p_nullityoverdate=value;
            }
            get
            {
                return p_nullityoverdate;
            }
        }

        /// <summary>
        /// StunDown
        /// </summary>
        public byte StunDown
        {
            set
            {
                p_stundown=value;
            }
            get
            {
                return p_stundown;
            }
        }

        /// <summary>
        /// MoorMachine
        /// </summary>
        public byte MoorMachine
        {
            set
            {
                p_moormachine=value;
            }
            get
            {
                return p_moormachine;
            }
        }

        /// <summary>
        /// IsAndroid
        /// </summary>
        public byte IsAndroid
        {
            set
            {
                p_isandroid=value;
            }
            get
            {
                return p_isandroid;
            }
        }

        /// <summary>
        /// WebLogonTimes
        /// </summary>
        public int WebLogonTimes
        {
            set
            {
                p_weblogontimes=value;
            }
            get
            {
                return p_weblogontimes;
            }
        }

        /// <summary>
        /// GameLogonTimes
        /// </summary>
        public int GameLogonTimes
        {
            set
            {
                p_gamelogontimes=value;
            }
            get
            {
                return p_gamelogontimes;
            }
        }

        /// <summary>
        /// PlayTimeCount
        /// </summary>
        public int PlayTimeCount
        {
            set
            {
                p_playtimecount=value;
            }
            get
            {
                return p_playtimecount;
            }
        }

        /// <summary>
        /// OnLineTimeCount
        /// </summary>
        public int OnLineTimeCount
        {
            set
            {
                p_onlinetimecount=value;
            }
            get
            {
                return p_onlinetimecount;
            }
        }

        /// <summary>
        /// LastLogonIP
        /// </summary>
        public string LastLogonIP
        {
            set
            {
                p_lastlogonip=value;
            }
            get
            {
                return p_lastlogonip;
            }
        }

        /// <summary>
        /// LastLogonDate
        /// </summary>
        public DateTime LastLogonDate
        {
            set
            {
                p_lastlogondate=value;
            }
            get
            {
                return p_lastlogondate;
            }
        }

        /// <summary>
        /// LastLogonMobile
        /// </summary>
        public string LastLogonMobile
        {
            set
            {
                p_lastlogonmobile=value;
            }
            get
            {
                return p_lastlogonmobile;
            }
        }

        /// <summary>
        /// LastLogonMachine
        /// </summary>
        public string LastLogonMachine
        {
            set
            {
                p_lastlogonmachine=value;
            }
            get
            {
                return p_lastlogonmachine;
            }
        }

        /// <summary>
        /// RegisterIP
        /// </summary>
        public string RegisterIP
        {
            set
            {
                p_registerip=value;
            }
            get
            {
                return p_registerip;
            }
        }

        /// <summary>
        /// RegisterDate
        /// </summary>
        public DateTime RegisterDate
        {
            set
            {
                p_registerdate=value;
            }
            get
            {
                return p_registerdate;
            }
        }

        /// <summary>
        /// RegisterMobile
        /// </summary>
        public string RegisterMobile
        {
            set
            {
                p_registermobile=value;
            }
            get
            {
                return p_registermobile;
            }
        }

        /// <summary>
        /// RegisterMachine
        /// </summary>
        public string RegisterMachine
        {
            set
            {
                p_registermachine=value;
            }
            get
            {
                return p_registermachine;
            }
        }

        /// <summary>
        /// RegisterOrigin
        /// </summary>
        public byte RegisterOrigin
        {
            set
            {
                p_registerorigin=value;
            }
            get
            {
                return p_registerorigin;
            }
        }

        /// <summary>
        /// PlatformID
        /// </summary>
        public Int16 PlatformID
        {
            set
            {
                p_platformid=value;
            }
            get
            {
                return p_platformid;
            }
        }

        /// <summary>
        /// UserUin
        /// </summary>
        public string UserUin
        {
            set
            {
                p_useruin=value;
            }
            get
            {
                return p_useruin;
            }
        }

        /// <summary>
        /// RankID
        /// </summary>
        public int RankID
        {
            set
            {
                p_rankid=value;
            }
            get
            {
                return p_rankid;
            }
        }

        /// <summary>
        /// AgentID
        /// </summary>
        public int AgentID
        {
            set
            {
                p_agentid=value;
            }
            get
            {
                return p_agentid;
            }
        }

        #endregion
    }
}

