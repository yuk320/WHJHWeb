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
    /// 实体类 SpreadConfig  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class SpreadConfig
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "SpreadConfig";

        #endregion 

        #region 私有变量

        private int p_configid;
        private int p_spreadnum;
        private int p_presentdiamond;
        private int p_presentpropid;
        private string p_presentpropname;
        private int p_presentpropnum;
        private DateTime p_updatetime;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化SpreadConfig
        /// </summary>
        public SpreadConfig() 
        {
            p_configid = 0;
            p_spreadnum = 0;
            p_presentdiamond = 0;
            p_presentpropid = 0;
            p_presentpropname = string.Empty;
            p_presentpropnum = 0;
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
        /// SpreadNum
        /// </summary>
        public int SpreadNum
        {
            set
            {
                p_spreadnum=value;
            }
            get
            {
                return p_spreadnum;
            }
        }

        /// <summary>
        /// PresentDiamond
        /// </summary>
        public int PresentDiamond
        {
            set
            {
                p_presentdiamond=value;
            }
            get
            {
                return p_presentdiamond;
            }
        }

        /// <summary>
        /// PresentPropID
        /// </summary>
        public int PresentPropID
        {
            set
            {
                p_presentpropid=value;
            }
            get
            {
                return p_presentpropid;
            }
        }

        /// <summary>
        /// PresentPropName
        /// </summary>
        public string PresentPropName
        {
            set
            {
                p_presentpropname=value;
            }
            get
            {
                return p_presentpropname;
            }
        }

        /// <summary>
        /// PresentPropNum
        /// </summary>
        public int PresentPropNum
        {
            set
            {
                p_presentpropnum=value;
            }
            get
            {
                return p_presentpropnum;
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

