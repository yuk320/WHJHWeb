using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Entity.Accounts
{
    public class WxUser
    {
        public string openid { get; set; }
        public string unionid { get; set; }
        public string nickname { get; set; }
        public byte sex { get; set; }
        public string headimgurl { get; set; }
    }
}
