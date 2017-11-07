using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;

namespace Sapphire.Core.HostService
{
    /// <summary>
    /// Locator which loads views using the project structure to enable runtime view edits。
    /// </summary>
    /// <remarks>
    /// Works as long as you have used the structure which is described in the namespace documentation。
    /// </remarks>
    public class PluginFileLocator : IViewFileLocator
    {
        private readonly string basePath;

        private IEnumerable<string> allowedFileExtensions;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginFileLocator"/> class。
        /// </summary>
        public PluginFileLocator()
        {
            this.basePath = Path.GetFullPath(HostingEnvironment.MapPath("~") + @"..");
        }

        #region IViewFileLocator Members

        /// <summary>
        /// Get full path to a file。
        /// </summary>
        /// <param name="uri">Requested uri。</param>
        /// <returns>
        /// Full disk path if found; otherwise null。
        /// </returns>
        public string GetFullPath(string uri)
        {
            var pathConfigs = PluginPathConfig.GetConfigs();
            var fixedUri = uri;
            if (fixedUri.StartsWith("~"))
            {
                fixedUri = VirtualPathUtility.ToAbsolute(uri);
            }

            var path = string.Empty;
            foreach (var pattern in pathConfigs.Keys)
            {
                var regex = new Regex(pattern, RegexOptions.IgnoreCase);
                var match = regex.Match(fixedUri);
                if (match.Length > 0)
                {
                    path = Regex.Replace(fixedUri, pattern, pathConfigs[pattern], RegexOptions.IgnoreCase);
                    path = string.Format("{0}\\{1}", this.basePath, path.Replace('/', '\\'));
                    break;
                }
            }

            if (!this.IsFileAllowed(uri))
            {
                return null;
            }

            if (File.Exists(path))
            {
                return path;
            }

            return null;
        }

        /// <summary>
        /// Set extensions that are allowed to be scanned。
        /// </summary>
        /// <param name="fileExtensions">File extensions without the dot。</param>
        public void SetAllowedExtensions(IEnumerable<string> fileExtensions)
        {
            this.allowedFileExtensions = fileExtensions;
        }

        /// <summary>
        /// determins if the found embedded file might be mapped and provided。
        /// </summary>
        /// <param name="fullPath">Full path to the file。</param>
        /// <returns> true if the file is allowed; otherwise false。</returns>
        protected virtual bool IsFileAllowed(string fullPath)
        {
            if (fullPath == null)
            {
                throw new ArgumentNullException("fullPath");
            }

            var extension = fullPath.Substring(fullPath.LastIndexOf('.') + 1);
            return this.allowedFileExtensions.Any(x => x == extension.ToLower());
        }

        #endregion
    }
}