/*
 * 版本： 4.0
 * 日期：2017/6/29 11:02:06
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Platform
{
    /// <summary>
    /// 实体类 StreamCreateTableFeeInfo  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class StreamCreateTableFeeInfo
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "StreamCreateTableFeeInfo";

        #endregion 

        #region 私有变量

        private int p_recordid;
        private int p_userid;
        private string p_nickname;
        private int p_serverid;
        private int p_roomid;
        private byte p_cardorbean;
        private Int64 p_cellscore;
        private Int16 p_joingamepeoplecount;
        private int p_countlimit;
        private int p_timelimit;
        private Int64 p_createtablefee;
        private DateTime p_createdate;
        private DateTime? p_dissumedate;
        private Int64 p_taxagency;
        private Int64 p_taxcount;
        private Int64 p_taxrevenue;
        private byte[] p_roomscoreinfo;
        private byte p_paymode;
        private byte p_roomstatus;
        private int p_needroomcard;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化StreamCreateTableFeeInfo
        /// </summary>
        public StreamCreateTableFeeInfo() 
        {
            p_recordid = 0;
            p_userid = 0;
            p_nickname = string.Empty;
            p_serverid = 0;
            p_roomid = 0;
            p_cardorbean = 0;
            p_cellscore = 0;
            p_joingamepeoplecount = 0;
            p_countlimit = 0;
            p_timelimit = 0;
            p_createtablefee = 0;
            p_createdate = DateTime.Now;
            p_dissumedate = null;
            p_taxagency = 0;
            p_taxcount = 0;
            p_taxrevenue = 0;
            p_roomscoreinfo = null;
            p_paymode = 0;
            p_roomstatus = 0;
            p_needroomcard = 0;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// RecordID
        /// </summary>
        public int RecordID
        {
            set
            {
                p_recordid=value;
            }
            get
            {
                return p_recordid;
            }
        }

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
        /// RoomID
        /// </summary>
        public int RoomID
        {
            set
            {
                p_roomid=value;
            }
            get
            {
                return p_roomid;
            }
        }

        /// <summary>
        /// CardOrBean
        /// </summary>
        public byte CardOrBean
        {
            set
            {
                p_cardorbean=value;
            }
            get
            {
                return p_cardorbean;
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
        /// JoinGamePeopleCount
        /// </summary>
        public Int16 JoinGamePeopleCount
        {
            set
            {
                p_joingamepeoplecount=value;
            }
            get
            {
                return p_joingamepeoplecount;
            }
        }

        /// <summary>
        /// CountLimit
        /// </summary>
        public int CountLimit
        {
            set
            {
                p_countlimit=value;
            }
            get
            {
                return p_countlimit;
            }
        }

        /// <summary>
        /// TimeLimit
        /// </summary>
        public int TimeLimit
        {
            set
            {
                p_timelimit=value;
            }
            get
            {
                return p_timelimit;
            }
        }

        /// <summary>
        /// CreateTableFee
        /// </summary>
        public Int64 CreateTableFee
        {
            set
            {
                p_createtablefee=value;
            }
            get
            {
                return p_createtablefee;
            }
        }

        /// <summary>
        /// CreateDate
        /// </summary>
        public DateTime CreateDate
        {
            set
            {
                p_createdate=value;
            }
            get
            {
                return p_createdate;
            }
        }

        /// <summary>
        /// DissumeDate
        /// </summary>
        public DateTime? DissumeDate
        {
            set
            {
                p_dissumedate=value;
            }
            get
            {
                return p_dissumedate;
            }
        }

        /// <summary>
        /// TaxAgency
        /// </summary>
        public Int64 TaxAgency
        {
            set
            {
                p_taxagency=value;
            }
            get
            {
                return p_taxagency;
            }
        }

        /// <summary>
        /// TaxCount
        /// </summary>
        public Int64 TaxCount
        {
            set
            {
                p_taxcount=value;
            }
            get
            {
                return p_taxcount;
            }
        }

        /// <summary>
        /// TaxRevenue
        /// </summary>
        public Int64 TaxRevenue
        {
            set
            {
                p_taxrevenue=value;
            }
            get
            {
                return p_taxrevenue;
            }
        }

        /// <summary>
        /// RoomScoreInfo
        /// </summary>
        public byte[] RoomScoreInfo
        {
            set
            {
                p_roomscoreinfo=value;
            }
            get
            {
                return p_roomscoreinfo;
            }
        }

        /// <summary>
        /// PayMode
        /// </summary>
        public byte PayMode
        {
            set
            {
                p_paymode=value;
            }
            get
            {
                return p_paymode;
            }
        }

        /// <summary>
        /// RoomStatus
        /// </summary>
        public byte RoomStatus
        {
            set
            {
                p_roomstatus=value;
            }
            get
            {
                return p_roomstatus;
            }
        }

        /// <summary>
        /// NeedRoomCard
        /// </summary>
        public int NeedRoomCard
        {
            set
            {
                p_needroomcard=value;
            }
            get
            {
                return p_needroomcard;
            }
        }

        #endregion
    }
}

