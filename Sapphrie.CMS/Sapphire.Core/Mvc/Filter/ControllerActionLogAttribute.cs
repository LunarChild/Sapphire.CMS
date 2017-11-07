//using System;
//using System.Web.Mvc;

//namespace Sapphire.Core.Mvc
//{
//    /// <summary>
//    /// 控制器操作日志过滤器。
//    /// </summary>
//    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
//    public class ControllerActionLogAttribute : ActionLogAttribute
//    {
//        private readonly AcceptVerbsAttribute innerAttribute;

//        /// <summary>
//        /// 通过指定日志标题、操作方法名称初始化 ControllerActionLogAttribute 类的新实例。
//        /// </summary>
//        /// <param name="title">日志标题。</param>
//        /// <param name="actionName">操作方法名称。</param>
//        public ControllerActionLogAttribute(string title, string actionName)
//            : this(title, actionName, HttpVerbs.Delete | HttpVerbs.Get | HttpVerbs.Head | HttpVerbs.Options | HttpVerbs.Patch | HttpVerbs.Post | HttpVerbs.Put)
//        {
//            this.ActionName = actionName;
//        }

//        /// <summary>
//        /// 通过指定日志标题、操作方法名称初始化 ControllerActionLogAttribute 类的新实例。
//        /// </summary>
//        /// <param name="title">日志标题。</param>
//        /// <param name="actionName">操作方法名称。</param>
//        /// <param name="httpVerbs">Http谓词。</param>
//        public ControllerActionLogAttribute(string title, string actionName, HttpVerbs httpVerbs)
//            : base(title)
//        {
//            this.ActionName = actionName;
//            this.innerAttribute = new AcceptVerbsAttribute(httpVerbs);
//        }

//        /// <summary>
//        /// 方法名称。
//        /// </summary>
//        public string ActionName { get; private set; }

//        /// <summary>
//        /// 验证当前Action是否有效。
//        /// </summary>
//        /// <param name="filterContext">筛选器上下文。</param>
//        /// <returns>有效返回True，否则返回False。</returns>
//        protected override bool OnActionValidating(ActionExecutingContext filterContext)
//        {
//            return filterContext.ActionDescriptor.ActionName.Equals(this.ActionName, StringComparison.CurrentCultureIgnoreCase) && this.innerAttribute.IsValidForRequest(filterContext, null);
//        }
//    }
//}