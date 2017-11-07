using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapphire.Core.Upload
{
    /// <summary>
    /// m
    /// </summary>
    public static class UploadPathRule
    {
        /// <summary>
        /// 单路径规则。
        /// </summary>
        public const string SinglePathRule = "{Year}：当前年份<br/>{Month}：当前月份<br/>{Day}：当前天<br/>{Hour}：当前小时<br/>{Minute}：当前分钟<br/>{Second}：当前秒数<br/>{Mime}：文件的MIME类型<br/>{FileType}：文件类型 <br/>{Origin}：文件原名<br/>{Random}：随机数<br/>{Guid}：全局唯一标识符<br/>{SiteId}：站点Id<br/>{SiteIdentifier}：站点标识符";

        /// <summary>
        /// 文件名路径规则。
        /// </summary>
        public const string FileNamePathRule = "";
    }
}
