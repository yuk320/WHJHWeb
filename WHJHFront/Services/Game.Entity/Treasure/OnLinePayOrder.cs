/*
 * 版本： 4.0
 * 日期：2017/6/8 10:38:52
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Treasure
{
    /// <summary>
    /// 实体类 OnLinePayOrder  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class OnLinePayOrder
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "OnLinePayOrder";

        #endregion 

        #region 私有变量

        private int p_onlineid;
        private int p_configid;
        private int p_shareid;
        private int p_userid;
        private int p_gameid;
        private string p_accounts;
        private string p_nickname;
        private string p_orderid;
        private byte p_ordertype;
        private decimal p_amount;
        private int p_diamond;
        private decimal p_presentscale;
        private int p_otherpresent;
        private byte p_orderstatus;
        private DateTime p_orderdate;
        private string p_orderaddress;
        private Int64 p_beforediamond;
        private DateTime? p_paydate;
        private string p_payaddress;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化OnLinePayOrder
        /// </summary>
        public OnLinePayOrder() 
        {
            p_onlineid = 0;
            p_configid = 0;
            p_shareid = 0;
            p_userid = 0;
            p_gameid = 0;
            p_accounts = string.Empty;
            p_nickname = string.Empty;
            p_orderid = string.Empty;
            p_ordertype = 0;
            p_amount = 0;
            p_diamond = 0;
            p_presentscale = 0;
            p_otherpresent = 0;
            p_orderstatus = 0;
            p_orderdate = DateTime.Now;
            p_orderaddress = string.Empty;
            p_beforediamond = 0;
            p_paydate = null;
            p_payaddress = string.Empty;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// OnLineID
        /// </summary>
        public int OnLineID
        {
            set
            {
                p_onlineid=value;
            }
            get
            {
                return p_onlineid;
            }
        }

        /// <summary>
        /// ConfigID
        /// </summary>
        public int ConfigID
        {
            set
            {
                p_configid=value;
            }
            get
            {
                return p_configid;
            }
        }

        /// <summary>
        /// ShareID
        /// </summary>
        public int ShareID
        {
            set
            {
                p_shareid=value;
            }
            get
            {
                return p_shareid;
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
        /// Accounts
        /// </summary>
        public string Accounts
        {
            set
            {
                p_accounts=value;
            }
            get
            {
                return p_accounts;
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
        /// OrderID
        /// </summary>
        public string OrderID
        {
            set
            {
                p_orderid=value;
            }
            get
            {
                return p_orderid;
            }
        }

        /// <summary>
        /// OrderType
        /// </summary>
        public byte OrderType
        {
            set
            {
                p_ordertype = value;
            }
            get
            {
                return p_ordertype;
            }
        }

        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount
        {
            set
            {
                p_amount=value;
            }
            get
            {
                return p_amount;
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
        /// PresentScale
        /// </summary>
        public decimal PresentScale
        {
            set
            {
                p_presentscale=value;
            }
            get
            {
                return p_presentscale;
            }
        }

        /// <summary>
        /// OtherPresent
        /// </summary>
        public int OtherPresent
        {
            set
            {
                p_otherpresent=value;
            }
            get
            {
                return p_otherpresent;
            }
        }

        /// <summary>
        /// OrderStatus
        /// </summary>
        public byte OrderStatus
        {
            set
            {
                p_orderstatus=value;
            }
            get
            {
                return p_orderstatus;
            }
        }

        /// <summary>
        /// OrderDate
        /// </summary>
        public DateTime OrderDate
        {
            set
            {
                p_orderdate=value;
            }
            get
            {
                return p_orderdate;
            }
        }

        /// <summary>
        /// OrderAddress
        /// </summary>
        public string OrderAddress
        {
            set
            {
                p_orderaddress=value;
            }
            get
            {
                return p_orderaddress;
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
        /// PayDate
        /// </summary>
        public DateTime? PayDate
        {
            set
            {
                p_paydate=value;
            }
            get
            {
                return p_paydate;
            }
        }

        /// <summary>
        /// PayAddress
        /// </summary>
        public string PayAddress
        {
            set
            {
                p_payaddress=value;
            }
            get
            {
                return p_payaddress;
            }
        }

        #endregion
    }
}

