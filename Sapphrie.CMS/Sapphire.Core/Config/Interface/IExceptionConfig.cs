namespace Sapphire.Core.Config
{
    /// <summary>
    /// 异常配置信息接口。
    /// </summary>
    public interface IExceptionConfig
    {
        /// <summary>
        /// 错误消息类型页面。0为友好错误页，1为详细错误页。
        /// </summary>
        int ErrorMessageType { get; set; }

        /// <summary>
        /// 日志记录文件是否开启。
        /// </summary>
        bool LogEnable { get; set; }

        /// <summary>
        /// 同一个错误消息记录间隔时间。
        /// </summary>
        int RecordTimeSpan { get; set; }

        /// <summary>
        /// 自定义404页面启用。
        /// </summary>
        bool NotFoundEnabled { get; set; }

        /// <summary>
        /// 自定义404模板。
        /// </summary>
        string NotFoundView { get; set; }
    }
}