using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Entity;
using Game.Kernel;

using Game.IData;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

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
