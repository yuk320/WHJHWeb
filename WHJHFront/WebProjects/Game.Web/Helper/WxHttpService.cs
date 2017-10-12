using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Game.Web.Helper
{
    public class WxHttpService
    {
        /// <summary>
        /// 处理http GET请求，返回数据
        /// </summary>
        /// <param name="url">请求的url地址</param>
        /// <returns>http GET成功后返回的数据，失败抛WebException异常</returns>
        public static string Get(string url)
        {
            GC.Collect();
            string result;

            HttpWebRequest request = null;
            HttpWebResponse response = null;

            //请求url以获取数据
            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if(url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            CheckValidationResult;
                }

                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "GET";

                //获取服务器返回
                response = (HttpWebResponse)request.GetResponse();

                //获取HTTP返回数据
                // ReSharper disable once AssignNullToNotNullAttribute
                StreamReader sr = new StreamReader(stream: response.GetResponseStream(), encoding: Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            finally
            {
                //关闭连接和流
                response?.Close();
                request?.Abort();
            }
            return result;
        }

        public static string Post(string xml, string url, int timeout)
        {
            GC.Collect();//垃圾回收，回收没有正常关闭的http连接

            string result;//返回结果

            HttpWebRequest request = null;
            HttpWebResponse response = null;

            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if(url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            CheckValidationResult;
                }
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.Timeout = timeout * 1000;

                //设置POST的数据类型和长度
                request.ContentType = "text/xml";
                byte[] data = Encoding.UTF8.GetBytes(xml);
                request.ContentLength = data.Length;

                //往服务器写入数据
                var reqStream = request.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();

                //获取服务端返回
                response = (HttpWebResponse)request.GetResponse();

                //获取服务端返回数据
                // ReSharper disable once AssignNullToNotNullAttribute
                StreamReader sr = new StreamReader(stream: response.GetResponseStream(), encoding: Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            finally
            {
                //关闭连接和流
                response?.Close();
                request?.Abort();
            }
            return result;
        }
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开    
            return true;
        }
    }
}