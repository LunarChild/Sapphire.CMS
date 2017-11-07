using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Sapphire.Core.HostService;

namespace Sapphire.Core.CommonHelper
{
    /// <summary>
    /// 处理模块路径的助手类。
    /// </summary>
    public static class ModulePathHelper
    {
        /// <summary>
        /// 获取文件在子模块或者主项目中的物理路径。优先查找子模块，不存在文件则查找主项目。
        /// </summary>
        /// <param name="virtualPath">虚拟路径。</param>
        /// <returns>文件的实际物理路径。</returns>
        public static string GetFilePath(string virtualPath)
        {
            if (!VisualStudioHelper.IsInVisualStudio)
            {
                return HostingEnvironment.MapPath(virtualPath);
            }

            var modulePaths = Directory.GetDirectories(Path.GetFullPath(HostingEnvironment.MapPath("~") + @".."));
            var fixedUrl = virtualPath;
            if (fixedUrl.StartsWith("~"))
            {
                fixedUrl = VirtualPathUtility.ToAbsolute(fixedUrl);
            }

            foreach (var modulePhysicsPath in
                modulePaths.Select(modulePath => string.Format("{0}{1}", modulePath, fixedUrl.Replace('/', '\\'))).Where(File.Exists))
            {
                return modulePhysicsPath;
            }

            return HostingEnvironment.MapPath(virtualPath);
        }
    }
}