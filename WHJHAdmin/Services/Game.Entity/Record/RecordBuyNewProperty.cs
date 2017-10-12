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
    /// 实体类 RecordBuyNewProperty  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RecordBuyNewProperty
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "RecordBuyNewProperty";

        #endregion 

        #region 私有变量

        private int p_recordid;
        private int p_userid;
        private int p_propertyid;
        private string p_propertyname;
        private int p_propertyprice;
        private int p_buynum;
        private Int64 p_beforediamond;
        private int p_diamond;
        private string p_clinetip;
        private DateTime p_collectdate;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化RecordBuyNewProperty
        /// </summary>
        public RecordBuyNewProperty() 
        {
            p_recordid = 0;
            p_userid = 0;
            p_propertyid = 0;
            p_propertyname = string.Empty;
            p_propertyprice = 0;
            p_buynum = 0;
            p_beforediamond = 0;
            p_diamond = 0;
            p_clinetip = string.Empty;
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
        /// PropertyID
        /// </summary>
        public int PropertyID
        {
            set
            {
                p_propertyid=value;
            }
            get
            {
                return p_propertyid;
            }
        }

        /// <summary>
        /// PropertyName
        /// </summary>
        public string PropertyName
        {
            set
            {
                p_propertyname=value;
            }
            get
            {
                return p_propertyname;
            }
        }

        /// <summary>
        /// PropertyPrice
        /// </summary>
        public int PropertyPrice
        {
            set
            {
                p_propertyprice=value;
            }
            get
            {
                return p_propertyprice;
            }
        }

        /// <summary>
        /// BuyNum
        /// </summary>
        public int BuyNum
        {
            set
            {
                p_buynum=value;
            }
            get
            {
                return p_buynum;
            }
        }

        /// <summary>
        /// BeforeDiamond
        /// </summary>
        public Int64 BeforeDiamond
        {
            set
            {
                p_beforediamond=value;
            }
            get
            {
                return p_beforediamond;
            }
        }

        /// <summary>
        /// Diamond
        /// </summary>
        public int Diamond
        {
            set
            {
                p_diamond=value;
            }
            get
            {
                return p_diamond;
            }
        }

        /// <summary>
        /// ClinetIP
        /// </summary>
        public string ClinetIP
        {
            set
            {
                p_clinetip=value;
            }
            get
            {
                return p_clinetip;
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

