using System.Collections.Generic;

namespace Sapphire.Core.HostService
{
    /// <summary>
    /// 模块化路径配置类。
    /// </summary>
    public class PluginPathConfig
    {
        /// <summary>
        /// 获取模块文件路径的配置。
        /// </summary>
        /// <returns>返回文件路径映射字典。</returns>
        public static Dictionary<string, string> GetConfigs()
        {
            var pathConfigs = new Dictionary<string, string>
            {
                { "(^/Admin/Content/(.*?)/.*)", "Sapphire.Modules.$2$1" },
                { "(^/Admin/Views/(.*?)/.*)", "Sapphire.Modules.$2$1" },
                { "(^/Content/.*?/(.*?)/.*)", "Sapphire.Modules.$2$1" },
                { "(^/Views/.*?/(.*?)/.*)", "Sapphire.Modules.$2$1" },
                { "(^/Views.Pad/.*?/(.*?)/.*)", "Sapphire.Modules.$2$1" },
                { "(^/Views.Phone/.*?/(.*?)/.*)", "Sapphire.Modules.$2$1" }
            };
            return pathConfigs;
        }
    }
}