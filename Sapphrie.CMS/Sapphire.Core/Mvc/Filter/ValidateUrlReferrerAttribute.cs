//using System;
//using System.Web;
//using System.Web.Mvc;
//using Sapphire.Core.Config;
//using Sapphire.Core.Logging;
//using Sapphire.Core.Properties;
//using Sapphire.Core.Utilities;

//namespace Sapphire.Core.Mvc
//{
//    /// <summary>
//    /// 验证来访页过滤器。
//    /// </summary>
//    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
//    public sealed class ValidateUrlReferrerAttribute : FilterAttribute, IAuthorizationFilter
//    {
//        /// <summary>
//        /// 授权时进行调用。
//        /// </summary>
//        /// <param name="filterContext">授权上下文。</param>
//        public void OnAuthorization(AuthorizationContext filterContext)
//        {
//            Check.NotNull(filterContext, "filterContext");

//            if (!SecurityConfig.Instance.EnableValidateUrlReferrer)
//            {
//                return;
//            }

//            var skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnyUrlReferrerAttribute), true) ||
//                                    filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnyUrlReferrerAttribute), true);
//            if (!skipAuthorization && !AuthorizeCore(filterContext.HttpContext))
//            {
//                HandleUnauthorizedRequest(filterContext);
//                Logger.AddLog(LogType.Security, "授权验证", string.Format("授权验证未通过,{0}", Resources.ValidateUrlReferrerAttribute_HandleUnauthorizedRequest_ErrorMessage));
//            }
//            else
//            {
//                Logger.AddLog(LogType.Security, "授权验证", "授权验证通过。");
//            }
//        }

//        private static bool AuthorizeCore(HttpContextBase httpContext)
//        {
//            var request = httpContext.ApplicationInstance.Context.Request;
//            if (request.UrlReferrer == null || request.UrlReferrer.Host.Length <= 0)
//            {
//                return false;
//            }

//            return string.Equals(request.Url.Host, request.UrlReferrer.Host, StringComparison.CurrentCultureIgnoreCase);
//        }

//        private static void HandleUnauthorizedRequest(AuthorizationContext filterContext)
//        {
//            filterContext.Result = new ErrorResult(Resources.ValidateUrlReferrerAttribute_HandleUnauthorizedRequest_ErrorMessage);
//        }
//    }
//}