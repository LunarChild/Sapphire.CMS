namespace Sapphire.Core.Config
{
    /// <summary>
    /// 网站安全配置。
    /// </summary>
    public static class SecurityConfig
    {
        /// <summary>
        /// 网站安全配置实例。
        /// </summary>
        public static ISecurityConfig Instance
        {
            get
            {
                return GlobalConfigProvider.Current.GetSecurityConfig();
            }
        }
    }
}