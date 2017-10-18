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
        public int code { get; set; } = 0;

        /// <summary>
        /// 消息内容
        /// </summary>
        public string msg { get; set; } = "";

        /// <summary>
        /// 数据项列表
        /// </summary>
        public Dictionary<string, object> data { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// 为数据项添加数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void AddDataItem( string key, object value )
        {
            data.Add( key, value );
        }

        /// <summary>
        /// 为数据项赋值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void SetDataItem( string key, object value )
        {
            data[key] = value;
        }

        /// <summary>
        /// 获取数据项值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public object GetDataItemValue( string key, object value )
        {
            return data[key];
        }

        /// <summary>
        /// 序列化AjaxJson
        /// </summary>
        /// <returns>Json字符串</returns>
        public string SerializeToJson()
        {
            return new JavaScriptSerializer().Serialize(this);
        }
    }
}
