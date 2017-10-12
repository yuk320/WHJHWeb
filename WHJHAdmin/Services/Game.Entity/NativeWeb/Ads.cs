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
    /// 实体类 Ads  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Ads
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "Ads";

        #endregion 

        #region 私有变量

        private int p_id;
        private string p_title;
        private string p_resourceurl;
        private string p_linkurl;
        private byte p_type;
        private int p_sortid;
        private string p_remark;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化Ads
        /// </summary>
        public Ads() 
        {
            p_id = 0;
            p_title = string.Empty;
            p_resourceurl = string.Empty;
            p_linkurl = string.Empty;
            p_type = 0;
            p_sortid = 0;
            p_remark = string.Empty;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            set
            {
                p_id=value;
            }
            get
            {
                return p_id;
            }
        }

        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            set
            {
                p_title=value;
            }
            get
            {
                return p_title;
            }
        }

        /// <summary>
        /// ResourceURL
        /// </summary>
        public string ResourceURL
        {
            set
            {
                p_resourceurl=value;
            }
            get
            {
                return p_resourceurl;
            }
        }

        /// <summary>
        /// LinkURL
        /// </summary>
        public string LinkURL
        {
            set
            {
                p_linkurl=value;
            }
            get
            {
                return p_linkurl;
            }
        }

        /// <summary>
        /// Type
        /// </summary>
        public byte Type
        {
            set
            {
                p_type=value;
            }
            get
            {
                return p_type;
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
        /// Remark
        /// </summary>
        public string Remark
        {
            set
            {
                p_remark=value;
            }
            get
            {
                return p_remark;
            }
        }

        #endregion
    }
}

