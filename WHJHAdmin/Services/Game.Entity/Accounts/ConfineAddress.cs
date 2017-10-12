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
    /// 实体类 ConfineAddress  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ConfineAddress
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "ConfineAddress";

        #endregion 

        #region 私有变量

        private string p_addrstring;
        private bool p_enjoinlogon;
        private bool p_enjoinregister;
        private DateTime? p_enjoinoverdate;
        private DateTime p_collectdate;
        private string p_collectnote;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化ConfineAddress
        /// </summary>
        public ConfineAddress() 
        {
            p_addrstring = string.Empty;
            p_enjoinlogon = false;
            p_enjoinregister = false;
            p_enjoinoverdate = null;
            p_collectdate = DateTime.Now;
            p_collectnote = string.Empty;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// AddrString
        /// </summary>
        public string AddrString
        {
            set
            {
                p_addrstring=value;
            }
            get
            {
                return p_addrstring;
            }
        }

        /// <summary>
        /// EnjoinLogon
        /// </summary>
        public bool EnjoinLogon
        {
            set
            {
                p_enjoinlogon=value;
            }
            get
            {
                return p_enjoinlogon;
            }
        }

        /// <summary>
        /// EnjoinRegister
        /// </summary>
        public bool EnjoinRegister
        {
            set
            {
                p_enjoinregister=value;
            }
            get
            {
                return p_enjoinregister;
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

        /// <summary>
        /// CollectNote
        /// </summary>
        public string CollectNote
        {
            set
            {
                p_collectnote=value;
            }
            get
            {
                return p_collectnote;
            }
        }

        #endregion
    }
}

