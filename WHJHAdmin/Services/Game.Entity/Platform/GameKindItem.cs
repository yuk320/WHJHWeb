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
    /// 实体类 GameKindItem  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class GameKindItem
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "GameKindItem";

        #endregion 

        #region 私有变量

        private int p_kindid;
        private int p_gameid;
        private int p_typeid;
        private int p_joinid;
        private int p_sortid;
        private string p_kindname;
        private string p_processname;
        private string p_gameruleurl;
        private string p_downloadurl;
        private int p_recommend;
        private int p_gameflag;
        private byte p_nullity;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化GameKindItem
        /// </summary>
        public GameKindItem() 
        {
            p_kindid = 0;
            p_gameid = 0;
            p_typeid = 0;
            p_joinid = 0;
            p_sortid = 0;
            p_kindname = string.Empty;
            p_processname = string.Empty;
            p_gameruleurl = string.Empty;
            p_downloadurl = string.Empty;
            p_recommend = 0;
            p_gameflag = 0;
            p_nullity = 0;
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
        /// TypeID
        /// </summary>
        public int TypeID
        {
            set
            {
                p_typeid=value;
            }
            get
            {
                return p_typeid;
            }
        }

        /// <summary>
        /// JoinID
        /// </summary>
        public int JoinID
        {
            set
            {
                p_joinid=value;
            }
            get
            {
                return p_joinid;
            }
        }

        /// <summary>
        /// SortID
        /// </summary>
        public int SortID
        {
            set
            {
                p_sortid=value;
            }
            get
            {
                return p_sortid;
            }
        }

        /// <summary>
        /// KindName
        /// </summary>
        public string KindName
        {
            set
            {
                p_kindname=value;
            }
            get
            {
                return p_kindname;
            }
        }

        /// <summary>
        /// ProcessName
        /// </summary>
        public string ProcessName
        {
            set
            {
                p_processname=value;
            }
            get
            {
                return p_processname;
            }
        }

        /// <summary>
        /// GameRuleUrl
        /// </summary>
        public string GameRuleUrl
        {
            set
            {
                p_gameruleurl=value;
            }
            get
            {
                return p_gameruleurl;
            }
        }

        /// <summary>
        /// DownLoadUrl
        /// </summary>
        public string DownLoadUrl
        {
            set
            {
                p_downloadurl=value;
            }
            get
            {
                return p_downloadurl;
            }
        }

        /// <summary>
        /// Recommend
        /// </summary>
        public int Recommend
        {
            set
            {
                p_recommend=value;
            }
            get
            {
                return p_recommend;
            }
        }

        /// <summary>
        /// GameFlag
        /// </summary>
        public int GameFlag
        {
            set
            {
                p_gameflag=value;
            }
            get
            {
                return p_gameflag;
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

        #endregion
    }
}

