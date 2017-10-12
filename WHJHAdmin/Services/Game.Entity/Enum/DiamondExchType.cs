using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Utils;

namespace Game.Entity.Enum
{
    /// <summary>
    /// 钻石兑换金币场景
    /// </summary>
    [Serializable]
    [EnumDescription("钻石兑换金币场景")]
    public enum DiamondExchType : int
    {
        /// <summary>
        /// 不能进行游戏
        /// </summary>
        [EnumDescription("APP")] APP = 0,

        /// <summary>
        /// 不能旁观游戏
        /// </summary>
        [EnumDescription("H5")] H5 = 1
    }
}