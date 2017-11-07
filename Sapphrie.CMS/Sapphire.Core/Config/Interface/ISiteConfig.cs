namespace Sapphire.Core.Config
{
    /// <summary>
    /// 网站信息配置接口。
    /// </summary>
    public interface ISiteConfig
    {
        /// <summary>
        /// 主域名。
        /// </summary>
        string Domain { get; set; }

        /// <summary>
        /// 主域名的端口号。
        /// </summary>
        int Port { get; set; }
    }
}