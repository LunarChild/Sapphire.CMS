using System.IO;

namespace Sapphire.Core.CommonHelper
{
    /// <summary>
    /// 目录操作助手类。
    /// </summary>
    public static class DirectoryHelper
    {
        /// <summary>
        /// 创建目录。
        /// </summary>
        /// <param name="directory">目录。</param>
        /// <returns>是否创建成功。</returns>
        public static bool CreateDirectory(string directory)
        {
            try
            {
                if (!CheckExistsDirectory(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 检查目录是否存在。
        /// </summary>
        /// <param name="directory">目录。</param>
        /// <returns>返回true表示存在，返回false表示不存在。</returns>
        public static bool CheckExistsDirectory(string directory)
        {
            return Directory.Exists(directory);
        }

        /// <summary>
        /// 将目录下的所有内容包括子目录移到新位置。
        /// </summary>
        /// <param name="sourceDirectoryName">要移动的目录完全路径。</param>
        /// <param name="destinationDirectory">新目录的完全路径。</param>
        /// <returns>是否移动成功。</returns>
        public static bool Move(string sourceDirectoryName, string destinationDirectory)
        {
            var sourceDir = new DirectoryInfo(sourceDirectoryName);
            try
            {
                Move(sourceDir, destinationDirectory);
                Directory.Delete(sourceDirectoryName, true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void Move(DirectoryInfo directoryInfo, string destinationDirectory)
        {
            if (!CreateDirectory(destinationDirectory))
            {
                return;
            }

            foreach (var file in directoryInfo.EnumerateFiles())
            {
                file.CopyTo(Path.Combine(destinationDirectory, file.Name), true);
            }

            foreach (var dirInfo in directoryInfo.EnumerateDirectories())
            {
                string destPath = Path.Combine(destinationDirectory, dirInfo.Name);
                Move(dirInfo, destPath);
            }
        }
    }
}