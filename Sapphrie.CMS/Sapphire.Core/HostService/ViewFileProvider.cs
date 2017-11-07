using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Caching;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Sapphire.Core.HostService
{
    /// <summary>
    /// 模板文件提供者。
    /// </summary>
    public class ViewFileProvider : IViewFileProvider
    {
        private readonly IViewFileLocator viewFileLocator;

        private readonly IExternalViewFixer viewFixer;

        /// <summary>
        /// 初始化 ViewFileProvider 类的新实例。
        /// </summary>
        /// <param name="viewFileLocator">模板文件定位器。</param>
        public ViewFileProvider(IViewFileLocator viewFileLocator)
        {
            if (viewFileLocator == null)
            {
                throw new ArgumentNullException("viewFileLocator");
            }

            this.viewFileLocator = viewFileLocator;
        }

        /// <summary>
        /// 初始化 ViewFileProvider 类的新实例。
        /// </summary>
        /// <param name="viewFileLocator">模板文件定位器。</param>
        /// <param name="viewFixer">扩展的模板固定器。</param>
        public ViewFileProvider(IViewFileLocator viewFileLocator, IExternalViewFixer viewFixer)
        {
            if (viewFileLocator == null)
            {
                throw new ArgumentNullException("viewFileLocator");
            }

            this.viewFileLocator = viewFileLocator;
            this.viewFixer = viewFixer;
            this.viewFileLocator.SetAllowedExtensions(new[] { "cshtml", "ascx", "aspx", "config" });
        }

        /// <summary>
        /// 允许的文件扩展名。
        /// </summary>
        /// <value>默认为cshtml、ascx、aspx。</value>
        public string[] AllowedFileExtensions
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                this.viewFileLocator.SetAllowedExtensions(value);
            }
        }

        #region IViewFileProvider Members

        /// <summary>
        /// Checks if a file exists in this provider。
        /// </summary>
        /// <param name="virtualPath"> Path 。</param>
        /// <returns> Determines if a file exists in this provider 。</returns>
        public bool FileExists(string virtualPath)
        {
            return this.viewFileLocator.GetFullPath(virtualPath) != null;
        }

        /// <summary>
        /// Creates a cache dependency based on the specified virtual paths。
        /// </summary>
        /// <param name="virtualPath">Virtual path like "~/Views/Home/Index.cshtml"。</param>
        /// <param name="virtualPathDependencies">An array of paths to other resources required by the primary virtual resource。</param>
        /// <param name="utcStart">The UTC time at which the virtual resources were read。</param>
        /// <returns>
        /// CacheDependency if found; otherwise false。
        /// </returns>
        public CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            string fullPath = this.viewFileLocator.GetFullPath(virtualPath);
            string webConfigFullPath = this.GetWebConfigFullPath(virtualPath);
            if (string.IsNullOrEmpty(webConfigFullPath))
            {
                return fullPath != null ? new CacheDependency(fullPath) : NoCache.Instance;
            }

            return fullPath != null ? new CacheDependency(new[] { fullPath, webConfigFullPath }) : NoCache.Instance;
        }

        /// <summary>
        /// Get file hash。
        /// </summary>
        /// <param name="virtualPath">Virtual path like "~/Views/Home/Index.cshtml"。</param>
        /// <param name="virtualPathDependencies">
        /// An array of paths to other virtual resources required by the primary virtual resource。
        /// </param>
        /// <returns>
        /// a new hash each time the file have changed (if file is found); otherwise null。
        /// </returns>
        public string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
        {
            string fullPath = this.viewFileLocator.GetFullPath(virtualPath);
            string hashValue = string.Empty;
            if (fullPath != null)
            {
                hashValue += File.GetLastWriteTime(fullPath).ToString(CultureInfo.InvariantCulture);
            }

            return string.IsNullOrEmpty(hashValue) ? null : hashValue;
        }

        /// <summary>
        /// Get the view。
        /// </summary>
        /// <param name="virtualPath"> Virtual path like "~/Views/Home/Index.cshtml" 。</param>
        /// <returns> File 。</returns>
        public VirtualFile GetFile(string virtualPath)
        {
            string fullPath = this.viewFileLocator.GetFullPath(virtualPath);
            if (fullPath == null)
            {
                return null;
            }

            string webConfigFullPath = this.GetWebConfigFullPath(virtualPath);
            FileStream webConfigFileStream = null;
            var fileView = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            if (!string.IsNullOrEmpty(webConfigFullPath))
            {
                webConfigFileStream = new FileStream(webConfigFullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }

            Stream fixedView = this.CorrectView(virtualPath, fileView, webConfigFileStream);
            return new FileResource(virtualPath, fixedView);
        }

        /// <summary>
        /// Returns a cache key to use for the specified virtual path。
        /// </summary>
        /// <param name="virtualPath">Virtual path like "~/Views/Home/Index.cshtml"。</param>
        /// <returns>CacheDependency if found; otherwise false。</returns>
        public string GetCacheKey(string virtualPath)
        {
            return null;
        }

        #endregion

        /// <summary>
        /// Used to adjust the external views before they are returned。
        /// </summary>
        /// <param name="virtualPath">Path to requested view。</param>
        /// <param name="fileStream">Loaded file。</param>
        /// <param name="webConfigStream">Stream that contain the web config。</param>
        /// <returns>Stream to use。</returns>
        protected virtual Stream CorrectView(string virtualPath, FileStream fileStream, FileStream webConfigStream)
        {
            IExternalViewFixer fixer = this.viewFixer ?? DependencyResolver.Current.GetService<IExternalViewFixer>();
            if (fixer == null)
            {
                return fileStream;
            }

            Stream fixedView = fixer.CorrectView(virtualPath, fileStream, webConfigStream);
            fileStream.Close();
            return fixedView;
        }

        private string GetWebConfigFullPath(string viewVirtualPath)
        {
            var regexAdminViewPathCurrent = new Regex("(^/Admin/Views/.*/).*cshtml$", RegexOptions.IgnoreCase);
            var regexAdminViewPathRoot = new Regex("(^/Admin/Views/.*?/).*/.*cshtml$", RegexOptions.IgnoreCase);
            var regexPadViewPath = new Regex("(^/Views.pad/.*?/.*?/).*cshtml$", RegexOptions.IgnoreCase);
            var regexPhoneViewPath = new Regex("(^/Views.phone/.*?/.*?/).*cshtml$", RegexOptions.IgnoreCase);
            //新的视图查找规则
            var regexViewPathCurrent = new Regex("(^/Views/.*?/.*/).*cshtml$", RegexOptions.IgnoreCase);
            var regexViewPathRoot = new Regex("(^/Views/.*?/.*?/).*/.*cshtml$", RegexOptions.IgnoreCase);

            Match matchAdminViewPathCurrent = regexAdminViewPathCurrent.Match(viewVirtualPath);
            Match matchViewPathCurrent = regexViewPathCurrent.Match(viewVirtualPath);
            Match matchAdminViewPathRoot = regexAdminViewPathRoot.Match(viewVirtualPath);
            Match matchViewPathRoot = regexViewPathRoot.Match(viewVirtualPath);
            Match matchPadViewPath = regexPadViewPath.Match(viewVirtualPath);
            Match matchPhoneViewPath = regexPhoneViewPath.Match(viewVirtualPath);

            string webConfigFullPath = string.Empty;
            if (matchAdminViewPathCurrent.Length > 0)
            {
                //// Admin Views。
                string adminFullPathCurrent = this.viewFileLocator.GetFullPath(matchAdminViewPathCurrent.Groups[1].Value + "Web.Config");
                string adminFullPathRoot = this.viewFileLocator.GetFullPath(matchAdminViewPathRoot.Groups[1].Value + "Web.Config");
                webConfigFullPath = !string.IsNullOrEmpty(adminFullPathCurrent) ? adminFullPathCurrent : adminFullPathRoot;
            }
            else if (matchViewPathCurrent.Length > 0)
            {
                //// Front Page Views。
                string fullPathCurrent = this.viewFileLocator.GetFullPath(matchViewPathCurrent.Groups[1].Value + "Web.Config");
                string fullPathRoot = this.viewFileLocator.GetFullPath(matchViewPathRoot.Groups[1].Value + "Web.Config");
                webConfigFullPath = !string.IsNullOrEmpty(fullPathCurrent) ? fullPathCurrent : fullPathRoot;
            }
            else if (matchPadViewPath.Length > 0)
            {
                webConfigFullPath = this.viewFileLocator.GetFullPath(matchPadViewPath.Groups[1].Value + "Web.Config");
            }
            else if (matchPhoneViewPath.Length > 0)
            {
                webConfigFullPath = this.viewFileLocator.GetFullPath(matchPhoneViewPath.Groups[1].Value + "Web.Config");
            }

            // 检查null时做了特殊处理，可能存在问题
            string commonWebConfig = Path.GetFullPath(HostingEnvironment.MapPath("~/Views/Web.Config") ?? "/Views/Web.Config");
            return string.IsNullOrEmpty(webConfigFullPath) ? commonWebConfig : webConfigFullPath;
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