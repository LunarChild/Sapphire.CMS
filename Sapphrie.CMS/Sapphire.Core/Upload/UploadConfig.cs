namespace Sapphire.Core.Upload
{
    /// <summary>
    /// 上传配置。
    /// </summary>
    public class UploadConfig : IUploadConfig
    {
        /// <summary>
        /// 是否允许上传。
        /// </summary>
        public bool EnableUpload { get; set; }

        /// <summary>
        /// 是否允许自动上传。
        /// </summary>
        public bool EnableAutoUpload { get; set; }

        /// <summary>
        /// 上传单个文件大小限制（单位：B）。
        /// </summary>
        public long FileSingleSizeLimit { get; set; }

        /// <summary>
        /// 允许上传的文件类型，不带点，多种文件类型之间以英文逗号（,）分隔。
        /// </summary>
        public string Extensions { get; set; }

        /// <summary>
        /// 上传路径规则（含文件名规则）。
        /// </summary>
        public string UploadPathRule { get; set; }

        /// <summary>
        /// 更改文件上传路径。
        /// </summary>
        public string UploadUrl { get; set; }

        /// <summary>
        /// 上传提供者的键。
        /// </summary>
        public string UploadProviderKey { get; set; }

        /// <summary>
        /// 上传路径规则的键
        /// </summary>
        public string UploadRuleKey { get; set; }

        /// <summary>
        /// 是否添加水印。
        /// </summary>
        public bool EnableWatermark { get; set; }
    }
}