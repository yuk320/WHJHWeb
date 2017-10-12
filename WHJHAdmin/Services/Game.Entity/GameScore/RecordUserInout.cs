/*
 * 版本： 4.0
 * 日期：2017/8/7 10:50:16
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.GameScore
{
    /// <summary>
    /// 实体类 RecordUserInout  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RecordUserInout
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "RecordUserInout";

        #endregion 

        #region 私有变量

        private int p_id;
        private int p_userid;
        private int p_kindid;
        private int p_serverid;
        private DateTime p_entertime;
        private Int64 p_enterscore;
        private Int64 p_enterinsure;
        private string p_entermachine;
        private string p_enterclientip;
        private DateTime? p_leavetime;
        private int p_leavereason;
        private string p_leavemachine;
        private string p_leaveclientip;
        private Int64 p_score;
        private Int64 p_insure;
        private Int64 p_revenue;
        private int p_wincount;
        private int p_lostcount;
        private int p_drawcount;
        private int p_fleecount;
        private int p_playtimecount;
        private int p_onlinetimecount;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化RecordUserInout
        /// </summary>
        public RecordUserInout() 
        {
            p_id = 0;
            p_userid = 0;
            p_kindid = 0;
            p_serverid = 0;
            p_entertime = DateTime.Now;
            p_enterscore = 0;
            p_enterinsure = 0;
            p_entermachine = string.Empty;
            p_enterclientip = string.Empty;
            p_leavetime = null;
            p_leavereason = 0;
            p_leavemachine = string.Empty;
            p_leaveclientip = string.Empty;
            p_score = 0;
            p_insure = 0;
            p_revenue = 0;
            p_wincount = 0;
            p_lostcount = 0;
            p_drawcount = 0;
            p_fleecount = 0;
            p_playtimecount = 0;
            p_onlinetimecount = 0;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            set
            {
                p_id=value;
            }
            get
            {
                return p_id;
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
        /// EnterTime
        /// </summary>
        public DateTime EnterTime
        {
            set
            {
                p_entertime=value;
            }
            get
            {
                return p_entertime;
            }
        }

        /// <summary>
        /// EnterScore
        /// </summary>
        public Int64 EnterScore
        {
            set
            {
                p_enterscore=value;
            }
            get
            {
                return p_enterscore;
            }
        }

        /// <summary>
        /// EnterInsure
        /// </summary>
        public Int64 EnterInsure
        {
            set
            {
                p_enterinsure=value;
            }
            get
            {
                return p_enterinsure;
            }
        }

        /// <summary>
        /// EnterMachine
        /// </summary>
        public string EnterMachine
        {
            set
            {
                p_entermachine=value;
            }
            get
            {
                return p_entermachine;
            }
        }

        /// <summary>
        /// EnterClientIP
        /// </summary>
        public string EnterClientIP
        {
            set
            {
                p_enterclientip=value;
            }
            get
            {
                return p_enterclientip;
            }
        }

        /// <summary>
        /// LeaveTime
        /// </summary>
        public DateTime? LeaveTime
        {
            set
            {
                p_leavetime=value;
            }
            get
            {
                return p_leavetime;
            }
        }

        /// <summary>
        /// LeaveReason
        /// </summary>
        public int LeaveReason
        {
            set
            {
                p_leavereason=value;
            }
            get
            {
                return p_leavereason;
            }
        }

        /// <summary>
        /// LeaveMachine
        /// </summary>
        public string LeaveMachine
        {
            set
            {
                p_leavemachine=value;
            }
            get
            {
                return p_leavemachine;
            }
        }

        /// <summary>
        /// LeaveClientIP
        /// </summary>
        public string LeaveClientIP
        {
            set
            {
                p_leaveclientip=value;
            }
            get
            {
                return p_leaveclientip;
            }
        }

        /// <summary>
        /// Score
        /// </summary>
        public Int64 Score
        {
            set
            {
                p_score=value;
            }
            get
            {
                return p_score;
            }
        }

        /// <summary>
        /// Insure
        /// </summary>
        public Int64 Insure
        {
            set
            {
                p_insure=value;
            }
            get
            {
                return p_insure;
            }
        }

        /// <summary>
        /// Revenue
        /// </summary>
        public Int64 Revenue
        {
            set
            {
                p_revenue=value;
            }
            get
            {
                return p_revenue;
            }
        }

        /// <summary>
        /// WinCount
        /// </summary>
        public int WinCount
        {
            set
            {
                p_wincount=value;
            }
            get
            {
                return p_wincount;
            }
        }

        /// <summary>
        /// LostCount
        /// </summary>
        public int LostCount
        {
            set
            {
                p_lostcount=value;
            }
            get
            {
                return p_lostcount;
            }
        }

        /// <summary>
        /// DrawCount
        /// </summary>
        public int DrawCount
        {
            set
            {
                p_drawcount=value;
            }
            get
            {
                return p_drawcount;
            }
        }

        /// <summary>
        /// FleeCount
        /// </summary>
        public int FleeCount
        {
            set
            {
                p_fleecount=value;
            }
            get
            {
                return p_fleecount;
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

        #endregion
    }
}

