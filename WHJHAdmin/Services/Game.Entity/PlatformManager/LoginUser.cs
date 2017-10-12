using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Entity.PlatformManager
{
    [Serializable]
    public class LoginUser
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public int LoginTimes { get; set; }
        public DateTime PreLogintime { get; set; }
        public string PreLoginIP { get; set; }
    }
}
