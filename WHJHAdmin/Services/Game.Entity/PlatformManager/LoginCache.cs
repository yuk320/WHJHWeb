using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Entity.PlatformManager
{
    /// <summary>
    /// 登录缓存类
    /// </summary>
    public class LoginCache
    {
        /// <summary>
        /// 缓存用户标识
        /// </summary>
        public int userId { get; set;}
        /// <summary>
        /// 授权页面缓存
        /// </summary>
        public List<ModulePage> pagePowerList { get; set; }
        /// <summary>
        /// 用户权限缓存
        /// </summary>
        public Dictionary<string, long> userPower { get; set; }
    }
}
