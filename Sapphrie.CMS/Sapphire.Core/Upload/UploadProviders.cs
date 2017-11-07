using System;

namespace Sapphire.Core.Upload
{
    /// <summary>
    /// 全局上传提供者容器类。
    /// </summary>
    public static class UploadProviders
    {
        private static readonly UploadProviderDictionary ProviderDictionary = new UploadProviderDictionary(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 静态初始化 UploadProviders 。
        /// </summary>
        static UploadProviders()
        {
            Register(new GlobalUploadProvider());
        }

        /// <summary>
        /// 上传提供者键值集合。
        /// </summary>
        public static UploadProviderDictionary Providers
        {
            get
            {
                return ProviderDictionary;
            }
        }

        /// <summary>
        /// 向提供者键值集合中添加上传提供者。
        /// </summary>
        /// <param name="uploadProvider">上传提供者。</param>
        public static void Register(IUploadProvider uploadProvider)
        {
            Providers.Add(uploadProvider.GetUploadProviderKey(), uploadProvider);
        }
    }
}