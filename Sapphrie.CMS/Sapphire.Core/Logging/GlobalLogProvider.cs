namespace Sapphire.Core.Logging
{
    /// <summary>
    /// 全局日志提供者。
    /// </summary>
    public static class GlobalLogProvider
    {
        /// <summary>
        /// 当前日志提供者。
        /// </summary>
        public static ILogProvider Current { internal get; set; }
    }
}