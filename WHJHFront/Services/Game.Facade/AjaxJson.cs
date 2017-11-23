using System.Collections.Generic;
using System.Web.Script.Serialization;
// ReSharper disable InconsistentNaming

namespace Game.Facade
{
    /// <summary>
    /// Ajax异步请求返回数据类
    /// </summary>
    public class AjaxJson
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code
        {
            get;
            set;
        }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string msg
        {
            get;
            set;
        }

        /// <summary>
        /// 数据项列表
        /// </summary>
        private Dictionary<string, object> _data = new Dictionary<string, object>();
        public Dictionary<string, object> data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public AjaxJson()
        {
            code = 0;
            msg = "";
            SetDataItem("apiVersion",AppConfig.ApiVersion); 
        }

        /// <summary>
        /// 为数据项赋值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void SetDataItem( string key, object value )
        {
            if (_data.ContainsKey(key))
            {
                _data[key] = value;
            }
            else
            {
                _data.Add(key, value);
            }
        }

        /// <summary>
        /// 获取数据项值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public object GetDataItemValue( string key, object value )
        {
            return _data[key];
        }

        /// <summary>
        /// 序列化AjaxJson
        /// </summary>
        /// <returns>Json字符串</returns>
        public string SerializeToJson()
        {
            return new JavaScriptSerializer().Serialize(this); ;
        }
    }
}
