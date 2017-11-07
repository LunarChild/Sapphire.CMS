using System;
using System.IO;
using System.Linq;
using System.Web;
using Sapphire.Core.HostService;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 模板路径帮助类。
    /// </summary>
    public class ViewPathHelper
    {
        /// <summary>
        /// 将模板的相对路径转换为物理路径。
        /// </summary>
        /// <param name="viewPath">模板的相对路径。</param>
        /// <returns>模板的物理路径。</returns>
        public static string ConvertToPhysicalPath(string viewPath)
        {
            var decodeViewPath = HttpUtility.UrlDecode(viewPath);
            string fullpath = HttpContext.Current.Server.MapPath(Path.Combine("/Views", decodeViewPath ?? string.Empty));
            if (VisualStudioHelper.IsInVisualStudio && !File.Exists(fullpath) && !Directory.Exists(fullpath))
            {
                var module = decodeViewPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[0];
                //判断是否存在该模块
                var modulePaths = Directory.GetDirectories(Path.GetFullPath(HttpContext.Current.Server.MapPath("~") + @".."), "Sapphire.*").Select(c => c.Substring(c.LastIndexOf('.') + 1));
                if (modulePaths.Contains(module))
                {
                    fullpath = fullpath.Replace("Sapphire.WebSite", "Sapphire.Modules." + module);
                }
            }

            return fullpath;
        }
    }
}