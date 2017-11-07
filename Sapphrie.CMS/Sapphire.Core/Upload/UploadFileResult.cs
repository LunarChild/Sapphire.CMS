namespace Sapphire.Core.Upload
{
    /// <summary>
    /// 上传文件结果。
    /// </summary>
    public class UploadFileResult
    {
        /// <summary>
        /// 文件表PE_Model_File的主键ID。
        /// </summary>
        public int FileId { get; set; }
        /// <summary>
        /// 图片的路径
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 文件原始名称名称。
        /// </summary>
        public string UploadFileName { get; set; }
        /// <summary>
        /// 文件扩展名。
        /// </summary>
        public string FileExtension { get; set; }
        /// <summary>
        /// 文件名称。
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 相对路径 + 文件名称。
        /// </summary>
        public string RelativePathFileName { get; set; }
        /// <summary>
        /// 图片的路径
        /// </summary>
        public string ThumbnailUrl { get; set; }
        /// <summary>
        /// 缩略图文件名称。
        /// </summary>
        public string ThumbnailFileName { get; set; }

        /// <summary>
        /// 缩略图 相对路径 + 文件名称。
        /// </summary>
        public string ThumbnailRelativePathFileName { get; set; }

        /// <summary>
        /// 是否存在错误。
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// 错误信息。
        /// </summary>
        public string ErrorMsg { get; set; }
    }
}