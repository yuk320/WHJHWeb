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
    /// 实体类 RecordGrantGameID  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RecordGrantGameID
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "RecordGrantGameID";

        #endregion 

        #region 私有变量

        private int p_recordid;
        private int p_masterid;
        private int p_userid;
        private int p_curgameid;
        private int p_regameid;
        private int p_idlevel;
        private string p_clientip;
        private DateTime p_collectdate;
        private string p_reason;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化RecordGrantGameID
        /// </summary>
        public RecordGrantGameID() 
        {
            p_recordid = 0;
            p_masterid = 0;
            p_userid = 0;
            p_curgameid = 0;
            p_regameid = 0;
            p_idlevel = 0;
            p_clientip = string.Empty;
            p_collectdate = DateTime.Now;
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
        /// CurGameID
        /// </summary>
        public int CurGameID
        {
            set
            {
                p_curgameid=value;
            }
            get
            {
                return p_curgameid;
            }
        }

        /// <summary>
        /// ReGameID
        /// </summary>
        public int ReGameID
        {
            set
            {
                p_regameid=value;
            }
            get
            {
                return p_regameid;
            }
        }

        /// <summary>
        /// IDLevel
        /// </summary>
        public int IDLevel
        {
            set
            {
                p_idlevel=value;
            }
            get
            {
                return p_idlevel;
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

