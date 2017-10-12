using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Game.Data.Factory;
using Game.IData;
using Game.Kernel;

namespace Game.Facade
{
    /// <summary>
    /// 网站外观
    /// </summary>
    public class GameMatchFacade
    {
        #region Fields

        private IGameMatchProvider gameMatchData;

        #endregion Fields

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public GameMatchFacade()
        {
            gameMatchData = ClassFactory.GetIGameMatchProvider();
        }
        #endregion
    }
}