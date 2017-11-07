namespace Sapphire.Core.Logging
{
    /// <summary>
    /// 日志提供者接口。
    /// </summary>
    public interface ILogProvider
    {
        /// <summary>
        /// 添加日志。
        /// </summary>
        /// <param name="log">日志实体。</param>
        void AddLog(ILog log);

        /// <summary>
        /// 实例化继承ILog接口类的新实例。
        /// </summary>
        /// <returns>ILog。</returns>
        ILog CreateLog();
    }
}