/*
 * 版本： 4.0
 * 日期：2017/9/15 10:17:15
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Record
{
    /// <summary>
    /// 实体类 RecordTreasureSerial  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RecordTreasureSerial
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "RecordTreasureSerial";

        #endregion 

        #region 私有变量

        private string p_serialnumber;
        private int p_masterid;
        private int p_userid;
        private int p_typeid;
        private Int64 p_curscore;
        private Int64 p_curinsurescore;
        private int p_changescore;
        private string p_clientip;
        private DateTime p_collectdate;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化RecordTreasureSerial
        /// </summary>
        public RecordTreasureSerial() 
        {
            p_serialnumber = string.Empty;
            p_masterid = 0;
            p_userid = 0;
            p_typeid = 0;
            p_curscore = 0;
            p_curinsurescore = 0;
            p_changescore = 0;
            p_clientip = string.Empty;
            p_collectdate = DateTime.Now;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// SerialNumber
        /// </summary>
        public string SerialNumber
        {
            set
            {
                p_serialnumber=value;
            }
            get
            {
                return p_serialnumber;
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
        /// TypeID
        /// </summary>
        public int TypeID
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
        /// ChangeScore
        /// </summary>
        public int ChangeScore
        {
            set
            {
                p_changescore=value;
            }
            get
            {
                return p_changescore;
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

