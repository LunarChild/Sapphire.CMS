using System;
using System.IO;
using System.Linq;
using System.Web;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 静态文件管理类。
    /// </summary>
    public class StaticPathHelper
    {
        /// <summary>
        /// 将模板的相对路径转换为物理路径。
        /// </summary>
        /// <param name="relativePath">模板的相对路径。</param>
        /// <returns>模板的物理路径。</returns>
        public static string GetPhysicalPath(string relativePath)
        {
            try
            {
                var decodePath = HttpUtility.UrlDecode(relativePath);
                if (decodePath != null)
                {
                    string[] segments = decodePath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    if (segments.Length > 1)
                    {
                        string fullpath = HttpContext.Current.Server.MapPath(decodePath);
                        if (segments.Length > 3 && segments[0] == "~")
                        {
                            // ~/Views/default/contentManage/...情况。
                            fullpath = BulidIncludingModuleFullPath(fullpath, segments[3]);
                        }

                        if (segments[0] != "~")
                        {
                            // /Views/default/contentManage/...情况。
                            fullpath = BulidIncludingModuleFullPath(fullpath, segments[2]);
                        }

                        return fullpath;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }

        private static string BulidIncludingModuleFullPath(string fullpath, string moduleName)
        {
            //判断是否存在该模块
            var modulePaths = Directory.GetDirectories(Path.GetFullPath(HttpContext.Current.Server.MapPath("~") + @".."), "Sapphire.*").Select(c => c.Substring(c.LastIndexOf('.') + 1));
            if (modulePaths.Contains(moduleName))
            {
                fullpath = fullpath.Replace("Sapphire.WebSite", "Sapphire.Modules." + moduleName);
            }

            return fullpath;
        }
    }
}