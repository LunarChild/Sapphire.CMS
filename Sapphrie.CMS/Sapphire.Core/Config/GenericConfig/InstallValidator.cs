using System.Configuration;

namespace Sapphire.Core.Config
{
    /// <summary>
    /// 安装程序配置信息。
    /// </summary>
    public static class InstallValidator
    {
        /// <summary>
        /// 数据库是否存在。
        /// </summary>
        /// <returns>数据库是否已经安装。</returns>
        public static bool DatabaseIsInstalled()
        {
            var databaseIsInstalled = ConfigurationManager.AppSettings["databaseIsInstalled"];
            if (databaseIsInstalled == "false")
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 管理配置信息是否设置。
        /// </summary>
        /// <returns>管理配置信息是否已经设置。</returns>
        public static bool InstallComplete()
        {
            var databaseIsInstalled = ConfigurationManager.AppSettings["InstallComplete"];
            if (databaseIsInstalled == "false")
            {
                return false;
            }

            return true;
        }
    }
}