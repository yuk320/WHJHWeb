/*
 * 版本： 4.0
 * 日期：2017/8/7 10:49:53
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Accounts
{
    /// <summary>
    /// 实体类 ConfineContent  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ConfineContent
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "ConfineContent";

        #endregion 

        #region 私有变量

        private int p_contentid;
        private string p_string;
        private DateTime? p_enjoinoverdate;
        private DateTime p_collectdate;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化ConfineContent
        /// </summary>
        public ConfineContent() 
        {
            p_contentid = 0;
            p_string = string.Empty;
            p_enjoinoverdate = null;
            p_collectdate = DateTime.Now;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// ContentID
        /// </summary>
        public int ContentID
        {
            set
            {
                p_contentid=value;
            }
            get
            {
                return p_contentid;
            }
        }

        /// <summary>
        /// String
        /// </summary>
        public string String
        {
            set
            {
                p_string=value;
            }
            get
            {
                return p_string;
            }
        }

        /// <summary>
        /// EnjoinOverDate
        /// </summary>
        public DateTime? EnjoinOverDate
        {
            set
            {
                p_enjoinoverdate=value;
            }
            get
            {
                return p_enjoinoverdate;
            }
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

