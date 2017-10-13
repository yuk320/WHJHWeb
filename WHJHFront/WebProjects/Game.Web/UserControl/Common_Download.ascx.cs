using System;
using Game.Facade;
using Game.Entity.NativeWeb;
using System.Drawing;
using ThoughtWorks.QRCode.Codec;
using System.Net;
using System.IO;
using System.Drawing.Drawing2D;

namespace Game.Web.UserControl
{
    public partial class Common_Download : System.Web.UI.UserControl
    {
        //公用属性
        protected string qrLink = string.Empty;

        /// <summary>
        /// 控件加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ConfigInfo info = Fetch.GetWebSiteConfig();
                if(info != null)
                {
                    qrLink = GetQrCode(info.Field1, 260);
                }
            }
        }

        /// <summary>
        /// 获取二维码图片
        /// </summary>
        /// <param name="sData"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private string GetQrCode(string sData, int size)
        {
            //二维码版本,大小获取
            Color qrCodeBackgroundColor = Color.White;
            Color qrCodeForegroundColor = Color.Black;
            int length = System.Text.Encoding.UTF8.GetBytes(sData).Length;

            //生成二维码数据
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;//使用H纠错级别
            qrCodeEncoder.QRCodeVersion = 0;
            var encodedData = qrCodeEncoder.Encode(sData, System.Text.Encoding.UTF8);

            //绘制图片
            int x = 0, y = 0;
            int w = 0, h = 0;
            // 二维码矩阵单边数据点数目
            int count = encodedData.Length;
            // 获取单个数据点边长
            double sideLength = Convert.ToDouble(size) / count;
            // 初始化背景色画笔
            SolidBrush backcolor = new SolidBrush(qrCodeBackgroundColor);
            // 初始化前景色画笔
            SolidBrush forecolor = new SolidBrush(qrCodeForegroundColor);
            // 定义画布
            Bitmap image = new Bitmap(size, size);
            // 获取GDI+绘图图画
            Graphics graph = Graphics.FromImage(image);
            // 先填充背景色
            graph.FillRectangle(backcolor, 0, 0, size, size);

            // 变量数据矩阵生成二维码
            for(int row = 0; row < count; row++)
            {
                for(int col = 0; col < count; col++)
                {
                    // 计算数据点矩阵起始坐标和宽高
                    x = Convert.ToInt32(Math.Round(col * sideLength));
                    y = Convert.ToInt32(Math.Round(row * sideLength));
                    w = Convert.ToInt32(Math.Ceiling((col + 1) * sideLength) - Math.Floor(col * sideLength));
                    h = Convert.ToInt32(Math.Ceiling((row + 1) * sideLength) - Math.Floor(row * sideLength));

                    // 绘制数据矩阵
                    graph.FillRectangle(encodedData[col][row] ? forecolor : backcolor, x, y, w, h);
                }
            }

            //添加LOGO
            string url = Fetch.GetUploadFileUrl("/Site/qrsmall.png");
            Bitmap logoImage = null;
            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            try
            {
                HttpWebResponse webReponse = (HttpWebResponse)webRequest.GetResponse();
                if(webReponse.StatusCode == HttpStatusCode.OK)
                {
                    using(Stream stream = webReponse.GetResponseStream())
                    {
                        System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                        logoImage = new Bitmap(img);
                        img.Dispose();

                        if(logoImage != null)
                        {
                            image = CoverImage(image, logoImage, graph);
                            logoImage.Dispose();
                        }

                        using(MemoryStream ms = new MemoryStream())
                        {
                            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            byte[] bts = ms.ToArray();
                            string baseImg = Convert.ToBase64String(bts);
                            image.Dispose();
                            return "data:image/jpeg;base64," + baseImg;
                        }
                    }
                }
                return "";
            }
            catch(Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 层叠图片
        /// </summary>
        /// <param name="original">原始图片(目前只支持正方形)</param>
        /// <param name="image">层叠图片(目前只支持正方形)</param>
        /// <returns>处理以后的图片</returns>
        private Bitmap CoverImage(Bitmap original, Bitmap image, Graphics graph = null)
        {
            //缩放附加图片
            int sideSLen = original.Width;
            int sideTLen = sideSLen / 4;
            image = ResizeImage(image, sideTLen, sideTLen);

            // 获取GDI+绘图图画
            graph = graph == null ? Graphics.FromImage(original) : graph;

            // 将附加图片绘制到原始图中央
            graph.DrawImage(image, (original.Width - sideTLen) / 2, (original.Height - sideTLen) / 2, sideTLen, sideTLen);

            // 释放GDI+绘图图画内存
            graph.Dispose();

            // 返回处理结果
            return original;
        }

        /// <summary>
        /// 图片缩放
        /// </summary>
        /// <param name="bmp">原始Bitmap</param>
        /// <param name="newW">新的宽度</param>
        /// <param name="newH">新的高度</param>
        /// <returns>处理以后的图片</returns>
        private Bitmap ResizeImage(Bitmap original, int width, int height)
        {
            try
            {
                Bitmap image = new Bitmap(width, height);
                Graphics graph = Graphics.FromImage(image);
                // 插值算法的质量
                graph.CompositingQuality = CompositingQuality.HighQuality;
                graph.SmoothingMode = SmoothingMode.HighQuality;
                graph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graph.DrawImage(original, new Rectangle(0, 0, width, height),
                    new Rectangle(0, 0, original.Width, original.Height), GraphicsUnit.Pixel);
                graph.Dispose();
                return image;
            }
            catch
            {
                return null;
            }
        }
    }
}