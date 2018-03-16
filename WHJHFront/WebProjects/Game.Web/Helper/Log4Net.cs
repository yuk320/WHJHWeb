using log4net;
using System;
using System.Web;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Game.Web.Helper
{
    public class Log4Net
    {
        /// <summary>
        /// 日志操作对象
        /// </summary>
        private static ILog Log => LogManager.GetLogger("LogInfo");

        /// <summary>
        /// 写入信息日志
        /// </summary>
        /// <param name="message">信息内容</param>
        /// <param name="tag"></param>
        public static void WriteInfoLog(string message, string tag = "")
        {
            HttpRequest req = HttpContext.Current.Request;
            tag = tag.Equals("") ? req.Url.AbsoluteUri : tag;
            message = $"[{req.UserHostAddress}] [{tag}] \t\r\n[消息]：{message}";
            Log.Info(message);
        }

        /// <summary>
        /// 写入错误日志
        /// </summary>
        /// <param name="ex">错误异常</param>
        /// <param name="tag"></param>
        public static void WriteErrorLog(Exception ex, string tag = "")
        {
            HttpRequest req = HttpContext.Current.Request;
            tag = tag.Equals("") ? req.Url.AbsoluteUri : tag;
            string message =
                $"[{req.UserHostAddress}] [{tag}] \t\r\n[当前堆栈]：{ex.StackTrace}\r\n[错误]：{ex.Message}";
            Log.Error(message);
        }
    }
}
