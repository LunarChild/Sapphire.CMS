namespace Sapphire.Core.Config
{
    /// <summary>
    /// 水印配置。
    /// </summary>
    public static class WatermarkConfig
    {
        /// <summary>
        /// 水印配置实例。
        /// </summary>
        public static IWatermarkConfig Instance
        {
            get
            {
                return GlobalConfigProvider.Current.GetWatermarkConfig();
            }
        }
    }
}
