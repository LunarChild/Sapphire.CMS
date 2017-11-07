namespace Sapphire.Core.Upload
{
    /// <summary>
    /// 多文件上传配置接口。
    /// </summary>
    public interface IMultipleFileUploadConfig : IUploadConfig
    {
        /// <summary>
        /// 上传文件总数量限制。
        /// </summary>
        int FileNumLimit { get; set; }

        /// <summary>
        /// 上传文件总大小限制（单位：B）。
        /// </summary>
        long FileSizeLimit { get; set; }
    }
}