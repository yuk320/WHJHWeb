using Game.Entity.Treasure;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Game.Web.Notify
{
    public partial class Apple : System.Web.UI.Page
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                int userId = GameRequest.GetQueryInt("UserID", 0);
                string orderId = GameRequest.GetQueryString("OrderID");
                int payAmount = GameRequest.GetQueryInt("PayAmount", 0);

                StreamReader sr = new StreamReader(Request.InputStream);
                string receiptData = sr.ReadToEnd();

                //苹果返回数据
                string rValue = GetAppInfo(receiptData);

                //苹果返回对象
                AppReceipt receipt = AppReceipt.DeserializeObject(rValue);
                if(receipt.Status == 0 && orderId == receipt.Receipt.transaction_id)
                {
                    OnLinePayOrder order = new OnLinePayOrder
                    {
                        OrderID = orderId,
                        UserID = userId,
                        PayAddress = GameRequest.GetUserIP(),
                        Amount = payAmount
                    };

                    Message umsg = FacadeManage.aideTreasureFacade.FinishOnLineOrderIOS(order, receipt.Receipt.product_id);
                    Response.Write(umsg.Success ? "0" : umsg.Content);
                }
                else
                {
                    Response.Write("失败");
                }
            }
        }

        /// <summary>
        /// 获取苹果充值数据
        /// </summary>
        /// <param name="receiptData"></param>
        /// <returns></returns>
        private string GetAppInfo(string receiptData)
        {
            string url = ApplicationSettings.Get("appUrl");
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"receipt-data\":\"" + receiptData + "\"}");

            //把sXmlMessage发送到指定的DsmpUrl地址上
            Encoding encode = Encoding.GetEncoding("utf-8");
            byte[] arrB = encode.GetBytes(sb.ToString());
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
            myReq.Method = "POST";
            myReq.ContentType = "application/x-www-form-urlencoded";
            myReq.ContentLength = arrB.Length;
            Stream outStream = myReq.GetRequestStream();
            outStream.Write(arrB, 0, arrB.Length);
            outStream.Close();

            //接收HTTP做出的响应
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            Stream receiveStream = myResp.GetResponseStream();
            if (receiveStream == null) return"";
            StreamReader readStream = new StreamReader(receiveStream, encode);
            string rValue = readStream.ReadToEnd();
            readStream.Close();
            myResp.Close();
            return rValue;
        }
    }
}