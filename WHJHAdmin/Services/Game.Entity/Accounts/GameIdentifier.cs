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
    /// 实体类 GameIdentifier  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class GameIdentifier
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "GameIdentifier";

        #endregion 

        #region 私有变量

        private int p_userid;
        private int p_gameid;
        private int p_idlevel;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化GameIdentifier
        /// </summary>
        public GameIdentifier() 
        {
            p_userid = 0;
            p_gameid = 0;
            p_idlevel = 0;
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
        /// IDLevel
        /// </summary>
        public int IDLevel
        {
            set
            {
                p_idlevel=value;
            }
            get
            {
                return p_idlevel;
            }
        }

        #endregion
    }
}

