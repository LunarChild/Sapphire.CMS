using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Caching;
using System.Web.Hosting;

namespace Sapphire.Core.HostService
{
    /// <summary>
    /// 自定义的虚拟路径提供者。
    /// </summary>
    public class PowerVirtualPathProvider : VirtualPathProvider
    {
        private static readonly PowerVirtualPathProvider Instance = new PowerVirtualPathProvider();

        private readonly List<IViewFileProvider> fileProviders = new List<IViewFileProvider>();

        /// <summary>
        /// 初始化 PowerVirtualPathProvider 类的新实例。
        /// </summary>
        private PowerVirtualPathProvider()
        {
        }

        /// <summary>
        /// 获得实例。
        /// </summary>
        public static PowerVirtualPathProvider Current
        {
            get
            {
                return Instance;
            }
        }

        /// <summary>
        /// 添加一个新的模板文件提供者。
        /// </summary>
        /// <param name="viewFileProvider">模板文件提供者。</param>
        public void Add(IViewFileProvider viewFileProvider)
        {
            if (viewFileProvider == null)
            {
                throw new ArgumentNullException("viewFileProvider");
            }

            this.fileProviders.Add(viewFileProvider);
        }

        /// <summary>
        /// 获取一个值，该值指示文件是否存在于虚拟文件系统中。
        /// </summary>
        /// <param name="virtualPath">虚拟文件的路径。</param>
        /// <returns>如果该文件存在于虚拟文件系统中，则为 true；否则为 false。</returns>
        public override bool FileExists(string virtualPath)
        {
            if (this.fileProviders.Any(provider => provider.FileExists(virtualPath)))
            {
                return true;
            }

            return base.FileExists(virtualPath);
        }

        /// <summary>
        /// 基于指定的虚拟路径创建一个缓存依赖项。
        /// </summary>
        /// <param name="virtualPath">主虚拟资源的路径。</param>
        /// <param name="virtualPathDependencies">一个路径数组，路径指向主要虚拟资源需要的其他资源。</param>
        /// <param name="utcStart">虚拟资源被读取的 UTC 时间。</param>
        /// <returns>指定虚拟资源的 CacheDependency 对象。</returns>
        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            var pathDependencies = virtualPathDependencies as IList<object> ?? virtualPathDependencies.Cast<object>().ToList();
            foreach (var provider in this.fileProviders)
            {
                var result = provider.GetCacheDependency(virtualPath, pathDependencies, utcStart);
                if (result is NoCache)
                {
                    return null;
                }

                if (result != null)
                {
                    return result;
                }
            }

            return base.GetCacheDependency(virtualPath, pathDependencies, utcStart);
        }

        /// <summary>
        /// 返回一个用于指定虚拟路径的缓存键。
        /// </summary>
        /// <param name="virtualPath">虚拟资源的路径。</param>
        /// <returns>所指定虚拟资源的缓存键。</returns>
        public override string GetCacheKey(string virtualPath)
        {
            foreach (var result in
                this.fileProviders.Select(provider => provider.GetCacheKey(virtualPath)).Where(result => result != null))
            {
                return result;
            }

            return base.GetCacheKey(virtualPath);
        }

        /// <summary>
        /// 从虚拟文件系统中获取一个虚拟文件。
        /// </summary>
        /// <param name="virtualPath">虚拟文件的路径。</param>
        /// <returns>VirtualFile 类的子代，该子代表示虚拟文件系统中的一个文件。</returns>
        public override VirtualFile GetFile(string virtualPath)
        {
            foreach (var provider in this.fileProviders)
            {
                var file = provider.GetFile(virtualPath);
                if (file != null)
                {
                    return file;
                }
            }

            return base.GetFile(virtualPath);
        }

        /// <summary>
        /// 返回指定虚拟路径的哈希值。
        /// </summary>
        /// <param name="virtualPath">主虚拟资源的路径。</param>
        /// <param name="virtualPathDependencies">一个路径数组，所包含的路径指向主要虚拟资源需要的其他虚拟资源。</param>
        /// <returns>指定虚拟路径的哈希值。</returns>
        public override string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
        {
            foreach (var result in
                this.fileProviders.Select(provider => provider.GetFileHash(virtualPath, virtualPathDependencies)).Where(result => result != null))
            {
                return result;
            }

            return base.GetFileHash(virtualPath, virtualPathDependencies);
        }
    }
}