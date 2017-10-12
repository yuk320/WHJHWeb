/*
 * 版本： 4.0
 * 日期：2017/8/23 11:37:00
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.NativeWeb
{
    /// <summary>
    /// 实体类 SystemNotice  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class SystemNotice
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "SystemNotice";

        #endregion 

        #region 私有变量

        private int p_noticeid;
        private string p_noticetitle;
        private string p_mobliecontent;
        private string p_webcontent;
        private int p_sortid;
        private string p_publisher;
        private DateTime p_publishertime;
        private bool p_ishot;
        private bool p_istop;
        private bool p_nullity;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化SystemNotice
        /// </summary>
        public SystemNotice() 
        {
            p_noticeid = 0;
            p_noticetitle = string.Empty;
            p_mobliecontent = string.Empty;
            p_webcontent = string.Empty;
            p_sortid = 0;
            p_publisher = string.Empty;
            p_publishertime = DateTime.Now;
            p_ishot = false;
            p_istop = false;
            p_nullity = false;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// NoticeID
        /// </summary>
        public int NoticeID
        {
            set
            {
                p_noticeid=value;
            }
            get
            {
                return p_noticeid;
            }
        }

        /// <summary>
        /// NoticeTitle
        /// </summary>
        public string NoticeTitle
        {
            set
            {
                p_noticetitle=value;
            }
            get
            {
                return p_noticetitle;
            }
        }

        /// <summary>
        /// MoblieContent
        /// </summary>
        public string MoblieContent
        {
            set
            {
                p_mobliecontent=value;
            }
            get
            {
                return p_mobliecontent;
            }
        }

        /// <summary>
        /// WebContent
        /// </summary>
        public string WebContent
        {
            set
            {
                p_webcontent=value;
            }
            get
            {
                return p_webcontent;
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
        /// Publisher
        /// </summary>
        public string Publisher
        {
            set
            {
                p_publisher=value;
            }
            get
            {
                return p_publisher;
            }
        }

        /// <summary>
        /// PublisherTime
        /// </summary>
        public DateTime PublisherTime
        {
            set
            {
                p_publishertime=value;
            }
            get
            {
                return p_publishertime;
            }
        }

        /// <summary>
        /// IsHot
        /// </summary>
        public bool IsHot
        {
            set
            {
                p_ishot=value;
            }
            get
            {
                return p_ishot;
            }
        }

        /// <summary>
        /// IsTop
        /// </summary>
        public bool IsTop
        {
            set
            {
                p_istop = value;
            }
            get
            {
                return p_istop;
            }
        }

        /// <summary>
        /// Nullity
        /// </summary>
        public bool Nullity
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

