using System.Diagnostics;

namespace Sapphire.Core.HostService
{
    /// <summary>
    /// Visual Studio 助手类。
    /// </summary>
    public static class VisualStudioHelper
    {
        /// <summary>
        /// 当前进程是否为 Visual Studio 或者其他调试器。
        /// </summary>
        public static bool IsInVisualStudio
        {
            get
            {
                if (Debugger.IsAttached)
                {
                    return true;
                }

                using (var process = Process.GetCurrentProcess())
                {
                    return process.ProcessName.ToUpperInvariant().Contains("DEVENV") || process.ProcessName.ToUpperInvariant().Contains("IISEXPRESS");
                }
            }
        }
    }
}