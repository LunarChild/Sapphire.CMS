namespace Sapphire.Core.Upload
{
    /// <summary>
    /// 上传配置接口。
    /// </summary>
    public interface IUploadConfig
    {
        /// <summary>
        /// 是否允许上传。
        /// </summary>
        bool EnableUpload { get; set; }

        /// <summary>
        /// 是否允许自动上传。
        /// </summary>
        bool EnableAutoUpload { get; set; }

        /// <summary>
        /// 上传单个文件大小限制（单位：B）。
        /// </summary>
        long FileSingleSizeLimit { get; set; }

        /// <summary>
        /// 允许上传的文件类型，不带点，多种文件类型之间以英文逗号（,）分隔。
        /// </summary>
        string Extensions { get; set; }

        /// <summary>
        /// 上传路径规则。
        /// </summary>
        string UploadPathRule { get; set; }

        /// <summary>
        /// 上传路径规则的关键字。
        /// </summary>
        string UploadRuleKey { get; set; }

        /// <summary>
        /// 更改文件上传路径。
        /// </summary>
        string UploadUrl { get; set; }

        /// <summary>
        /// 上传提供者的键。
        /// </summary>
        string UploadProviderKey { get; set; }

        /// <summary>
        /// 是否添加水印。
        /// </summary>
        bool EnableWatermark { get; set; }
    }
}