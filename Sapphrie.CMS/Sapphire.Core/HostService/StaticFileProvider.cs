using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Caching;
using System.Web.Hosting;

namespace Sapphire.Core.HostService
{
    /// <summary>
    /// 静态文件提供者。
    /// </summary>
    /// <remarks>提供对图片、脚本、样式表等静态文件的访问。</remarks>
    public class StaticFileProvider : IViewFileProvider
    {
        private readonly IViewFileLocator staticFileLocator;

        /// <summary>
        /// 初始化 StaticFileProvider 类的新实例。
        /// </summary>
        /// <param name="staticFileLocator">Plugin directory of the web site。</param>
        public StaticFileProvider(IViewFileLocator staticFileLocator)
        {
            if (staticFileLocator == null)
            {
                throw new ArgumentNullException("staticFileLocator");
            }

            this.staticFileLocator = staticFileLocator;
            this.staticFileLocator.SetAllowedExtensions(new[] { "png", "jpg", "jpeg", "gif", "css", "js", "swf" });
        }

        /// <summary>
        /// 获取和设置提供者支持的文件扩展名（不包括符号点）。
        /// </summary>
        /// <value>Default is cshtml, ascx and aspx.。</value>
        public string[] AllowedFileExtensions
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                this.staticFileLocator.SetAllowedExtensions(value);
            }
        }

        /// <summary>
        /// 获取一个值，该值指示文件是否存在于虚拟文件系统中。
        /// </summary>
        /// <param name="virtualPath">虚拟文件的路径。</param>
        /// <returns>如果该文件存在于虚拟文件系统中，则为 true；否则为 false。</returns>
        public bool FileExists(string virtualPath)
        {
            return this.staticFileLocator.GetFullPath(virtualPath) != null;
        }

        /// <summary>
        /// 基于指定的虚拟路径创建一个缓存依赖项。
        /// </summary>
        /// <param name="virtualPath">主虚拟资源的路径。</param>
        /// <param name="virtualPathDependencies">一个路径数组，路径指向主要虚拟资源需要的其他资源。</param>
        /// <param name="utcStart">虚拟资源被读取的 UTC 时间。</param>
        /// <returns>指定虚拟资源的 CacheDependency 对象。</returns>
        public CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            var fullPath = this.staticFileLocator.GetFullPath(virtualPath);
            var dependencyList = virtualPathDependencies.Cast<string>().ToList();
            if (fullPath != null)
            {
                return new CacheDependency(fullPath);
            }

            if (dependencyList.Count > 0)
            {
                fullPath = this.staticFileLocator.GetFullPath(dependencyList[0]);
                return new CacheDependency(fullPath);
            }

            return null;
        }

        /// <summary>
        /// 返回一个用于指定虚拟路径的缓存键。
        /// </summary>
        /// <param name="virtualPath">虚拟资源的路径。</param>
        /// <returns>所指定虚拟资源的缓存键。</returns>
        public string GetCacheKey(string virtualPath)
        {
            return null;
        }

        /// <summary>
        /// 返回指定虚拟路径的哈希值。
        /// </summary>
        /// <param name="virtualPath">主虚拟资源的路径。</param>
        /// <param name="virtualPathDependencies">一个路径数组，所包含的路径指向主要虚拟资源需要的其他虚拟资源。</param>
        /// <returns>指定虚拟路径的哈希值。</returns>
        public string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
        {
            var fullPath = this.staticFileLocator.GetFullPath(virtualPath);
            return fullPath != null ? File.GetLastWriteTime(fullPath).ToString(CultureInfo.InvariantCulture) : null;
        }

        /// <summary>
        /// 从虚拟文件系统中获取一个虚拟文件。
        /// </summary>
        /// <param name="virtualPath">虚拟文件的路径。</param>
        /// <returns>VirtualFile 类的子代，该子代表示虚拟文件系统中的一个文件。</returns>
        public VirtualFile GetFile(string virtualPath)
        {
            var fullPath = this.staticFileLocator.GetFullPath(virtualPath);
            if (fullPath == null)
            {
                return null;
            }

            var fileView = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return new FileResource(virtualPath, fileView);
        }

        #region Nested type: FileResource

        private class FileResource : VirtualFile
        {
            private readonly Stream stream;

            public FileResource(string virtualPath, Stream stream)
                : base(virtualPath)
            {
                this.stream = stream;
            }

            public override bool IsDirectory
            {
                get
                {
                    return false;
                }
            }

            public override Stream Open()
            {
                return this.stream;
            }
        }

        #endregion
    }
}