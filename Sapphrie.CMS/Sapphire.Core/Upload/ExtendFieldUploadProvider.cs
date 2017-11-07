using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Text;

namespace Sapphire.Core.Upload
{
    /// <summary>
    /// 扩展字段上传提供者
    /// </summary>
    public abstract class ExtendFieldUploadProvider : IUploadProvider, IExtendFieldUploadConfig
    {
        private const string TypeNameSuffix = "UploadProvider";
        private const string Slash = "/";
        private const string Point = ".";
        private const string Year = "{Year}";
        private const string Month = "{Month}";
        private const string Day = "{Day}";
        private const string Hour = "{Hour}";
        private const string Minute = "{Minute}";
        private const string Second = "{Second}";
        private const string FormatYear = "yyyy";
        private const string FormatMonth = "MM";
        private const string FormatDay = "dd";
        private const string FormatHour = "HH";
        private const string FormatMinute = "mm";
        private const string FormatSecond = "ss";
        private const string Mime = "{Mime}";
        private const string FileType = "{FileType}";
        private const string Origin = "{Origin}";
        private const string Guid = "{Guid}";
        private const string Random = "{Random}";
        private const string DateTimeFormat = "yyyyMMddHHmmss";
        private const string RandomString = "0123456789";
        private const string SiteId = "{SiteId}";
        private const string SiteIdentifier = "{SiteIdentifier}";
        private static readonly Random Rand = new Random(unchecked((int)DateTime.Now.Ticks));

        /// <summary>
        /// 获取扩展字段上传配置
        /// </summary>
        /// <param name="fieldId">扩展字段Id。</param>
        /// <returns>扩展字段上传配置。</returns>
        public abstract IUploadConfig GetUploadConfig(int fieldId);

        /// <summary>
        /// 获取上传提供者的键。
        /// </summary>
        /// <returns>上传提供者的键。</returns>
        public string GetUploadProviderKey()
        {
            return this.GetType().Name.Replace(TypeNameSuffix, string.Empty);
        }

        /// <summary>
        /// 解析上传路径规则。
        /// </summary>
        /// <param name="uploadPathRule">上传路径规则。</param>
        /// <param name="file">PowerHttpFile对象实例。</param>
        /// <param name="uploadRuleKeys">替换规则占位符时需要使用到的参数集合。</param>
        /// <returns>解析后的上传路径（已追加上传文件扩展名）。</returns>
        public string ResolveUploadPath(string uploadPathRule, PowerHttpFile file, NameValueCollection uploadRuleKeys)
        {
            if (string.IsNullOrEmpty(uploadPathRule))
            {
                return file.FileName;
            }

            var uploadPath = string.Concat(uploadPathRule, Path.GetExtension(file.FileName));

            uploadPath = uploadPath.Replace(Year, DateTime.Now.ToString(FormatYear, DateTimeFormatInfo.InvariantInfo));
            uploadPath = uploadPath.Replace(Month, DateTime.Now.ToString(FormatMonth, DateTimeFormatInfo.InvariantInfo));
            uploadPath = uploadPath.Replace(Day, DateTime.Now.ToString(FormatDay, DateTimeFormatInfo.InvariantInfo));
            uploadPath = uploadPath.Replace(Hour, DateTime.Now.ToString(FormatHour, DateTimeFormatInfo.InvariantInfo));
            uploadPath = uploadPath.Replace(Minute, DateTime.Now.ToString(FormatMinute, DateTimeFormatInfo.InvariantInfo));
            uploadPath = uploadPath.Replace(Second, DateTime.Now.ToString(FormatSecond, DateTimeFormatInfo.InvariantInfo));
            var mime = MimeDictionary.GetMime(Path.GetExtension(file.FileName));
            if (mime.Length > 0)
            {
                mime = mime.Substring(0, mime.IndexOf(Slash, StringComparison.Ordinal));
            }

            uploadPath = uploadPath.Replace(Mime, mime);
            var extension = Path.GetExtension(file.FileName);
            if (extension != null)
            {
                uploadPath = uploadPath.Replace(FileType, extension.Replace(Point, string.Empty));
            }

            uploadPath = uploadPath.Replace(Origin, Path.GetFileNameWithoutExtension(file.FileName));
            uploadPath = uploadPath.Replace(Random, this.GetFileRndName());
            uploadPath = uploadPath.Replace(Guid, System.Guid.NewGuid().ToString("N"));
            uploadPath = uploadPath.Replace(SiteId, uploadRuleKeys["SiteId"]);
            uploadPath = uploadPath.Replace(SiteIdentifier, uploadRuleKeys["SiteIdentifier"]);

            uploadPath = this.ResolveUploadPathCore(uploadPath, uploadRuleKeys);

            return uploadPath;
        }

        /// <summary>
        /// 解析上传路径规则，替换非公共的占位符。
        /// </summary>
        /// <param name="uploadPathRule">上传目录规则（含文件名，不含后缀）。</param>
        /// <param name="uploadRuleKeys">上传目录规则、文件名规则。键是规则名，值是替换规则使用的字符。</param>
        /// <returns>上传目录的相对路径（包含文件名和后缀）。</returns>
        /// <remarks>子类可重写此方法，解析上传目录和文件名中非公共的占位符。</remarks>
        protected virtual string ResolveUploadPathCore(string uploadPathRule, NameValueCollection uploadRuleKeys)
        {
            return uploadPathRule;
        }

        /// <summary>
        /// 获取按照年月时分秒随机数生成的文件名。
        /// </summary>
        /// <returns>随机文件名。</returns>
        /// <remarks>文件名称后增加的4位随机字符串。</remarks>
        protected string GetFileRndName()
        {
            return this.GetFileRndName(4);
        }

        /// <summary>
        /// 获取按照年月时分秒随机数生成的文件名。
        /// </summary>
        /// <param name="length">文件名称后增加的随机字符串的长度。</param>
        /// <returns>随机文件名。</returns>
        protected string GetFileRndName(int length)
        {
            return DateTime.Now.ToString(DateTimeFormat, CultureInfo.CurrentCulture) + this.GetRandomString(RandomString, length);
        }

        /// <summary>
        /// 获取指定长度和字符的随机字符串。
        /// 通过调用 Random 类的 Next() 方法，先获得一个大于或等于 0 而小于 chars 长度的整数。
        /// 以该数作为索引值，从可用字符串中随机取字符，以指定的密码长度为循环次数。
        /// 依次连接取得的字符，最后即得到所需的随机密码串了。
        /// </summary>
        /// <param name="chars">随机字符串里包含的字符。</param>
        /// <param name="length">随机字符串的长度。</param>
        /// <returns>随机产生的字符串。</returns>
        private string GetRandomString(string chars, int length)
        {
            var randomString = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                var randNum = Rand.Next(chars.Length);
                randomString.Append(chars[randNum]);
            }

            return randomString.ToString();
        }
    }
}