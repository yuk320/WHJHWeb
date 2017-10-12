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
    /// 实体类 GameScoreLocker  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class GameScoreLocker
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "GameScoreLocker";

        #endregion 

        #region 私有变量

        private int p_userid;
        private int p_kindid;
        private int p_serverid;
        private int p_enterid;
        private string p_enterip;
        private string p_entermachine;
        private DateTime p_collectdate;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化GameScoreLocker
        /// </summary>
        public GameScoreLocker() 
        {
            p_userid = 0;
            p_kindid = 0;
            p_serverid = 0;
            p_enterid = 0;
            p_enterip = string.Empty;
            p_entermachine = string.Empty;
            p_collectdate = DateTime.Now;
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
        /// EnterID
        /// </summary>
        public int EnterID
        {
            set
            {
                p_enterid=value;
            }
            get
            {
                return p_enterid;
            }
        }

        /// <summary>
        /// EnterIP
        /// </summary>
        public string EnterIP
        {
            set
            {
                p_enterip=value;
            }
            get
            {
                return p_enterip;
            }
        }

        /// <summary>
        /// EnterMachine
        /// </summary>
        public string EnterMachine
        {
            set
            {
                p_entermachine=value;
            }
            get
            {
                return p_entermachine;
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

