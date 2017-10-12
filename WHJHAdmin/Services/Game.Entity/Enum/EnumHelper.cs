using System;
using System.Collections.Generic;
using Game.Utils;

namespace Game.Entity.Enum
{
    public class EnumHelper
    {
        /// <summary>
        /// 基类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetDesc(object type)
        {
            return EnumDescription.GetFieldText(type);
        }

        /// <summary>
        /// 获取钻石兑换金币场景集合
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static IList<EnumDescription> GetList(Type t)
        {
            IList<EnumDescription> list = EnumDescription.GetFieldTexts(t);
            return list;
        }
    }
}
