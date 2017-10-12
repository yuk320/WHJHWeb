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
    /// 实体类 RecordAccountsExpend  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RecordAccountsExpend
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "RecordAccountsExpend";

        #endregion 

        #region 私有变量

        private int p_recordid;
        private int p_opermasterid;
        private int p_userid;
        private string p_reaccounts;
        private byte p_type;
        private string p_clientip;
        private DateTime p_collectdate;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化RecordAccountsExpend
        /// </summary>
        public RecordAccountsExpend() 
        {
            p_recordid = 0;
            p_opermasterid = 0;
            p_userid = 0;
            p_reaccounts = string.Empty;
            p_type = 0;
            p_clientip = string.Empty;
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
        /// OperMasterID
        /// </summary>
        public int OperMasterID
        {
            set
            {
                p_opermasterid=value;
            }
            get
            {
                return p_opermasterid;
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
        /// ReAccounts
        /// </summary>
        public string ReAccounts
        {
            set
            {
                p_reaccounts=value;
            }
            get
            {
                return p_reaccounts;
            }
        }

        /// <summary>
        /// Type
        /// </summary>
        public byte Type
        {
            set
            {
                p_type=value;
            }
            get
            {
                return p_type;
            }
        }

        /// <summary>
        /// ClientIP
        /// </summary>
        public string ClientIP
        {
            set
            {
                p_clientip=value;
            }
            get
            {
                return p_clientip;
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

