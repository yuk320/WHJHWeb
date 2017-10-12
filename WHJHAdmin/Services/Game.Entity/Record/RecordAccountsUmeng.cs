/*
 * 版本： 4.0
 * 日期：2017/8/7 10:51:13
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Record
{
    /// <summary>
    /// 实体类 RecordAccountsUmeng  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RecordAccountsUmeng
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "RecordAccountsUmeng";

        #endregion 

        #region 私有变量

        private Int64 p_recordid;
        private int p_masterid;
        private int p_userid;
        private byte p_pushtype;
        private string p_pushcontent;
        private DateTime p_pushtime;
        private string p_puship;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化RecordAccountsUmeng
        /// </summary>
        public RecordAccountsUmeng() 
        {
            p_recordid = 0;
            p_masterid = 0;
            p_userid = 0;
            p_pushtype = 0;
            p_pushcontent = string.Empty;
            p_pushtime = DateTime.Now;
            p_puship = string.Empty;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// RecordID
        /// </summary>
        public Int64 RecordID
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
        /// MasterID
        /// </summary>
        public int MasterID
        {
            set
            {
                p_masterid=value;
            }
            get
            {
                return p_masterid;
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
        /// PushType
        /// </summary>
        public byte PushType
        {
            set
            {
                p_pushtype=value;
            }
            get
            {
                return p_pushtype;
            }
        }

        /// <summary>
        /// PushContent
        /// </summary>
        public string PushContent
        {
            set
            {
                p_pushcontent=value;
            }
            get
            {
                return p_pushcontent;
            }
        }

        /// <summary>
        /// PushTime
        /// </summary>
        public DateTime PushTime
        {
            set
            {
                p_pushtime=value;
            }
            get
            {
                return p_pushtime;
            }
        }

        /// <summary>
        /// PushIP
        /// </summary>
        public string PushIP
        {
            set
            {
                p_puship=value;
            }
            get
            {
                return p_puship;
            }
        }

        #endregion
    }
}

