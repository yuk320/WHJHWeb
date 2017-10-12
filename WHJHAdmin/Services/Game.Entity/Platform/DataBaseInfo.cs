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
    /// 实体类 DataBaseInfo  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class DataBaseInfo
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "DataBaseInfo";

        #endregion 

        #region 私有变量

        private int p_dbinfoid;
        private string p_dbaddr;
        private int p_dbport;
        private string p_dbuser;
        private string p_dbpassword;
        private string p_machineid;
        private string p_information;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化DataBaseInfo
        /// </summary>
        public DataBaseInfo() 
        {
            p_dbinfoid = 0;
            p_dbaddr = string.Empty;
            p_dbport = 0;
            p_dbuser = string.Empty;
            p_dbpassword = string.Empty;
            p_machineid = string.Empty;
            p_information = string.Empty;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// DBInfoID
        /// </summary>
        public int DBInfoID
        {
            set
            {
                p_dbinfoid=value;
            }
            get
            {
                return p_dbinfoid;
            }
        }

        /// <summary>
        /// DBAddr
        /// </summary>
        public string DBAddr
        {
            set
            {
                p_dbaddr=value;
            }
            get
            {
                return p_dbaddr;
            }
        }

        /// <summary>
        /// DBPort
        /// </summary>
        public int DBPort
        {
            set
            {
                p_dbport=value;
            }
            get
            {
                return p_dbport;
            }
        }

        /// <summary>
        /// DBUser
        /// </summary>
        public string DBUser
        {
            set
            {
                p_dbuser=value;
            }
            get
            {
                return p_dbuser;
            }
        }

        /// <summary>
        /// DBPassword
        /// </summary>
        public string DBPassword
        {
            set
            {
                p_dbpassword=value;
            }
            get
            {
                return p_dbpassword;
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
        /// Information
        /// </summary>
        public string Information
        {
            set
            {
                p_information=value;
            }
            get
            {
                return p_information;
            }
        }

        #endregion
    }
}

