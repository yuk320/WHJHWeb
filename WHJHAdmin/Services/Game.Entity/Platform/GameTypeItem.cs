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
    /// 实体类 GameTypeItem  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class GameTypeItem
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "GameTypeItem";

        #endregion 

        #region 私有变量

        private int p_typeid;
        private int p_joinid;
        private int p_sortid;
        private string p_typename;
        private byte p_nullity;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化GameTypeItem
        /// </summary>
        public GameTypeItem() 
        {
            p_typeid = 0;
            p_joinid = 0;
            p_sortid = 0;
            p_typename = string.Empty;
            p_nullity = 0;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// TypeID
        /// </summary>
        public int TypeID
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
        /// TypeName
        /// </summary>
        public string TypeName
        {
            set
            {
                p_typename=value;
            }
            get
            {
                return p_typename;
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

