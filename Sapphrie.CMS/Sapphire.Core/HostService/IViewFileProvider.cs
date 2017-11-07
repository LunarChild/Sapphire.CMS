using System;
using System.Collections;
using System.Web.Caching;
using System.Web.Hosting;

namespace Sapphire.Core.HostService
{
    /// <summary>
    /// Provides view files to <see cref="PowerVirtualPathProvider"/>。
    /// </summary>
    public interface IViewFileProvider
    {
        /// <summary>
        /// 获取一个值，该值指示文件是否存在于虚拟文件系统中。
        /// </summary>
        /// <param name="virtualPath">虚拟文件的路径。</param>
        /// <returns>如果该文件存在于虚拟文件系统中，则为 true；否则为 false。</returns>
        bool FileExists(string virtualPath);

        /// <summary>
        /// 基于指定的虚拟路径创建一个缓存依赖项。
        /// </summary>
        /// <param name="virtualPath">主虚拟资源的路径。</param>
        /// <param name="virtualPathDependencies">一个路径数组，路径指向主要虚拟资源需要的其他资源。</param>
        /// <param name="utcStart">虚拟资源被读取的 UTC 时间。</param>
        /// <returns>指定虚拟资源的 CacheDependency 对象。</returns>
        CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart);

        /// <summary>
        /// 返回一个用于指定虚拟路径的缓存键。
        /// </summary>
        /// <param name="virtualPath">虚拟资源的路径。</param>
        /// <returns>所指定虚拟资源的缓存键。</returns>
        string GetCacheKey(string virtualPath);

        /// <summary>
        /// 返回指定虚拟路径的哈希值。
        /// </summary>
        /// <param name="virtualPath">主虚拟资源的路径。</param>
        /// <param name="virtualPathDependencies">一个路径数组，所包含的路径指向主要虚拟资源需要的其他虚拟资源。</param>
        /// <returns>指定虚拟路径的哈希值。</returns>
        string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies);

        /// <summary>
        /// 从虚拟文件系统中获取一个虚拟文件。
        /// </summary>
        /// <param name="virtualPath">虚拟文件的路径。</param>
        /// <returns>VirtualFile 类的子代，该子代表示虚拟文件系统中的一个文件。</returns>
        VirtualFile GetFile(string virtualPath);
    }
}