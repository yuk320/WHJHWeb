using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Entity.Treasure
{
    public class AppReceiptInfo
    {
        #region Fields

        private int m_quantity;
        private string m_product_id;
        private string m_transaction_id;
        private string m_purchase_date;
        private string m_original_transaction_id;
        private string m_original_purchase_date;
        private string m_app_item_id;
        private string m_version_external_identifier;
        private string m_bid;
        private string m_bvrs;
        #endregion

        #region 构造方法

        public AppReceiptInfo()
        {
            m_quantity = 0;
            m_product_id = "";
            m_transaction_id = "";
            m_purchase_date = "";
            m_original_transaction_id = "";
            m_original_purchase_date = "";
            m_app_item_id = "";
            m_version_external_identifier = "";
            m_bid = "";
            m_bvrs = "";
        }
        #endregion

        #region 公开属性

        public int quantity
        {
            get { return m_quantity; }
            set { m_quantity = value; }
        }

        public string product_id
        {
            get { return m_product_id; }
            set { m_product_id = value; }
        }

        public string transaction_id
        {
            get { return m_transaction_id; }
            set { m_transaction_id = value; }
        }

        public string purchase_date
        {
            get { return m_purchase_date; }
            set { m_purchase_date = value; }
        }

        public string original_transaction_id
        {
            get { return m_original_transaction_id; }
            set { m_original_transaction_id = value; }
        }

        public string original_purchase_date
        {
            get { return m_original_purchase_date; }
            set { m_original_purchase_date = value; }
        }

        public string app_item_id
        {
            get { return m_app_item_id; }
            set { m_app_item_id = value; }
        }

        public string version_external_identifier
        {
            get { return m_version_external_identifier; }
            set { m_version_external_identifier = value; }
        }

        public string bid
        {
            get { return m_bid; }
            set { m_bid = value; }
        }

        public string bvrs
        {
            get { return m_bvrs; }
            set { m_bvrs = value; }
        }
        #endregion

        #region 公开方法

        /// <summary>
        /// 序列化为Json对象
        /// </summary>
        /// <returns></returns>
        public string SerializeText()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// 反序列化Json对象
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static AppReceiptInfo DeserializeObject(string jsonText)
        {
            return JsonConvert.DeserializeObject<AppReceiptInfo>(jsonText);
        }

        #endregion
    }
}
