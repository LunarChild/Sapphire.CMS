using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Sapphire.Core.CommonHelper
{
    /// <summary>
    /// 图片生成助手。
    /// </summary>
    public class ImageHelper
    {
        private ImageHelper(string path, int width, int height)
        {
            this.Server = HttpContext.Current.Server;
            this.OriginalPath = path.Replace('\\', '/');
            this.Url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            this.ThumbnailWidth = width;
            this.ThumbnailHeight = height;
            this.GetThumbnailPath();
        }
        private ImageHelper(string path, int width)
        {
            this.Server = HttpContext.Current.Server;
            this.OriginalPath = path.Replace('\\', '/');
            this.Url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            this.ThumbnailWidth = width;
            this.GetThumbnailPath();
        }
        private string Path { get; set; }

        private string OriginalPath { get; set; }

        private string ThumbnailName { get; set; }
        /// <summary>
        /// 文件路径，要物理路径。
        /// </summary>
        private string ThumbnailPath { get; set; }

        private int ThumbnailWidth { get; set; }

        private int ThumbnailHeight { get; set; }

        private HttpServerUtility Server { get; set; }

        private UrlHelper Url { get; set; }

        /// <summary>
        /// 获取缩略图。
        /// </summary>
        /// <param name="path">原始图片路径。</param>
        /// <param name="width">缩略图宽度。</param>
        /// <param name="height">缩略图高度。</param>
        /// <returns>缩略图路径。</returns>
        public static string GetThumbnail(string path, int width, int height)
        {
            //Bitmap
            return new ImageHelper(path, width, height).GetThumbnail();
        }        
        private string GetThumbnail()
        {
            var original = Image.FromFile(this.Server.MapPath(this.OriginalPath));
            //不是按照指定的宽高直接缩放，而是根据图片的宽高计算是按照宽还是按照高进行缩放生成缩略图
            if (original.Width <= original.Height)//自适应高度
            {
                this.ThumbnailHeight = (original.Height * this.ThumbnailWidth) / original.Width;
            }
            else
            {
                this.ThumbnailWidth = (original.Width * this.ThumbnailHeight) / original.Height;
            }
            this.GetThumbnailPath();
            //
            var localThumbnailPath = this.Server.MapPath(this.ThumbnailPath);
            if (!File.Exists(localThumbnailPath))
            {
                var thumbnail = original.GetThumbnailImage(this.ThumbnailWidth, this.ThumbnailHeight, this.ThumbnailCallback, IntPtr.Zero);
                thumbnail.Save(localThumbnailPath, original.RawFormat);
            }
            return this.Url.Content(this.ThumbnailPath);
        }
        private void GetThumbnailPath()
        {
            this.GetPath();
            this.GetThumbnailName();
            this.ThumbnailPath = System.IO.Path.Combine(this.Path, this.ThumbnailName).Replace('\\', '/');
        }

        private void GetPath()
        {
            this.Path = this.OriginalPath.Substring(0, this.OriginalPath.LastIndexOf('/'));
        }

        private void GetThumbnailName()
        {
            this.ThumbnailName = string.Format(
                "{0}_{1}_{2}{3}",
                System.IO.Path.GetFileNameWithoutExtension(this.OriginalPath),
                this.ThumbnailWidth,
                this.ThumbnailHeight,
                System.IO.Path.GetExtension(this.OriginalPath));
        }

        private bool ThumbnailCallback()
        {
            return false;
        }
    }
}