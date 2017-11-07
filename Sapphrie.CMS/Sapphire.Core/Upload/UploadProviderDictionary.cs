using System.Collections.Generic;

namespace Sapphire.Core.Upload
{
    /// <summary>
    /// 上传提供者集合。
    /// </summary>
    public class UploadProviderDictionary : Dictionary<string, IUploadProvider>
    {
        /// <summary>
        /// 初始化 UploadProviderDictionary 类的新实例，该实例为空且具有默认的初始容量，并使用指定的 IEqualityComparer。
        /// </summary>
        /// <param name="comparer">比较键时要使用的 IEqualityComparer 实现。</param>
        public UploadProviderDictionary(IEqualityComparer<string> comparer)
            : base(comparer)
        {
        }
    }
}