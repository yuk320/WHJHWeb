/*
 * 版本： 4.0
 * 日期：2017/7/12 15:20:08
 *
 * 描述：实体类
 *
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.NativeWeb
{
    /// <summary>
    /// 实体类 CacheScoreRank  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class CacheScoreRank
    {
        #region 常量

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "CacheScoreRank";

        #endregion

        #region 私有变量

        private int p_dateid;
        private int p_userid;
        private int p_gameid;
        private string p_nickname;
        private string p_faceurl;
        private int p_systemfaceid;
        private int p_ranknum;
        private Int64 p_score;
        private DateTime p_collectdate;

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化CacheScoreRank
        /// </summary>
        public CacheScoreRank()
        {
            p_dateid = 0;
            p_userid = 0;
            p_gameid = 0;
            p_nickname = string.Empty;
            p_faceurl = string.Empty;
            p_systemfaceid = 0;
            p_ranknum = 0;
            p_score = 0;
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
        /// NickName
        /// </summary>
        public string NickName
        {
            set
            {
                p_nickname=value;
            }
            get
            {
                return p_nickname;
            }
        }

        /// <summary>
        /// FaceUrl
        /// </summary>
        public string FaceUrl
        {
            set
            {
                p_faceurl = value;
            }
            get
            {
                return p_faceurl;
            }
        }

        /// <summary>
        /// SystemFaceID
        /// </summary>
        public int SystemFaceID
        {
            set
            {
                p_systemfaceid=value;
            }
            get
            {
                return p_systemfaceid;
            }
        }

        /// <summary>
        /// RankNum
        /// </summary>
        public int RankNum
        {
            set
            {
                p_ranknum=value;
            }
            get
            {
                return p_ranknum;
            }
        }

        /// <summary>
        /// Score
        /// </summary>
        public Int64 Score
        {
            set
            {
                p_score=value;
            }
            get
            {
                return p_score;
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

        #region 虚拟属性

        /// <summary>
        /// LastLogonAddress
        /// </summary>
        public string LastLogonAddress {get;set;}

        #endregion
    }
}

