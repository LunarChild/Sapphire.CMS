using System.ComponentModel.DataAnnotations;

namespace Sapphire.Core.Logging
{
    /// <summary>
    /// 日志级别。
    /// </summary>
    public enum LogLevelType
    {
        /// <summary>
        /// 信息。
        /// </summary>
        [Display(Name = "信息")]
        Info,

        /// <summary>
        /// 警告。
        /// </summary>
        [Display(Name = "警告")]
        Warn,

        /// <summary>
        /// 错误。
        /// </summary>
        [Display(Name = "错误")]
        Error
    }
}