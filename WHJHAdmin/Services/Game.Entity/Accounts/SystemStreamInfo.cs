/*
 * 版本： 4.0
 * 日期：2017/9/14 9:55:37
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Accounts
{
    /// <summary>
    /// 实体类 SystemStreamInfo  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class SystemStreamInfo
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "SystemStreamInfo";

        #endregion 

        #region 私有变量

        private int p_dateid;
        private int p_weblogonsuccess;
        private int p_webregistersuccess;
        private int p_gamelogonsuccess;
        private int p_gameregistersuccess;
        private DateTime p_collectdate;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化SystemStreamInfo
        /// </summary>
        public SystemStreamInfo() 
        {
            p_dateid = 0;
            p_weblogonsuccess = 0;
            p_webregistersuccess = 0;
            p_gamelogonsuccess = 0;
            p_gameregistersuccess = 0;
            p_collectdate = DateTime.Now;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// DateID
        /// </summary>
        public int DateID
        {
            set
            {
                p_dateid=value;
            }
            get
            {
                return p_dateid;
            }
        }

        /// <summary>
        /// WebLogonSuccess
        /// </summary>
        public int WebLogonSuccess
        {
            set
            {
                p_weblogonsuccess=value;
            }
            get
            {
                return p_weblogonsuccess;
            }
        }

        /// <summary>
        /// WebRegisterSuccess
        /// </summary>
        public int WebRegisterSuccess
        {
            set
            {
                p_webregistersuccess=value;
            }
            get
            {
                return p_webregistersuccess;
            }
        }

        /// <summary>
        /// GameLogonSuccess
        /// </summary>
        public int GameLogonSuccess
        {
            set
            {
                p_gamelogonsuccess=value;
            }
            get
            {
                return p_gamelogonsuccess;
            }
        }

        /// <summary>
        /// GameRegisterSuccess
        /// </summary>
        public int GameRegisterSuccess
        {
            set
            {
                p_gameregistersuccess=value;
            }
            get
            {
                return p_gameregistersuccess;
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

