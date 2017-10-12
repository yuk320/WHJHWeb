/*
 * 版本： 4.0
 * 日期：2017/8/7 10:51:13
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Record
{
    /// <summary>
    /// 实体类 RecordPresentCurrency  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RecordPresentCurrency
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "RecordPresentCurrency";

        #endregion 

        #region 私有变量

        private int p_recordid;
        private int p_sourceuserid;
        private Int64 p_sourcediamond;
        private int p_targetuserid;
        private Int64 p_targetdiamond;
        private byte p_targetagentlevel;
        private int p_presentdiamond;
        private string p_clientip;
        private DateTime p_collectdate;
        private string p_collectnote;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化RecordPresentCurrency
        /// </summary>
        public RecordPresentCurrency() 
        {
            p_recordid = 0;
            p_sourceuserid = 0;
            p_sourcediamond = 0;
            p_targetuserid = 0;
            p_targetdiamond = 0;
            p_targetagentlevel = 0;
            p_presentdiamond = 0;
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
        /// SourceDiamond
        /// </summary>
        public Int64 SourceDiamond
        {
            set
            {
                p_sourcediamond=value;
            }
            get
            {
                return p_sourcediamond;
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
        /// TargetDiamond
        /// </summary>
        public Int64 TargetDiamond
        {
            set
            {
                p_targetdiamond=value;
            }
            get
            {
                return p_targetdiamond;
            }
        }

        /// <summary>
        /// TargetAgentLevel
        /// </summary>
        public byte TargetAgentLevel
        {
            set
            {
                p_targetagentlevel=value;
            }
            get
            {
                return p_targetagentlevel;
            }
        }

        /// <summary>
        /// PresentDiamond
        /// </summary>
        public int PresentDiamond
        {
            set
            {
                p_presentdiamond=value;
            }
            get
            {
                return p_presentdiamond;
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

