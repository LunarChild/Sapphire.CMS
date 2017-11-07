namespace Sapphire.Core.Principal
{
    /// <summary>
    /// 全局管理员提供者。
    /// </summary>
    public static class GlobalSiteProvider
    {
        /// <summary>
        /// 当前使用的站点提供者。
        /// </summary>
        public static ISiteProvider Current { internal get; set; }
    }
}