using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Entity.Treasure
{
    public class AppReceipt
    {
        #region Fields

        private int m_status;								   //返回状态
        private AppReceiptInfo m_receipt;                     //返回数据    
        #endregion

        #region 构造方法

        public AppReceipt()
        {
            m_status = 0;
            m_receipt = new AppReceiptInfo();
        }
        #endregion

        #region 公开属性

        /// <summary>
        /// 返回状态
        /// </summary>
        public int Status
        {
            get { return m_status; }
            set { m_status = value; }
        }

        /// <summary>
        /// 返回数据
        /// </summary>		
        public AppReceiptInfo Receipt
        {
            get { return m_receipt; }
            set { m_receipt = value; }
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
        public static AppReceipt DeserializeObject(string jsonText)
        {
            AppReceipt receipt = JsonConvert.DeserializeObject<AppReceipt>(jsonText);
            return receipt;
        }

        #endregion
    }
}
