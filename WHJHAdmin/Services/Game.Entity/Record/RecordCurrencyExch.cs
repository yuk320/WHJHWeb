/*
 * 版本： 4.0
 * 日期：2017/9/13 10:17:33
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Record
{
    /// <summary>
    /// 实体类 RecordCurrencyExch  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RecordCurrencyExch
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "RecordCurrencyExch";

        #endregion 

        #region 私有变量

        private int p_recordid;
        private int p_userid;
        private byte p_typeid;
        private Int64 p_curdiamond;
        private int p_exchdiamond;
        private Int64 p_curscore;
        private Int64 p_curinsurescore;
        private Int64 p_presentgold;
        private string p_clientip;
        private DateTime p_collectdate;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化RecordCurrencyExch
        /// </summary>
        public RecordCurrencyExch() 
        {
            p_recordid = 0;
            p_userid = 0;
            p_typeid = 0;
            p_curdiamond = 0;
            p_exchdiamond = 0;
            p_curscore = 0;
            p_curinsurescore = 0;
            p_presentgold = 0;
            p_clientip = string.Empty;
            p_collectdate = DateTime.Now;
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
        /// TypeID
        /// </summary>
        public byte TypeID
        {
            set
            {
                p_typeid=value;
            }
            get
            {
                return p_typeid;
            }
        }

        /// <summary>
        /// CurDiamond
        /// </summary>
        public Int64 CurDiamond
        {
            set
            {
                p_curdiamond=value;
            }
            get
            {
                return p_curdiamond;
            }
        }

        /// <summary>
        /// ExchDiamond
        /// </summary>
        public int ExchDiamond
        {
            set
            {
                p_exchdiamond=value;
            }
            get
            {
                return p_exchdiamond;
            }
        }

        /// <summary>
        /// CurScore
        /// </summary>
        public Int64 CurScore
        {
            set
            {
                p_curscore=value;
            }
            get
            {
                return p_curscore;
            }
        }

        /// <summary>
        /// CurInsureScore
        /// </summary>
        public Int64 CurInsureScore
        {
            set
            {
                p_curinsurescore=value;
            }
            get
            {
                return p_curinsurescore;
            }
        }

        /// <summary>
        /// PresentGold
        /// </summary>
        public Int64 PresentGold
        {
            set
            {
                p_presentgold=value;
            }
            get
            {
                return p_presentgold;
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

        #endregion
    }
}

