using System;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Sapphire.Core.Config;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 路由基类。
    /// </summary>
    public abstract class SapphireRouteBase : Route
    {
        private static readonly string[] RequiredKeys = { "area", "controller", "action" };

        /// <summary>
        /// 使用指定的 URL 模式、默认参数值、约束、自定义值和处理程序类初始化 System.Web.Routing.Route 类的新实例。
        /// </summary>
        /// <param name="url">路由的 URL 模式。</param>
        /// <param name="defaults">要在 URL 不包含所有参数时使用的值。</param>
        /// <param name="dataTokens">传递到路由处理程序但未用于确定该路由是否匹配特定 URL 模式的自定义值。 这些值会传递到路由处理程序，以便用于处理请求。</param>
        /// <param name="constraints">URL 参数的有效值的正则表达式。</param>
        /// <param name="isDomainResolve">是否进行子域名站点解析。</param>
        /// <param name="isLowerCase">是否启用小写Url。</param>
        protected SapphireRouteBase(string url, object defaults, object dataTokens, object constraints, bool isDomainResolve, bool isLowerCase)
            : base(url, new RouteValueDictionary(defaults), new RouteValueDictionary(constraints), new RouteValueDictionary(dataTokens), new MvcRouteHandler())
        {
            this.DomainResolve = isDomainResolve;
            this.LowercaseResolve = isLowerCase;
        }

        /// <summary>
        /// 是否进行子域名站点解析。
        /// </summary>
        public bool DomainResolve { get; set; }

        /// <summary>
        /// 是否启用小写Url。
        /// </summary>
        public bool LowercaseResolve { get; set; }

        /// <summary>
        /// 返回有关所请求路由的信息。
        /// </summary>
        /// <returns>
        /// 一个对象，其中包含路由定义中的值。
        /// </returns>
        /// <param name="httpContext">一个对象，封装有关 HTTP 请求的信息。</param>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var routeData = base.GetRouteData(httpContext);
            if (routeData != null && this.DomainResolve)
            {
                var domainAnalyzer = SiteRouteAnalyzers.Current;
                var site = domainAnalyzer.GetSite(this.GetDomainPath(httpContext));
                routeData.Values.Add("SiteId", site.SiteId);
                routeData.Values.Add("SiteIdentifier", site.Identifier);
            }

            return routeData;
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
            if (this.LowercaseResolve && SecurityConfig.Instance.EnableLowerUrl)
            {
                using (RouteTable.Routes.GetWriteLock())
                {
                    this.LowerRouteValues(values);
                    this.Url = this.Url.ToLower();
                }
            }

            return base.GetVirtualPath(requestContext, values);
        }

        private void LowerRouteValues(RouteValueDictionary values)
        {
            foreach (var key in RequiredKeys)
            {
                if (values.ContainsKey(key) == false)
                {
                    continue;
                }

                var value = values[key];
                if (value == null)
                {
                    continue;
                }

                var valueString = Convert.ToString(value, CultureInfo.InvariantCulture);
                values[key] = valueString.ToLower();
            }

            var otherKyes = values.Keys
                                  .Except(RequiredKeys, StringComparer.InvariantCultureIgnoreCase)
                                  .ToArray();

            foreach (var key in otherKyes)
            {
                var value = values[key];
                values.Remove(key);
                values.Add(key.ToLower(), value);
            }
        }

        private string GetDomainPath(HttpContextBase httpContext)
        {
            string requestDomain = httpContext.Request.Headers["host"];
            if (!string.IsNullOrEmpty(requestDomain))
            {
                if (requestDomain.IndexOf(":", StringComparison.Ordinal) > 0)
                {
                    requestDomain = requestDomain.Substring(0, requestDomain.IndexOf(":", StringComparison.Ordinal));
                }
            }
            else
            {
                if (httpContext.Request.Url != null)
                {
                    requestDomain = httpContext.Request.Url.Host;
                }
            }

            return requestDomain;
        }
    }
}