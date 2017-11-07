using System.Web.Routing;
using Sapphire.Core.Config;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 提供用于注册区域后台路由。
    /// </summary>
    public class ManagePathRoute : SapphireRoute
    {
        /// <summary>
        /// 通过指定URL模式、默认的参数值、URL 参数的有效值的正则表达式和URL自定义值初始化ManagePathRoute的新实例。
        /// </summary>
        /// <param name="url">URL模式。</param>
        /// <param name="defaults">默认的参数值。</param>
        /// <param name="dataTokens">URL自定义值。</param>
        /// <param name="constraints">URL 参数的有效值的正则表达式。</param>
        public ManagePathRoute(string url, object defaults = null, object dataTokens = null, object constraints = null)
            : base(url, defaults, dataTokens, constraints)
        {
            this.Constraints.Add("admin", new ManagePathRouteConstraint());
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
            if (!values.ContainsKey("admin"))
            {
                values.Add("admin", SecurityConfig.Instance.ManagePath);
                if (values["action"] == null)
                {
                    values["action"] = "Index";
                }
            }

            return base.GetVirtualPath(requestContext, values);
        }
    }
}