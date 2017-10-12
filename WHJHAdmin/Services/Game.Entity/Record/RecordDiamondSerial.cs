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
    /// 实体类 RecordDiamondSerial  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RecordDiamondSerial
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "RecordDiamondSerial";

        #endregion 

        #region 私有变量

        private string p_serialnumber;
        private int p_masterid;
        private int p_userid;
        private int p_typeid;
        private Int64 p_curdiamond;
        private int p_changediamond;
        private string p_clientip;
        private DateTime p_collectdate;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化RecordDiamondSerial
        /// </summary>
        public RecordDiamondSerial() 
        {
            p_serialnumber = string.Empty;
            p_masterid = 0;
            p_userid = 0;
            p_typeid = 0;
            p_curdiamond = 0;
            p_changediamond = 0;
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
        /// ChangeDiamond
        /// </summary>
        public int ChangeDiamond
        {
            set
            {
                p_changediamond=value;
            }
            get
            {
                return p_changediamond;
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

