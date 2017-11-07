//using System;
//using System.Linq;
//using System.Web.Mvc;
//using Sapphire.Core.Menu;
//using Sapphire.Core.Properties;
//using Sapphire.Core.Utilities;
//using Sapphire.Core.Web;

//namespace Sapphire.Core.Mvc
//{
//    /// <summary>
//    /// 管理员授权过滤器。
//    /// </summary>
//    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
//    public sealed class AdminAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
//    {
//        /// <summary>
//        /// 构造函数。
//        /// </summary>
//        public AdminAuthorizeAttribute()
//        {
//        }

//        /// <summary>
//        /// 构造函数。
//        /// </summary>
//        /// <param name="authorizeArea">用于验证的模块。</param>
//        /// <param name="authorizeController">用于验证的控制器。</param>
//        /// <param name="authorizeAction">用于验证的Action。</param>
//        public AdminAuthorizeAttribute(string authorizeArea, string authorizeController, string authorizeAction)
//        {
//            this.AuthorizeArea = authorizeArea;
//            this.AuthorizeController = authorizeController;
//            this.AuthorizeAction = authorizeAction;
//        }

//        /// <summary>
//        /// 构造函数。
//        /// </summary>
//        /// <param name="authorizeController">用于验证的控制器。</param>
//        /// <param name="authorizeAction">用于验证的Action。</param>
//        public AdminAuthorizeAttribute(string authorizeController, string authorizeAction)
//        {
//            this.AuthorizeController = authorizeController;
//            this.AuthorizeAction = authorizeAction;
//        }

//        /// <summary>
//        /// 构造函数。
//        /// </summary>
//        /// <param name="authorizeAction">用于验证的Action。</param>
//        public AdminAuthorizeAttribute(string authorizeAction)
//        {
//            this.AuthorizeAction = authorizeAction;
//        }

//        /// <summary>
//        /// 用于验证的模块。
//        /// </summary>
//        public string AuthorizeArea { get; set; }

//        /// <summary>
//        /// 用于验证的控制器。
//        /// </summary>
//        public string AuthorizeController { get; set; }

//        /// <summary>
//        /// 用于验证的Action。
//        /// </summary>
//        public string AuthorizeAction { get; set; }

//        /// <summary>
//        /// 后台管理员授权。
//        /// </summary>
//        /// <param name="filterContext">授权上下文实体对象。</param>
//        public void OnAuthorization(AuthorizationContext filterContext)
//        {
//            Check.NotNull(filterContext, "filterContext");
//            var area = !string.IsNullOrEmpty(this.AuthorizeArea) ? this.AuthorizeArea : (filterContext.RequestContext.RouteData.DataTokens["area"] != null ? filterContext.RequestContext.RouteData.DataTokens["area"].ToString() : string.Empty);
//            var controller = !string.IsNullOrEmpty(this.AuthorizeController) ? this.AuthorizeController : filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
//            var action = !string.IsNullOrEmpty(this.AuthorizeAction) ? this.AuthorizeAction : filterContext.ActionDescriptor.ActionName;
//            var actionskipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) || filterContext.ActionDescriptor.IsDefined(typeof(AllowAnyRoleAttribute), true);
//            var controllerskipAuthorization = filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnyRoleAttribute), true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
//            var skipAuthorization = actionskipAuthorization || (controllerskipAuthorization && !filterContext.ActionDescriptor.IsDefined(typeof(AdminAuthorizeAttribute), true));
//            if (skipAuthorization)
//            {
//                return;
//            }

//            var moduleInfo = MenuModel.GetModuleInfo(area, controller, action);
//            if (moduleInfo == null)
//            {
//                moduleInfo = MenuModel.GetMatchModuleInfo(area, controller, action);
//            }

//            if (moduleInfo == null || !SiteManager.SiteEnable(moduleInfo.IncludeSiteIds, moduleInfo.ExcludeSiteIds))
//            {
//                HandleExcludeSiteRequest(filterContext);
//            }

//            var adminPrincipal = SiteContext.Current.Admin;
//            if (adminPrincipal.IsSuperAdmin)
//            {
//                return;
//            }

//            var isPlatform = false;
//            if (moduleInfo.MenuType != null)
//            {
//                isPlatform = moduleInfo.MenuType == (int)MenuType.Platform;
//            }

//            if (isPlatform)
//            {
//                filterContext.RequestContext.RouteData.DataTokens["IsPlatform"] = true;
//                if (!adminPrincipal.Roles.Any())
//                {
//                    HandleUnauthorizedRequest(filterContext);
//                }

//                var roleIds = Manager.GetRoleIds(area, controller, action);
//                if (!adminPrincipal.HasRole(roleIds))
//                {
//                    HandleUnauthorizedRequest(filterContext);
//                }
//            }
//            else
//            {
//                filterContext.RequestContext.RouteData.DataTokens["IsPlatform"] = false;
//                var state = SiteManager.SiteAuthorize();
//                if (state == SiteUserIdentity.Unauthorized)
//                {
//                    HandleUnauthorizedSiteRequest(filterContext);
//                }

//                //如果当前请求的action是子站的，并且当前管理员是子站超管，验证通过。
//                if (state == SiteUserIdentity.SupperSiteAdmin)
//                {
//                    return;
//                }

//                var roleIds = SiteManager.GetSitePermissionSetIds(area, controller, action);
//                if (!adminPrincipal.HasSitePermissionSet(roleIds))
//                {
//                    HandleUnauthorizedRequest(filterContext);
//                }
//            }
//        }

//        /// <summary>
//        /// 处理未授权请求。
//        /// </summary>
//        /// <param name="filterContext">
//        /// 授权上下文。
//        /// </param>
//        private static void HandleUnauthorizedRequest(AuthorizationContext filterContext)
//        {
//            Check.NotNull(filterContext, "filterContext");

//            if (filterContext.HttpContext.Request.IsAjaxRequest())
//            {
//                filterContext.HttpContext.Response.StatusCode = 403;
//                filterContext.Result = filterContext.Result ?? new MessageResult(MessageType.Error, Resources.AdminAuthorizeAttribute_HandleUnauthorizedRequest_UnauthorizedRequest);
//            }
//            else
//            {
//                filterContext.Result = filterContext.Result ?? new ErrorResult(Resources.AdminAuthorizeAttribute_HandleUnauthorizedRequest_UnauthorizedRequest);
//            }
//        }

//        /// <summary>
//        /// 处理未授权请求。
//        /// </summary>
//        /// <param name="filterContext">授权上下文。</param>
//        private static void HandleUnauthorizedSiteRequest(AuthorizationContext filterContext)
//        {
//            Check.NotNull(filterContext, "filterContext");
//            var returnUrl = UrlHelperExtension.GetAdminLoginUrl();
//            if (filterContext.HttpContext.Request.IsAjaxRequest())
//            {
//                filterContext.HttpContext.Response.StatusCode = 403;
//                filterContext.Result = filterContext.Result ?? new MessageResult(MessageType.Error, Resources.AdminAuthorizeAttribute_HandleUnauthorizedRequest_UnauthorizedRequest, string.Empty, returnUrl);
//            }
//            else
//            {
//                filterContext.Result = filterContext.Result ?? new ErrorResult(Resources.AdminAuthorizeAttribute_HandleUnauthorizedRequest_UnauthorizedRequest, returnUrl);
//            }
//        }

//        /// <summary>
//        /// 处理未授权请求。
//        /// </summary>
//        /// <param name="filterContext"> 授权上下文。 </param>
//        private static void HandleExcludeSiteRequest(AuthorizationContext filterContext)
//        {
//            Check.NotNull(filterContext, "filterContext");

//            if (filterContext.HttpContext.Request.IsAjaxRequest())
//            {
//                filterContext.HttpContext.Response.StatusCode = 403;
//                filterContext.Result = filterContext.Result ?? new MessageResult(MessageType.Error, "当前站点不包含此模块，请联系平台管理员！");
//            }
//            else
//            {
//                filterContext.Result = filterContext.Result ?? new ErrorResult("当前站点不包含此模块，请联系平台管理员！");
//            }
//        }
//    }
//}