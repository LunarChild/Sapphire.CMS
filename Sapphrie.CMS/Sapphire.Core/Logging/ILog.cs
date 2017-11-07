using System;

namespace Sapphire.Core.Logging
{
    /// <summary>
    /// 日志实体接口。
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// 日志ID。
        /// </summary>
        int LogId { get; set; }

        /// <summary>
        /// 标题。
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// 日志类型。
        /// </summary>
        LogType LogType { get; set; }

        /// <summary>
        /// 记录时间。
        /// </summary>
        DateTime LogTime { get; set; }

        /// <summary>
        /// 来源。
        /// </summary>
        string Source { get; set; }

        /// <summary>
        /// 用户名。
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// IP地址。
        /// </summary>
        string IpAddress { get; set; }

        /// <summary>
        /// 经过序列化的详细信息。
        /// </summary>
        string Details { get; set; }
    }
}