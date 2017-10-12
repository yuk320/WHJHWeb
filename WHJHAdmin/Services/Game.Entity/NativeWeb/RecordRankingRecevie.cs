/*
 * 版本： 4.0
 * 日期：2017/8/7 10:50:29
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.NativeWeb
{
    /// <summary>
    /// 实体类 RecordRankingRecevie  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RecordRankingRecevie
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "RecordRankingRecevie";

        #endregion 

        #region 私有变量

        private int p_recordid;
        private int p_dateid;
        private int p_userid;
        private int p_gameid;
        private string p_nickname;
        private int p_systemfaceid;
        private int p_customfaceid;
        private int p_typeid;
        private int p_rankid;
        private Int64 p_rankvalue;
        private int p_diamond;
        private DateTime p_validitytime;
        private bool p_receivestate;
        private Int64 p_beforediamond;
        private string p_receiveip;
        private DateTime? p_receivetime;
        private DateTime p_collectdate;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化RecordRankingRecevie
        /// </summary>
        public RecordRankingRecevie() 
        {
            p_recordid = 0;
            p_dateid = 0;
            p_userid = 0;
            p_gameid = 0;
            p_nickname = string.Empty;
            p_systemfaceid = 0;
            p_customfaceid = 0;
            p_typeid = 0;
            p_rankid = 0;
            p_rankvalue = 0;
            p_diamond = 0;
            p_validitytime = DateTime.Now;
            p_receivestate = false;
            p_beforediamond = 0;
            p_receiveip = string.Empty;
            p_receivetime = null;
            p_collectdate = DateTime.Now;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// RecordID
        /// </summary>
        public int RecordID
        {
            set
            {
                p_recordid=value;
            }
            get
            {
                return p_recordid;
            }
        }

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
        /// CustomFaceID
        /// </summary>
        public int CustomFaceID
        {
            set
            {
                p_customfaceid=value;
            }
            get
            {
                return p_customfaceid;
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
        /// RankID
        /// </summary>
        public int RankID
        {
            set
            {
                p_rankid=value;
            }
            get
            {
                return p_rankid;
            }
        }

        /// <summary>
        /// RankValue
        /// </summary>
        public Int64 RankValue
        {
            set
            {
                p_rankvalue=value;
            }
            get
            {
                return p_rankvalue;
            }
        }

        /// <summary>
        /// Diamond
        /// </summary>
        public int Diamond
        {
            set
            {
                p_diamond=value;
            }
            get
            {
                return p_diamond;
            }
        }

        /// <summary>
        /// ValidityTime
        /// </summary>
        public DateTime ValidityTime
        {
            set
            {
                p_validitytime=value;
            }
            get
            {
                return p_validitytime;
            }
        }

        /// <summary>
        /// ReceiveState
        /// </summary>
        public bool ReceiveState
        {
            set
            {
                p_receivestate=value;
            }
            get
            {
                return p_receivestate;
            }
        }

        /// <summary>
        /// BeforeDiamond
        /// </summary>
        public Int64 BeforeDiamond
        {
            set
            {
                p_beforediamond=value;
            }
            get
            {
                return p_beforediamond;
            }
        }

        /// <summary>
        /// ReceiveIP
        /// </summary>
        public string ReceiveIP
        {
            set
            {
                p_receiveip=value;
            }
            get
            {
                return p_receiveip;
            }
        }

        /// <summary>
        /// ReceiveTime
        /// </summary>
        public DateTime? ReceiveTime
        {
            set
            {
                p_receivetime=value;
            }
            get
            {
                return p_receivetime;
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

