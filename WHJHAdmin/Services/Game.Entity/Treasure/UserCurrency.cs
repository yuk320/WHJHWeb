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
    /// 实体类 UserCurrency  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class UserCurrency
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "UserCurrency";

        #endregion 

        #region 私有变量

        private int p_userid;
        private Int64 p_diamond;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化UserCurrency
        /// </summary>
        public UserCurrency() 
        {
            p_userid = 0;
            p_diamond = 0;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// UserID
        /// </summary>
        public int UserID
        {
            set
            {
                p_userid=value;
            }
            get
            {
                return p_userid;
            }
        }

        /// <summary>
        /// Diamond
        /// </summary>
        public Int64 Diamond
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

        #endregion
    }
}

