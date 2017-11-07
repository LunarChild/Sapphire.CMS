//using System.ComponentModel.DataAnnotations.Schema;
//using System.IO;
//using Sapphire.Core.Data;

//namespace Sapphire.Core.Upload
//{
//    /// <summary>
//    /// 文件上传信息。
//    /// </summary>
//    public class FileUploadInfo : BaseJson
//    {
//        private string fileTitle;

//        /// <summary>
//        /// 文件路径。
//        /// </summary>
//        [NotMapped]
//        public string FileUrl { get; set; }

//        /// <summary>
//        /// 文件标题。
//        /// </summary>
//        [NotMapped]
//        public string FileTitle
//        {
//            get
//            {
//                return string.IsNullOrEmpty(this.fileTitle) ? Path.GetFileName(this.FileUrl) : this.fileTitle;
//            }

//            set
//            {
//                this.fileTitle = value;
//            }
//        }

//        /// <summary>
//        /// 文件简介。
//        /// </summary>
//        [NotMapped]
//        public string FileIntro { get; set; }

//        /// <summary>
//        /// 文件大小。
//        /// </summary>
//        [NotMapped]
//        public string FileSize { get; set; }
//    }
//}