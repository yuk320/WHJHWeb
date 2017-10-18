using Game.Kernel;
using Game.Entity.PlatformManager;
using System.Data;

namespace Game.IData
{
    /// <summary>
    /// 后台数据层接口
    /// </summary>
    public interface IPlatformManagerDataProvider //: IProvider
    {
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
        PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby);
        #endregion

        #region 管理员管理
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="user">管理员信息</param>
        /// <returns></returns>
        Message UserLogon(Base_Users user);
        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <param name="user">管理员信息</param>
        int RegisterUser(Base_Users user);
        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="userIdList">管理员列表</param>
        int DeleteUser(string userIdList);
        /// <summary>
        /// 修改管理员密码
        /// </summary>
        /// <param name="userid">管理员标识</param>
        /// <param name="password">新登录密码</param>
        int ModifyUserLogonPass(int userid, string password);
        /// <summary>
        /// 冻结解冻管理员
        /// </summary>
        /// <param name="userIdList">管理员列表</param>
        /// <param name="nullity">管理员状态</param>
        int NullityUser(string userIdList, int nullity);
        /// <summary>
        /// 修改管理员信息
        /// </summary>
        /// <param name="user">管理员信息</param>
        int ModifyUserInfo(Base_Users user);
        /// <summary>
        /// 获取管理员信息
        /// </summary>
        /// <param name="userId">管理员标识</param>
        /// <returns></returns>
        Base_Users GetUserByUserId(int userId);
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        DataSet GetUserList();
        #endregion

        #region 角色管理
        /// <summary>
        /// 获取管理员角色
        /// </summary>
        /// <param name="roleId">角色标识</param>
        /// <returns></returns>
        Base_Roles GetRoleInfo(int roleId);
        /// <summary>
        /// 新增管理员角色
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns></returns>
        int InsertRole(Base_Roles role);
        /// <summary>
        /// 修改管理员角色
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns></returns>
        int UpdateRole(Base_Roles role);
        /// <summary>
        /// 删除管理员角色
        /// </summary>
        /// <param name="idlist">标识列表</param>
        int DeleteRole(string idlist);
        #endregion

        #region 菜单列表
        /// <summary>
        /// 获取用户菜单列表
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <returns></returns>
        DataSet GetMenuByUserId(int userId);
        /// <summary>
        /// 获取用户权限列表
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <returns></returns>
        DataSet GetPermissionByUserId(int userId);
        /// <summary>
        /// 获取父级菜单列表
        /// </summary>
        /// <returns></returns>
        DataSet GetModuleParentList();
        /// <summary>
        /// 获取子级菜单列表
        /// </summary>
        /// <param name="moduleId">父级菜单标识</param>
        /// <returns></returns>
        DataSet GetModuleListByModuleId(int moduleId);
        /// <summary>
        /// 获取菜单权限列表
        /// </summary>
        /// <param name="moduleId">菜单标识</param>
        /// <returns></returns>
        DataSet GetModulePermissionList(int moduleId);
        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <param name="roleId">角色标识</param>
        /// <returns></returns>
        DataSet GetRolePermissionList(int roleId);
        /// <summary>
        /// 新增角色权限
        /// </summary>
        /// <param name="rolePermission">角色权限信息</param>
        /// <returns></returns>
        int InsertRolePermission(Base_RolePermission rolePermission);
        /// <summary>
        /// 删除角色权限
        /// </summary>
        /// <param name="roleId">角色标识</param>
        /// <returns></returns>
        int DeleteRolePermission(int roleId);
        #endregion
    }
}
