/*
 * 版本： 4.0
 * 日期：2017/8/7 10:49:53
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Accounts
{
    /// <summary>
    /// 实体类 SystemStatusInfo  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class SystemStatusInfo
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "SystemStatusInfo";

        #endregion 

        #region 私有变量

        private string p_statusname;
        private int p_statusvalue;
        private string p_statusstring;
        private string p_statustip;
        private string p_statusdescription;
        private int p_sortid;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化SystemStatusInfo
        /// </summary>
        public SystemStatusInfo() 
        {
            p_statusname = string.Empty;
            p_statusvalue = 0;
            p_statusstring = string.Empty;
            p_statustip = string.Empty;
            p_statusdescription = string.Empty;
            p_sortid = 0;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// StatusName
        /// </summary>
        public string StatusName
        {
            set
            {
                p_statusname=value;
            }
            get
            {
                return p_statusname;
            }
        }

        /// <summary>
        /// StatusValue
        /// </summary>
        public int StatusValue
        {
            set
            {
                p_statusvalue=value;
            }
            get
            {
                return p_statusvalue;
            }
        }

        /// <summary>
        /// StatusString
        /// </summary>
        public string StatusString
        {
            set
            {
                p_statusstring=value;
            }
            get
            {
                return p_statusstring;
            }
        }

        /// <summary>
        /// StatusTip
        /// </summary>
        public string StatusTip
        {
            set
            {
                p_statustip=value;
            }
            get
            {
                return p_statustip;
            }
        }

        /// <summary>
        /// StatusDescription
        /// </summary>
        public string StatusDescription
        {
            set
            {
                p_statusdescription=value;
            }
            get
            {
                return p_statusdescription;
            }
        }

        /// <summary>
        /// SortID
        /// </summary>
        public int SortID
        {
            set
            {
                p_sortid=value;
            }
            get
            {
                return p_sortid;
            }
        }

        #endregion
    }
}

