using System;
using System.Collections.Generic;

namespace Sapphire.Core.Upload
{
    /// <summary>
    /// Mime集合。
    /// </summary>
    public static class MimeDictionary
    {
        private const string Comma = ",";
        private const string Point = ".";
        private static readonly Dictionary<string, string> Mimes = new Dictionary<string, string>();

        /// <summary>
        /// 静态初始化 MimeDictionary 。
        /// </summary>
        static MimeDictionary()
        {
            Register("jpe", "image/jpeg");
            Register("jpeg", "image/jpeg");
            Register("jpg", "image/jpeg");
            Register("gif", "image/gif");
            Register("png", "image/png");
            Register("bmp", "image/bmp");

            Register("flv", "video/x-flv");
            Register("mp4", "audio/mpeg");
            Register("mov", "video/quicktime");

            Register("txt", "text/plain");
        }

        /// <summary>
        /// 向Mime集合中添加Mime。
        /// </summary>
        /// <param name="fileType">文件类型。</param>
        /// <param name="mimeType">Mime类型。</param>
        public static void Register(string fileType, string mimeType)
        {
            Mimes.Add(RemovePoint(fileType), mimeType);
        }

        /// <summary>
        /// 根据文件类型获取Mime。
        /// </summary>
        /// <param name="fileType">文件类型。</param>
        /// <returns>Mime类型。</returns>
        public static string GetMime(string fileType)
        {
            fileType = RemovePoint(fileType).ToLower();

            if (string.IsNullOrEmpty(fileType) || !Mimes.ContainsKey(fileType))
            {
                return string.Empty;
            }

            return Mimes[fileType];
        }

        /// <summary>
        /// 根据多个文件类型获取Mime，以英文逗号(,)隔开。
        /// </summary>
        /// <param name="fileType">多个文件类型，以英文逗号(,)隔开。</param>
        /// <returns>Mime类型。</returns>
        public static string GetMimes(string fileType)
        {
            if (string.IsNullOrEmpty(fileType))
            {
                return string.Empty;
            }

            var fileTypes = fileType.Split(new[] { Comma }, StringSplitOptions.RemoveEmptyEntries);
            if (fileTypes.Length <= 0)
            {
                return string.Empty;
            }

            var mimes = string.Empty;
            foreach (var type in fileTypes)
            {
                var mime = GetMime(type);
                if (string.IsNullOrEmpty(mime))
                {
                    continue;
                }

                mimes = string.Concat(mimes, Comma, GetMime(type));
            }

            return mimes.Length < 2 ? mimes : mimes.Substring(1, mimes.Length - 1);
        }

        /// <summary>
        /// 删除扩展名中的点号(.)。
        /// </summary>
        /// <param name="fileType">带点号的扩展名。</param>
        /// <returns>扩展名。</returns>
        private static string RemovePoint(string fileType)
        {
            return fileType.Replace(Point, string.Empty);
        }
    }
}