using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Globalization;
using LitJson;
using Game.Facade;
using System.Web.SessionState;
using Game.Entity.PlatformManager;
using Game.Utils;
using Game.Kernel;
using Game.Entity.NativeWeb;

namespace Game.Web.Tools
{
    /// <summary>
    /// KindEditorUpload 的摘要说明
    /// </summary>
    public class KindEditorUpload : IHttpHandler, IRequiresSessionState
    {
        private HttpContext context;

        public void ProcessRequest( HttpContext context )
        {
            //文件保存地址
            string typeInfo = GameRequest.GetQueryString( "type" );
            string savePath = string.Empty;
            string saveUrl = string.Empty;
            switch( typeInfo )
            {
                case "rules":
                    savePath = "/Upload/Rules/";
                    saveUrl = "/Upload/Rules/";
                    break;
                case "news":
                    savePath = "/Upload/News/";
                    saveUrl = "/Upload/News/";
                    break;
                default:
                    savePath = "/Upload/Unknown/";
                    saveUrl = "/Upload/Unknown/";
                    break;
            }

            //文件属性
            Hashtable extTable = new Hashtable();
            extTable.Add( "image", "gif,jpg,jpeg,png,bmp" );
            extTable.Add( "flash", "swf,flv" );
            extTable.Add( "media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb" );
            int maxSize = 1024 * 1024;

            //验证文件
            this.context = context;
            HttpPostedFile imgFile = context.Request.Files["imgFile"];
            if( imgFile == null )
            {
                ShowError( "请选择文件。" );
                return;
            }
            try
            {
                System.Drawing.Image image = System.Drawing.Image.FromStream(imgFile.InputStream);
                image.Dispose();
            }
            catch 
            {
                ShowError("非法文件，目前只支持图片格式文件,对您使用不便感到非常抱歉。");
                return;
            }

            string dirPath = context.Server.MapPath( savePath );
            if( !Directory.Exists( dirPath ) )
            {
                Directory.CreateDirectory( dirPath );
            }
            string dirName = context.Request.QueryString["dir"];
            if( string.IsNullOrEmpty( dirName ) )
            {
                dirName = "image";
            }
            if( !extTable.ContainsKey( dirName ) )
            {
                ShowError( "目录名不正确。" );
                return;
            }
            string fileName = imgFile.FileName;
            string fileExt = Path.GetExtension( fileName ).ToLower();
            if( imgFile.InputStream == null || imgFile.InputStream.Length > maxSize )
            {
                ShowError( "上传文件大小超过限制。" );
                return;
            }
            if( string.IsNullOrEmpty( fileExt ) || Array.IndexOf( ( (string)extTable[dirName] ).Split( ',' ), fileExt.Substring( 1 ).ToLower() ) == -1 )
            {
                ShowError( "上传文件扩展名是不允许的扩展名。\n只允许" + ( (string)extTable[dirName] ) + "格式。" );
                return;
            }

            //创建文件夹
            if( !Directory.Exists( dirPath ) )
            {
                Directory.CreateDirectory( dirPath );
            }

            //保存图片
            string newFileName = DateTime.Now.ToString( "yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo ) + fileExt;
            string filePath = dirPath + newFileName;
            imgFile.SaveAs( filePath );

            //返回成功信息
            ConfigInfo info = FacadeManage.aideNativeWebFacade.GetConfigInfo("WebSiteConfig");
            string imageDomain = info != null ? (info.Field2 + "/") : "/";

            string fileUrl = imageDomain + saveUrl.Replace( "Upload/", "" ) + newFileName;
            Hashtable hash = new Hashtable();
            hash["error"] = 0;
            hash["url"] = fileUrl;
            context.Response.AddHeader( "Content-Type", "text/html; charset=UTF-8" );
            context.Response.Write( JsonMapper.ToJson( hash ) );
            context.Response.End();
        }

        /// <summary>
        /// 返回错误信息
        /// </summary>
        /// <param name="message"></param>
        private void ShowError( string message )
        {
            Hashtable hash = new Hashtable();
            hash["error"] = 1;
            hash["message"] = message;
            context.Response.AddHeader( "Content-Type", "text/html; charset=UTF-8" );
            context.Response.Write( JsonMapper.ToJson( hash ) );
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}