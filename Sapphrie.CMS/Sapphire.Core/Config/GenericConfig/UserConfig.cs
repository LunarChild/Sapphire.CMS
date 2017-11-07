namespace Sapphire.Core.Config
{
    /// <summary>
    /// 网站用户配置。
    /// </summary>
    public static class UserConfig
    {
        /// <summary>
        /// 网站用户配置实例。
        /// </summary>
        public static IUserConfig Instance
        {
            get
            {
                return GlobalConfigProvider.Current.GetUserConfig();
            }
        }
    }
}