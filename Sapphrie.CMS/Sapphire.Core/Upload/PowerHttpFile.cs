using System.IO;
using System.Web;
using Sapphire.Core.CommonHelper;

namespace Sapphire.Core.Upload
{
    /// <summary>
    /// PowerHttpFile。
    /// </summary>
    public sealed class PowerHttpFile
    {
        /// <summary>
        /// 文件的大小（以字节为单位）。
        /// </summary>
        public int ContentLength { get; set; }

        /// <summary>
        /// 文件的 MIME 内容类型。
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// 文件的完全限定名称。
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 指向文件的Stram对象实例。
        /// </summary>
        public Stream Stream { get; set; }

        /// <summary>
        /// HttpPostedFile到PowerHttpFile的隐式转换。
        /// </summary>
        /// <param name="file">HttpPostedFile对象实例。</param>
        /// <returns>PowerHttpFile对象实例。</returns>
        public static implicit operator PowerHttpFile(HttpPostedFile file)
        {
            return new PowerHttpFile
            {
                FileName = file.FileName,
                ContentLength = file.ContentLength,
                ContentType = file.ContentType,
                Stream = file.InputStream
            };
        }

        /// <summary>
        /// 保存文件。
        /// </summary>
        /// <param name="path">文件路径。</param>
        /// <returns>保存结果。</returns>
        public bool SaveAs(string path)
        {
            return FileHelper.SaveFile(path, this.Stream);
        }
    }
}