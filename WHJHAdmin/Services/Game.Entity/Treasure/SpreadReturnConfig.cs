/*
 * 版本： 4.0
 * 日期：2017/11/13 16:51:24
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Data;

namespace Game.Entity.Treasure
{
    /// <summary>
    /// 实体类 SpreadReturnConfig  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class SpreadReturnConfig
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "SpreadReturnConfig";

        #endregion 

        #region 私有变量

        private int p_configid;
        private int p_spreadlevel;
        private decimal p_presentscale;
        private bool p_nullity;
        private DateTime p_updatetime;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化SpreadConfig
        /// </summary>
        public SpreadReturnConfig() 
        {
            p_configid = 0;
            p_spreadlevel = 0;
            p_presentscale = 0;
            p_nullity = false;
            p_updatetime = DateTime.Now;
        }

        public SpreadReturnConfig(DataRow dr)
        {
            if (dr["ConfigID"] != null) p_configid = Convert.ToInt32(dr["ConfigID"]);
            if (dr["SpreadLevel"] != null) p_spreadlevel = Convert.ToInt32(dr["SpreadLevel"]);
            if (dr["PresentScale"] != null) p_presentscale = Convert.ToDecimal(dr["PresentScale"]);
            if (dr["Nullity"] != null) p_nullity = Convert.ToBoolean(dr["Nullity"]);
            if (dr["UpdateTime"] != null) p_updatetime = Convert.ToDateTime(dr["UpdateTime"]);
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
        /// SpreadLevel
        /// </summary>
        public int SpreadLevel
        {
            set
            {
                p_spreadlevel=value;
            }
            get
            {
                return p_spreadlevel;
            }
        }

        /// <summary>
        /// PresentScale
        /// </summary>
        public decimal PresentScale
        {
            set
            {
                p_presentscale=value;
            }
            get
            {
                return p_presentscale;
            }
        }

        /// <summary>
        /// Nullity
        /// </summary>
        public bool Nullity
        {
            set
            {
                p_nullity=value;
            }
            get
            {
                return p_nullity;
            }
        }

        /// <summary>
        /// UpdateTime
        /// </summary>
        public DateTime UpdateTime
        {
            set
            {
                p_updatetime=value;
            }
            get
            {
                return p_updatetime;
            }
        }

        #endregion
    }
}

