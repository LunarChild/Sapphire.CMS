//using System;
//using System.Web.Mvc;
//using System.Web.Mvc.Filters;
//using Sapphire.Core.Principal;
//using Sapphire.Core.Properties;
//using Sapphire.Core.Utilities;
//using Sapphire.Core.Web;

//namespace Sapphire.Core.Mvc
//{
//    /// <summary>
//    /// 管理员身份验证过滤器。
//    /// </summary>
//    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
//    public sealed class AdminAuthenticateAttribute : FilterAttribute, IAuthenticationFilter
//    {
//        /// <summary>
//        /// 后台管理员认证。
//        /// </summary>
//        /// <param name="filterContext">认证上下文实体对象。</param>
//        public void OnAuthentication(AuthenticationContext filterContext)
//        {
//            Check.NotNull(filterContext, "filterContext");

//            var context = filterContext.HttpContext.ApplicationInstance.Context;
//            var adminCookieName = ManageCookies.AdminCookieName;
//            var adminAuthenticationTicket = ManageCookies.ExtractTicketFromCookie(context, adminCookieName);
//            if (adminAuthenticationTicket != null)
//            {
//                ManageCookies.SlidingExpiration(context, adminAuthenticationTicket, adminCookieName);
//                var adminPrincipal = AdminPrincipal.CreatePrincipal(adminAuthenticationTicket);
//                if (adminPrincipal.Identity.IsAuthenticated)
//                {
//                    adminPrincipal.AdministratorInfo = Manager.GetAdministratorByAdminName(adminPrincipal.AdministratorName);
//                    SiteContext.Current.Admin = adminPrincipal;
//                    SiteContext.Current.Admin.Roles = Manager.GetRoleIds(adminPrincipal.AdministratorName);
//                    SiteContext.Current.Admin.SitePermissionSets = Manager.GetSitePermissionSetIds(adminPrincipal.AdministratorName);
//                }
//            }

//            var skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
//                                    filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
//            if (skipAuthorization)
//            {
//                return;
//            }

//            if (!this.AuthenticateCore(filterContext))
//            {
//                this.HandleUnauthenticatedRequest(filterContext);
//            }
//        }

//        /// <summary>
//        /// 管理员认证通道。
//        /// </summary>
//        /// <param name="filterContext">认证通道上下文实体对象。</param>
//        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
//        {
//        }

//        /// <summary>
//        /// 认证核心。
//        /// </summary>
//        /// <param name="filterContext">认证上下文实体对象。</param>
//        /// <returns>是否认证。</returns>
//        private bool AuthenticateCore(AuthenticationContext filterContext)
//        {
//            var principal = SiteContext.Current.Admin;
//            if (principal == null || !principal.Identity.IsAuthenticated)
//            {
//                return false;
//            }

//            if (principal.AdministratorInfo == null)
//            {
//                return false;
//            }

//            // 检测访问过程中管理员密码是否已被修改，防止同一台机器中不同的站点的验证票被共用
//            if (principal.AdministratorPassword != principal.AdministratorInfo.AdministratorPassword)
//            {
//                return false;
//            }

//            // 管理员是否被锁定
//            if (principal.AdministratorInfo.Locked)
//            {
//                return false;
//            }

//            // 是否允许多人同时登录
//            if (!principal.AdministratorInfo.EnableMultiLogin)
//            {
//                if (principal.AdministratorInfo.RandomPassword != principal.RandomPassword)
//                {
//                    var returnUrl = UrlHelperExtension.GetAdminLoginUrl();
//                    filterContext.Result = new ErrorResult("此管理员不允许多人同时登录，请重新登录。", returnUrl);
//                    return false;
//                }
//            }

//            return true;
//        }

//        /// <summary>
//        /// 处理未认证请求。
//        /// </summary>
//        /// <param name="filterContext">
//        /// 认证上下文实体对象。
//        /// </param>
//        private void HandleUnauthenticatedRequest(AuthenticationContext filterContext)
//        {
//            Check.NotNull(filterContext, "filterContext");
//            //UNDONE 出现认证失败的时候把HttpContext标头设置成不缓存，解决火狐下会在本地缓存一条301跳转错误的缓存记录，导致之后跳转错误的问题。
//            filterContext.HttpContext.Response.Cache.SetNoStore();
//            if (filterContext.HttpContext.Request.IsAjaxRequest())
//            {
//                filterContext.HttpContext.Response.StatusCode = 403;
//                filterContext.Result = filterContext.Result ??
//                                       new MessageResult(
//                                           MessageType.Error,
//                                           Resources.AdminAuthenticateAttribute_HandleUnauthorizedRequest_Timeout,
//                                           string.Empty,
//                                           UrlHelperExtension.GetAdminLoginUrl());
//            }
//            else
//            {
//                filterContext.Result = filterContext.Result ?? new RedirectResult(UrlHelperExtension.GetAdminLoginUrl(), true);
//            }
//        }
//    }
//}