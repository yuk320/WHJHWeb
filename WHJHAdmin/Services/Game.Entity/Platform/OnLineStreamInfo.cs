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
    /// 实体类 OnLineStreamInfo  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class OnLineStreamInfo
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "OnLineStreamInfo";

        #endregion 

        #region 私有变量

        private int p_id;
        private string p_machineid;
        private string p_machineserver;
        private DateTime p_insertdatetime;
        private int p_onlinecountsum;
        private int p_androidcountsum;
        private string p_onlinecountkind;
        private string p_androidcountkind;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化OnLineStreamInfo
        /// </summary>
        public OnLineStreamInfo() 
        {
            p_id = 0;
            p_machineid = string.Empty;
            p_machineserver = string.Empty;
            p_insertdatetime = DateTime.Now;
            p_onlinecountsum = 0;
            p_androidcountsum = 0;
            p_onlinecountkind = string.Empty;
            p_androidcountkind = string.Empty;
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
        /// MachineID
        /// </summary>
        public string MachineID
        {
            set
            {
                p_machineid=value;
            }
            get
            {
                return p_machineid;
            }
        }

        /// <summary>
        /// MachineServer
        /// </summary>
        public string MachineServer
        {
            set
            {
                p_machineserver=value;
            }
            get
            {
                return p_machineserver;
            }
        }

        /// <summary>
        /// InsertDateTime
        /// </summary>
        public DateTime InsertDateTime
        {
            set
            {
                p_insertdatetime=value;
            }
            get
            {
                return p_insertdatetime;
            }
        }

        /// <summary>
        /// OnLineCountSum
        /// </summary>
        public int OnLineCountSum
        {
            set
            {
                p_onlinecountsum=value;
            }
            get
            {
                return p_onlinecountsum;
            }
        }

        /// <summary>
        /// AndroidCountSum
        /// </summary>
        public int AndroidCountSum
        {
            set
            {
                p_androidcountsum=value;
            }
            get
            {
                return p_androidcountsum;
            }
        }

        /// <summary>
        /// OnLineCountKind
        /// </summary>
        public string OnLineCountKind
        {
            set
            {
                p_onlinecountkind=value;
            }
            get
            {
                return p_onlinecountkind;
            }
        }

        /// <summary>
        /// AndroidCountKind
        /// </summary>
        public string AndroidCountKind
        {
            set
            {
                p_androidcountkind=value;
            }
            get
            {
                return p_androidcountkind;
            }
        }

        #endregion
    }
}

