using Game.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Entity.Enum
{
    [Serializable]
    [EnumDescription("道具类型")]
    public enum GamePropertyKind : int
    {
        /// <summary>
        /// 礼物
        /// </summary>
        [EnumDescription( "礼物" )]
        KIND1 = 1,

        /// <summary>
        /// 宝石
        /// </summary>
        [EnumDescription( "宝石" )]
        KIND2 = 2,

        /// <summary>
        /// 双卡 
        /// </summary>
        [EnumDescription( "双卡" )]
        KIND3 = 3,

        /// <summary>
        /// 防身 
        /// </summary>
        [EnumDescription( "防身" )]
        KIND4 = 4,

        /// <summary>
        /// 防踢 
        /// </summary>
        [EnumDescription( "防踢" )]
        KIND5 = 5,

        /// <summary>
        /// vip 
        /// </summary>
        [EnumDescription( "vip" )]
        KIND6 = 6,

        /// <summary>
        /// 大喇叭 
        /// </summary>
        [EnumDescription( "大喇叭" )]
        KIND7 = 7,

        /// <summary>
        /// 小喇叭 
        /// </summary>
        [EnumDescription( "小喇叭" )]
        KIND8 = 8,

        /// <summary>
        /// 负分清零 
        /// </summary>
        [EnumDescription( "负分清零" )]
        KIND9 = 9,

        /// <summary>
        /// 逃跑 
        /// </summary>
        [EnumDescription( "逃跑" )]
        KIND10 = 10,

        /// <summary>
        /// 礼包 
        /// </summary>
        [EnumDescription( "礼包" )]
        KIND11 = 11,

        /// <summary>
        /// 金币 
        /// </summary>
        [EnumDescription( "金币" )]
        KIND12 = 12
    }

    /// <summary>
    /// 辅助类
    /// </summary>
    public class GamePropertyKindHelper
    {
        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetGamePropertyKindDes(GamePropertyKind kind)
        {
            return EnumDescription.GetFieldText(kind);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static IList<EnumDescription> GetGamePropertyKindList(Type t)
        {
            IList<EnumDescription> list = EnumDescription.GetFieldTexts(t);
            return list;
        }
    }
}
