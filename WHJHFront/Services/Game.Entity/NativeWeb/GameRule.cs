/*
 * 版本： 4.0
 * 日期：2017/8/28 10:06:58
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.NativeWeb
{
    /// <summary>
    /// 实体类 GameRule  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class GameRule
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "GameRule";

        #endregion 

        #region 私有变量

        private int p_kindid;
        private string p_kindname;
        private string p_kindicon;
        private string p_kindintro;
        private string p_kindrule;
        private byte p_nullity;
        private int p_sortid;
        private DateTime p_collectdate;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化GameRule
        /// </summary>
        public GameRule() 
        {
            p_kindid = 0;
            p_kindname = string.Empty;
            p_kindicon = string.Empty;
            p_kindintro = string.Empty;
            p_kindrule = string.Empty;
            p_nullity = 0;
            p_sortid = 0;
            p_collectdate = DateTime.Now;
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
        /// KindIcon
        /// </summary>
        public string KindIcon
        {
            set
            {
                p_kindicon=value;
            }
            get
            {
                return p_kindicon;
            }
        }

        /// <summary>
        /// KindIntro
        /// </summary>
        public string KindIntro
        {
            set
            {
                p_kindintro=value;
            }
            get
            {
                return p_kindintro;
            }
        }

        /// <summary>
        /// KindRule
        /// </summary>
        public string KindRule
        {
            set
            {
                p_kindrule=value;
            }
            get
            {
                return p_kindrule;
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

        /// <summary>
        /// 玩法排序
        /// </summary>
        public int SortID
        {
            set { p_sortid = value; }
            get { return p_nullity; }
        }

        /// <summary>
        /// CollectDate
        /// </summary>
        public DateTime CollectDate
        {
            set
            {
                p_collectdate=value;
            }
            get
            {
                return p_collectdate;
            }
        }

        #endregion
    }
}

