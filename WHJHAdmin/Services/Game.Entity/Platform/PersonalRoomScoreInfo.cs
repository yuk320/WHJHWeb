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
    /// 实体类 PersonalRoomScoreInfo  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class PersonalRoomScoreInfo
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "PersonalRoomScoreInfo";

        #endregion 

        #region 私有变量

        private int p_userid;
        private string p_personalroomguid;
        private int p_roomid;
        private Int64 p_score;
        private int p_wincount;
        private int p_lostcount;
        private int p_drawcount;
        private int p_fleecount;
        private DateTime p_writetime;
        private int p_playbackcode;
        private int p_chairid;
        private int p_kindid;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化PersonalRoomScoreInfo
        /// </summary>
        public PersonalRoomScoreInfo() 
        {
            p_userid = 0;
            p_personalroomguid = string.Empty;
            p_roomid = 0;
            p_score = 0;
            p_wincount = 0;
            p_lostcount = 0;
            p_drawcount = 0;
            p_fleecount = 0;
            p_writetime = DateTime.Now;
            p_playbackcode = 0;
            p_chairid = 0;
            p_kindid = 0;
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
        /// PersonalRoomGUID
        /// </summary>
        public string PersonalRoomGUID
        {
            set
            {
                p_personalroomguid=value;
            }
            get
            {
                return p_personalroomguid;
            }
        }

        /// <summary>
        /// RoomID
        /// </summary>
        public int RoomID
        {
            set
            {
                p_roomid=value;
            }
            get
            {
                return p_roomid;
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
        /// WinCount
        /// </summary>
        public int WinCount
        {
            set
            {
                p_wincount=value;
            }
            get
            {
                return p_wincount;
            }
        }

        /// <summary>
        /// LostCount
        /// </summary>
        public int LostCount
        {
            set
            {
                p_lostcount=value;
            }
            get
            {
                return p_lostcount;
            }
        }

        /// <summary>
        /// DrawCount
        /// </summary>
        public int DrawCount
        {
            set
            {
                p_drawcount=value;
            }
            get
            {
                return p_drawcount;
            }
        }

        /// <summary>
        /// FleeCount
        /// </summary>
        public int FleeCount
        {
            set
            {
                p_fleecount=value;
            }
            get
            {
                return p_fleecount;
            }
        }

        /// <summary>
        /// WriteTime
        /// </summary>
        public DateTime WriteTime
        {
            set
            {
                p_writetime=value;
            }
            get
            {
                return p_writetime;
            }
        }

        /// <summary>
        /// PlayBackCode
        /// </summary>
        public int PlayBackCode
        {
            set
            {
                p_playbackcode=value;
            }
            get
            {
                return p_playbackcode;
            }
        }

        /// <summary>
        /// ChairID
        /// </summary>
        public int ChairID
        {
            set
            {
                p_chairid=value;
            }
            get
            {
                return p_chairid;
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

        #endregion
    }
}

