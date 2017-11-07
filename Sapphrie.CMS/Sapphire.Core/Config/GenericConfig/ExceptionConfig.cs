namespace Sapphire.Core.Config
{
    /// <summary>
    /// 异常配置信息。
    /// </summary>
    public static class ExceptionConfig
    {
        /// <summary>
        /// 异常配置信息实例。
        /// </summary>
        public static IExceptionConfig Instance
        {
            get
            {
                return GlobalConfigProvider.Current.GetExceptionConfig();
            }
        }
    }
}