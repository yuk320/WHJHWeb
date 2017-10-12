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
    /// 实体类 GameGameItem  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class GameGameItem
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "GameGameItem";

        #endregion 

        #region 私有变量

        private int p_gameid;
        private string p_gamename;
        private int p_suporttype;
        private string p_databaseaddr;
        private string p_databasename;
        private int p_serverversion;
        private int p_clientversion;
        private string p_serverdllname;
        private string p_clientexename;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化GameGameItem
        /// </summary>
        public GameGameItem() 
        {
            p_gameid = 0;
            p_gamename = string.Empty;
            p_suporttype = 0;
            p_databaseaddr = string.Empty;
            p_databasename = string.Empty;
            p_serverversion = 0;
            p_clientversion = 0;
            p_serverdllname = string.Empty;
            p_clientexename = string.Empty;
        }

        #endregion

        #region 公共属性 

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
        /// GameName
        /// </summary>
        public string GameName
        {
            set
            {
                p_gamename=value;
            }
            get
            {
                return p_gamename;
            }
        }

        /// <summary>
        /// SuportType
        /// </summary>
        public int SuportType
        {
            set
            {
                p_suporttype=value;
            }
            get
            {
                return p_suporttype;
            }
        }

        /// <summary>
        /// DataBaseAddr
        /// </summary>
        public string DataBaseAddr
        {
            set
            {
                p_databaseaddr=value;
            }
            get
            {
                return p_databaseaddr;
            }
        }

        /// <summary>
        /// DataBaseName
        /// </summary>
        public string DataBaseName
        {
            set
            {
                p_databasename=value;
            }
            get
            {
                return p_databasename;
            }
        }

        /// <summary>
        /// ServerVersion
        /// </summary>
        public int ServerVersion
        {
            set
            {
                p_serverversion=value;
            }
            get
            {
                return p_serverversion;
            }
        }

        /// <summary>
        /// ClientVersion
        /// </summary>
        public int ClientVersion
        {
            set
            {
                p_clientversion=value;
            }
            get
            {
                return p_clientversion;
            }
        }

        /// <summary>
        /// ServerDLLName
        /// </summary>
        public string ServerDLLName
        {
            set
            {
                p_serverdllname=value;
            }
            get
            {
                return p_serverdllname;
            }
        }

        /// <summary>
        /// ClientExeName
        /// </summary>
        public string ClientExeName
        {
            set
            {
                p_clientexename=value;
            }
            get
            {
                return p_clientexename;
            }
        }

        #endregion
    }
}

