using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using LitJson;
using System.Collections.Generic;
using Game.Entity.PlatformManager;
using Game.Facade;
using System.Web.SessionState;
using Game.Entity.NativeWeb;
using Game.Utils;

namespace Game.Web.Tools
{
    public class KindEditorFileManager : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest( HttpContext context )
        {
            //根目录路径，相对路径
            string rootPath = "/Upload/";

            //根目录URL，可以指定绝对路径，比如 http://www.yoursite.com/attached/
            ConfigInfo info = FacadeManage.aideNativeWebFacade.GetConfigInfo("WebSiteConfig");
            string rootUrl = info != null ? (info.Field2 + "/") : "/";

            //图片扩展名
            string fileTypes = "gif,jpg,jpeg,png,bmp";

            string currentPath = "";
            string currentUrl = "";
            string currentDirPath = "";
            string moveupDirPath = "";

            //创建目录
            string dirPath = context.Server.MapPath( rootPath );
            if( !Directory.Exists( dirPath ) )
            {
                Directory.CreateDirectory( dirPath );
            }

            //根据path参数，设置各路径和URL
            string path = context.Request.QueryString["path"];
            path = string.IsNullOrEmpty( path ) ? "" : path;
            if( path == "" )
            {
                currentPath = dirPath;
                currentUrl = rootUrl;
                currentDirPath = "";
                moveupDirPath = "";
            }
            else
            {
                currentPath = dirPath + path;
                currentUrl = rootUrl + path;
                currentDirPath = path;
                moveupDirPath = Regex.Replace( currentDirPath, @"(.*?)[^\/]+\/$", "$1" );
            }

            //排序形式，name or size or type
            string order = context.Request.QueryString["order"];
            order = string.IsNullOrEmpty( order ) ? "" : order.ToLower();

            //不允许使用..移动到上一级目录
            if( Regex.IsMatch( path, @"\.\." ) )
            {
                context.Response.Write( "Access is not allowed." );
                return;
            }
            //最后一个字符不是/
            if( path != "" && !path.EndsWith( "/" ) )
            {
                context.Response.Write( "Parameter is not valid." );
                return;
            }
            //目录不存在或不是目录
            if( !Directory.Exists( currentPath ) )
            {
                context.Response.Write( "Directory does not exist." );
                return;
            }

            //遍历目录取得文件信息
            string[] dirList = Directory.GetDirectories( currentPath );
            string[] fileList = Directory.GetFiles( currentPath );

            switch( order )
            {
                case "size":
                    Array.Sort( dirList, new NameSorter() );
                    Array.Sort( fileList, new SizeSorter() );
                    break;
                case "type":
                    Array.Sort( dirList, new NameSorter() );
                    Array.Sort( fileList, new TypeSorter() );
                    break;
                case "name":
                default:
                    Array.Sort( dirList, new NameSorter() );
                    Array.Sort( fileList, new NameSorter() );
                    break;
            }

            Hashtable result = new Hashtable();
            result["moveup_dir_path"] = moveupDirPath;
            result["current_dir_path"] = currentDirPath;
            result["current_url"] = currentUrl;
            result["total_count"] = dirList.Length + fileList.Length;
            List<Hashtable> dirFileList = new List<Hashtable>();
            result["file_list"] = dirFileList;
            for( int i = 0; i < dirList.Length; i++ )
            {
                DirectoryInfo dir = new DirectoryInfo( dirList[i] );
                Hashtable hash = new Hashtable();
                hash["is_dir"] = true;
                hash["has_file"] = ( dir.GetFileSystemInfos().Length > 0 );
                hash["filesize"] = 0;
                hash["is_photo"] = false;
                hash["filetype"] = "";
                hash["filename"] = dir.Name;
                hash["datetime"] = dir.LastWriteTime.ToString( "yyyy-MM-dd HH:mm:ss" );
                dirFileList.Add( hash );
            }
            for( int i = 0; i < fileList.Length; i++ )
            {
                FileInfo file = new FileInfo( fileList[i] );
                Hashtable hash = new Hashtable();
                hash["is_dir"] = false;
                hash["has_file"] = false;
                hash["filesize"] = file.Length;
                hash["is_photo"] = ( Array.IndexOf( fileTypes.Split( ',' ), file.Extension.Substring( 1 ).ToLower() ) >= 0 );
                hash["filetype"] = file.Extension.Substring( 1 );
                hash["filename"] = file.Name;
                hash["datetime"] = file.LastWriteTime.ToString( "yyyy-MM-dd HH:mm:ss" );
                dirFileList.Add( hash );
            }
            context.Response.AddHeader( "Content-Type", "application/json; charset=UTF-8" );
            context.Response.Write( JsonMapper.ToJson( result ) );
            context.Response.End();
        }

        public class NameSorter : IComparer
        {
            public int Compare( object x, object y )
            {
                if( x == null && y == null )
                {
                    return 0;
                }
                if( x == null )
                {
                    return -1;
                }
                if( y == null )
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo( x.ToString() );
                FileInfo yInfo = new FileInfo( y.ToString() );

                return xInfo.FullName.CompareTo( yInfo.FullName );
            }
        }

        public class SizeSorter : IComparer
        {
            public int Compare( object x, object y )
            {
                if( x == null && y == null )
                {
                    return 0;
                }
                if( x == null )
                {
                    return -1;
                }
                if( y == null )
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo( x.ToString() );
                FileInfo yInfo = new FileInfo( y.ToString() );

                return xInfo.Length.CompareTo( yInfo.Length );
            }
        }

        public class TypeSorter : IComparer
        {
            public int Compare( object x, object y )
            {
                if( x == null && y == null )
                {
                    return 0;
                }
                if( x == null )
                {
                    return -1;
                }
                if( y == null )
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo( x.ToString() );
                FileInfo yInfo = new FileInfo( y.ToString() );

                return xInfo.Extension.CompareTo( yInfo.Extension );
            }
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}