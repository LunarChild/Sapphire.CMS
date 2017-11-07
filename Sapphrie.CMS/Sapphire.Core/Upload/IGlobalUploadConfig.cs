namespace Sapphire.Core.Upload
{
    /// <summary>
    /// 全局上传配置接口。
    /// </summary>
    public interface IGlobalUploadConfig : IUploadConfig
    {
        /// <summary>
        /// 上传目录。
        /// </summary>
        string UploadDirectory { get; set; }

        /// <summary>
        /// 上传目录前缀。
        /// </summary>
        string UploadPathPerfix { get; set; }
    }
}