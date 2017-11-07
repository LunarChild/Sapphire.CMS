using System.Web.Mvc;
using Sapphire.Core.CommonHelper;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 区域注册基类。
    /// </summary>
    public abstract class BaseAreaRegistration : AreaRegistration
    {
        private const string FrontControllersNamespace = "Sapphire.Modules.{0}.Controllers";
        private const string AdminControllersNamespace = "Sapphire.Modules.{0}.Admin.Controllers";
        private const string FrontMapRouteUrl = "{0}/{{controller}}/{{action}}/{{id}}";
        private const string AdminMapRouteUrl = "{{admin}}/{0}/{{controller}}/{{action}}/{{id}}";

        /// <summary>
        /// 注册模块前台路由时指定的默认控制器名称。
        /// </summary>
        public virtual string FrontDefaultController
        {
            get
            {
                return "Home";
            }
        }

        /// <summary>
        /// 区域前台路由名称。
        /// </summary>
        protected string AreaFrontRouteName
        {
            get
            {
                return string.Format(UrlHelperExtension.FrontRouteNameFormat, this.AreaName);
            }
        }

        /// <summary>
        /// 区域前台控制器命名空间。
        /// </summary>
        protected string AreaFrontControllersNamespace
        {
            get
            {
                return string.Format(FrontControllersNamespace, this.AreaName);
            }
        }

        /// <summary>
        /// 区域前台路由约束。
        /// </summary>
        protected object AreaFrontRouteDataTokens
        {
            get
            {
                return new { area = this.AreaName, Namespaces = new[] { this.AreaFrontControllersNamespace } };
            }
        }

        /// <summary>
        /// 使用指定区域的上下文信息在 ASP.NET MVC 应用程序内注册某个区域。
        /// </summary>
        /// <param name="context">对注册区域所需的信息进行封装。</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            Check.NotNull(context, "context");
            this.RegisterFrontAreaList(context);
            this.RegisterFrontArea(context);
            this.RegisterAdminArea(context);
        }

        /// <summary>
        /// 注册区域前台路由。
        /// </summary>
        /// <param name="context">对注册区域所需的信息进行封装。</param>
        public virtual void RegisterFrontArea(AreaRegistrationContext context)
        {
            context.Routes.Add(
                this.AreaFrontRouteName,
                new SapphireRoute(
                    string.Format(FrontMapRouteUrl, this.AreaName),
                    new { controller = this.FrontDefaultController, action = "Index", id = UrlParameter.Optional },
                    this.AreaFrontRouteDataTokens));
        }

        /// <summary>
        /// 注册区域前台列表页路由。
        /// </summary>
        /// <param name="context">对注册区域所需的信息进行封装。</param>
        public virtual void RegisterFrontAreaList(AreaRegistrationContext context)
        {
            context.Routes.Add(
                this.AreaFrontRouteName + ".List",
                new SapphireListRoute(
                    string.Format("{0}/{{controller}}/{{action}}_{{pageid}}", this.AreaName),
                    new { controller = this.FrontDefaultController, action = "list", pageid = UrlParameter.Optional },
                    this.AreaFrontRouteDataTokens,
                    new { pageid = @"([1-9]\d*)" }));
        }

        /// <summary>
        /// 注册区域后台路由。
        /// </summary>
        /// <param name="context">对注册区域所需的信息进行封装。</param>
        public virtual void RegisterAdminArea(AreaRegistrationContext context)
        {
            context.Routes.Add(
                string.Format(UrlHelperExtension.AdminRouteNameFormat, this.AreaName),
                new ManagePathRoute(
                    string.Format(AdminMapRouteUrl, this.AreaName),
                    new { action = "Index", id = UrlParameter.Optional },
                    new { area = this.AreaName, Namespaces = new[] { string.Format(AdminControllersNamespace, this.AreaName) } }));
        }
    }
}