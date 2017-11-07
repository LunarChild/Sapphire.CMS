namespace Sapphire.Core.Config
{
    /// <summary>
    /// 水印配置。
    /// </summary>
    public static class ThumbnailConfig
    {
        /// <summary>
        /// 水印配置实例。
        /// </summary>
        public static IThumbnailConfig Instance
        {
            get
            {
                return GlobalConfigProvider.Current.GetThumbnailConfig();
            }
        }
    }
}
