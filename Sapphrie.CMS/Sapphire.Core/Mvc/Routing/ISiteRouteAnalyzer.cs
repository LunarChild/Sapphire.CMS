using Sapphire.Core.Web;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 站点路由解析器。
    /// </summary>
    public interface ISiteRouteAnalyzer
    {
        /// <summary>
        /// 根据当前请求地址获取站点。
        /// </summary>
        /// <param name="host">当前请求的Host信息。</param>
        /// <returns>站点。</returns>
        ISite GetSite(string host);
    }
}