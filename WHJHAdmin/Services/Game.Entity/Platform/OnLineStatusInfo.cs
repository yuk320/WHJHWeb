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
    /// 实体类 OnLineStatusInfo  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class OnLineStatusInfo
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "OnLineStatusInfo";

        #endregion 

        #region 私有变量

        private int p_kindid;
        private int p_serverid;
        private int p_onlinecount;
        private DateTime p_insertdatetime;
        private DateTime p_modifydatetime;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化OnLineStatusInfo
        /// </summary>
        public OnLineStatusInfo() 
        {
            p_kindid = 0;
            p_serverid = 0;
            p_onlinecount = 0;
            p_insertdatetime = DateTime.Now;
            p_modifydatetime = DateTime.Now;
        }

        #endregion

        #region 公共属性 

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
        /// OnLineCount
        /// </summary>
        public int OnLineCount
        {
            set
            {
                p_onlinecount=value;
            }
            get
            {
                return p_onlinecount;
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
        /// ModifyDateTime
        /// </summary>
        public DateTime ModifyDateTime
        {
            set
            {
                p_modifydatetime=value;
            }
            get
            {
                return p_modifydatetime;
            }
        }

        #endregion
    }
}

