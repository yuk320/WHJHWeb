using Game.Kernel;
using Game.IData;

namespace Game.Data
{
    public class GameMatchDataProvider : BaseDataProvider,IGameMatchProvider
    {
        #region 构造方法

        public GameMatchDataProvider(string connString)
            : base(connString)
        {

        }

        #endregion
    }
}
