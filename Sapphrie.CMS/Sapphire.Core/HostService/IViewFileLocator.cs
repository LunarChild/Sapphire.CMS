using System.Collections.Generic;

namespace Sapphire.Core.HostService
{
    /// <summary>
    /// 定位文件类。
    /// </summary>
    public interface IViewFileLocator
    {
        /// <summary>
        /// Get full path to a file。
        /// </summary>
        /// <param name="uri">Requested uri。</param>
        /// <returns>Full disk path if found; otherwise null。</returns>
        string GetFullPath(string uri);

        /// <summary>
        /// Set extensions that are allowed to be scanned。
        /// </summary>
        /// <param name="fileExtensions">File extensions without the dot。</param>
        void SetAllowedExtensions(IEnumerable<string> fileExtensions);
    }
}