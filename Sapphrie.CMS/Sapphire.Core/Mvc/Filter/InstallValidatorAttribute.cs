//using System.Web.Mvc;

//namespace Sapphire.Core.Mvc
//{
//    /// <summary>
//    /// 数据库没有安装时使用的过滤器。
//    /// </summary>
//    public class InstallValidatorAttribute : ActionFilterAttribute
//    {
//        /// <summary>
//        /// 在执行操作方法之前由 ASP.NET MVC 框架调用。
//        /// </summary>
//        /// <param name="filterContext">筛选器上下文。</param>
//        public override void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            if (filterContext.ActionDescriptor.ControllerDescriptor.ControllerName == "Install" && filterContext.HttpContext.Request.RawUrl != "/")
//            {
//                return;
//            }

//            filterContext.Result = new RedirectResult("/Install");
//            base.OnActionExecuting(filterContext);
//        }
//    }
//}
