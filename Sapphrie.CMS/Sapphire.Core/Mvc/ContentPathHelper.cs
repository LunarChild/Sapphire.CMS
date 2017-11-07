using System;
using System.IO;
using System.Web;
using Sapphire.Core.HostService;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 内容路径帮助类。
    /// </summary>
    public class ContentPathHelper
    {
        /// <summary>
        /// 将内容文件的相对路径转换为物理路径。
        /// </summary>
        /// <param name="contentPath">内容文件的相对路径。</param>
        /// <returns>内容文件的物理路径。</returns>
        public static string ConvertToPhysicalPath(string contentPath)
        {
            string fullpath = HttpContext.Current.Server.MapPath(Path.Combine("/Content", contentPath));
            if (VisualStudioHelper.IsInVisualStudio && !File.Exists(fullpath) && !Directory.Exists(fullpath))
            {
                var module = contentPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[0];
                fullpath = fullpath.Replace("Sapphrie.WebSite", "Sapphrie.Modules." + module);
            }

            return fullpath;
        }
    }
}