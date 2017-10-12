using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Data.Factory;
using Game.IData;
using Game.Kernel;
using Game.Utils;
using Game.Entity.Accounts;
using System.Data;
using System.Data.Common;

namespace Game.Facade
{
    /// <summary>
    /// 网站外观
    /// </summary>
    public class GameMatchFacade
    {

        #region Fields

        private IGameMatchProvider gameMatchData;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public GameMatchFacade()
        {
            gameMatchData = ClassFactory.GetIGameMatchProvider();
        }

        #endregion 构造函数
    }
}
