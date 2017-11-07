using System.Web.Mvc;
using Sapphire.Core.CommonHelper;
using Sapphire.Core.Web;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 扩展Razor引擎。
    /// </summary>
    public class RazorEngineExpand : RazorViewEngine
    {
        /// <summary>
        /// 查找模板。
        /// </summary>
        /// <param name="controllerContext">控制器上下文。</param>
        /// <param name="viewName">模板名称。</param>
        /// <param name="masterName">母板页名称。</param>
        /// <param name="useCache">是否使用缓存。</param>
        /// <returns>返回模板引擎结果。</returns>
        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            Check.NotNull(controllerContext, "controllerContext");

            if (controllerContext.Controller is AdminBaseController)
            {
                this.SetAdmin();
            }
            else
            {
                this.SetFront();
            }

            return base.FindView(controllerContext, viewName, masterName, useCache);
        }

        /// <summary>
        /// 查找标签。
        /// </summary>
        /// <param name="controllerContext">控制器上下文。</param>
        /// <param name="partialViewName">标签名称。</param>
        /// <param name="useCache">是否使用缓存。</param>
        /// <returns>返回模板引擎结果。</returns>
        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            Check.NotNull(controllerContext, "controllerContext");

            if (controllerContext.Controller is AdminBaseController)
            {
                this.SetAdmin();
            }
            else
            {
                this.SetFront();
            }

            return base.FindPartialView(controllerContext, partialViewName, useCache);
        }

        /// <summary>
        /// 创建标签。
        /// </summary>
        /// <param name="controllerContext">控制器上下文。</param>
        /// <param name="partialPath">标签路径。</param>
        /// <returns>返回模板对象。</returns>
        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return new RazorView(controllerContext, partialPath, null, false, this.FileExtensions, this.ViewPageActivator);
        }

        /// <summary>
        /// 创建模板。
        /// </summary>
        /// <param name="controllerContext">控制器上下文。</param>
        /// <param name="viewPath">模板路径。</param>
        /// <param name="masterPath">母板页路径。</param>
        /// <returns>返回模板对象。</returns>
        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return new RazorView(controllerContext, viewPath, masterPath, true, this.FileExtensions, this.ViewPageActivator);
        }

        /// <summary>
        /// 设置管理后台路由。
        /// </summary>
        private void SetAdmin()
        {
            this.AreaPartialViewLocationFormats = new[] { "~/Admin/Views/{2}/{1}/{0}.cshtml", "~/Admin/Views/Shared/{0}.cshtml", "~/Admin/Views/{2}/Shared/{0}.cshtml" };

            this.AreaViewLocationFormats = new[] { "~/Admin/Views/{2}/{1}/{0}.cshtml", "~/Admin/Views/{2}/Shared/{0}.cshtml" };

            this.AreaMasterLocationFormats = new[] { "~/Admin/Views/{2}/{1}/{0}.cshtml", "~/Admin/Views/Shared/{0}.cshtml" };

            this.ViewLocationFormats = new[] { "~/Admin/Views/{1}/{0}.cshtml", "~/Admin/Views/Shared/{0}.cshtml" };

            this.MasterLocationFormats = new[] { "~/Admin/Views/{1}/{0}.cshtml", "~/Admin/Views/Shared/{0}.cshtml" };

            this.PartialViewLocationFormats = new[] { "~/Admin/Views/{1}/{0}.cshtml", "~/Admin/Views/Shared/{0}.cshtml" };

            this.FileExtensions = new[] { "cshtml" };
        }

        /// <summary>
        /// 设置前台路由。
        /// </summary>
        private void SetFront()
        {
            //新的测试规则
            string siteIdentifier = SiteManager.GetSiteIdentifier(SiteContext.Current.SiteId);
            this.AreaPartialViewLocationFormats = new[] { "~/Views/" + siteIdentifier + "/{2}/Shared/{0}.cshtml", "~/Views/" + siteIdentifier + "/Shared/{0}.cshtml", "~/Views/Project/{2}/Shared/{0}.cshtml", "~/Views/Project/Shared/{0}.cshtml" };

            this.AreaViewLocationFormats = new[] { "~/Views/" + siteIdentifier + "/{2}/{1}/{0}.cshtml", "~/Views/" + siteIdentifier + "/Shared/{0}.cshtml", "~/Views/Project/{2}/{1}/{0}.cshtml", "~/Views/Project/Shared/{0}.cshtml" };

            this.AreaMasterLocationFormats = new[] { "~/Views/" + siteIdentifier + "/{2}/Shared/{0}.cshtml", "~/Views/" + siteIdentifier + "/Shared/{0}.cshtml", "~/Views/Project/{2}/Shared/{0}.cshtml", "~/Views/Project/Shared/{0}.cshtml" };

            this.PartialViewLocationFormats = new[] { "~/Views/" + siteIdentifier + "/Shared/{0}.cshtml", "~/Views/Project/Shared/{0}.cshtml" };

            this.MasterLocationFormats = new[] { "~/Views/" + siteIdentifier + "/Shared/{0}.cshtml", "~/Views/Project/Shared/{0}.cshtml" };

            this.ViewLocationFormats = new[] { "~/Views/" + siteIdentifier + "/{1}/{0}.cshtml", "~/Views/" + siteIdentifier + "/Shared/{0}.cshtml", "~/Views/Project/{1}/{0}.cshtml", "~/Views/Project/Shared/{0}.cshtml" };

            this.FileExtensions = new[] { "cshtml" };

            //// 原规则
            //this.AreaPartialViewLocationFormats = new[] { "~/Views/{2}/{1}/Shared/{0}.cshtml", "~/Views/{2}/Shared/{0}.cshtml", "~/Views/Shared/{0}.cshtml" };

            //this.AreaViewLocationFormats = new[] { "~/Views/{2}/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" };

            //this.AreaMasterLocationFormats = new[] { "~/Views/{2}/Shared/{0}.cshtml", "~/Views/Shared/{0}.cshtml" };

            //this.ViewLocationFormats = new[] { "~/Views/Default/{1}/{0}.cshtml", "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" };

            //this.MasterLocationFormats = new[] { "~/Views/Default/Shared/{0}.cshtml", "~/Views/Shared/{0}.cshtml" };

            //this.PartialViewLocationFormats = new[] { "~/Views/Default/{1}/{0}.cshtml", "~/Views/Default/Shared/{0}.cshtml", "~/Views/Shared/{0}.cshtml" };

            //this.FileExtensions = new[] { "cshtml" };
        }
    }
}