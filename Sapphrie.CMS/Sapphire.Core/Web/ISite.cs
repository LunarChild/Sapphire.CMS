namespace Sapphire.Core.Web
{
    /// <summary>
    /// 路由站点参数的模型。
    /// </summary>
    public interface ISite
    {
        /// <summary>
        /// 站点编号。
        /// </summary>
        int SiteId { get; set; }

        /// <summary>
        /// 站点名称。
        /// </summary>
        string SiteName { get; set; }

        /// <summary>
        /// 站点标识符。
        /// </summary>
        string Identifier { get; set; }

        /// <summary>
        /// 子域名。
        /// </summary>
        string Subdomain { get; set; }
    }
}