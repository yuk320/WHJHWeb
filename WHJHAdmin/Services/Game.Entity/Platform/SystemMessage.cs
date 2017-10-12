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
    /// 实体类 SystemMessage  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class SystemMessage
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "SystemMessage";

        #endregion 

        #region 私有变量

        private int p_id;
        private int p_messagetype;
        private string p_serverrange;
        private string p_messagestring;
        private DateTime p_starttime;
        private DateTime p_concludetime;
        private int p_timerate;
        private byte p_nullity;
        private DateTime p_createdate;
        private int p_createmasterid;
        private DateTime p_updatedate;
        private int p_updatemasterid;
        private int p_updatecount;
        private string p_collectnote;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化SystemMessage
        /// </summary>
        public SystemMessage() 
        {
            p_id = 0;
            p_messagetype = 0;
            p_serverrange = string.Empty;
            p_messagestring = string.Empty;
            p_starttime = DateTime.Now;
            p_concludetime = DateTime.Now;
            p_timerate = 0;
            p_nullity = 0;
            p_createdate = DateTime.Now;
            p_createmasterid = 0;
            p_updatedate = DateTime.Now;
            p_updatemasterid = 0;
            p_updatecount = 0;
            p_collectnote = string.Empty;
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
        /// MessageType
        /// </summary>
        public int MessageType
        {
            set
            {
                p_messagetype=value;
            }
            get
            {
                return p_messagetype;
            }
        }

        /// <summary>
        /// ServerRange
        /// </summary>
        public string ServerRange
        {
            set
            {
                p_serverrange=value;
            }
            get
            {
                return p_serverrange;
            }
        }

        /// <summary>
        /// MessageString
        /// </summary>
        public string MessageString
        {
            set
            {
                p_messagestring=value;
            }
            get
            {
                return p_messagestring;
            }
        }

        /// <summary>
        /// StartTime
        /// </summary>
        public DateTime StartTime
        {
            set
            {
                p_starttime=value;
            }
            get
            {
                return p_starttime;
            }
        }

        /// <summary>
        /// ConcludeTime
        /// </summary>
        public DateTime ConcludeTime
        {
            set
            {
                p_concludetime=value;
            }
            get
            {
                return p_concludetime;
            }
        }

        /// <summary>
        /// TimeRate
        /// </summary>
        public int TimeRate
        {
            set
            {
                p_timerate=value;
            }
            get
            {
                return p_timerate;
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
        /// CreateDate
        /// </summary>
        public DateTime CreateDate
        {
            set
            {
                p_createdate=value;
            }
            get
            {
                return p_createdate;
            }
        }

        /// <summary>
        /// CreateMasterID
        /// </summary>
        public int CreateMasterID
        {
            set
            {
                p_createmasterid=value;
            }
            get
            {
                return p_createmasterid;
            }
        }

        /// <summary>
        /// UpdateDate
        /// </summary>
        public DateTime UpdateDate
        {
            set
            {
                p_updatedate=value;
            }
            get
            {
                return p_updatedate;
            }
        }

        /// <summary>
        /// UpdateMasterID
        /// </summary>
        public int UpdateMasterID
        {
            set
            {
                p_updatemasterid=value;
            }
            get
            {
                return p_updatemasterid;
            }
        }

        /// <summary>
        /// UpdateCount
        /// </summary>
        public int UpdateCount
        {
            set
            {
                p_updatecount=value;
            }
            get
            {
                return p_updatecount;
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

