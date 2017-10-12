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
    /// 实体类 RecordEveryDayCurrency  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RecordEveryDayCurrency
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "RecordEveryDayCurrency";

        #endregion 

        #region 私有变量

        private int p_dateid;
        private Int64 p_firstdiamond;
        private Int64 p_seconddiamond;
        private Int64 p_thirddiamond;
        private Int64 p_totaldiamond;
        private Int64 p_syspresentdiamond;
        private Int64 p_adminpresentdiamond;
        private Int64 p_paydiamond;
        private Int64 p_roomcostdiamond;
        private Int64 p_propcostdiamond;
        private Int64 p_aagcostdiamond;
        private DateTime p_collectdate;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化RecordEveryDayCurrency
        /// </summary>
        public RecordEveryDayCurrency() 
        {
            p_dateid = 0;
            p_firstdiamond = 0;
            p_seconddiamond = 0;
            p_thirddiamond = 0;
            p_totaldiamond = 0;
            p_syspresentdiamond = 0;
            p_adminpresentdiamond = 0;
            p_paydiamond = 0;
            p_roomcostdiamond = 0;
            p_propcostdiamond = 0;
            p_aagcostdiamond = 0;
            p_collectdate = DateTime.Now;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// DateID
        /// </summary>
        public int DateID
        {
            set
            {
                p_dateid=value;
            }
            get
            {
                return p_dateid;
            }
        }

        /// <summary>
        /// FirstDiamond
        /// </summary>
        public Int64 FirstDiamond
        {
            set
            {
                p_firstdiamond=value;
            }
            get
            {
                return p_firstdiamond;
            }
        }

        /// <summary>
        /// SecondDiamond
        /// </summary>
        public Int64 SecondDiamond
        {
            set
            {
                p_seconddiamond=value;
            }
            get
            {
                return p_seconddiamond;
            }
        }

        /// <summary>
        /// ThirdDiamond
        /// </summary>
        public Int64 ThirdDiamond
        {
            set
            {
                p_thirddiamond=value;
            }
            get
            {
                return p_thirddiamond;
            }
        }

        /// <summary>
        /// TotalDiamond
        /// </summary>
        public Int64 TotalDiamond
        {
            set
            {
                p_totaldiamond=value;
            }
            get
            {
                return p_totaldiamond;
            }
        }

        /// <summary>
        /// SysPresentDiamond
        /// </summary>
        public Int64 SysPresentDiamond
        {
            set
            {
                p_syspresentdiamond=value;
            }
            get
            {
                return p_syspresentdiamond;
            }
        }

        /// <summary>
        /// AdminPresentDiamond
        /// </summary>
        public Int64 AdminPresentDiamond
        {
            set
            {
                p_adminpresentdiamond=value;
            }
            get
            {
                return p_adminpresentdiamond;
            }
        }

        /// <summary>
        /// PayDiamond
        /// </summary>
        public Int64 PayDiamond
        {
            set
            {
                p_paydiamond=value;
            }
            get
            {
                return p_paydiamond;
            }
        }

        /// <summary>
        /// RoomCostDiamond
        /// </summary>
        public Int64 RoomCostDiamond
        {
            set
            {
                p_roomcostdiamond=value;
            }
            get
            {
                return p_roomcostdiamond;
            }
        }

        /// <summary>
        /// PropCostDiamond
        /// </summary>
        public Int64 PropCostDiamond
        {
            set
            {
                p_propcostdiamond=value;
            }
            get
            {
                return p_propcostdiamond;
            }
        }

        /// <summary>
        /// AAGCostDiamond
        /// </summary>
        public Int64 AAGCostDiamond
        {
            set
            {
                p_aagcostdiamond=value;
            }
            get
            {
                return p_aagcostdiamond;
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

