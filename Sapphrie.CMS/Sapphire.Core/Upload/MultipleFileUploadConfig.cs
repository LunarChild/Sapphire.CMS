namespace Sapphire.Core.Upload
{
    /// <summary>
    /// 多文件上传配置。
    /// </summary>
    public class MultipleFileUploadConfig : UploadConfig, IMultipleFileUploadConfig
    {
        /// <summary>
        /// 上传文件总数量限制。
        /// </summary>
        public int FileNumLimit { get; set; }

        /// <summary>
        /// 上传文件总大小限制（单位：B）。
        /// </summary>
        public long FileSizeLimit { get; set; }
    }
}