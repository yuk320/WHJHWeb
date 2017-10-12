/*
 * 版本： 4.0
 * 日期：2017/8/7 10:50:43
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Platform
{
    /// <summary>
    /// 实体类 GameNodeItem  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class GameNodeItem
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "GameNodeItem";

        #endregion 

        #region 私有变量

        private int p_nodeid;
        private int p_kindid;
        private int p_joinid;
        private int p_sortid;
        private string p_nodename;
        private byte p_nullity;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化GameNodeItem
        /// </summary>
        public GameNodeItem() 
        {
            p_nodeid = 0;
            p_kindid = 0;
            p_joinid = 0;
            p_sortid = 0;
            p_nodename = string.Empty;
            p_nullity = 0;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// NodeID
        /// </summary>
        public int NodeID
        {
            set
            {
                p_nodeid=value;
            }
            get
            {
                return p_nodeid;
            }
        }

        /// <summary>
        /// KindID
        /// </summary>
        public int KindID
        {
            set
            {
                p_kindid=value;
            }
            get
            {
                return p_kindid;
            }
        }

        /// <summary>
        /// JoinID
        /// </summary>
        public int JoinID
        {
            set
            {
                p_joinid=value;
            }
            get
            {
                return p_joinid;
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
        /// NodeName
        /// </summary>
        public string NodeName
        {
            set
            {
                p_nodename=value;
            }
            get
            {
                return p_nodename;
            }
        }

        /// <summary>
        /// Nullity
        /// </summary>
        public byte Nullity
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

        #endregion
    }
}

