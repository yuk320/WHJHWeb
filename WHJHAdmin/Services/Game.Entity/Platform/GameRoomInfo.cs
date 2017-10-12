/*
 * 版本： 4.0
 * 日期：2017/8/7 10:50:43
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Platform
{
    /// <summary>
    /// 实体类 GameRoomInfo  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class GameRoomInfo
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "GameRoomInfo";

        #endregion 

        #region 私有变量

        private int p_serverid;
        private string p_servername;
        private int p_kindid;
        private int p_nodeid;
        private int p_sortid;
        private int p_gameid;
        private int p_tablecount;
        private int p_serverkind;
        private int p_servertype;
        private int p_serverport;
        private int p_serverlevel;
        private string p_serverpasswd;
        private string p_databasename;
        private string p_databaseaddr;
        private Int64 p_cellscore;
        private byte p_revenueratio;
        private Int64 p_servicescore;
        private Int64 p_restrictscore;
        private Int64 p_mintablescore;
        private Int64 p_minenterscore;
        private Int64 p_maxenterscore;
        private int p_minentermember;
        private int p_maxentermember;
        private int p_maxplayer;
        private int p_serverrule;
        private int p_distributerule;
        private int p_mindistributeuser;
        private int p_distributetimespace;
        private int p_distributedrawcount;
        private int p_minpartakegameuser;
        private int p_maxpartakegameuser;
        private int p_attachuserright;
        private string p_servicemachine;
        private string p_customrule;
        private byte p_nullity;
        private string p_servernote;
        private DateTime p_createdatetime;
        private DateTime p_modifydatetime;
        private string p_enterpassword;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化GameRoomInfo
        /// </summary>
        public GameRoomInfo() 
        {
            p_serverid = 0;
            p_servername = string.Empty;
            p_kindid = 0;
            p_nodeid = 0;
            p_sortid = 0;
            p_gameid = 0;
            p_tablecount = 0;
            p_serverkind = 0;
            p_servertype = 0;
            p_serverport = 0;
            p_serverlevel = 0;
            p_serverpasswd = string.Empty;
            p_databasename = string.Empty;
            p_databaseaddr = string.Empty;
            p_cellscore = 0;
            p_revenueratio = 0;
            p_servicescore = 0;
            p_restrictscore = 0;
            p_mintablescore = 0;
            p_minenterscore = 0;
            p_maxenterscore = 0;
            p_minentermember = 0;
            p_maxentermember = 0;
            p_maxplayer = 0;
            p_serverrule = 0;
            p_distributerule = 0;
            p_mindistributeuser = 0;
            p_distributetimespace = 0;
            p_distributedrawcount = 0;
            p_minpartakegameuser = 0;
            p_maxpartakegameuser = 0;
            p_attachuserright = 0;
            p_servicemachine = string.Empty;
            p_customrule = string.Empty;
            p_nullity = 0;
            p_servernote = string.Empty;
            p_createdatetime = DateTime.Now;
            p_modifydatetime = DateTime.Now;
            p_enterpassword = string.Empty;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// ServerID
        /// </summary>
        public int ServerID
        {
            set
            {
                p_serverid=value;
            }
            get
            {
                return p_serverid;
            }
        }

        /// <summary>
        /// ServerName
        /// </summary>
        public string ServerName
        {
            set
            {
                p_servername=value;
            }
            get
            {
                return p_servername;
            }
        }

        /// <summary>
        /// KindID
        /// </summary>
        public int KindID
        {
            set
            {
                p_kindid=value;
            }
            get
            {
                return p_kindid;
            }
        }

        /// <summary>
        /// NodeID
        /// </summary>
        public int NodeID
        {
            set
            {
                p_nodeid=value;
            }
            get
            {
                return p_nodeid;
            }
        }

        /// <summary>
        /// SortID
        /// </summary>
        public int SortID
        {
            set
            {
                p_sortid=value;
            }
            get
            {
                return p_sortid;
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
        /// TableCount
        /// </summary>
        public int TableCount
        {
            set
            {
                p_tablecount=value;
            }
            get
            {
                return p_tablecount;
            }
        }

        /// <summary>
        /// ServerKind
        /// </summary>
        public int ServerKind
        {
            set
            {
                p_serverkind=value;
            }
            get
            {
                return p_serverkind;
            }
        }

        /// <summary>
        /// ServerType
        /// </summary>
        public int ServerType
        {
            set
            {
                p_servertype=value;
            }
            get
            {
                return p_servertype;
            }
        }

        /// <summary>
        /// ServerPort
        /// </summary>
        public int ServerPort
        {
            set
            {
                p_serverport=value;
            }
            get
            {
                return p_serverport;
            }
        }

        /// <summary>
        /// ServerLevel
        /// </summary>
        public int ServerLevel
        {
            set
            {
                p_serverlevel=value;
            }
            get
            {
                return p_serverlevel;
            }
        }

        /// <summary>
        /// ServerPasswd
        /// </summary>
        public string ServerPasswd
        {
            set
            {
                p_serverpasswd=value;
            }
            get
            {
                return p_serverpasswd;
            }
        }

        /// <summary>
        /// DataBaseName
        /// </summary>
        public string DataBaseName
        {
            set
            {
                p_databasename=value;
            }
            get
            {
                return p_databasename;
            }
        }

        /// <summary>
        /// DataBaseAddr
        /// </summary>
        public string DataBaseAddr
        {
            set
            {
                p_databaseaddr=value;
            }
            get
            {
                return p_databaseaddr;
            }
        }

        /// <summary>
        /// CellScore
        /// </summary>
        public Int64 CellScore
        {
            set
            {
                p_cellscore=value;
            }
            get
            {
                return p_cellscore;
            }
        }

        /// <summary>
        /// RevenueRatio
        /// </summary>
        public byte RevenueRatio
        {
            set
            {
                p_revenueratio=value;
            }
            get
            {
                return p_revenueratio;
            }
        }

        /// <summary>
        /// ServiceScore
        /// </summary>
        public Int64 ServiceScore
        {
            set
            {
                p_servicescore=value;
            }
            get
            {
                return p_servicescore;
            }
        }

        /// <summary>
        /// RestrictScore
        /// </summary>
        public Int64 RestrictScore
        {
            set
            {
                p_restrictscore=value;
            }
            get
            {
                return p_restrictscore;
            }
        }

        /// <summary>
        /// MinTableScore
        /// </summary>
        public Int64 MinTableScore
        {
            set
            {
                p_mintablescore=value;
            }
            get
            {
                return p_mintablescore;
            }
        }

        /// <summary>
        /// MinEnterScore
        /// </summary>
        public Int64 MinEnterScore
        {
            set
            {
                p_minenterscore=value;
            }
            get
            {
                return p_minenterscore;
            }
        }

        /// <summary>
        /// MaxEnterScore
        /// </summary>
        public Int64 MaxEnterScore
        {
            set
            {
                p_maxenterscore=value;
            }
            get
            {
                return p_maxenterscore;
            }
        }

        /// <summary>
        /// MinEnterMember
        /// </summary>
        public int MinEnterMember
        {
            set
            {
                p_minentermember=value;
            }
            get
            {
                return p_minentermember;
            }
        }

        /// <summary>
        /// MaxEnterMember
        /// </summary>
        public int MaxEnterMember
        {
            set
            {
                p_maxentermember=value;
            }
            get
            {
                return p_maxentermember;
            }
        }

        /// <summary>
        /// MaxPlayer
        /// </summary>
        public int MaxPlayer
        {
            set
            {
                p_maxplayer=value;
            }
            get
            {
                return p_maxplayer;
            }
        }

        /// <summary>
        /// ServerRule
        /// </summary>
        public int ServerRule
        {
            set
            {
                p_serverrule=value;
            }
            get
            {
                return p_serverrule;
            }
        }

        /// <summary>
        /// DistributeRule
        /// </summary>
        public int DistributeRule
        {
            set
            {
                p_distributerule=value;
            }
            get
            {
                return p_distributerule;
            }
        }

        /// <summary>
        /// MinDistributeUser
        /// </summary>
        public int MinDistributeUser
        {
            set
            {
                p_mindistributeuser=value;
            }
            get
            {
                return p_mindistributeuser;
            }
        }

        /// <summary>
        /// DistributeTimeSpace
        /// </summary>
        public int DistributeTimeSpace
        {
            set
            {
                p_distributetimespace=value;
            }
            get
            {
                return p_distributetimespace;
            }
        }

        /// <summary>
        /// DistributeDrawCount
        /// </summary>
        public int DistributeDrawCount
        {
            set
            {
                p_distributedrawcount=value;
            }
            get
            {
                return p_distributedrawcount;
            }
        }

        /// <summary>
        /// MinPartakeGameUser
        /// </summary>
        public int MinPartakeGameUser
        {
            set
            {
                p_minpartakegameuser=value;
            }
            get
            {
                return p_minpartakegameuser;
            }
        }

        /// <summary>
        /// MaxPartakeGameUser
        /// </summary>
        public int MaxPartakeGameUser
        {
            set
            {
                p_maxpartakegameuser=value;
            }
            get
            {
                return p_maxpartakegameuser;
            }
        }

        /// <summary>
        /// AttachUserRight
        /// </summary>
        public int AttachUserRight
        {
            set
            {
                p_attachuserright=value;
            }
            get
            {
                return p_attachuserright;
            }
        }

        /// <summary>
        /// ServiceMachine
        /// </summary>
        public string ServiceMachine
        {
            set
            {
                p_servicemachine=value;
            }
            get
            {
                return p_servicemachine;
            }
        }

        /// <summary>
        /// CustomRule
        /// </summary>
        public string CustomRule
        {
            set
            {
                p_customrule=value;
            }
            get
            {
                return p_customrule;
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
        /// ServerNote
        /// </summary>
        public string ServerNote
        {
            set
            {
                p_servernote=value;
            }
            get
            {
                return p_servernote;
            }
        }

        /// <summary>
        /// CreateDateTime
        /// </summary>
        public DateTime CreateDateTime
        {
            set
            {
                p_createdatetime=value;
            }
            get
            {
                return p_createdatetime;
            }
        }

        /// <summary>
        /// ModifyDateTime
        /// </summary>
        public DateTime ModifyDateTime
        {
            set
            {
                p_modifydatetime=value;
            }
            get
            {
                return p_modifydatetime;
            }
        }

        /// <summary>
        /// EnterPassword
        /// </summary>
        public string EnterPassword
        {
            set
            {
                p_enterpassword=value;
            }
            get
            {
                return p_enterpassword;
            }
        }

        #endregion
    }
}

