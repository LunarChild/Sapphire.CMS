namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 站点路由解析器。
    /// </summary>
    public static class SiteRouteAnalyzers
    {
        /// <summary>
        /// 当前的站点路由解析器。
        /// </summary>
        public static ISiteRouteAnalyzer Current { get; set; }
    }
}