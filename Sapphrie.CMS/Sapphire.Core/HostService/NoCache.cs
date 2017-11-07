using System.Web.Caching;

namespace Sapphire.Core.HostService
{
    /// <summary>
    /// Use this implementation to indicate that no cache should be used。
    /// </summary>
    public class NoCache : CacheDependency
    {
        private static readonly NoCache InternalInstance = new NoCache();

        /// <summary>
        /// Prevents a default instance of the <see cref="NoCache"/> class from being created。
        /// </summary>
        private NoCache()
        {
        }

        /// <summary>
        /// 获取当前实例。
        /// </summary>
        public static NoCache Instance
        {
            get
            {
                return InternalInstance;
            }
        }
    }
}