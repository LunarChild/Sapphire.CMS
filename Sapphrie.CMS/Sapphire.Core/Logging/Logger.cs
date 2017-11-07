using Newtonsoft.Json;

namespace Sapphire.Core.Logging
{
    /// <summary>
    /// 日志记录器。
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// 实例化继承ILog接口类的新实例。
        /// </summary>
        /// <returns>ILog。</returns>
        public static ILog CreateLog()
        {
            return GlobalLogProvider.Current.CreateLog();
        }

        /// <summary>
        /// 添加日志。
        /// </summary>
        /// <param name="log">日志实体。</param>
        public static void AddLog(ILog log)
        {
            GlobalLogProvider.Current.AddLog(log);
        }

        /// <summary>
        /// 添加日记。
        /// </summary>
        /// <param name="logType">日记模型。</param>
        /// <param name="title">日记标题。</param>
        /// <param name="details">详细详细。</param>
        /// <param name="userName">用户名。</param>
        public static void AddLog(LogType logType, string title = null, string details = null, string userName = null)
        {
            ILog log = CreateLog();
            log.LogType = logType;
            log.Title = title;
            log.Details = JsonConvert.SerializeObject(details);
            if (!string.IsNullOrWhiteSpace(userName))
            {
                log.UserName = userName;
            }

            AddLog(log);
        }
    }
}