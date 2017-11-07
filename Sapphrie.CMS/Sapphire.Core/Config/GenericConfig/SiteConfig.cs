namespace Sapphire.Core.Config
{
    /// <summary>
    /// 网站信息配置。
    /// </summary>
    public static class SiteConfig
    {
        /// <summary>
        /// 网站信息配置实例。
        /// </summary>
        public static ISiteConfig Instance
        {
            get
            {
                return GlobalConfigProvider.Current.GetSiteConfig();
            }
        }
    }
}