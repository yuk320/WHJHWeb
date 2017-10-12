using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Facade
{
    /// <summary>
    /// 逻辑层管理类
    /// </summary>
    public class FacadeManage
    {
        private static object lockObj = new object();

        ///// <summary>
        ///// 前台逻辑
        ///// </summary>
        private static volatile NativeWebFacade _aideNativeWebFacade;
        public static NativeWebFacade aideNativeWebFacade
        {
            get
            {
                if( _aideNativeWebFacade == null )
                {
                    lock( lockObj )
                    {
                        if( _aideNativeWebFacade == null )
                            _aideNativeWebFacade = new NativeWebFacade();
                    }
                }
                return _aideNativeWebFacade;
            }
        }

        ///// <summary>
        ///// 平台逻辑
        ///// </summary>
        private static volatile PlatformFacade _aidePlatformFacade;
        public static PlatformFacade aidePlatformFacade
        {
            get
            {
                if( _aidePlatformFacade == null )
                {
                    lock( lockObj )
                    {
                        if( _aidePlatformFacade == null )
                            _aidePlatformFacade = new PlatformFacade();
                    }
                }
                return _aidePlatformFacade;
            }
        }

        ///// <summary>
        ///// 游戏币库逻辑
        ///// </summary>
        private static volatile TreasureFacade _aideTreasureFacade;
        public static TreasureFacade aideTreasureFacade
        {
            get
            {
                if( _aideTreasureFacade == null )
                {
                    lock( lockObj )
                    {
                        if( _aideTreasureFacade == null )
                            _aideTreasureFacade = new TreasureFacade();
                    }
                }
                return _aideTreasureFacade;
            }
        }

        ///// <summary>
        ///// 帐号库逻辑
        ///// </summary>
        private static volatile AccountsFacade _aideAccountsFacade;
        public static AccountsFacade aideAccountsFacade
        {
            get
            {
                if( _aideAccountsFacade == null )
                {
                    lock( lockObj )
                    {
                        if( _aideAccountsFacade == null )
                            _aideAccountsFacade = new AccountsFacade();
                    }
                }
                return _aideAccountsFacade;
            }
        }

        ///// <summary>
        ///// 记录库逻辑
        ///// </summary>
        private static volatile RecordFacade _aideRecordFacade;
        public static RecordFacade aideRecordFacade
        {
            get
            {
                if( _aideRecordFacade == null )
                {
                    lock( lockObj )
                    {
                        if( _aideRecordFacade == null )
                            _aideRecordFacade = new RecordFacade();
                    }
                }
                return _aideRecordFacade;
            }
        }

        ///// <summary>
        ///// 比赛库逻辑
        ///// </summary>
        private static volatile GameMatchFacade _aideGameMatchFacade;
        public static GameMatchFacade aideGameMatchFacade
        {
            get
            {
                if( _aideGameMatchFacade == null )
                {
                    lock( lockObj )
                    {
                        if( _aideGameMatchFacade == null )
                            _aideGameMatchFacade = new GameMatchFacade();
                    }
                }
                return _aideGameMatchFacade;
            }
        }
    }
}
