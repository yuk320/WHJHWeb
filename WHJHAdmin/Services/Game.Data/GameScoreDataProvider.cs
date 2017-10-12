using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Kernel;
using Game.IData;
using System.Data;
using Game.Entity.GameScore;
using System.Data.Common;

namespace Game.Data
{
    public class GameScoreDataProvider : BaseDataProvider,IGameScoreDataProvider
    {
        #region Fields

        private ITableProvider aideGameScoreInfo;

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public GameScoreDataProvider( string connString )
            : base( connString )
        {
            aideGameScoreInfo = GetTableProvider( GameScoreInfo.Tablename );
        }

        #endregion
    }
}
