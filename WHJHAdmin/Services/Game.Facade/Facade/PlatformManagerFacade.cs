using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Data.Factory;
using Game.IData;
using Game.Kernel;
using Game.Entity.PlatformManager;
using Game.Utils;
using System.Data;
using Game.Entity;
using Game.Utils.Cache;

namespace Game.Facade
{
    public class PlatformManagerFacade
    {
        #region Fields

        private IPlatformManagerDataProvider aidePlatformManagerData;

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
            return aidePlatformManagerData.GetList(tableName, pageIndex, pageSize, condition, orderby);
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public PlatformManagerFacade()
        {
            aidePlatformManagerData = ClassFactory.GetIPlatformManagerDataProvider();
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
            return aidePlatformManagerData.UserLogon(user);
        }
        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <param name="user">管理员信息</param>
        public int RegisterUser(Base_Users user)
        {
            return aidePlatformManagerData.RegisterUser(user);
        }
        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="userIDList">管理员列表</param>
        public int DeleteUser(string userIDList)
        {
            return aidePlatformManagerData.DeleteUser(userIDList);
        }
        /// <summary>
        /// 修改管理员密码
        /// </summary>
        /// <param name="userid">管理员标识</param>
        /// <param name="password">新登录密码</param>
        public int ModifyUserLogonPass(int userid, string password)
        {
            return aidePlatformManagerData.ModifyUserLogonPass(userid, password);
        }
        /// <summary>
        /// 冻结解冻管理员
        /// </summary>
        /// <param name="userIDList">管理员列表</param>
        /// <param name="nullity">管理员状态</param>
        public int NullityUser(string userIDList, int nullity)
        {
            return aidePlatformManagerData.NullityUser(userIDList, nullity);
        }
        /// <summary>
        /// 修改管理员信息
        /// </summary>
        /// <param name="user">管理员信息</param>
        public int ModifyUserInfo(Base_Users user)
        {
            return aidePlatformManagerData.ModifyUserInfo(user);
        }
        /// <summary>
        /// 获取管理员信息
        /// </summary>
        /// <param name="userID">管理员标识</param>
        /// <returns></returns>
        public Base_Users GetUserByUserID(int userID)
        {
            return aidePlatformManagerData.GetUserByUserID(userID);
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetUserList()
        {
            return aidePlatformManagerData.GetUserList();
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
            return aidePlatformManagerData.GetRoleInfo(roleID);
        }
        /// <summary>
        /// 新增管理员角色
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns></returns>
        public int InsertRole(Base_Roles role)
        {
            return aidePlatformManagerData.InsertRole(role);
        }
        /// <summary>
        /// 修改管理员角色
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns></returns>
        public int UpdateRole(Base_Roles role)
        {
            return aidePlatformManagerData.UpdateRole(role);
        }
        /// <summary>
        /// 删除管理员角色
        /// </summary>
        /// <param name="idlist">标识列表</param>
        public int DeleteRole(string idlist)
        {
            return aidePlatformManagerData.DeleteRole(idlist);
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
            return aidePlatformManagerData.GetMenuByUserID(userID);
        }
        /// <summary>
        /// 获取用户权限列表
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <returns></returns>
        public DataSet GetPermissionByUserID(int userID)
        {
            return aidePlatformManagerData.GetPermissionByUserID(userID);
        }
        /// <summary>
        /// 获取父级菜单列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetModuleParentList()
        {
            return aidePlatformManagerData.GetModuleParentList();
        }
        /// <summary>
        /// 获取子级菜单列表
        /// </summary>
        /// <param name="moduleID">父级菜单标识</param>
        /// <returns></returns>
        public DataSet GetModuleListByModuleID(int moduleID)
        {
            return aidePlatformManagerData.GetModuleListByModuleID(moduleID);
        }
        /// <summary>
        /// 获取菜单权限列表
        /// </summary>
        /// <param name="moduleID">菜单标识</param>
        /// <returns></returns>
        public DataSet GetModulePermissionList(int moduleID)
        {
            return aidePlatformManagerData.GetModulePermissionList(moduleID);
        }
        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <param name="roleID">角色标识</param>
        /// <returns></returns>
        public DataSet GetRolePermissionList(int roleID)
        {
            return aidePlatformManagerData.GetRolePermissionList(roleID);
        }
        /// <summary>
        /// 新增角色权限
        /// </summary>
        /// <param name="rolePermission">角色权限信息</param>
        /// <returns></returns>
        public int InsertRolePermission(Base_RolePermission rolePermission)
        {
            return aidePlatformManagerData.InsertRolePermission(rolePermission);
        }
        /// <summary>
        /// 删除角色权限
        /// </summary>
        /// <param name="roleID">角色标识</param>
        /// <returns></returns>
        public int DeleteRolePermission(int roleID)
        {
            return aidePlatformManagerData.DeleteRolePermission(roleID);
        }
        #endregion
    }
}
