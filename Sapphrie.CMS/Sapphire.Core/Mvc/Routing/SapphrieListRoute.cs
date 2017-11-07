using System.Web.Routing;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 提供用于注册区域前台列表页的路由。
    /// </summary>
    public class SapphireListRoute : SapphireRoute
    {
        private const string PageId = "pageid";

        private const string PageIdPlaceholder = "_{pageid}";

        /// <summary>
        /// 通过指定URL模式、默认的参数值、URL 参数的有效值的正则表达式和URL自定义值初始化ManagePathRoute的新实例。
        /// </summary>
        /// <param name="url">URL模式。</param>
        /// <param name="defaults">默认的参数值。</param>
        /// <param name="dataTokens">URL自定义值。</param>
        /// <param name="constraints">URL 参数的有效值的正则表达式。</param>
        /// <param name="isDomainResolve">是否进行子域名站点解析。</param>
        /// <param name="isLowerCaseResolve">是否启用小写Url。</param>
        public SapphireListRoute(string url, object defaults = null, object dataTokens = null, object constraints = null, bool isDomainResolve = true, bool isLowerCaseResolve = true)
            : base(url, defaults, dataTokens, constraints, isDomainResolve, isLowerCaseResolve)
        {
        }

        /// <summary>
        /// 返回与路由关联的 URL 的相关信息。
        /// </summary>
        /// <returns>
        /// 一个包含与路由关联的 URL 的相关信息的对象。
        /// </returns>
        /// <param name="requestContext">一个对象，封装有关所请求的路由的信息。</param>
        /// <param name="values">一个包含路由参数的对象。</param>
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            var virtualPathData = base.GetVirtualPath(requestContext, values);
            if (values.ContainsKey(PageId) && this.Url.Contains(PageIdPlaceholder) && values[PageId].ToString() == "1")
            {
                if (virtualPathData != null)
                {
                    virtualPathData.VirtualPath = virtualPathData.VirtualPath.Replace("_1", string.Empty);
                }
            }

            return virtualPathData;
        }
    }
}