using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Sapphire.Core.CommonHelper
{
    /// <summary>
    /// 文件操作助手类。
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// 删除文件。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        public static void DeleteFile(string filePath)
        {
            if (!CheckExistsFile(filePath))
            {
                return;
            }

            File.Delete(filePath);
        }

        /// <summary>
        /// 检查文件是否存在。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        /// <returns>返回true表示存在，返回false表示不存在。</returns>
        public static bool CheckExistsFile(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// 将数据写入文件。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        /// <param name="fileBytes">字节数组。</param>
        /// <returns>保存结果。</returns>
        public static bool SaveFile(string filePath, byte[] fileBytes)
        {
            return SaveFile(filePath, new MemoryStream(fileBytes));
        }

        /// <summary>
        /// 保存文件。
        /// </summary>
        /// <param name="filePath">文件路径。</param>
        /// <param name="stream">Stream对象实例。</param>
        /// <returns>保存结果。</returns>
        public static bool SaveFile(string filePath, Stream stream)
        {
            var bytes = new byte[1024];
            try
            {
               
                var director = filePath.Substring(0, filePath.LastIndexOf(@"\", StringComparison.Ordinal));
                DirectoryHelper.CreateDirectory(director);
                Bitmap roatimg = RotateImage(stream);
                roatimg.Save(filePath);
                //using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                //{

                //    int length = stream.Read(bytes, 0, bytes.Length);
                //    while (length > 0)
                //    {
                //        fileStream.Write(bytes, 0, length);
                //        length = stream.Read(bytes, 0, bytes.Length);
                //    }

                //    fileStream.Flush();
                //}

                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>  
        /// 根据图片exif调整方向  
        /// </summary>  
        /// <param name="sm"></param>  
        /// <returns></returns>  
        public static Bitmap RotateImage(Stream sm)
        {
            Image img = Image.FromStream(sm);
            var exif = img.PropertyItems;
            byte orien = 0;
            var item = exif.Where(m => m.Id == 274).ToArray();
            if (item.Length > 0)
                orien = item[0].Value[0];
            switch (orien)
            {
                case 2:
                    img.RotateFlip(RotateFlipType.RotateNoneFlipX);//horizontal flip  
                    break;
                case 3:
                    img.RotateFlip(RotateFlipType.Rotate180FlipNone);//right-top  
                    break;
                case 4:
                    img.RotateFlip(RotateFlipType.RotateNoneFlipY);//vertical flip  
                    break;
                case 5:
                    img.RotateFlip(RotateFlipType.Rotate90FlipX);
                    break;
                case 6:
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);//right-top  
                    break;
                case 7:
                    img.RotateFlip(RotateFlipType.Rotate270FlipX);
                    break;
                case 8:
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);//left-bottom  
                    break;
                default:
                    break;
            }
            return (Bitmap)img;
        }
        /// <summary>
        /// 检查文件是否重名，并返回新的文件名。
        /// </summary>
        /// <param name="directory">文件保存的目录。</param>
        /// <param name="fileName">文件名。</param>
        /// <returns>返回新的文件名。</returns>
        public static string CheckRepeatName(string directory, string fileName)
        {
            var newFileName = fileName;
            if (CheckExistsFile(Path.Combine(directory, fileName)))
            {
                var str = newFileName.Split('.');
                var name = str[0];
                var extension = str.Length > 1 ? str[1] : string.Empty;
                var i = 0;
                do
                {
                    i++;
                    newFileName = string.Format("{0}({1}).{2}", name, i, extension);
                }
                while (CheckExistsFile(Path.Combine(directory, newFileName)));
            }

            return newFileName;
        }
    }
}