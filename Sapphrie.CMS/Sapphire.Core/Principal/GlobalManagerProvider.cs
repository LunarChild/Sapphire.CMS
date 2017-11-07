namespace Sapphire.Core.Principal
{
    /// <summary>
    /// 全局管理员提供者。
    /// </summary>
    public static class GlobalManagerProvider
    {
        /// <summary>
        /// 当前使用的管理员提供者。
        /// </summary>
        public static IManagerProvider Current { internal get; set; }
    }
}