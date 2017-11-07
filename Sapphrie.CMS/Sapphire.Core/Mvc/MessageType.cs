using System;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 消息类型。
    /// </summary>
    [Flags]
    public enum MessageType
    {
        /// <summary>
        /// 成功。
        /// </summary>
        Success,

        /// <summary>
        /// 错误。
        /// </summary>
        Error,

        /// <summary>
        /// 警告。
        /// </summary>
        Warning,

        /// <summary>
        /// 加载。
        /// </summary>
        Loading,
    }
}