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
    /// 实体类 RecordDrawInfo  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RecordDrawInfo
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "RecordDrawInfo";

        #endregion 

        #region 私有变量

        private int p_drawid;
        private int p_kindid;
        private int p_serverid;
        private int p_tableid;
        private int p_usercount;
        private int p_androidcount;
        private Int64 p_waste;
        private Int64 p_revenue;
        private DateTime p_starttime;
        private DateTime p_concludetime;
        private DateTime p_inserttime;
        private byte[] p_drawcourse;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化RecordDrawInfo
        /// </summary>
        public RecordDrawInfo() 
        {
            p_drawid = 0;
            p_kindid = 0;
            p_serverid = 0;
            p_tableid = 0;
            p_usercount = 0;
            p_androidcount = 0;
            p_waste = 0;
            p_revenue = 0;
            p_starttime = DateTime.Now;
            p_concludetime = DateTime.Now;
            p_inserttime = DateTime.Now;
            p_drawcourse = null;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// DrawID
        /// </summary>
        public int DrawID
        {
            set
            {
                p_drawid=value;
            }
            get
            {
                return p_drawid;
            }
        }

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
        /// ServerID
        /// </summary>
        public int ServerID
        {
            set
            {
                p_serverid=value;
            }
            get
            {
                return p_serverid;
            }
        }

        /// <summary>
        /// TableID
        /// </summary>
        public int TableID
        {
            set
            {
                p_tableid=value;
            }
            get
            {
                return p_tableid;
            }
        }

        /// <summary>
        /// UserCount
        /// </summary>
        public int UserCount
        {
            set
            {
                p_usercount=value;
            }
            get
            {
                return p_usercount;
            }
        }

        /// <summary>
        /// AndroidCount
        /// </summary>
        public int AndroidCount
        {
            set
            {
                p_androidcount=value;
            }
            get
            {
                return p_androidcount;
            }
        }

        /// <summary>
        /// Waste
        /// </summary>
        public Int64 Waste
        {
            set
            {
                p_waste=value;
            }
            get
            {
                return p_waste;
            }
        }

        /// <summary>
        /// Revenue
        /// </summary>
        public Int64 Revenue
        {
            set
            {
                p_revenue=value;
            }
            get
            {
                return p_revenue;
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
        /// DrawCourse
        /// </summary>
        public byte[] DrawCourse
        {
            set
            {
                p_drawcourse=value;
            }
            get
            {
                return p_drawcourse;
            }
        }

        #endregion
    }
}

