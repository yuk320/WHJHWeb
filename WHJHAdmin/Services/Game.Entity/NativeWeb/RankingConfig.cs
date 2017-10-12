/*
 * 版本： 4.0
 * 日期：2017/8/7 10:50:29
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.NativeWeb
{
    /// <summary>
    /// 实体类 RankingConfig  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RankingConfig
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "RankingConfig";

        #endregion 

        #region 私有变量

        private int p_configid;
        private byte p_typeid;
        private int p_rankid;
        private int p_diamond;
        private int p_validitytime;
        private DateTime p_updatetime;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化RankingConfig
        /// </summary>
        public RankingConfig() 
        {
            p_configid = 0;
            p_typeid = 0;
            p_rankid = 0;
            p_diamond = 0;
            p_validitytime = 0;
            p_updatetime = DateTime.Now;
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
        /// TypeID
        /// </summary>
        public byte TypeID
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
        /// RankID
        /// </summary>
        public int RankID
        {
            set
            {
                p_rankid=value;
            }
            get
            {
                return p_rankid;
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
        /// ValidityTime
        /// </summary>
        public int ValidityTime
        {
            set
            {
                p_validitytime=value;
            }
            get
            {
                return p_validitytime;
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

