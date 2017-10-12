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
    /// 实体类 IndividualDatum  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class IndividualDatum
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "IndividualDatum";

        #endregion 

        #region 私有变量

        private int p_userid;
        private string p_qq;
        private string p_email;
        private string p_seatphone;
        private string p_mobilephone;
        private string p_dwellingplace;
        private string p_postalcode;
        private DateTime p_collectdate;
        private string p_usernote;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化IndividualDatum
        /// </summary>
        public IndividualDatum() 
        {
            p_userid = 0;
            p_qq = string.Empty;
            p_email = string.Empty;
            p_seatphone = string.Empty;
            p_mobilephone = string.Empty;
            p_dwellingplace = string.Empty;
            p_postalcode = string.Empty;
            p_collectdate = DateTime.Now;
            p_usernote = string.Empty;
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
        /// QQ
        /// </summary>
        public string QQ
        {
            set
            {
                p_qq=value;
            }
            get
            {
                return p_qq;
            }
        }

        /// <summary>
        /// EMail
        /// </summary>
        public string EMail
        {
            set
            {
                p_email=value;
            }
            get
            {
                return p_email;
            }
        }

        /// <summary>
        /// SeatPhone
        /// </summary>
        public string SeatPhone
        {
            set
            {
                p_seatphone=value;
            }
            get
            {
                return p_seatphone;
            }
        }

        /// <summary>
        /// MobilePhone
        /// </summary>
        public string MobilePhone
        {
            set
            {
                p_mobilephone=value;
            }
            get
            {
                return p_mobilephone;
            }
        }

        /// <summary>
        /// DwellingPlace
        /// </summary>
        public string DwellingPlace
        {
            set
            {
                p_dwellingplace=value;
            }
            get
            {
                return p_dwellingplace;
            }
        }

        /// <summary>
        /// PostalCode
        /// </summary>
        public string PostalCode
        {
            set
            {
                p_postalcode=value;
            }
            get
            {
                return p_postalcode;
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

        /// <summary>
        /// UserNote
        /// </summary>
        public string UserNote
        {
            set
            {
                p_usernote=value;
            }
            get
            {
                return p_usernote;
            }
        }

        #endregion
    }
}

