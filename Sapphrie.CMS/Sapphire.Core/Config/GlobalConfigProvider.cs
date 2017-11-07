namespace Sapphire.Core.Config
{
    /// <summary>
    /// 全局配置信息提供者。
    /// </summary>
    public static class GlobalConfigProvider
    {
        /// <summary>
        /// 当前配置信息提供者。
        /// </summary>
        public static IConfigProvider Current { internal get; set; }
    }
}