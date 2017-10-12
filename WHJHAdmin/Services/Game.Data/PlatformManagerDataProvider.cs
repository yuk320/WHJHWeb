using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Kernel;
using Game.IData;
using Game.Entity.PlatformManager;
using System.Data;
using System.Data.Common;

namespace Game.Data
{
    /// <summary>
    /// 后台数据层
    /// </summary>
    public class PlatformManagerDataProvider : BaseDataProvider, IPlatformManagerDataProvider
    {
        #region 构造方法
        /// <summary>
        /// 构造函数
        /// </summary>
        public PlatformManagerDataProvider(string connString)
            : base(connString)
        {
        }
        #endregion

        #region 公用分页
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pageIndex">页下标</param>
        /// <param name="pageSize">页显示数</param>
        /// <param name="condition">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        public PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(tableName, orderby, condition, pageIndex, pageSize);
            return GetPagerSet2(pagerPrams);
        }
        #endregion

        #region 管理员管理
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="user">管理员信息</param>
        /// <returns></returns>
        public Message UserLogon(Base_Users user)
        {
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strUserName", user.Username));
            prams.Add(Database.MakeInParam("strPassword", user.Password));
            prams.Add(Database.MakeInParam("strClientIP", user.LastLoginIP));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessageForObject<LoginUser>(Database, "NET_PM_UserLogon", prams);
        }
        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <param name="user">管理员信息</param>
        public int RegisterUser(Base_Users user)
        {
            string sqlQuery = @"INSERT INTO Base_Users(Username,[Password],RoleID,Nullity,PreLogintime,PreLoginIP,
                            LastLogintime,LastLoginIP,LoginTimes,IsBand,BandIP,IsAssist) VALUES(@Username,@Password,
                            @RoleID,@Nullity,@PreLogintime,@PreLoginIP,@LastLogintime,@LastLoginIP,@LoginTimes,@IsBand,@BandIP,@IsAssist)";

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("Username", user.Username));
            prams.Add(Database.MakeInParam("Password", user.Password));
            prams.Add(Database.MakeInParam("RoleID", user.RoleID));
            prams.Add(Database.MakeInParam("Nullity", user.Nullity));
            prams.Add(Database.MakeInParam("PreLogintime", user.PreLogintime));
            prams.Add(Database.MakeInParam("PreLoginIP", user.PreLoginIP));
            prams.Add(Database.MakeInParam("LastLogintime", user.LastLogintime));
            prams.Add(Database.MakeInParam("LastLoginIP", user.LastLoginIP));
            prams.Add(Database.MakeInParam("LoginTimes", user.LoginTimes));
            prams.Add(Database.MakeInParam("IsBand", user.IsBand));
            prams.Add(Database.MakeInParam("BandIP", user.BandIP));
            prams.Add(Database.MakeInParam("IsAssist", user.IsAssist));

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }
        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="userIDList">管理员列表</param>
        public int DeleteUser(string userIDList)
        {
            string sqlQuery = string.Format("DELETE Base_Users WHERE UserID in ({0})", userIDList);
            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery);
        }
        /// <summary>
        /// 修改管理员密码
        /// </summary>
        /// <param name="userid">管理员标识</param>
        /// <param name="password">新登录密码</param>
        public int ModifyUserLogonPass(int userid, string password)
        {
            string sqlQuery = "UPDATE Base_Users SET Password = @Password WHERE UserID= @UserID";

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("UserID", userid));
            prams.Add(Database.MakeInParam("Password", password));

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }
        /// <summary>
        /// 冻结解冻管理员
        /// </summary>
        /// <param name="userIDList">管理员列表</param>
        /// <param name="nullity">管理员状态</param>
        public int NullityUser(string userIDList, int nullity)
        {
            string sqlQuery = string.Format("UPDATE Base_Users SET Nullity={0} WHERE UserID IN ({1})", nullity, userIDList);
            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery);
        }
        /// <summary>
        /// 修改管理员信息
        /// </summary>
        /// <param name="user">管理员信息</param>
        public int ModifyUserInfo(Base_Users user)
        {
            string sqlQuery = @"UPDATE Base_Users SET Password=@Password,RoleID=@RoleID,Nullity=@Nullity,
                        IsAssist=@IsAssist,IsBand=@IsBand,BandIP=@BandIP WHERE UserID=@UserID";

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("Password", user.Password));
            prams.Add(Database.MakeInParam("RoleID", user.RoleID));
            prams.Add(Database.MakeInParam("Nullity", user.Nullity));
            prams.Add(Database.MakeInParam("IsBand", user.IsBand));
            prams.Add(Database.MakeInParam("BandIP", user.BandIP));
            prams.Add(Database.MakeInParam("UserID", user.UserID));
            prams.Add(Database.MakeInParam("IsAssist", user.IsAssist));

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }
        /// <summary>
        /// 获取管理员信息
        /// </summary>
        /// <param name="userID">管理员标识</param>
        /// <returns></returns>
        public Base_Users GetUserByUserID(int userID)
        {
            string sqlQuery = string.Format("SELECT * FROM Base_Users WITH(NOLOCK) WHERE UserID={0}", userID);
            return Database.ExecuteObject<Base_Users>(sqlQuery);
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetUserList()
        {
            string sqlQuery = @"SELECT UserID, RoleID,Rolename=CASE UserID WHEN 1 THEN N'超级管理员' 
                ELSE (SELECT RoleName FROM Base_Roles(NOLOCK) WHERE RoleID=u.RoleID) END,
                UserName,PreLogintime,PreLoginIP,LastLogintime,LastLoginIP,LoginTimes,IsBand,BandIP 
                FROM Base_Users(NOLOCK) AS u WHERE UserID>1";
            return Database.ExecuteDataset(sqlQuery);
        }
        #endregion

        #region 角色管理
        /// <summary>
        /// 获取管理员角色
        /// </summary>
        /// <param name="roleID">角色标识</param>
        /// <returns></returns>
        public Base_Roles GetRoleInfo(int roleID)
        {
            string sqlQuery = string.Format("SELECT * FROM Base_Roles WITH(NOLOCK) WHERE RoleID= {0}", roleID);
            return Database.ExecuteObject<Base_Roles>(sqlQuery);
        }
        /// <summary>
        /// 新增管理员角色
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns></returns>
        public int InsertRole(Base_Roles role)
        {
            string sqlQuery = @"INSERT INTO Base_Roles(RoleName,[Description]) VALUES(@RoleName,@Description)";

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("RoleName", role.RoleName));
            prams.Add(Database.MakeInParam("Description", role.Description));

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }
        /// <summary>
        /// 修改管理员角色
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns></returns>
        public int UpdateRole(Base_Roles role)
        {
            string sqlQuery = @"UPDATE Base_Roles SET RoleName=@RoleName,[Description]=@Description WHERE RoleID=@RoleID";

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("RoleName", role.RoleName));
            prams.Add(Database.MakeInParam("Description", role.Description));
            prams.Add(Database.MakeInParam("RoleID", role.RoleID));

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }
        /// <summary>
        /// 删除管理员角色
        /// </summary>
        /// <param name="idlist">标识列表</param>
        public int DeleteRole(string idlist)
        {
            string sqlQuery = string.Format("DELETE Base_Roles WHERE RoleID IN({0})", idlist);
            return Database.ExecuteNonQuery(sqlQuery);
        }
        #endregion

        #region 菜单列表
        /// <summary>
        /// 获取用户菜单列表
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <returns></returns>
        public DataSet GetMenuByUserID(int userID)
        {
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwUserID", userID));

            return Database.ExecuteDataset(CommandType.StoredProcedure, "NET_PM_GetMenuByUserID", prams.ToArray());
        }
        /// <summary>
        /// 获取用户权限列表
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <returns></returns>
        public DataSet GetPermissionByUserID(int userID)
        {
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwUserID", userID));

            return Database.ExecuteDataset(CommandType.StoredProcedure, "NET_PM_GetPermissionByUserID", prams.ToArray());
        }
        /// <summary>
        /// 获取父级菜单列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetModuleParentList()
        {
            string sqlQuery = "SELECT * FROM Base_Module WITH(NOLOCK) WHERE ParentID=0 AND Nullity=0 ORDER BY OrderNo";
            return Database.ExecuteDataset(CommandType.Text, sqlQuery);
        }
        /// <summary>
        /// 获取子级菜单列表
        /// </summary>
        /// <param name="moduleID">父级菜单标识</param>
        /// <returns></returns>
        public DataSet GetModuleListByModuleID(int moduleID)
        {
            string sqlQuery = string.Format("SELECT * FROM Base_Module WITH(NOLOCK) WHERE ParentID={0} ORDER BY OrderNo", moduleID);
            return Database.ExecuteDataset(CommandType.Text, sqlQuery);
        }
        /// <summary>
        /// 获取菜单权限列表
        /// </summary>
        /// <param name="moduleID">菜单标识</param>
        /// <returns></returns>
        public DataSet GetModulePermissionList(int moduleID)
        {
            string sqlQuery = string.Format("SELECT * FROM Base_ModulePermission WITH(NOLOCK) WHERE ModuleID={0}", moduleID);
            return Database.ExecuteDataset(CommandType.Text, sqlQuery);
        }
        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <param name="roleID">角色标识</param>
        /// <returns></returns>
        public DataSet GetRolePermissionList(int roleID)
        {
            string sqlQuery = string.Format("SELECT * FROM Base_RolePermission WITH(NOLOCK) WHERE RoleID={0}", roleID);
            return Database.ExecuteDataset(CommandType.Text, sqlQuery);
        }
        /// <summary>
        /// 新增角色权限
        /// </summary>
        /// <param name="rolePermission">角色权限信息</param>
        /// <returns></returns>
        public int InsertRolePermission(Base_RolePermission rolePermission)
        {
            string sqlQuery = @"INSERT INTO Base_RolePermission VALUES(@RoleID,
                    @ModuleID,@ManagerPermission,@OperationPermission,@StateFlag)";

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("RoleID", rolePermission.RoleID));
            prams.Add(Database.MakeInParam("ModuleID", rolePermission.ModuleID));
            prams.Add(Database.MakeInParam("ManagerPermission", rolePermission.ManagerPermission));
            prams.Add(Database.MakeInParam("OperationPermission", rolePermission.OperationPermission));
            prams.Add(Database.MakeInParam("StateFlag", rolePermission.StateFlag));

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray());
        }
        /// <summary>
        /// 删除角色权限
        /// </summary>
        /// <param name="roleID">角色标识</param>
        /// <returns></returns>
        public int DeleteRolePermission(int roleID)
        {
            string sqlQuery = string.Format("DELETE Base_RolePermission WHERE RoleID ={0}", roleID);
            return Database.ExecuteNonQuery(sqlQuery);
        }
        #endregion
    }
}
