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
    public partial class LogoSet : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            Random rd = new Random();
            igAdminLogo.ImageUrl = "/Upload/Site/adminlogo.png?" + rd.Next(100, 1000000).ToString();
            igFrontLogo.ImageUrl = "/Upload/Site/frontlogo.png?" + rd.Next(100, 1000000).ToString();
            igQrSmall.ImageUrl = "/Upload/Site/qrsmall.png?" + rd.Next(100, 1000000).ToString();
            igTopLogo.ImageUrl = "/Upload/Site/toplogo.png?" + rd.Next(100, 1000000).ToString();
        }
        /// <summary>
        /// 数据保存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.Edit);

            Message msg = new Message();
            msg = SaveImage(fuAdminLogo, "/Upload/Site", "adminlogo.png");
            if(!msg.Success)
            {
                ShowError(msg.Content);
                return;
            }
            msg = SaveImage(fuFrontLogo, "/Upload/Site", "frontlogo.png");
            if(!msg.Success)
            {
                ShowError(msg.Content);
                return;
            }
            msg = SaveImage(fuQrSmall, "/Upload/Site", "qrsmall.png");
            if(!msg.Success)
            {
                ShowError(msg.Content);
                return;
            }
            msg = SaveImage(fuTopLogo, "/Upload/Site", "toplogo.png");
            if(!msg.Success)
            {
                ShowError(msg.Content);
                return;
            }
            ShowInfo("操作成功");
            Response.Redirect("LogoSet.aspx?");
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
                    msg.Content = "LOGO必须为PNG格式";
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
