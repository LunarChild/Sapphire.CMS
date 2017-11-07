using System.ComponentModel.DataAnnotations;

namespace Sapphire.Core.Logging
{
    /// <summary>
    /// 日志类型。
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// 操作。
        /// </summary>
        [Display(Name = "操作")]
        Action,

        /// <summary>
        /// 安全。
        /// </summary>
        [Display(Name = "安全")]
        Security
    }
}