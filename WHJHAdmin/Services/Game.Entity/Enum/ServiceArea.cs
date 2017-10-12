using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Utils;

namespace Game.Entity.Enum
{
    /// <summary>
    ///道具作用范围
    /// </summary>
    [Serializable]
    [EnumDescription( "道具作用范围" )]
    public enum ServiceArea : int
    {
        /// <summary>
        /// 自己
        /// </summary>
        [EnumDescription( "自己" )]
        A_MYSELF = 1,

        /// <summary>
        /// 玩家 
        /// </summary>
        [EnumDescription( "玩家" )]
        A_PLAYER = 2,

        /// <summary>
        /// 旁观 
        /// </summary>
        [EnumDescription( "旁观" )]
        A_LOOKER = 4

    }

    /// <summary>
    /// 辅助类
    /// </summary>
    public class ServiceAreaHelper
    {
        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetServiceAreaDes( ServiceArea status )
        {
            return EnumDescription.GetFieldText( status );
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static IList<EnumDescription> GetServiceAreaList( Type t )
        {
            IList<EnumDescription> list = EnumDescription.GetFieldTexts( t );
            return list;
        }
    }
}
