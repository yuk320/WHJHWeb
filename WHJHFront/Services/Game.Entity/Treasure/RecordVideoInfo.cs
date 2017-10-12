/*
 * 版本： 4.0
 * 日期：2017/3/21 15:02:35
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Treasure
{
    /// <summary>
    /// 实体类 RecordVideoInfo  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class RecordVideoInfo
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "RecordVideoInfo";

        #endregion 

        #region 私有变量

        private string p_videonumber;
        private int p_roomid;
        private int p_tableid;
        private byte[] p_videodata;
        private int p_playbackid;
        private DateTime? p_playbackstarttime;
        private DateTime p_videobuildtime;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化RecordVideoInfo
        /// </summary>
        public RecordVideoInfo() 
        {
            p_videonumber = string.Empty;
            p_roomid = 0;
            p_tableid = 0;
            p_videodata = null;
            p_playbackid = 0;
            p_playbackstarttime = null;
            p_videobuildtime = DateTime.Now;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// VideoNumber
        /// </summary>
        public string VideoNumber
        {
            set
            {
                p_videonumber=value;
            }
            get
            {
                return p_videonumber;
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
        /// TableId
        /// </summary>
        public int TableId
        {
            set
            {
                p_tableid=value;
            }
            get
            {
                return p_tableid;
            }
        }

        /// <summary>
        /// VideoData
        /// </summary>
        public byte[] VideoData
        {
            set
            {
                p_videodata=value;
            }
            get
            {
                return p_videodata;
            }
        }

        /// <summary>
        /// PlayBackId
        /// </summary>
        public int PlayBackId
        {
            set
            {
                p_playbackid=value;
            }
            get
            {
                return p_playbackid;
            }
        }

        /// <summary>
        /// PlayBackStartTime
        /// </summary>
        public DateTime? PlayBackStartTime
        {
            set
            {
                p_playbackstarttime=value;
            }
            get
            {
                return p_playbackstarttime;
            }
        }

        /// <summary>
        /// VideoBuildTime
        /// </summary>
        public DateTime VideoBuildTime
        {
            set
            {
                p_videobuildtime=value;
            }
            get
            {
                return p_videobuildtime;
            }
        }

        #endregion
    }
}

