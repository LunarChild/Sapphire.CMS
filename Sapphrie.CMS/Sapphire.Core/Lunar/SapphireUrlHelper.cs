using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sapphire.Core.Lunar
{
    /// <summary>
    /// 路径助手。
    /// </summary>
    public class SapphireUrlHelper
    {
        /// <summary>
        /// 模板上下文。
        /// </summary>
        internal static ViewContext ViewContext { get; set; }

        /// <summary>
        /// 模板数据。
        /// </summary>
        internal static ViewDataDictionary ViewData
        {
            get
            {
                return ViewDataContainer.ViewData;
            }
        }

        /// <summary>
        /// 路由集合。
        /// </summary>
        internal static RouteCollection RouteCollection { get; set; }

        /// <summary>
        /// 模板数据容器。
        /// </summary>
        internal static IViewDataContainer ViewDataContainer { get; set; }

        /// <summary>
        /// 替换当前Url的指定参数值，生成新的Url字符串。
        /// </summary>
        /// <param name="parameterName">Url中的查询参数名。</param>
        /// <param name="value">替换的查询参数值。</param>
        /// <returns>替换查询参数的Url。</returns>
        public static string RelpaceParameter(string parameterName, object value)
        {
            var request = HttpContext.Current.Request;
            var query = request.Url.Query;

            if (string.IsNullOrEmpty(query))
            {
                query = string.Format("?{0}={1}", parameterName, value);
            }
            else
            {
                if (query.Contains(parameterName))
                {
                    query = query.Replace(parameterName + "=" + request.QueryString[parameterName], parameterName + "=" + value);
                }
                else
                {
                    query = query + string.Format("&{0}={1}", parameterName, value);
                }
            }

            if (request.RequestContext.HttpContext.Request.Url != null)
            {
                return request.RequestContext.HttpContext.Request.Url.AbsolutePath + query;
            }

            return query;
        }

        /// <summary>
        /// 返回一个包含 URL 的字符串。
        /// </summary>
        /// <returns>一个字符串，其中包含 URL。</returns>
        public string Url()
        {
            return HttpContext.Current.Request.RawUrl;
        }

        /// <summary>
        /// 返回一个包含 URL 的字符串。
        /// </summary>
        /// <param name="actionName">操作名称。</param>
        /// <returns>一个字符串，其中包含 URL。</returns>
        public string Url(string actionName)
        {
            return this.Url(null /* routeName */, actionName, null, null /* routeValues */);
        }

        /// <summary>
        /// 返回一个包含 URL 的字符串。
        /// </summary>
        /// <param name="actionName">操作名称。</param>
        /// <param name="routeValues">路由值。</param>
        /// <returns>一个字符串，其中包含 URL。</returns>
        public string Url(string actionName, object routeValues)
        {
            return this.Url(null /* routeName */, actionName, null /* controllerName */, new RouteValueDictionary(routeValues));
        }

        /// <summary>
        /// 返回一个包含 URL 的字符串。
        /// </summary>
        /// <param name="actionName">操作名称。</param>
        /// <param name="routeValues">路由值。</param>
        /// <returns>一个字符串，其中包含 URL。</returns>
        public string Url(string actionName, RouteValueDictionary routeValues)
        {
            return this.Url(null /* routeName */, actionName, null /* controllerName */, routeValues);
        }

        /// <summary>
        /// 返回一个包含 URL 的字符串。
        /// </summary>
        /// <param name="actionName">操作名称。</param>
        /// <param name="controllerName">控制器名称。</param>
        /// <returns>一个字符串，其中包含 URL。</returns>
        public string Url(string actionName, string controllerName)
        {
            return this.Url(null /* routeName */, actionName, controllerName, null /* routeValues */);
        }

        /// <summary>
        /// 返回一个包含 URL 的字符串。
        /// </summary>
        /// <param name="actionName">操作名称。</param>
        /// <param name="controllerName">控制器名称。</param>
        /// <param name="routeValues">路由值。</param>
        /// <returns>一个字符串，其中包含 URL。</returns>
        public string Url(string actionName, string controllerName, object routeValues)
        {
            return this.Url(null /* routeName */, actionName, controllerName, new RouteValueDictionary(routeValues));
        }

        /// <summary>
        /// 返回一个包含 URL 的字符串。
        /// </summary>
        /// <param name="actionName">操作名称。</param>
        /// <param name="controllerName">控制器名称。</param>
        /// <param name="routeValues">路由值。</param>
        /// <returns>一个字符串，其中包含 URL。</returns>
        public string Url(string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            return this.Url(null /* routeName */, actionName, controllerName, routeValues);
        }

        /// <summary>
        /// 返回一个包含 URL 的字符串。
        /// </summary>
        /// <param name="actionName">操作名称。</param>
        /// <param name="controllerName">控制器名称。</param>
        /// <param name="routeValues">路由值。</param>
        /// <param name="protocol">HTTP 协议。</param>
        /// <returns>一个字符串，其中包含 URL。</returns>
        public string Url(string actionName, string controllerName, object routeValues, string protocol)
        {
            // 需要重写实现
            return UrlHelper.GenerateUrl(
                null /* routeName */,
                actionName,
                controllerName,
                protocol,
                null /* hostName */,
                null /* fragment */,
                new RouteValueDictionary(routeValues),
                RouteCollection,
                ViewContext.RequestContext,
                true /* includeImplicitMvcValues */);
        }

        /// <summary>
        /// 返回一个包含 URL 的字符串。
        /// </summary>
        /// <param name="actionName">操作名称。</param>
        /// <param name="controllerName">控制器名称。</param>
        /// <param name="routeValues">路由值。</param>
        /// <param name="protocol">HTTP 协议。</param>
        /// <param name="hostName">主机名。</param>
        /// <returns>一个字符串，其中包含 URL。</returns>
        public string Url(string actionName, string controllerName, RouteValueDictionary routeValues, string protocol, string hostName)
        {
            // 需要重写实现
            return UrlHelper.GenerateUrl(
                null /* routeName */,
                actionName,
                controllerName,
                protocol,
                hostName,
                null /* fragment */,
                routeValues,
                RouteCollection,
                ViewContext.RequestContext,
                true /* includeImplicitMvcValues */);
        }

        private string Url(string routeName, string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            var isStaticFlag = ViewData[EngineHelper.GenerateStaticHtmlFlagKey] != null;

            // 生成规则根据实际内容模型需要重写
            if (isStaticFlag)
            {
                if (routeValues.ContainsKey("ContentId"))
                {
                    // 内容页
                    var contentId = routeValues["ContentId"].ToString();
                    var contentUrl = "/" + EngineHelper.GenerateStaticHtmlFileDirectory + "/Article_" + contentId + ".html";
                    return contentUrl;
                }

                if (routeValues.ContainsKey("CategoryId"))
                {
                    // 列表页
                    var categoryId = routeValues["CategoryId"].ToString();
                    var categoryUrl = "/" + EngineHelper.GenerateStaticHtmlFileDirectory + "/Category_" + categoryId + ".html";
                    return categoryUrl;
                }
            }

            return UrlHelper.GenerateUrl(routeName, actionName, controllerName, routeValues, RouteCollection, ViewContext.RequestContext, true /* includeImplicitMvcValues */);
        }
    }
}