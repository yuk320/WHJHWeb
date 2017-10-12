/*
 * 版本： 4.0
 * 日期：2017/7/18 10:41:54
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Record
{
    /// <summary>
    /// 实体类 RecordRegisterGrant  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RecordRegisterGrant
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "RecordRegisterGrant";

        #endregion 

        #region 私有变量

        private int p_userid;
        private string p_registerip;
        private DateTime p_registerdate;
        private string p_registermachine;
        private byte p_registerorigin;
        private int p_grantdiamond;
        private bool p_isreceive;
        private DateTime? p_receivedate;
        private string p_receiveip;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化RecordRegisterGrant
        /// </summary>
        public RecordRegisterGrant() 
        {
            p_userid = 0;
            p_registerip = string.Empty;
            p_registerdate = DateTime.Now;
            p_registermachine = string.Empty;
            p_registerorigin = 0;
            p_grantdiamond = 0;
            p_isreceive = false;
            p_receivedate = null;
            p_receiveip = string.Empty;
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
        /// RegisterIP
        /// </summary>
        public string RegisterIP
        {
            set
            {
                p_registerip=value;
            }
            get
            {
                return p_registerip;
            }
        }

        /// <summary>
        /// RegisterDate
        /// </summary>
        public DateTime RegisterDate
        {
            set
            {
                p_registerdate=value;
            }
            get
            {
                return p_registerdate;
            }
        }

        /// <summary>
        /// RegisterMachine
        /// </summary>
        public string RegisterMachine
        {
            set
            {
                p_registermachine=value;
            }
            get
            {
                return p_registermachine;
            }
        }

        /// <summary>
        /// RegisterOrigin
        /// </summary>
        public byte RegisterOrigin
        {
            set
            {
                p_registerorigin=value;
            }
            get
            {
                return p_registerorigin;
            }
        }

        /// <summary>
        /// GrantDiamond
        /// </summary>
        public int GrantDiamond
        {
            set
            {
                p_grantdiamond=value;
            }
            get
            {
                return p_grantdiamond;
            }
        }

        /// <summary>
        /// IsReceive
        /// </summary>
        public bool IsReceive
        {
            set
            {
                p_isreceive=value;
            }
            get
            {
                return p_isreceive;
            }
        }

        /// <summary>
        /// ReceiveDate
        /// </summary>
        public DateTime? ReceiveDate
        {
            set
            {
                p_receivedate=value;
            }
            get
            {
                return p_receivedate;
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

        #endregion
    }
}

