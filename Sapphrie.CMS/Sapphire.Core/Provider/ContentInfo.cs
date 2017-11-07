using System;

namespace Sapphire.Core.Provider
{
    /// <summary>
    /// 内容信息。
    /// </summary>
    public class ContentInfo
    {
        /// <summary>
        /// 标题。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 发布时间。
        /// </summary>
        public DateTime? PublishTime { get; set; }
    }
}