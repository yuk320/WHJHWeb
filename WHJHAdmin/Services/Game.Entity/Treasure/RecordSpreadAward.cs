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
    /// 实体类 RecordSpreadAward  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RecordSpreadAward
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "RecordSpreadAward";

        #endregion 

        #region 私有变量

        private int p_recordid;
        private int p_userid;
        private int p_usernum;
        private int p_configid;
        private int p_spreadnum;
        private Int64 p_currentdiamond;
        private int p_presentdiamond;
        private int p_presentpropid;
        private string p_presentpropname;
        private int p_presentpropnum;
        private string p_clientip;
        private DateTime p_collectdate;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化RecordSpreadAward
        /// </summary>
        public RecordSpreadAward() 
        {
            p_recordid = 0;
            p_userid = 0;
            p_usernum = 0;
            p_configid = 0;
            p_spreadnum = 0;
            p_currentdiamond = 0;
            p_presentdiamond = 0;
            p_presentpropid = 0;
            p_presentpropname = string.Empty;
            p_presentpropnum = 0;
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
        /// UserNum
        /// </summary>
        public int UserNum
        {
            set
            {
                p_usernum=value;
            }
            get
            {
                return p_usernum;
            }
        }

        /// <summary>
        /// ConfigID
        /// </summary>
        public int ConfigID
        {
            set
            {
                p_configid=value;
            }
            get
            {
                return p_configid;
            }
        }

        /// <summary>
        /// SpreadNum
        /// </summary>
        public int SpreadNum
        {
            set
            {
                p_spreadnum=value;
            }
            get
            {
                return p_spreadnum;
            }
        }

        /// <summary>
        /// CurrentDiamond
        /// </summary>
        public Int64 CurrentDiamond
        {
            set
            {
                p_currentdiamond=value;
            }
            get
            {
                return p_currentdiamond;
            }
        }

        /// <summary>
        /// PresentDiamond
        /// </summary>
        public int PresentDiamond
        {
            set
            {
                p_presentdiamond=value;
            }
            get
            {
                return p_presentdiamond;
            }
        }

        /// <summary>
        /// PresentPropID
        /// </summary>
        public int PresentPropID
        {
            set
            {
                p_presentpropid=value;
            }
            get
            {
                return p_presentpropid;
            }
        }

        /// <summary>
        /// PresentPropName
        /// </summary>
        public string PresentPropName
        {
            set
            {
                p_presentpropname=value;
            }
            get
            {
                return p_presentpropname;
            }
        }

        /// <summary>
        /// PresentPropNum
        /// </summary>
        public int PresentPropNum
        {
            set
            {
                p_presentpropnum=value;
            }
            get
            {
                return p_presentpropnum;
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

