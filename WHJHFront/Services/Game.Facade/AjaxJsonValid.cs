namespace Game.Facade
{
    /// <summary>
    /// 默认数据项带Valid的Ajax异步请求返回数据类
    /// </summary>
    public class AjaxJsonValid : AjaxJson
    {
        /// <summary>
        /// 构造函数 带“valid”数据项
        /// </summary>
        public AjaxJsonValid( )
        {
            SetDataItem( "valid", false );
        }

        /// <summary>
        /// 构造函数 带“valid”数据项并赋值
        /// </summary>
        /// <param name="result"></param>
        public AjaxJsonValid( bool result )
        {
            SetDataItem( "valid", result );
        }

        /// <summary>
        /// 设置Valid项值
        /// </summary>
        public void SetValidDataValue( bool result )
        {
            SetDataItem( "valid", result );
        }
    }
}
