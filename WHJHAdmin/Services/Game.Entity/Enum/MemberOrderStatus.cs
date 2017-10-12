using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Utils;

namespace Game.Entity.Enum
{
    /// <summary>
    /// 会员等级
    /// </summary>
    [Serializable]
    [EnumDescription( "会员等级" )]
    public enum MemberOrderStatus : int
    {
        /// <summary>
        /// 普通玩家
        /// </summary>
        [EnumDescription( "普通玩家" )]
        普通玩家 = 0,
        /// <summary>
        /// VIP1 
        /// </summary>
        [EnumDescription( "VIP1" )]
        VIP1 = 1,

        /// <summary>
        /// VIP2
        /// </summary>
        [EnumDescription("VIP2")]
        VIP2 = 2,

        /// <summary>
        /// VIP3
        /// </summary>
        [EnumDescription("VIP3")]
        VIP3 = 3,

        /// <summary>
        /// VIP4
        /// </summary>
        [EnumDescription("VIP4")]
        VIP4 = 4,

        /// <summary>
        /// VIP5
        /// </summary>
        [EnumDescription( "VIP5" )]
        VIP5 = 5
    }

    /// <summary>
    /// 辅助类
    /// </summary>
    public class MemberOrderHelper
    {
        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetMemberOrderStatusDes( MemberOrderStatus status )
        {
            return EnumDescription.GetFieldText( status );
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static IList<EnumDescription> GetMemberOrderStatusList( Type t )
        {
            IList<EnumDescription> list = EnumDescription.GetFieldTexts( t );
            return list;
        }       
    }
}
