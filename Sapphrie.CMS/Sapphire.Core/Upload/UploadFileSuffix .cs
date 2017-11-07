using System.Collections.Generic;

namespace Sapphire.Core.Upload
{
    /// <summary>
    /// 常用文件后缀容器静态类。
    /// </summary>
    public static class UploadFileSuffix
    {
        /// <summary>
        /// 图片常用后缀。
        /// </summary>
        private static IEnumerable<string> imageSuffixList = new[] { "bmp", "gif", "jpg", "jpeg", "png" };

        /// <summary>
        /// 视频常用后缀。
        /// </summary>
        private static IEnumerable<string> videoSuffixList = new[] { "wmv", "rm", "rmvb", "3gp", "mp4", "avi", "mkv", "flv" };

        /// <summary>
        /// 文档常用后缀。
        /// </summary>
        private static IEnumerable<string> docSuffixList = new[] { "txt", "doc", "wps", "pdf", "xls", "xlsx", "docx", "cshtml", "ppt", "pptx" };

        /// <summary>
        /// 压缩文件常用后缀。
        /// </summary>
        private static IEnumerable<string> zipSuffixList = new[] { "rar", "zip", "gz", "z", "7z" };

        /// <summary>
        /// 音频常用后缀。
        /// </summary>
        private static IEnumerable<string> audioSuffixList = new[] { "mp3", "wma" };
    }
}