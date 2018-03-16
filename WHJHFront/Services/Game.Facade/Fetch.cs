using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Game.Utils;
using Game.Utils.Cache;
using System.IO;
using Game.Entity.NativeWeb;
using Game.Entity.Accounts;
using System.Security.Cryptography;
using Game.Facade.Enum;
using System.Drawing;
using ThoughtWorks.QRCode.Codec;
using System.Net;
using System.Drawing.Drawing2D;

// ReSharper disable InconsistentNaming

namespace Game.Facade
{
    /// <summary>
    /// 为网站提供全局服务，如：用户Cookie读写等等
    /// </summary>
    public static class Fetch
    {
        #region 构造方法

        #endregion

        #region 公用方法

        /// <summary>
        /// 接口签名验证
        /// </summary>
        /// <param name="signStr">待验证签名字符串</param>
        /// <param name="signData">验证签名数据</param>
        /// <returns></returns>
        public static AjaxJsonValid VerifySignData(string signStr, string signData)
        {
            AjaxJsonValid ajv = new AjaxJsonValid();
            if (!string.IsNullOrEmpty(signData) && Utility.MD5(signStr) == signData) return ajv;
            ajv.code = (int) ApiCode.VertySignErrorCode;
            ajv.msg = EnumHelper.GetDesc(ApiCode.VertySignErrorCode);
            return ajv;
        }

        /// <summary>
        /// 获取交易流水号
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string GetOrderIDByPrefix(string prefix)
        {
            //构造订单号 (形如:20101201102322159111111)
            int orderIDLength = 32;
            int randomLength = 6;
            StringBuffer tradeNoBuffer = new StringBuffer();

            tradeNoBuffer += prefix;
            tradeNoBuffer += TextUtility.GetDateTimeLongString();

            if ((tradeNoBuffer.Length + randomLength) > orderIDLength)
                randomLength = orderIDLength - tradeNoBuffer.Length;

            tradeNoBuffer += TextUtility.CreateRandom(randomLength, 1, 0, 0, 0, "");

            return tradeNoBuffer.ToString();
        }

        /// <summary>
        /// 获取给定日期距离1900-01-01的天数
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int GetDateID(DateTime dateTime)
        {
            TimeSpan ts1 = new TimeSpan(dateTime.Ticks);
            TimeSpan ts2 = new TimeSpan(Convert.ToDateTime("1900-01-01").Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts.Days;
        }

        /// <summary>
        /// 客户端终端类型
        /// </summary>
        /// <returns>1:终端为android手机 2：终端为苹果的ipad、iphone、ipod</returns>
        public static int GetTerminalType(HttpRequest request)
        {
            string userAgent = request.Headers["User-Agent"];
            if (userAgent == null)
            {
                return 0;
            }
            userAgent = userAgent.ToLower();
            if (userAgent.Contains("android"))
            {
                return 1;
            }
            else if (userAgent.Contains("ipad") || userAgent.Contains("iphone"))
            {
                return 2;
            }
            return 0;
        }

        /// <summary>
        /// 判断是否微信内置浏览器
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool isWeChat(HttpRequest request)
        {
            string userAgent = request.Headers["User-Agent"];
            return userAgent != null && userAgent.Contains("MicroMessenger");
        }

        /// <summary>
        /// 获取网站配置缓存
        /// </summary>
        /// <returns></returns>
        public static IList<ConfigInfo> GetConfigInfoList()
        {
            IList<ConfigInfo> list = null;
            object obj = WHCache.Default.Get<AspNetCache>(AppConfig.WebSiteConfigCache);
            if (obj != null)
            {
                list = obj as List<ConfigInfo>;
            }
            if (obj == null || list == null)
            {
                list = FacadeManage.aideNativeWebFacade.GetConfigInfoList();
                WHCache.Default.Save<AspNetCache>(AppConfig.WebSiteConfigCache, list, AppConfig.ResourceTimeOut);
            }
            return list;
        }

        /// <summary>
        /// 获取网站广告缓存
        /// </summary>
        /// <returns></returns>
        public static IList<Ads> GetAdsList()
        {
            IList<Ads> list = null;
            object obj = WHCache.Default.Get<AspNetCache>(AppConfig.AdsConfigCache);
            if (obj != null)
            {
                list = obj as List<Ads>;
            }
            if (obj == null || list == null)
            {
                list = FacadeManage.aideNativeWebFacade.GetAdsList();
                WHCache.Default.Save<AspNetCache>(AppConfig.AdsConfigCache, list, AppConfig.ResourceTimeOut);
            }
            return list;
        }

        /// <summary>
        /// 获取网站站点配置
        /// </summary>
        /// <returns></returns>
        public static ConfigInfo GetWebSiteConfig()
        {
            IList<ConfigInfo> list = GetConfigInfoList();
            foreach (var item in list)
            {
                if (item.ConfigKey == AppConfig.SiteConfigKey.WebSiteConfig.ToString())
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取下载地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetDownLoadUrl(HttpRequest request)
        {
            //获取客户端类型
            int terminalType = GetTerminalType(request);
            //下载地址数据集合
            IList<ConfigInfo> list = GetConfigInfoList();
            foreach (var item in list)
            {
                if (terminalType == 1 && item.ConfigKey == AppConfig.SiteConfigKey.MobilePlatformVersion.ToString())
                {
                    return item.Field6;
                }
                else if (terminalType == 2 && item.ConfigKey ==
                         AppConfig.SiteConfigKey.MobilePlatformVersion.ToString())
                {
                    return item.Field5;
                }
            }
            return "";
        }

        /// <summary>
        /// 获取上传的图片URL
        /// </summary>
        /// <param name="imageDomain">图片服务器地址</param>
        /// <param name="fileUrl">图片相对路径</param>
        /// <returns></returns>
        public static string GetUploadFileUrl(string imageDomain, string fileUrl)
        {
            return imageDomain + fileUrl.ToLower().Replace("upload/", "");
        }

        /// <summary>
        /// 获取上传的图片URL
        /// </summary>
        /// <param name="fileUrl">图片相对路径</param>
        /// <returns></returns>
        public static string GetUploadFileUrl(string fileUrl)
        {
            ConfigInfo config = GetWebSiteConfig();
            return GetUploadFileUrl((config != null ? config.Field2 : ""), fileUrl);
        }

        /// <summary>
        /// 获取客服服务配置
        /// </summary>
        /// <returns></returns>
        public static ConfigInfo GetCustomerService()
        {
            IList<ConfigInfo> list = GetConfigInfoList();
            foreach (var item in list)
            {
                if (item.ConfigKey == AppConfig.SiteConfigKey.SysCustomerService.ToString())
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取注册来源
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public static string RegisterOrigin(int typeId)
        {
            string rValue;
            switch (typeId)
            {
                case 0:
                    rValue = "PC";
                    break;
                case 1:
                    rValue = "模拟器";
                    break;
                case 16:
                case 17:
                case 18:
                    rValue = "Android";
                    break;
                case 32:
                    rValue = "iTouch";
                    break;
                case 48:
                case 49:
                case 50:
                    rValue = "iPhone";
                    break;
                case 64:
                case 65:
                case 66:
                    rValue = "iPad";
                    break;
                case 81:
                    rValue = "WEB推广页";
                    break;
                case 82:
                    rValue = "WEB约战页";
                    break;
                case 90:
                    rValue = "H5";
                    break;
                default:
                    rValue = "未知";
                    break;
            }
            return rValue;
        }

        /// <summary>
        /// 格式化时间换行显示
        /// </summary>
        /// <returns></returns>
        public static string FormatTimeWrap(DateTime time)
        {
            return time.ToString("yyyy-MM-dd") + "<br/>" + time.ToString("HH:mm:ss");
        }

        /// <summary>
        /// 获取微信参数
        /// </summary>
        /// <param name="encrypt">微信加密参数字符</param>
        /// <returns></returns>
        public static WxUser GetWxUser(string encrypt)
        {
            string decparam = DESDecrypt(encrypt, AppConfig.WxUrlKey);
            if (string.IsNullOrEmpty(decparam) || decparam.IndexOf(',') <= 0)
            {
                return null;
            }
            string[] param = decparam.Split(',');
            if (param.Length != 5)
            {
                return null;
            }
            try
            {
                WxUser wu = new WxUser
                {
                    openid = param[0].Substring(1, param[0].Length - 2),
                    unionid = param[1].Substring(1, param[1].Length - 2),
                    nickname = param[2].Substring(1, param[2].Length - 2),
                    sex = Convert.ToByte(param[3].Substring(1, param[3].Length - 2)),
                    headimgurl = param[4].Substring(1, param[4].Length - 2)
                };
                return wu;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region 登录用户

        /// <summary>
        /// 设置用户cookie
        /// </summary>
        /// <param name="userTicket">用户信息</param>
        public static void SetUserCookie(UserTicketInfo userTicket)
        {
            if (userTicket == null || userTicket.UserID <= 0)
            {
                return;
            }
            WHCache.Default.Save<SessionCache>(AppConfig.UserLoginCacheKey, userTicket, AppConfig.UserLoginTimeOut);
        }

        /// <summary>
        /// 获取用户对象
        /// </summary>
        /// <returns></returns>
        public static UserTicketInfo GetUserCookie()
        {
            object obj = WHCache.Default.Get<SessionCache>(AppConfig.UserLoginCacheKey);
            return obj as UserTicketInfo;
        }

        /// <summary>
        /// 删除用户cookie
        /// </summary>
        public static void DeleteUserCookie()
        {
            WHCache.Default.Delete<SessionCache>(AppConfig.UserLoginCacheKey);
        }

        #endregion

        #region DES 加密解密

        /// <summary>
        /// 进行DES加密。
        /// </summary>
        /// <param name="pToEncrypt">要加密的字符串。</param>
        /// <param name="sKey">密钥，且必须为8位。</param>
        /// <returns>以Base64格式返回的加密字符串。</returns>
        public static string DESEncrypt(string pToEncrypt, string sKey)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt);
                des.Key = Encoding.UTF8.GetBytes(sKey);
                des.IV = Encoding.UTF8.GetBytes(sKey);
                MemoryStream ms = new MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Convert.ToBase64String(ms.ToArray());
                ms.Close();
                return str;
            }
        }

        // <summary>
        // 进行DES解密。
        // </summary>
        // <param name="pToDecrypt">要解密的以Base64</param>
        // <param name="sKey">密钥，且必须为8位。</param>
        // <returns>已解密的字符串。</returns>
        public static string DESDecrypt(string pToDecrypt, string sKey)
        {
            byte[] inputByteArray = Convert.FromBase64String(pToDecrypt);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = Encoding.UTF8.GetBytes(sKey);
                des.IV = Encoding.UTF8.GetBytes(sKey);
                MemoryStream ms = new MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return str;
            }
        }

        #endregion

        #region AES 加密解密

        /// <summary>
        /// H5 公用加密方式
        /// </summary>
        /// <param name="inputdata">加密数据</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">向量</param>
        /// <returns></returns>
        public static string AESEncrypt(string inputdata, string key, string iv)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(key);
            byte[] ivArray = Encoding.UTF8.GetBytes(iv);
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(inputdata);

            RijndaelManaged rDel = new RijndaelManaged
            {
                Key = keyArray,
                IV = ivArray,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.Zeros
            };

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        #endregion

        #region SHA-256 加密

        /// <summary>
        /// SHA256 哈希值
        /// </summary>
        /// <param name="strIN"></param>
        /// <param name="isBase64"></param>
        /// <returns></returns>
        public static string SHA256Encrypt(string strIN,bool isBase64 = false)
        {
            //string strIN = getstrIN(strIN);
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            var tmpByte = sha256.ComputeHash(GetKeyByteArray(strIN));
            sha256.Clear();
            StringBuilder sb = new StringBuilder();
            if (isBase64) return Convert.ToBase64String(tmpByte);
            foreach (byte t in tmpByte)
            {
                sb.Append(t.ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// string 转为 byte[]
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
        private static byte[] GetKeyByteArray(string strKey)
        {
            return Encoding.UTF8.GetBytes(strKey);
        }

        #endregion

        #region 画二维码

        /// <summary>
        /// 获取二维码图片
        /// </summary>
        /// <param name="sData"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string GetQrCode(string sData, int size)
        {
            //二维码版本,大小获取
            Color qrCodeBackgroundColor = Color.White;
            Color qrCodeForegroundColor = Color.Black;
            int length = System.Text.Encoding.UTF8.GetBytes(sData).Length;

            //生成二维码数据
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H; //使用H纠错级别
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
            for (int row = 0; row < count; row++)
            {
                for (int col = 0; col < count; col++)
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
            HttpWebRequest webRequest = (HttpWebRequest) HttpWebRequest.Create(url);
            try
            {
                HttpWebResponse webReponse = (HttpWebResponse) webRequest.GetResponse();
                if (webReponse.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream stream = webReponse.GetResponseStream())
                    {
                        System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                        logoImage = new Bitmap(img);
                        img.Dispose();

                        if (logoImage != null)
                        {
                            image = CoverImage(image, logoImage, graph);
                            logoImage.Dispose();
                        }

                        using (MemoryStream ms = new MemoryStream())
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
            catch (Exception)
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
        private static Bitmap CoverImage(Bitmap original, Bitmap image, Graphics graph = null)
        {
            //缩放附加图片
            int sideSLen = original.Width;
            int sideTLen = sideSLen / 4;
            image = ResizeImage(image, sideTLen, sideTLen);

            // 获取GDI+绘图图画
            graph = graph == null ? Graphics.FromImage(original) : graph;

            // 将附加图片绘制到原始图中央
            graph.DrawImage(image, (original.Width - sideTLen) / 2, (original.Height - sideTLen) / 2, sideTLen,
                sideTLen);

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
        private static Bitmap ResizeImage(Bitmap original, int width, int height)
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

        #endregion

        #region 日期格式与Unix时间戳

        /// <summary>
        /// DateTime转为Uinx时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ConvertDateTimeToUnix(DateTime time)
        {
            return ((time.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString();
        }

        /// <summary>
        /// Uinx时间戳转为DateTime
        /// </summary>
        /// <param name="unix"></param>
        /// <returns></returns>
        public static DateTime ConvertUnixToDateTime(string unix)
        {
            DateTime startUnixTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc), TimeZoneInfo.Local);
            return startUnixTime.AddSeconds(double.Parse(unix));
        }

        #endregion
    }
}
