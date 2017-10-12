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
    /// 实体类 AccountsUmeng  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class AccountsUmeng
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "AccountsUmeng";

        #endregion 

        #region 私有变量

        private int p_userid;
        private int p_gameid;
        private byte p_devicetype;
        private string p_devicetoken;
        private DateTime p_updatetime;
        private string p_updateaddress;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化AccountsUmeng
        /// </summary>
        public AccountsUmeng() 
        {
            p_userid = 0;
            p_gameid = 0;
            p_devicetype = 0;
            p_devicetoken = string.Empty;
            p_updatetime = DateTime.Now;
            p_updateaddress = string.Empty;
        }

        #endregion

        #region 公共属性 

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
        /// GameID
        /// </summary>
        public int GameID
        {
            set
            {
                p_gameid=value;
            }
            get
            {
                return p_gameid;
            }
        }

        /// <summary>
        /// DeviceType
        /// </summary>
        public byte DeviceType
        {
            set
            {
                p_devicetype=value;
            }
            get
            {
                return p_devicetype;
            }
        }

        /// <summary>
        /// DeviceToken
        /// </summary>
        public string DeviceToken
        {
            set
            {
                p_devicetoken=value;
            }
            get
            {
                return p_devicetoken;
            }
        }

        /// <summary>
        /// UpdateTime
        /// </summary>
        public DateTime UpdateTime
        {
            set
            {
                p_updatetime=value;
            }
            get
            {
                return p_updatetime;
            }
        }

        /// <summary>
        /// UpdateAddress
        /// </summary>
        public string UpdateAddress
        {
            set
            {
                p_updateaddress=value;
            }
            get
            {
                return p_updateaddress;
            }
        }

        #endregion
    }
}

