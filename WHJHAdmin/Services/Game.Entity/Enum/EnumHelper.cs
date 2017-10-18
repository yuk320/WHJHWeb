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
        /// <param name="t"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetDesc(Type t,object type)
        {
            return EnumDescription.GetFieldText(t,type);
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
