using Game.Data.Factory;

namespace Game.Facade.Facade
{
    public class GameScoreFacade
    {
        #region Fields

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public GameScoreFacade( string conn )
        {
            ClassFactory.GetIGameScoreDataProvider( conn );
        }

        #endregion
    }
}
