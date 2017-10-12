using System.Collections.Generic;
using System.Web.Script.Serialization;

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
        private int _code;
        public int code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
            }
        }

        /// <summary>
        /// 消息内容
        /// </summary>
        private string _msg;
        public string msg
        {
            get
            {
                return _msg;
            }
            set
            {
                _msg = value;
            }
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
            _code = 0;
        }

        /// <summary>
        /// 为数据项添加数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void AddDataItem( string key, object value )
        {
            _data.Add( key, value );
        }

        /// <summary>
        /// 为数据项赋值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void SetDataItem( string key, object value )
        {
            _data[key] = value;
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
            string data = new JavaScriptSerializer().Serialize( this );
            return data;
        }
    }
}
