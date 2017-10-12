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
    /// 实体类 GamePageItem  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class GamePageItem
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "GamePageItem";

        #endregion 

        #region 私有变量

        private int p_pageid;
        private int p_kindid;
        private int p_nodeid;
        private int p_sortid;
        private int p_operatetype;
        private string p_displayname;
        private string p_responseurl;
        private byte p_nullity;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化GamePageItem
        /// </summary>
        public GamePageItem() 
        {
            p_pageid = 0;
            p_kindid = 0;
            p_nodeid = 0;
            p_sortid = 0;
            p_operatetype = 0;
            p_displayname = string.Empty;
            p_responseurl = string.Empty;
            p_nullity = 0;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// PageID
        /// </summary>
        public int PageID
        {
            set
            {
                p_pageid=value;
            }
            get
            {
                return p_pageid;
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
        /// OperateType
        /// </summary>
        public int OperateType
        {
            set
            {
                p_operatetype=value;
            }
            get
            {
                return p_operatetype;
            }
        }

        /// <summary>
        /// DisplayName
        /// </summary>
        public string DisplayName
        {
            set
            {
                p_displayname=value;
            }
            get
            {
                return p_displayname;
            }
        }

        /// <summary>
        /// ResponseUrl
        /// </summary>
        public string ResponseUrl
        {
            set
            {
                p_responseurl=value;
            }
            get
            {
                return p_responseurl;
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

