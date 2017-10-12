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
    /// 实体类 AccountsFace  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class AccountsFace
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "AccountsFace";

        #endregion 

        #region 私有变量

        private int p_id;
        private int p_userid;
        private byte[] p_customface;
        private DateTime p_inserttime;
        private string p_insertaddr;
        private string p_insertmachine;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化AccountsFace
        /// </summary>
        public AccountsFace() 
        {
            p_id = 0;
            p_userid = 0;
            p_customface = null;
            p_inserttime = DateTime.Now;
            p_insertaddr = string.Empty;
            p_insertmachine = string.Empty;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            set
            {
                p_id=value;
            }
            get
            {
                return p_id;
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
        /// CustomFace
        /// </summary>
        public byte[] CustomFace
        {
            set
            {
                p_customface=value;
            }
            get
            {
                return p_customface;
            }
        }

        /// <summary>
        /// InsertTime
        /// </summary>
        public DateTime InsertTime
        {
            set
            {
                p_inserttime=value;
            }
            get
            {
                return p_inserttime;
            }
        }

        /// <summary>
        /// InsertAddr
        /// </summary>
        public string InsertAddr
        {
            set
            {
                p_insertaddr=value;
            }
            get
            {
                return p_insertaddr;
            }
        }

        /// <summary>
        /// InsertMachine
        /// </summary>
        public string InsertMachine
        {
            set
            {
                p_insertmachine=value;
            }
            get
            {
                return p_insertmachine;
            }
        }

        #endregion
    }
}

