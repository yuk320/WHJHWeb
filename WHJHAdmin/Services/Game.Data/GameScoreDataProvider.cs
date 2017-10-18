using Game.Kernel;
using Game.IData;

namespace Game.Data
{
    public sealed class GameScoreDataProvider : BaseDataProvider,IGameScoreDataProvider
    {
        #region Fields


        #endregion

        #region 构造方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public GameScoreDataProvider( string connString )
            : base( connString )
        {
        }

        #endregion
    }
}
