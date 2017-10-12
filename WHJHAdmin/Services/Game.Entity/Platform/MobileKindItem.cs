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
    /// 实体类 MobileKindItem  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class MobileKindItem
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "MobileKindItem";

        #endregion 

        #region 私有变量

        private int p_kindid;
        private string p_kindname;
        private int p_typeid;
        private string p_modulename;
        private int p_clientversion;
        private int p_resversion;
        private int p_sortid;
        private int p_kindmark;
        private byte p_nullity;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化MobileKindItem
        /// </summary>
        public MobileKindItem() 
        {
            p_kindid = 0;
            p_kindname = string.Empty;
            p_typeid = 0;
            p_modulename = string.Empty;
            p_clientversion = 0;
            p_resversion = 0;
            p_sortid = 0;
            p_kindmark = 0;
            p_nullity = 0;
        }

        #endregion

        #region 公共属性 

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
        /// KindName
        /// </summary>
        public string KindName
        {
            set
            {
                p_kindname=value;
            }
            get
            {
                return p_kindname;
            }
        }

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
        /// ModuleName
        /// </summary>
        public string ModuleName
        {
            set
            {
                p_modulename=value;
            }
            get
            {
                return p_modulename;
            }
        }

        /// <summary>
        /// ClientVersion
        /// </summary>
        public int ClientVersion
        {
            set
            {
                p_clientversion=value;
            }
            get
            {
                return p_clientversion;
            }
        }

        /// <summary>
        /// ResVersion
        /// </summary>
        public int ResVersion
        {
            set
            {
                p_resversion=value;
            }
            get
            {
                return p_resversion;
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
        /// KindMark
        /// </summary>
        public int KindMark
        {
            set
            {
                p_kindmark=value;
            }
            get
            {
                return p_kindmark;
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

