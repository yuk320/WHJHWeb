/*
 * 版本： 4.0
 * 日期：2017/9/13 15:45:38
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Treasure
{
    /// <summary>
    /// 实体类 CurrencyExchConfig  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class CurrencyExchConfig
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "CurrencyExchConfig";

        #endregion 

        #region 私有变量

        private int p_configid;
        private string p_configname;
        private int p_diamond;
        private Int64 p_exchgold;
        private byte p_imagetype;
        private int p_sortid;
        private DateTime p_configtime;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化CurrencyExchConfig
        /// </summary>
        public CurrencyExchConfig() 
        {
            p_configid = 0;
            p_configname = string.Empty;
            p_diamond = 0;
            p_exchgold = 0;
            p_imagetype = 0;
            p_sortid = 0;
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
        /// ConfigName
        /// </summary>
        public string ConfigName
        {
            set
            {
                p_configname=value;
            }
            get
            {
                return p_configname;
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
        /// ExchGold
        /// </summary>
        public Int64 ExchGold
        {
            set
            {
                p_exchgold=value;
            }
            get
            {
                return p_exchgold;
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

