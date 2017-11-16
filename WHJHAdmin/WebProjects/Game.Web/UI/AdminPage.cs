using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Game.Facade;
using Game.Utils;
using Game.Entity;
using Game.Entity.Platform;
using Game.Entity.Treasure;
using Game.Entity.Accounts;
using Game.Entity.PlatformManager;
using Game.Entity.Enum;
using System.IO;
using Game.Utils.Cache;
using System.Text.RegularExpressions;
using Game.Entity.NativeWeb;


namespace Game.Web.UI
{
    /// <summary>
    /// 后台基类
    /// </summary>
    public abstract partial class AdminPage : Page
    {
        #region Fields

        /// <summary>
        /// 模块标识
        /// </summary>
        protected internal int moduleID;

        /// <summary>
        /// 用户对象
        /// </summary>
        protected internal LoginUser userExt;

        /// <summary>
        /// 用户权限
        /// </summary>
        protected internal Dictionary<string, long> userPower;

        /// <summary>
        /// 命令
        /// </summary>
        protected string StrCmd
        {
            get { return GameRequest.GetQueryString("cmd"); }
        }

        /// <summary>
        /// Get方法传递的整型参数 param
        /// </summary>
        protected int IntParam
        {
            get { return GameRequest.GetQueryInt("param", 0); }
        }

        /// <summary>
        /// Get方法传递的字符串参数 param
        /// </summary>
        protected string StrParam
        {
            get { return GameRequest.GetQueryString("param"); }
        }

        /// <summary>
        /// Post方法传递的整型列表参数 cid
        /// </summary>
        protected string StrCIdList
        {
            get
            {
                string[] strArray = GameRequest.GetFormString("cid").Trim().Split(new char[] {','});
                StringBuilder builder = new StringBuilder();
                foreach (string str2 in strArray)
                {
                    if (Utils.Validate.IsNumeric(str2))
                    {
                        builder.Append(str2 + ",");
                    }
                }
                return builder.ToString().TrimEnd(new char[] {','});
            }
        }

        /// <summary>
        /// Post方法传递的字符串型列表参数 cid
        /// </summary>
        protected string StrCStrList
        {
            get
            {
                string[] strArray = GameRequest.GetFormString("cid").Trim().Split(new char[] {','});
                StringBuilder builder = new StringBuilder();
                foreach (string str2 in strArray)
                {
                    builder.Append("'" + str2 + "'" + ",");
                }
                return builder.ToString().TrimEnd(new char[] {','});
            }
        }

        #endregion

        #region 构造方法

        /// <summary>
        /// 页面载入
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            //获取登录用户
            userExt = Fetch.GetLoginUser();
            if (userExt == null || userExt.UserID <= 0)
            {
                RedirectToLogin("/Login.aspx");
                return;
            }
            //获取缓存资源信息
            LoginCache cache = Fetch.GetLoginResources(userExt.UserID);
            //验证页面授权
            string pageName = GameRequest.GetPageName();
            ModulePage modulePage = (cache != null & cache.pagePowerList != null)
                ? cache.pagePowerList.Where(p => p.PageName.ToLower().Contains(pageName)).FirstOrDefault<ModulePage>()
                : null;
            if (modulePage == null || modulePage.ModuleID <= 0)
            {
                RedirectToLogin("/Login.aspx");
                return;
            }
            moduleID = modulePage.ModuleID;
            //验证用户权限
            userPower = (cache != null & cache.userPower != null) ? cache.userPower : null;
            if (userExt == null || userPower == null || userPower.Count == 0)
            {
                RedirectToLogin("/Login.aspx");
                return;
            }
            if (userExt.RoleID != 1)
            {
                if (!userPower.ContainsKey(moduleID.ToString()) ||
                    (userPower[moduleID.ToString()] & Convert.ToInt64(Permission.Read)) <= 0)
                {
                    Redirect("/NotPower.html");
                    return;
                }
            }
        }

        #endregion

        #region 权限检查

        /// <summary>
        /// 验证当前页面的操作权限
        /// </summary>
        /// <param name="permission">权限值</param>
        protected void AuthUserOperationPermission(Permission permission)
        {
            if (userExt == null || userExt.UserID <= 0 || moduleID <= 0 || userPower == null || userPower.Count <= 0)
            {
                RedirectToLogin("/Login.aspx");
                return;
            }
            if (userExt.RoleID != 1)
            {
                if (!userPower.ContainsKey(moduleID.ToString()) ||
                    (userPower[moduleID.ToString()] & Convert.ToInt64(permission)) <= 0)
                {
                    Redirect("/NotPower.html");
                    return;
                }
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="url"></param>
        protected void Redirect(string url)
        {
            Fetch.Redirect(url);
        }

        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="url"></param>
        private void RedirectToLogin(string url)
        {
            Random rd = new Random();
            Page.Response.Clear();
            Page.ClientScript.RegisterStartupScript(typeof(Page), "",
                string.Format("top.location.href='/Login.aspx?{0}';", rd.Next(1000)), true);
            return;
        }

        /// <summary>
        /// 错误显示
        /// </summary>
        /// <param name="msg"></param>
        protected void ShowError(string msg)
        {
            Page.ClientScript.RegisterStartupScript(typeof(Page), "", string.Format("showError(\"{0}\");", msg), true);
        }

        /// <summary>
        /// 错误显示
        /// </summary>
        /// <param name="msg"></param>
        protected void ShowError(string msg, string url, int timeout)
        {
            Page.ClientScript.RegisterStartupScript(typeof(Page), "",
                string.Format("showError(\"{0}\");setTimeout(\"Redirect('{1}')\",{2})", msg, url, timeout), true);
        }

        /// <summary>
        /// 信息显示
        /// </summary>
        /// <param name="msg"></param>
        protected void ShowInfo(string msg)
        {
            Page.ClientScript.RegisterStartupScript(typeof(Page), "", string.Format("showInfo(\"{0}\");", msg), true);
        }

        /// <summary>
        /// 信息显示并跳转
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="url"></param>
        protected void ShowInfo(string msg, string url, int timeout)
        {
            Page.ClientScript.RegisterStartupScript(typeof(Page), "",
                string.Format("showInfo(\"{0}\");setTimeout(\"Redirect('{1}')\",{2})", msg, url, timeout), true);
        }

        /// <summary>
        /// 弹出提示信息
        /// </summary>
        /// <param name="msg"></param>
        protected void MessageBox(string msg)
        {
            Page.ClientScript.RegisterStartupScript(typeof(Page), "", string.Format("alert(\"{0}\");", msg), true);
        }

        /// <summary>
        /// 弹出提示信息并跳转
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="ReturnUrl"></param>
        protected void MessageBox(string msg, string ReturnUrl)
        {
            Page.ClientScript.RegisterStartupScript(typeof(Page), "",
                string.Format("alert(\"{0}\");parent.location.href='{1}'", msg, ReturnUrl), true);
        }

        /// <summary>
        /// 弹出提示信息
        /// </summary>
        /// <param name="msg"></param>
        protected void MessageBoxClose(string msg)
        {
            Page.ClientScript.RegisterStartupScript(typeof(Page), "",
                string.Format("alert(\"{0}\");window.close();", msg), true);
        }

        /// <summary>
        /// 弹出提示信息，父页面刷新
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ReturnUrl"></param>
        protected void MessageBoxCloseRef(string msg)
        {
            Page.ClientScript.RegisterStartupScript(typeof(Page), "",
                string.Format(
                    "alert(\"{0}\");opener.document.location.href=opener.document.location.href,window.close();", msg),
                true);
        }

        #endregion

        #region 数据部分

        #region 用户信息

        /// <summary>
        /// 获取用户标识
        /// </summary>
        /// <param name="gameID">游戏id</param>
        /// <returns>用户ID</returns>
        protected int GetUserIDByGameID(int gameID)
        {
            AccountsInfo accountsInfo = FacadeManage.aideAccountsFacade.GetAccountInfoByGameId(gameID);
            return accountsInfo == null ? 0 : accountsInfo.UserID;
        }

        /// <summary>
        /// 获得用户昵称
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <returns></returns>
        public string GetNickNameByUserID(int userID)
        {
            AccountsInfo model = FacadeManage.aideAccountsFacade.GetAccountInfoByUserId(userID);
            return model != null ? model.NickName : "";
        }

        /// <summary>
        /// 获取游戏id
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <returns></returns>
        protected string GetGameID(int userID)
        {
            AccountsInfo accounts = FacadeManage.aideAccountsFacade.GetAccountInfoByUserId(userID);
            return accounts != null ? accounts.GameID.ToString() : "";
        }

        #endregion

        #region 流水信息

        /// <summary>
        /// 根据不同需求获取
        /// </summary>
        /// <param name="t"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        protected IList<EnumDescription> GetSerialTypeList(Type t, string type = "")
        {
            IList<EnumDescription> list = EnumHelper.GetList(t);
            if (type == "consume")
            {
                if (t == typeof(DiamondSerialType))
                {
                    list = list.Where(a => a.EnumValue == 7 || (a.EnumValue >= 9 && a.EnumValue <= 12)).ToList();

                }
                else if (t == typeof(GoldSerialType))
                {
                    list = list.Where(a => a.EnumValue == 2 || a.EnumValue == 4 || a.EnumValue == 8).ToList();
                }
            }
            else if (type == "income")
            {
                if (t == typeof(DiamondSerialType))
                {
                    list = list.Where(a => a.EnumValue != 7 && (a.EnumValue < 9 || a.EnumValue > 12)).ToList();
                }
                else if (t == typeof(GoldSerialType))
                {
                    list = list.Where(a => a.EnumValue != 2 && a.EnumValue != 4 && a.EnumValue != 6 &&
                                           a.EnumValue != 7 && a.EnumValue != 8).ToList();
                }
            }
            return list;
        }

        /// <summary>
        /// 获取钻石获取类型
        /// </summary>
        /// <param name="typeid">类型标识</param>
        /// <returns></returns>
        protected string GetGoldType(int typeid)
        {
            return EnumHelper.GetDesc(typeof(GoldSerialType), typeid);
        }

        /// <summary>
        /// 获取钻石获取类型
        /// </summary>
        /// <param name="typeid">类型标识</param>
        /// <returns></returns>
        protected string GetDiamondType(int typeid)
        {
            return EnumHelper.GetDesc(typeof(DiamondSerialType), typeid);
        }

        #endregion

        #region 管理员信息

        /// <summary>
        /// 获取管理员帐号
        /// </summary>
        /// <param name="masterID">管理员标识</param>
        /// <returns></returns>
        protected string GetMasterName(int masterID)
        {
            Base_Users user = FacadeManage.aidePlatformManagerFacade.GetUserByUserId(masterID);
            return user != null ? user.Username : "";
        }

        /// <summary>
        /// 获取角色名称
        /// </summary>
        /// <param name="roleID">角色标识</param>
        /// <returns></returns>
        protected string GetRoleName(int roleID)
        {
            Base_Roles role = FacadeManage.aidePlatformManagerFacade.GetRoleInfo(roleID);
            return role != null ? role.RoleName : "";
        }

        #endregion

        #region 公用方法

        /// <summary>
        /// 根据IP的地理位置
        /// </summary>
        /// <param name="IP">IP</param>
        /// <returns></returns>
        protected string GetAddressWithIP(string IP)
        {
            return IPQuery.GetAddressWithIP(IP);
        }

        /// <summary>
        /// 启动禁用状态(0:启用; 1:禁止)
        /// </summary>
        /// <param name="nullity">状态标识</param>
        /// <returns></returns>
        protected string GetNullityStatus(byte nullity)
        {
            return nullity == 0 ? "<span>启用</span>" : "<span class='hong'>禁用</span>";
        }

        /// <summary>
        /// 计算版本号
        /// </summary>
        /// <param name="version">版本</param>
        /// <returns></returns>
        public string CalVersion(int version)
        {
            string returnValue = "";
            returnValue += (version >> 24).ToString() + ".";
            returnValue += (((version >> 16) << 24) >> 24).ToString() + ".";
            returnValue += (((version >> 8) << 24) >> 24).ToString() + ".";
            returnValue += ((version << 24) >> 24).ToString();
            return returnValue;
        }

        /// <summary>
        /// 还原版本号
        /// </summary>
        /// <param name="version">版本</param>
        /// <returns></returns>
        public int CalVersion2(string version)
        {
            int rValue = 0;
            string[] verArray = version.Split('.');
            rValue = (int.Parse(verArray[0]) << 24) | (int.Parse(verArray[1]) << 16) | (int.Parse(verArray[2]) << 8) |
                     int.Parse(verArray[3]);
            return rValue;
        }

        /// <summary>
        /// 获取注册来源
        /// </summary>
        /// <param name="origin">注册类型</param>
        /// <returns></returns>
        public string GetRegisterOrigin(byte origin)
        {
            string rValue = "";
            switch (origin)
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
        /// 获取游戏名称
        /// </summary>
        public string GetGameKindName(int kindid)
        {
            GameGameItem item = FacadeManage.aidePlatformFacade.GetGameGameItemInfo(kindid);
            return item != null ? item.GameName : "";
        }

        /// <summary>
        /// 获取房间名称
        /// </summary>
        public string GetGameRoomName(int serverid)
        {
            GameRoomInfo room = FacadeManage.aidePlatformFacade.GetGameRoomInfoInfo(serverid);
            return room != null ? room.ServerName : "";
        }

        #endregion

        #endregion
    }
}