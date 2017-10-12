/*
 * 版本： 4.0
 * 日期：2017/8/7 10:51:24
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Treasure
{
    /// <summary>
    /// 实体类 GameScoreInfo  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class GameScoreInfo
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "GameScoreInfo";

        #endregion 

        #region 私有变量

        private int p_userid;
        private Int64 p_score;
        private Int64 p_revenue;
        private Int64 p_insurescore;
        private int p_wincount;
        private int p_lostcount;
        private int p_drawcount;
        private int p_fleecount;
        private int p_userright;
        private int p_masterright;
        private byte p_masterorder;
        private int p_alllogontimes;
        private int p_playtimecount;
        private int p_onlinetimecount;
        private string p_lastlogonip;
        private DateTime p_lastlogondate;
        private string p_lastlogonmachine;
        private string p_registerip;
        private DateTime p_registerdate;
        private string p_registermachine;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化GameScoreInfo
        /// </summary>
        public GameScoreInfo() 
        {
            p_userid = 0;
            p_score = 0;
            p_revenue = 0;
            p_insurescore = 0;
            p_wincount = 0;
            p_lostcount = 0;
            p_drawcount = 0;
            p_fleecount = 0;
            p_userright = 0;
            p_masterright = 0;
            p_masterorder = 0;
            p_alllogontimes = 0;
            p_playtimecount = 0;
            p_onlinetimecount = 0;
            p_lastlogonip = string.Empty;
            p_lastlogondate = DateTime.Now;
            p_lastlogonmachine = string.Empty;
            p_registerip = string.Empty;
            p_registerdate = DateTime.Now;
            p_registermachine = string.Empty;
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
        /// InsureScore
        /// </summary>
        public Int64 InsureScore
        {
            set
            {
                p_insurescore=value;
            }
            get
            {
                return p_insurescore;
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
        /// AllLogonTimes
        /// </summary>
        public int AllLogonTimes
        {
            set
            {
                p_alllogontimes=value;
            }
            get
            {
                return p_alllogontimes;
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

        #endregion
    }
}

