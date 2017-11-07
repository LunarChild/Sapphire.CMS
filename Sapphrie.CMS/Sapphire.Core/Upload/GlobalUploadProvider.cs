using Sapphire.Core.Config;

namespace Sapphire.Core.Upload
{
    /// <summary>
    /// 全局上传提供者。
    /// </summary>
    public class GlobalUploadProvider : GeneralFieldUploadProvider
    {
        /// <summary>
        /// 用于查找上传提供者的键。
        /// </summary>
        public const string UploadProviderKey = "Global";

        /// <summary>
        /// 获取上传配置。
        /// </summary>
        /// <returns>上传配置。</returns>
        public override IUploadConfig GetUploadConfig()
        {
            return GlobalUploadConfig.Instance;
        }

        /// <summary>
        /// 获取上传提供者的键。
        /// </summary>
        /// <returns>上传提供者的键。</returns>
        public override string GetUploadProviderKey()
        {
            return UploadProviderKey;
        }
    }
}