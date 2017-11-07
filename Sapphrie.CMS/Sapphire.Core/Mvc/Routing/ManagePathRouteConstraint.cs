using System;
using System.Web;
using System.Web.Routing;
using Sapphire.Core.Config;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 定义某 URL 参数值是否与管理员目录具有相同的值的协定。
    /// </summary>
    public class ManagePathRouteConstraint : IRouteConstraint
    {
        /// <summary>
        /// 确定 URL 参数是否与管理员目录具有相同的值。
        /// </summary>
        /// <param name="httpContext">一个对象，封装有关 HTTP 请求的信息。</param>
        /// <param name="route">此约束所属的对象。</param>
        /// <param name="parameterName">正在检查的参数的名称。</param>
        /// <param name="values">一个包含 URL 的参数的对象。</param>
        /// <param name="routeDirection">一个对象，指示在处理传入请求或生成 URL 时，是否正在执行约束检查。</param>
        /// <returns>如果 URL 参数与管理员目录具有相同的值，则为 true；否则为 false。</returns>
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.ContainsKey(parameterName))
            {
                return false;
            }

            return string.Equals(values[parameterName].ToString(), SecurityConfig.Instance.ManagePath, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}