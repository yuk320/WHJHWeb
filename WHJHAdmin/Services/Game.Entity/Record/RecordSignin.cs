/*
 * 版本： 4.0
 * 日期：2017/8/7 10:51:13
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Record
{
    /// <summary>
    /// 实体类 RecordSignin  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RecordSignin
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "RecordSignin";

        #endregion 

        #region 私有变量

        private int p_recordid;
        private int p_userid;
        private int p_gold;
        private string p_clinetip;
        private string p_clinetmachine;
        private DateTime p_collectdate;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化RecordSignin
        /// </summary>
        public RecordSignin() 
        {
            p_recordid = 0;
            p_userid = 0;
            p_gold = 0;
            p_clinetip = string.Empty;
            p_clinetmachine = string.Empty;
            p_collectdate = DateTime.Now;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// RecordID
        /// </summary>
        public int RecordID
        {
            set
            {
                p_recordid=value;
            }
            get
            {
                return p_recordid;
            }
        }

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
        /// Gold
        /// </summary>
        public int Gold
        {
            set
            {
                p_gold=value;
            }
            get
            {
                return p_gold;
            }
        }

        /// <summary>
        /// ClinetIP
        /// </summary>
        public string ClinetIP
        {
            set
            {
                p_clinetip=value;
            }
            get
            {
                return p_clinetip;
            }
        }

        /// <summary>
        /// ClinetMachine
        /// </summary>
        public string ClinetMachine
        {
            set
            {
                p_clinetmachine=value;
            }
            get
            {
                return p_clinetmachine;
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

