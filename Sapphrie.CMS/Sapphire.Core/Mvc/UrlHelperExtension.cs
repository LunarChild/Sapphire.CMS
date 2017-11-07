using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Sapphire.Core.Lunar;
using Sapphire.Core.CommonHelper;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// UrlHelper扩展类。
    /// </summary>
    public static class UrlHelperExtension
    {
        /// <summary>
        /// 后台主页路由名称。
        /// </summary>
        public const string AdminHomeRouteName = "Admin.Home";

        /// <summary>
        /// 后台公共路由名称。
        /// </summary>
        public const string AdminCommonRouteName = "Admin.Common";

        /// <summary>
        /// 后台路由名称格式化字符串。
        /// </summary>
        public const string AdminRouteNameFormat = "Admin.{0}";

        /// <summary>
        /// 前台路由名称格式化字符串。
        /// </summary>
        public const string FrontRouteNameFormat = "Front.{0}";

        /// <summary>
        /// 使用指定的操作名称生成后台操作方法的完全限定 URL。
        /// </summary>
        /// <param name="urlHelper">用于为应用程序内的 ASP.NET MVC 生成 URL 的方法。</param>
        /// <param name="actionName">操作方法的名称。</param>
        /// <returns>后台操作方法的完全限定 URL。</returns>
        public static string AdminAction(this UrlHelper urlHelper, string actionName)
        {
            return AdminAction(urlHelper, actionName, null);
        }

        /// <summary>
        /// 使用指定的操作名称和控制器名称生成后台操作方法的完全限定 URL。
        /// </summary>
        /// <param name="urlHelper">用于为应用程序内的 ASP.NET MVC 生成 URL 的方法。</param>
        /// <param name="actionName">操作方法的名称。</param>
        /// <param name="controllerName">控制器的名称。</param>
        /// <returns>后台操作方法的完全限定 URL。</returns>
        public static string AdminAction(this UrlHelper urlHelper, string actionName, string controllerName)
        {
            return AdminAction(urlHelper, actionName, controllerName, null);
        }

        /// <summary>
        /// 使用指定的操作名称、控制器名称和区域名称生成后台操作方法的完全限定 URL。
        /// </summary>
        /// <param name="urlHelper">用于为应用程序内的 ASP.NET MVC 生成 URL 的方法。</param>
        /// <param name="actionName">操作方法的名称。</param>
        /// <param name="controllerName">控制器的名称。</param>
        /// <param name="areaName">区域名称。</param>
        /// <returns>后台操作方法的完全限定 URL。</returns>
        public static string AdminAction(this UrlHelper urlHelper, string actionName, string controllerName, string areaName)
        {
            return AdminAction(urlHelper, actionName, controllerName, areaName, null);
        }

        /// <summary>
        /// 使用指定的操作名称和路由值生成后台操作方法的完全限定 URL。
        /// </summary>
        /// <param name="urlHelper">用于为应用程序内的 ASP.NET MVC 生成 URL 的方法。</param>
        /// <param name="actionName">操作方法的名称。</param>
        /// <param name="routeValues">一个包含路由参数的对象。</param>
        /// <returns>后台操作方法的完全限定 URL。</returns>
        public static string AdminAction(this UrlHelper urlHelper, string actionName, object routeValues)
        {
            return AdminAction(urlHelper, actionName, null, routeValues);
        }

        /// <summary>
        /// 使用指定的操作名称、控制器名称和路由值生成后台操作方法的完全限定 URL。
        /// </summary>
        /// <param name="urlHelper">用于为应用程序内的 ASP.NET MVC 生成 URL 的方法。</param>
        /// <param name="actionName">操作方法的名称。</param>
        /// <param name="controllerName">控制器的名称。</param>
        /// <param name="routeValues">一个包含路由参数的对象。</param>
        /// <returns>后台操作方法的完全限定 URL。</returns>
        public static string AdminAction(this UrlHelper urlHelper, string actionName, string controllerName, object routeValues)
        {
            return AdminAction(urlHelper, actionName, controllerName, null, routeValues);
        }

        /// <summary>
        /// 使用指定的操作名称、控制器名称、区域名称和路由值生成后台操作方法的完全限定 URL。
        /// </summary>
        /// <param name="urlHelper">用于为应用程序内的 ASP.NET MVC 生成 URL 的方法。</param>
        /// <param name="actionName">操作方法的名称。</param>
        /// <param name="controllerName">控制器的名称。</param>
        /// <param name="areaName">区域名称。</param>
        /// <param name="routeValues">一个包含路由参数的对象。</param>
        /// <returns>后台操作方法的完全限定 URL。</returns>
        public static string AdminAction(this UrlHelper urlHelper, string actionName, string controllerName, string areaName, object routeValues)
        {
            return UrlHelper.GenerateUrl(
                GetAdminRouteName(areaName),
                actionName,
                controllerName,
                routeValues as RouteValueDictionary ?? (RouteValueDictionary)HtmlHelper.ObjectToDictionary(routeValues),
                urlHelper.RouteCollection,
                urlHelper.RequestContext,
                true);
        }

        /// <summary>
        /// 使用指定的操作名称生成后台操作方法的完全限定 URL。
        /// </summary>
        /// <param name="urlHelper">用于为应用程序内的 ASP.NET MVC 生成 URL 的方法。</param>
        /// <param name="actionName">操作方法的名称。</param>
        /// <returns>后台操作方法的完全限定 URL。</returns>
        public static string AdminHomeAction(this UrlHelper urlHelper, string actionName)
        {
            return UrlHelper.GenerateUrl(
                AdminHomeRouteName,
                actionName,
                null,
                null,
                urlHelper.RouteCollection,
                urlHelper.RequestContext,
                false);
        }

        /// <summary>
        /// 使用指定的操作名称生成后台操作方法的完全限定 URL。
        /// </summary>
        /// <param name="urlHelper">用于为应用程序内的 ASP.NET MVC 生成 URL 的方法。</param>
        /// <param name="actionName">操作方法的名称。</param>
        /// <param name="routeValues">一个包含路由参数的对象。</param>
        /// <returns>后台操作方法的完全限定 URL。</returns>
        public static string AdminHomeAction(this UrlHelper urlHelper, string actionName, object routeValues)
        {
            return UrlHelper.GenerateUrl(
                AdminHomeRouteName,
                actionName,
                null,
                routeValues as RouteValueDictionary ?? (RouteValueDictionary)HtmlHelper.ObjectToDictionary(routeValues),
                urlHelper.RouteCollection,
                urlHelper.RequestContext,
                false);
        }

        /// <summary>
        /// 使用指定的操作名称和控制器名称生成后台操作方法的完全限定 URL。
        /// </summary>
        /// <param name="urlHelper">用于为应用程序内的 ASP.NET MVC 生成 URL 的方法。</param>
        /// <param name="actionName">操作方法的名称。</param>
        /// <param name="controller">控制器的名称。</param>
        /// <returns>后台操作方法的完全限定 URL。</returns>
        public static string AdminCommonAction(this UrlHelper urlHelper, string actionName, string controller)
        {
            return AdminCommonAction(urlHelper, actionName, controller, null);
        }

        /// <summary>
        /// 使用指定的操作名称和控制器名称生成后台操作方法的完全限定 URL。
        /// </summary>
        /// <param name="urlHelper">用于为应用程序内的 ASP.NET MVC 生成 URL 的方法。</param>
        /// <param name="actionName">操作方法的名称。</param>
        /// <param name="controller">控制器的名称。</param>
        /// <param name="routeValues">一个包含路由参数的对象。</param>
        /// <returns>后台操作方法的完全限定 URL。</returns>
        public static string AdminCommonAction(this UrlHelper urlHelper, string actionName, string controller, object routeValues)
        {
            return UrlHelper.GenerateUrl(
                AdminCommonRouteName,
                actionName,
                controller,
                routeValues as RouteValueDictionary ?? (RouteValueDictionary)HtmlHelper.ObjectToDictionary(routeValues),
                urlHelper.RouteCollection,
                urlHelper.RequestContext,
                false);
        }

        /// <summary>
        /// 获取当前请求的后台路由名称。
        /// </summary>
        /// <returns>后台路由名称。</returns>
        public static string GetAdminRouteName()
        {
            return GetAdminRouteName(null);
        }

        /// <summary>
        /// 获取指定区域的后台路由名称。
        /// </summary>
        /// <param name="areaName">区域名称。</param>
        /// <returns>后台路由名称。</returns>
        public static string GetAdminRouteName(string areaName)
        {
            return string.Format(AdminRouteNameFormat, areaName ?? HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"]);
        }

        /// <summary>
        /// 获取后台登录页 URL 地址。
        /// </summary>
        /// <returns>后台登录页 URL 地址。</returns>
        public static string GetAdminLoginUrl()
        {
            return new UrlHelper(HttpContext.Current.Request.RequestContext).AdminHomeAction("Login");
        }

        /// <summary>
        /// 生成内容路径。
        /// </summary>
        /// <param name="urlHelper">UrlHelper。</param>
        /// <param name="path">路径。</param>
        /// <returns>返回内容路径。</returns>
        public static string GetContentUrl(this SapphireUrlHelper urlHelper, string path)
        {
            Check.NotNull(urlHelper, "urlHelper");
            return PathHelper.GetContentUrl(path);
        }

        /// <summary>
        /// 替换文本字符串里面的上传路径。
        /// </summary>
        /// <param name="urlHelper">UrlHelper。</param>
        /// <param name="body">需要替换的文本。</param>
        /// <returns>替换后的文本。</returns>
        public static string ReplaceUploadPath(this SapphireUrlHelper urlHelper, string body)
        {
            Check.NotNull(urlHelper, "urlHelper");
            return PathHelper.ReplaceUploadPath(body);
        }

        /// <summary>
        /// 获取上传路径。
        /// </summary>
        /// <param name="urlHelper">UrlHelper。</param>
        /// <param name="path">需要替换的目录。</param>
        /// <returns>替换后的目录。</returns>
        public static string GetUploadPath(this SapphireUrlHelper urlHelper, string path)
        {
            Check.NotNull(urlHelper, "urlHelper");
            return PathHelper.GetUploadPath(path);
        }

        /// <summary>
        /// 给url增加当前http请求中存在的hash值。
        /// </summary>
        /// <param name="url">需要增加hash值的url。</param>
        /// <returns>返回添加了hash值的url。</returns>
        public static string AddCurrentHash(string url)
        {
            var state = HttpContext.Current.Request["state"];
            if (!string.IsNullOrWhiteSpace(state) && !Regex.IsMatch(state, "#"))
            {
                url = url.Split(new[] { '#' })[0] + "##" + state;
            }

            return url;
        }
    }
}