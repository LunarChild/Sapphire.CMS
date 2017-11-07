//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Mvc;
//using Sapphire.Core.Menu;
//using Sapphire.Core.Web;

//namespace Sapphire.Core.Mvc
//{
//    /// <summary>
//    /// 权限帮助类。
//    /// </summary>
//    public class PurviewHelper
//    {
//        /// <summary>
//        /// 菜单模型缓存键。
//        /// </summary>
//        private const string AuthorizePurviewCacheKey = "CK_AuthorizePurview_CacheKey";

//        /// <summary>
//        /// 验证指定的controller跟action是否有权限。
//        /// </summary>
//        /// <param name="context">ControllerContext。</param>
//        /// <param name="actionName">action名称。</param>
//        /// <returns>认证结果。</returns>
//        public static bool Authorize(ControllerContext context, string actionName)
//        {
//            return Authorize(context, string.Empty, actionName);
//        }

//        /// <summary>
//        /// 验证指定的controller跟action是否有权限。
//        /// </summary>
//        /// <param name="context">ControllerContext。</param>
//        /// <param name="controllerName">控制器名称。</param>
//        /// <param name="actionName">action名称。</param>
//        /// <returns>认证结果。</returns>
//        public static bool Authorize(ControllerContext context, string controllerName, string actionName)
//        {
//            if (string.IsNullOrEmpty(actionName))
//            {
//                return false;
//            }

//            if (string.IsNullOrEmpty(controllerName))
//            {
//                controllerName = context.RequestContext.RouteData.Values["controller"].ToString();
//            }

//            var purviewModel = GetPurviewModelFromCache(context, string.Empty, controllerName, actionName);
//            if (purviewModel.SkipAuthorize)
//            {
//                return true;
//            }

//            var moduleInfo = MenuModel.GetModuleInfo(purviewModel.AuthorizeArea, purviewModel.AuthorizeController, purviewModel.AuthorizeAction);
//            if (moduleInfo.MenuType == null)
//            {
//                moduleInfo = MenuModel.GetMatchModuleInfo(purviewModel.AuthorizeArea, purviewModel.AuthorizeController, purviewModel.AuthorizeAction);
//            }

//            bool isPlatform = false;
//            if (moduleInfo.MenuType != null)
//            {
//                isPlatform = moduleInfo.MenuType == (int)MenuType.Platform;
//            }

//            if (isPlatform)
//            {
//                return PlatformAuthorize(purviewModel.AuthorizeArea, purviewModel.AuthorizeController, purviewModel.AuthorizeAction);
//            }

//            return SiteAuthorize(purviewModel.AuthorizeArea, purviewModel.AuthorizeController, purviewModel.AuthorizeAction);
//        }

//        /// <summary>
//        /// 角色权限认证。
//        /// </summary>
//        /// <param name="area">区域。</param>
//        /// <param name="controllerName">控制器名称。</param>
//        /// <param name="actionName">action名称。</param>
//        /// <returns>认证是否通过。</returns>
//        public static bool PlatformAuthorize(string area, string controllerName, string actionName)
//        {
//            var adminPrincipal = SiteContext.Current.Admin;
//            if (adminPrincipal.IsSuperAdmin)
//            {
//                return true;
//            }

//            int[] roleIds;
//            var moduleInfo = MenuModel.GetModuleInfo(area, controllerName, actionName);
//            if (string.Compare(actionName, moduleInfo.Purview, StringComparison.OrdinalIgnoreCase) == 0 || string.IsNullOrEmpty(moduleInfo.Purview))
//            {
//                roleIds = Manager.GetRoleIds(area, controllerName, actionName);
//            }
//            else
//            {
//                roleIds = Manager.GetRoleIds(area, controllerName, moduleInfo.Purview);
//            }

//            if (adminPrincipal.HasRole(roleIds))
//            {
//                return true;
//            }

//            return false;
//        }

//        /// <summary>
//        /// 角色权限认证。
//        /// </summary>
//        /// <param name="area">区域。</param>
//        /// <param name="controllerName">控制器名称。</param>
//        /// <param name="actionName">action名称。</param>
//        /// <returns>认证是否通过。</returns>
//        public static bool SiteAuthorize(string area, string controllerName, string actionName)
//        {
//            var adminPrincipal = SiteContext.Current.Admin;
//            if (adminPrincipal.IsSuperAdmin)
//            {
//                return true;
//            }

//            SiteUserIdentity state = SiteManager.SiteAuthorize();
//            if (state == SiteUserIdentity.Unauthorized)
//            {
//                return false;
//            }

//            //如果当前管理员是子站超管，验证通过。
//            if (state == SiteUserIdentity.SupperSiteAdmin)
//            {
//                return true;
//            }

//            int[] roleIds;
//            var moduleInfo = MenuModel.GetModuleInfo(area, controllerName, actionName);
//            if (string.Compare(actionName, moduleInfo.Purview, StringComparison.OrdinalIgnoreCase) == 0 || string.IsNullOrEmpty(moduleInfo.Purview))
//            {
//                roleIds = SiteManager.GetSitePermissionSetIds(area, controllerName, actionName);
//            }
//            else
//            {
//                roleIds = SiteManager.GetSitePermissionSetIds(area, controllerName, moduleInfo.Purview);
//            }

//            if (adminPrincipal.HasSitePermissionSet(roleIds))
//            {
//                return true;
//            }

//            return false;
//        }

//        /// <summary>
//        /// 从缓存中获取权限认证实体。
//        /// </summary>
//        /// <param name="context">ControllerContext。</param>
//        /// <param name="area">区域。</param>
//        /// <param name="controller">控制器名称。</param>
//        /// <param name="action">action名称。</param>
//        /// <returns>权限认证实体。</returns>
//        public static AuthorizePurviewModel GetPurviewModelFromCache(ControllerContext context, string area, string controller, string action)
//        {
//            if (string.IsNullOrEmpty(area))
//            {
//                area = context.RequestContext.RouteData.DataTokens["area"].ToString();
//            }

//            var newPurviewList = new List<AuthorizePurviewModel>();
//            var purviewlist = SapphrieCache.Get(AuthorizePurviewCacheKey, newPurviewList.ToList);
//            if (purviewlist.Any(x => x.Action.ToLower() == action.ToLower() && x.Controller.ToLower() == controller.ToLower() && x.Area.ToLower() == area.ToLower()))
//            {
//                return purviewlist.First(x => x.Action.ToLower() == action.ToLower() && x.Controller.ToLower() == controller.ToLower() && x.Area.ToLower() == area.ToLower());
//            }

//            var purviewModel = GetActionAuthorizeAttribute(context, area, controller, action);
//            purviewlist.Add(purviewModel);
//            SapphrieCache.Set(AuthorizePurviewCacheKey, purviewlist.ToList);
//            return purviewModel;
//        }

//        /// <summary>
//        /// 根据传入的控制器与动作名称，获取权限认证实体。
//        /// </summary>
//        /// <param name="context">模板Context。</param>
//        /// <param name="area">区域名称。</param>
//        /// <param name="controller">控制器名称。</param>
//        /// <param name="action">action名称。</param>
//        /// <returns>权限验证实体。</returns>
//        private static AuthorizePurviewModel GetActionAuthorizeAttribute(ControllerContext context, string area, string controller, string action)
//        {
//            ControllerBase controllerBase;
//            if (string.IsNullOrEmpty(controller))
//            {
//                controllerBase = context.Controller;
//                controller = context.RequestContext.RouteData.Values["controller"].ToString();
//            }
//            else
//            {
//                var controllerFactory = ControllerBuilder.Current.GetControllerFactory();
//                controllerBase = (ControllerBase)controllerFactory.CreateController(context.RequestContext, controller);
//            }

//            var purviewModel = new AuthorizePurviewModel { AuthorizeAction = action, Action = action, AuthorizeController = controller, Controller = controller, Area = area, AuthorizeArea = area, SkipAuthorize = false };
//            var controllerDescriptor = new ReflectedControllerDescriptor(controllerBase.GetType());
//            var controllerContext = new ControllerContext(context.RequestContext, controllerBase);
//            var actionDescriptor = controllerDescriptor.FindAction(controllerContext, action);

//            var skipAuthorization = actionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) || actionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
//            if (skipAuthorization)
//            {
//                purviewModel.SkipAuthorize = true;
//            }

//            skipAuthorization = actionDescriptor.IsDefined(typeof(AllowAnyRoleAttribute), true) || actionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnyRoleAttribute), true);
//            if (skipAuthorization)
//            {
//                purviewModel.SkipAuthorize = true;
//            }

//            var filters = new FilterInfo(FilterProviders.Providers.GetFilters(controllerContext, actionDescriptor));
//            foreach (var authorizationFilter in filters.AuthorizationFilters)
//            {
//                if (authorizationFilter.GetType().Name == "AdminAuthorizeAttribute")
//                {
//                    var adminAuthorizeAttributeFilter = (AdminAuthorizeAttribute)authorizationFilter;
//                    if (!string.IsNullOrEmpty(adminAuthorizeAttributeFilter.AuthorizeController))
//                    {
//                        purviewModel.AuthorizeController = adminAuthorizeAttributeFilter.AuthorizeController;
//                    }

//                    if (!string.IsNullOrEmpty(adminAuthorizeAttributeFilter.AuthorizeAction))
//                    {
//                        purviewModel.AuthorizeAction = adminAuthorizeAttributeFilter.AuthorizeAction;
//                    }
//                }
//            }

//            return purviewModel;
//        }
//    }
//}