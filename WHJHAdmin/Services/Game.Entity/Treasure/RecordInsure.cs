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
    /// 实体类 RecordInsure  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RecordInsure
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "RecordInsure";

        #endregion 

        #region 私有变量

        private int p_recordid;
        private int p_kindid;
        private int p_serverid;
        private int p_sourceuserid;
        private Int64 p_sourcegold;
        private Int64 p_sourcebank;
        private int p_targetuserid;
        private Int64 p_targetgold;
        private Int64 p_targetbank;
        private Int64 p_swapscore;
        private Int64 p_revenue;
        private byte p_isgameplaza;
        private byte p_tradetype;
        private string p_clientip;
        private DateTime p_collectdate;
        private string p_collectnote;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化RecordInsure
        /// </summary>
        public RecordInsure() 
        {
            p_recordid = 0;
            p_kindid = 0;
            p_serverid = 0;
            p_sourceuserid = 0;
            p_sourcegold = 0;
            p_sourcebank = 0;
            p_targetuserid = 0;
            p_targetgold = 0;
            p_targetbank = 0;
            p_swapscore = 0;
            p_revenue = 0;
            p_isgameplaza = 0;
            p_tradetype = 0;
            p_clientip = string.Empty;
            p_collectdate = DateTime.Now;
            p_collectnote = string.Empty;
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
        /// SourceUserID
        /// </summary>
        public int SourceUserID
        {
            set
            {
                p_sourceuserid=value;
            }
            get
            {
                return p_sourceuserid;
            }
        }

        /// <summary>
        /// SourceGold
        /// </summary>
        public Int64 SourceGold
        {
            set
            {
                p_sourcegold=value;
            }
            get
            {
                return p_sourcegold;
            }
        }

        /// <summary>
        /// SourceBank
        /// </summary>
        public Int64 SourceBank
        {
            set
            {
                p_sourcebank=value;
            }
            get
            {
                return p_sourcebank;
            }
        }

        /// <summary>
        /// TargetUserID
        /// </summary>
        public int TargetUserID
        {
            set
            {
                p_targetuserid=value;
            }
            get
            {
                return p_targetuserid;
            }
        }

        /// <summary>
        /// TargetGold
        /// </summary>
        public Int64 TargetGold
        {
            set
            {
                p_targetgold=value;
            }
            get
            {
                return p_targetgold;
            }
        }

        /// <summary>
        /// TargetBank
        /// </summary>
        public Int64 TargetBank
        {
            set
            {
                p_targetbank=value;
            }
            get
            {
                return p_targetbank;
            }
        }

        /// <summary>
        /// SwapScore
        /// </summary>
        public Int64 SwapScore
        {
            set
            {
                p_swapscore=value;
            }
            get
            {
                return p_swapscore;
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
        /// IsGamePlaza
        /// </summary>
        public byte IsGamePlaza
        {
            set
            {
                p_isgameplaza=value;
            }
            get
            {
                return p_isgameplaza;
            }
        }

        /// <summary>
        /// TradeType
        /// </summary>
        public byte TradeType
        {
            set
            {
                p_tradetype=value;
            }
            get
            {
                return p_tradetype;
            }
        }

        /// <summary>
        /// ClientIP
        /// </summary>
        public string ClientIP
        {
            set
            {
                p_clientip=value;
            }
            get
            {
                return p_clientip;
            }
        }

        /// <summary>
        /// CollectDate
        /// </summary>
        public DateTime CollectDate
        {
            set
            {
                p_collectdate=value;
            }
            get
            {
                return p_collectdate;
            }
        }

        /// <summary>
        /// CollectNote
        /// </summary>
        public string CollectNote
        {
            set
            {
                p_collectnote=value;
            }
            get
            {
                return p_collectnote;
            }
        }

        #endregion
    }
}

