using Game.Utils;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Game.Facade
{
    public class Umeng
    {
        private static string AppUrl = "http://msg.umeng.com/api/send";
        private static string AppKey_Android = ApplicationSettings.Get("AppKey_Android");
        private static string AppSecret_Android = ApplicationSettings.Get("AppSecret_Android");
        private static string AppKey_IOS = ApplicationSettings.Get("AppKey_IOS");
        private static string AppSecret_IOS = ApplicationSettings.Get("AppSecret_IOS");
        private static string UmengProductionMode = ApplicationSettings.Get("UMengProductionMode");
        /// <summary>
        /// 发送推送信息
        /// </summary>
        /// <param name="DeviceType">设备类型（0 表示android 1 表示IOS ）</param>
        /// <param name="Text">推送信息内容</param>
        /// <param name="SendType">消息类型（unicast 单播  broadcast 广播 listcast-列播）</param>
        /// <param name="token">设备唯一标识（当类型为unicast时必须填写）</param>
        public static bool SendMessage(int DeviceType, string Text, string SendType, string startTime, string endTime, string token = "")
        {
            try
            {
                //获取时间戳
                string timestamp = GetTimeStamp();
                //数据签名
                string mysign = string.Empty;
                //参数集合
                StringBuilder sb = new StringBuilder();
                if(DeviceType == 1)
                {
                    //苹果参数集合体
                    sb.Append("{");
                    sb.AppendFormat("\"appkey\":\"{0}\",", AppKey_IOS);
                    sb.AppendFormat("\"timestamp\":{0},", timestamp);
                    sb.AppendFormat("\"type\":\"{0}\",", SendType);
                    if(SendType != "broadcast")
                    {
                        sb.AppendFormat("\"device_tokens\":\"{0}\",", token);
                    }
                    sb.Append("\"payload\":{");
                    sb.Append("\"aps\":{");
                    sb.AppendFormat("\"alert\":\"{0}\"", Text);
                    sb.Append("}");
                    sb.Append("},");
                    sb.Append("\"policy\":{");
                    sb.AppendFormat("\"start_time\":\"{0}\",", startTime);
                    sb.AppendFormat("\"expire_time\":\"{0}\",", endTime);
                    sb.Append("},");
                    sb.AppendFormat("\"production_mode\":\"{0}\",", UmengProductionMode);
                    sb.AppendFormat("\"description\":\"{0}\"", Text);
                    sb.Append("}");
                    mysign = GetMD5("POST" + AppUrl + sb.ToString() + AppSecret_IOS);
                }
                else
                {
                    //安卓参数集合体
                    sb.Append("{");
                    sb.AppendFormat("\"appkey\":\"{0}\",", AppKey_Android);
                    sb.AppendFormat("\"timestamp\":{0},", timestamp);
                    sb.AppendFormat("\"type\":\"{0}\",", SendType);
                    if(SendType != "broadcast")
                    {
                        sb.AppendFormat("\"device_tokens\":\"{0}\",", token);
                    }
                    sb.Append("\"payload\":{");
                    sb.AppendFormat("\"display_type\":\"{0}\",", "notification");
                    sb.Append("\"body\":{");
                    sb.AppendFormat("\"ticker\":\"{0}\",", Text);
                    sb.AppendFormat("\"title\":\"{0}\",", Text);
                    sb.AppendFormat("\"text\":\"{0}\",", Text);
                    sb.AppendFormat("\"after_open\":\"{0}\"", "go_app");
                    sb.Append("}");
                    sb.Append("},");
                    sb.Append("\"policy\":{");
                    sb.AppendFormat("\"start_time\":\"{0}\",", startTime);
                    sb.AppendFormat("\"expire_time\":\"{0}\",", endTime);
                    sb.Append("},");
                    sb.AppendFormat("\"production_mode\":\"{0}\",", UmengProductionMode);
                    sb.AppendFormat("\"description\":\"{0}\"", Text);
                    sb.Append("}");
                    mysign = GetMD5("POST" + AppUrl + sb.ToString() + AppSecret_Android);
                }

                //请求接口地址
                string requestUrl = AppUrl + "?sign=" + mysign;
                TextLogger.Write("UMeng RequestUrl:" + requestUrl+" sb:"+sb.ToString());
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                request.Method = "POST";

                byte[] bs = Encoding.UTF8.GetBytes(sb.ToString());
                request.ContentLength = bs.Length;
                using(Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                    reqStream.Close();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                HttpStatusCode statusCode = response.StatusCode;
                TextLogger.Write("UMeng Response:" + response.StatusCode);

                return true;
            }
            catch(WebException e)
            {
                if(e.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpStatusCode code = ((HttpWebResponse)e.Response).StatusCode;
                    string descript = ((HttpWebResponse)e.Response).StatusDescription;

                    Stream myResponseStream = ((HttpWebResponse)e.Response).GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    string retString = myStreamReader.ReadToEnd();
                    TextLogger.Write("UMeng Response:" + retString);
                }
                return false;
            }
        }

        private static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        private static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder strReturn = new StringBuilder();

            for(int i = 0; i < result.Length; i++)
            {
                strReturn.Append(Convert.ToString(result[i], 16).PadLeft(2, '0'));
            }
            return strReturn.ToString().PadLeft(32, '0');
        }
    }
}
