namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 提供用于定义区域前台的路由。
    /// </summary>
    public class SapphireRoute : SapphireRouteBase
    {
        /// <summary>
        /// 通过指定URL模式、默认的参数值、URL 参数的有效值的正则表达式和URL自定义值初始化ManagePathRoute的新实例。
        /// </summary>
        /// <param name="url">URL模式。</param>
        /// <param name="defaults">默认的参数值。</param>
        /// <param name="dataTokens">传递到路由处理程序但未用于确定该路由是否匹配特定 URL 模式的自定义值。 这些值会传递到路由处理程序，以便用于处理请求。</param>
        /// <param name="constraints">URL 参数的有效值的正则表达式。</param>
        /// <param name="isDomainResolve">是否进行子域名站点解析。</param>
        /// <param name="isLowerCaseResolve">是否启用小写Url。</param>
        public SapphireRoute(string url, object defaults = null, object dataTokens = null, object constraints = null, bool isDomainResolve = true, bool isLowerCaseResolve = true)
            : base(url, defaults, dataTokens, constraints, isDomainResolve, isLowerCaseResolve)
        {
        }
    }
}