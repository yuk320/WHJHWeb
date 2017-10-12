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
    /// 实体类 SensitiveWords  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class SensitiveWords
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "SensitiveWords";

        #endregion 

        #region 私有变量

        private string p_forbidwords;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化SensitiveWords
        /// </summary>
        public SensitiveWords() 
        {
            p_forbidwords = string.Empty;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// ForbidWords
        /// </summary>
        public string ForbidWords
        {
            set
            {
                p_forbidwords=value;
            }
            get
            {
                return p_forbidwords;
            }
        }

        #endregion
    }
}

