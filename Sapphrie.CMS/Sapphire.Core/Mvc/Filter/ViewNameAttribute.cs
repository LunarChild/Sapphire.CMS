using System;
using System.Web.Mvc;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 指定模板名称过滤器。
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class ViewNameAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="viewName">模板名称。</param>
        public ViewNameAttribute(string viewName)
        {
            this.ViewName = viewName;
        }

        /// <summary>
        /// 模板名称。
        /// </summary>
        public string ViewName { get; private set; }

        /// <summary>
        /// 在执行操作方法后由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <param name="filterContext">筛选器上下文。</param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var result = filterContext.Result as ViewResultBase;
            if (result != null && string.IsNullOrEmpty(result.ViewName))
            {
                result.ViewName = this.ViewName;
            }
        }
    }
}