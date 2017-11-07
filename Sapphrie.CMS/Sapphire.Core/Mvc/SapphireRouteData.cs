using System.Web.Routing;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 接收反序列化后的数据类。
    /// </summary>
    public class SapphireRouteData
    {
        /// <summary>
        /// 一个包含路由参数的对象。
        /// </summary>
        public RouteValueDictionary RouteValues { get; set; }

        /// <summary>
        /// 用于生成 URL 的路由的名称。
        /// </summary>
        public string RouteName { get; set; }
    }
}