/*
 * 版本： 4.0
 * 日期：2017/9/12 17:08:00
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Record
{
    /// <summary>
    /// 实体类 RecordGrantTreasure  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RecordGrantTreasure
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "RecordGrantTreasure";

        #endregion 

        #region 私有变量

        private int p_recordid;
        private int p_masterid;
        private string p_clientip;
        private DateTime p_collectdate;
        private int p_userid;
        private Int64 p_curgold;
        private Int64 p_addgold;
        private string p_reason;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化RecordGrantTreasure
        /// </summary>
        public RecordGrantTreasure() 
        {
            p_recordid = 0;
            p_masterid = 0;
            p_clientip = string.Empty;
            p_collectdate = DateTime.Now;
            p_userid = 0;
            p_curgold = 0;
            p_addgold = 0;
            p_reason = string.Empty;
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
        /// MasterID
        /// </summary>
        public int MasterID
        {
            set
            {
                p_masterid=value;
            }
            get
            {
                return p_masterid;
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
        /// CurGold
        /// </summary>
        public Int64 CurGold
        {
            set
            {
                p_curgold=value;
            }
            get
            {
                return p_curgold;
            }
        }

        /// <summary>
        /// AddGold
        /// </summary>
        public Int64 AddGold
        {
            set
            {
                p_addgold=value;
            }
            get
            {
                return p_addgold;
            }
        }

        /// <summary>
        /// Reason
        /// </summary>
        public string Reason
        {
            set
            {
                p_reason=value;
            }
            get
            {
                return p_reason;
            }
        }

        #endregion
    }
}

