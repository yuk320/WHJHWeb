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
    /// 实体类 AppPayConfig  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class AppPayConfig
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "AppPayConfig";

        #endregion 

        #region 私有变量

        private int p_configid;
        private string p_appleid;
        private string p_payname;
        private byte p_paytype;
        private decimal p_payprice;
        private byte p_payidentity;
        private byte p_imagetype;
        private int p_sortid;
        private int p_diamond;
        private int p_presentdiamond;
        private DateTime p_configtime;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化AppPayConfig
        /// </summary>
        public AppPayConfig() 
        {
            p_configid = 0;
            p_appleid = string.Empty;
            p_payname = string.Empty;
            p_paytype = 0;
            p_payprice = 0;
            p_payidentity = 0;
            p_imagetype = 0;
            p_sortid = 0;
            p_diamond = 0;
            p_presentdiamond = 0;
            p_configtime = DateTime.Now;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// ConfigID
        /// </summary>
        public int ConfigID
        {
            set
            {
                p_configid=value;
            }
            get
            {
                return p_configid;
            }
        }

        /// <summary>
        /// AppleID
        /// </summary>
        public string AppleID
        {
            set
            {
                p_appleid=value;
            }
            get
            {
                return p_appleid;
            }
        }

        /// <summary>
        /// PayName
        /// </summary>
        public string PayName
        {
            set
            {
                p_payname=value;
            }
            get
            {
                return p_payname;
            }
        }

        /// <summary>
        /// PayType
        /// </summary>
        public byte PayType
        {
            set
            {
                p_paytype=value;
            }
            get
            {
                return p_paytype;
            }
        }

        /// <summary>
        /// PayPrice
        /// </summary>
        public decimal PayPrice
        {
            set
            {
                p_payprice=value;
            }
            get
            {
                return p_payprice;
            }
        }

        /// <summary>
        /// PayIdentity
        /// </summary>
        public byte PayIdentity
        {
            set
            {
                p_payidentity=value;
            }
            get
            {
                return p_payidentity;
            }
        }

        /// <summary>
        /// ImageType
        /// </summary>
        public byte ImageType
        {
            set
            {
                p_imagetype=value;
            }
            get
            {
                return p_imagetype;
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
        /// PresentDiamond
        /// </summary>
        public int PresentDiamond
        {
            set
            {
                p_presentdiamond = value;
            }
            get
            {
                return p_presentdiamond;
            }
        }

        /// <summary>
        /// ConfigTime
        /// </summary>
        public DateTime ConfigTime
        {
            set
            {
                p_configtime=value;
            }
            get
            {
                return p_configtime;
            }
        }

        #endregion
    }
}

