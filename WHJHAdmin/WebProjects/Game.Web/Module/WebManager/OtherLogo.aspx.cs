using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Utils;
using Game.Web.UI;
using Game.Kernel;
using System.Text;
using Game.Entity.NativeWeb;
using Game.Entity.Accounts;
using Game.Entity.Enum;
using System.IO;

namespace Game.Web.Module.WebManager
{
    public partial class OtherLogo : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            Random rd = new Random();
            igMobileLogo.ImageUrl = "/Upload/Site/mobilelogo.png?" + rd.Next(100, 1000000).ToString();
            igMobileBg.ImageUrl = "/Upload/Site/mobilebg.png?" + rd.Next(100, 1000000).ToString();
            igMobileDownLad.ImageUrl = "/Upload/Site/mobiledownLad.png?" + rd.Next(100, 1000000).ToString();
        }
        /// <summary>
        /// 数据保存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.Edit);

            Message msg = new Message();
            msg = SaveImage(fuMobileLogo, "/Upload/Site", "mobilelogo.png");
            if(!msg.Success)
            {
                ShowError(msg.Content);
                return;
            }
            msg = SaveImage(fuMobileBg, "/Upload/Site", "mobilebg.png");
            if(!msg.Success)
            {
                ShowError(msg.Content);
                return;
            }
            msg = SaveImage(fuMobileDownLad, "/Upload/Site", "mobiledownLad.png");
            if(!msg.Success)
            {
                ShowError(msg.Content);
                return;
            }

            ShowInfo("操作成功");
            Response.Redirect("OtherLogo.aspx?param=20");
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="fileControl"></param>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected Message SaveImage(FileUpload fileControl, string path, string fileName)
        {
            Message msg = new Message();

            //验证图片
            HttpPostedFile file = fileControl.PostedFile;
            if(file.ContentLength != 0)
            {
                if(fileControl.FileName.Substring(fileControl.FileName.LastIndexOf(".") + 1).ToLower() != "png")
                {
                    msg.Content = "图片必须为PNG格式";
                    msg.Success = false;
                    return msg;
                }
                try
                {
                    // 转化图片
                    System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream);
                }
                catch
                {
                    msg.Content = "不是合法的图片";
                    msg.Success = false;
                    return msg;
                }
            }
            else
            {
                return msg;
            }

            //检查目录
            string serverPath = Server.MapPath(path);
            if(!Directory.Exists(serverPath))
            {
                Directory.CreateDirectory(serverPath);
            }
            //检查文件
            string filePath = Server.MapPath(path + "/" + fileName);
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            //保存图片
            file.SaveAs(filePath);
            return msg;
        }
    }
}