using Game.IData;
using Game.Kernel;

namespace Game.Data
{
    public class GameMatchDataProvider : BaseDataProvider, IGameMatchProvider
    {
        #region 构造方法

        public GameMatchDataProvider(string connString)
            : base(connString)
        {
        }

        #endregion 构造方法
    }
}