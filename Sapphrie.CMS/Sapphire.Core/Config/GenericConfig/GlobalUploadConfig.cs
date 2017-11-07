using Sapphire.Core.Upload;

namespace Sapphire.Core.Config
{
    /// <summary>
    /// 全局上传配置。
    /// </summary>
    public static class GlobalUploadConfig
    {
        /// <summary>
        /// 全局上传配置实例。
        /// </summary>
        public static IGlobalUploadConfig Instance
        {
            get
            {
                return GlobalConfigProvider.Current.GetGlobalUploadConfig();
            }
        }
    }
}