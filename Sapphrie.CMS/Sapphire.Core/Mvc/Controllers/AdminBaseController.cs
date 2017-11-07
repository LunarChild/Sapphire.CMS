using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using Sapphire.Core.CommonHelper;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 后台控制器基类。
    /// </summary>
    //[ValidateUrlReferrer]
    //[AdminAuthenticate]
    //[AdminAuthorize]
    public class AdminBaseController : Controller
    {
        /// <summary>
        /// 构造后台控制器基类。
        /// </summary>
        protected AdminBaseController()
        {
            this.ViewData["InitData"] = new Dictionary<string, object>();
        }

        /// <summary>
        /// 使用操作名称重定向到后台指定的操作。
        /// </summary>
        /// <param name="actionName">操作的名称。</param>
        /// <returns>重定向结果对象。</returns>
        protected internal RedirectResult RedirectToAdminAction(string actionName)
        {
            return this.RedirectToAdminAction(actionName, null, null);
        }

        /// <summary>
        /// 使用操作名称和路由值重定向到后台指定的操作。
        /// </summary>
        /// <param name="actionName">操作的名称。</param>
        /// <param name="routeValues">路由的参数。</param>
        /// <returns>重定向结果对象。</returns>
        protected internal RedirectResult RedirectToAdminAction(string actionName, object routeValues)
        {
            return this.RedirectToAdminAction(actionName, null, routeValues);
        }

        /// <summary>
        /// 使用操作名称和控制器名称重定向到后台指定的操作。
        /// </summary>
        /// <param name="actionName">操作的名称。</param>
        /// <param name="controllerName">控制器的名称。</param>
        /// <returns>重定向结果对象。</returns>
        protected internal RedirectResult RedirectToAdminAction(string actionName, string controllerName)
        {
            return this.RedirectToAdminAction(actionName, controllerName, null);
        }

        /// <summary>
        /// 使用操作名称、控制器名称和路由字典重定向到后台指定的操作。
        /// </summary>
        /// <param name="actionName">操作的名称。</param>
        /// <param name="controllerName">控制器的名称。</param>
        /// <param name="routeValues">路由的参数。</param>
        /// <returns>重定向结果对象。</returns>
        protected internal RedirectResult RedirectToAdminAction(string actionName, string controllerName, object routeValues)
        {
            return this.Redirect(this.Url.AdminAction(actionName, controllerName, routeValues));
        }

        /// <summary>
        /// 使用操作名称重定向到后台指定的操作。
        /// </summary>
        /// <param name="actionName">操作的名称。</param>
        /// <returns>重定向结果对象。</returns>
        protected internal RedirectResult RedirectToAdminHomeAction(string actionName)
        {
            return this.Redirect(this.Url.AdminHomeAction(actionName));
        }

        /// <summary>
        /// 获取类型标注的显示名称。
        /// </summary>
        /// <param name="type">类型。</param>
        /// <returns>类型标注的显示名称。</returns>
        protected string GetModelDisplayName(Type type)
        {
            var metadata = ModelMetadataProviders.Current.GetMetadataForType(null, type);
            return metadata == null ? string.Empty : metadata.DisplayName;
        }

        /// <summary>
        /// 创建一个表示错误页的ErrorResult实例。
        /// </summary>
        /// <param name="errorMessage">错误信息。</param>
        /// <returns>返回包含错误信息的错误页。</returns>
        protected ErrorResult Error(string errorMessage)
        {
            return this.Error(errorMessage, string.Empty);
        }

        /// <summary>
        /// 创建一个表示错误页的ErrorResult实例。
        /// </summary>
        /// <param name="errorMessage">错误信息。</param>
        /// <param name="returnUrl">返回地址。</param>
        /// <returns>返回包含错误信息和返回地址的错误页。</returns>
        protected ErrorResult Error(string errorMessage, string returnUrl)
        {
            return new ErrorResult(errorMessage, returnUrl);
        }

        ///// <summary>
        ///// 验证失败结果。
        ///// </summary>
        ///// <returns>返回验证失败结果。</returns>
        //protected ActionResult ValidationErrorResult()
        //{
        //    var htmlSummary = new StringBuilder();
        //    foreach (var modelState in this.ModelState.Values)
        //    {
        //        foreach (var modelError in modelState.Errors)
        //        {
        //            var errorText = modelError.ErrorMessage;
        //            if (!string.IsNullOrEmpty(errorText))
        //            {
        //                htmlSummary.AppendLine(errorText.Replace(Environment.NewLine, string.Empty));
        //            }

        //            if (modelError.Exception != null)
        //            {
        //                htmlSummary.AppendLine(modelError.Exception.Message.Replace(Environment.NewLine, string.Empty));
        //            }
        //        }
        //    }

        //    if (this.Request.IsAjaxRequest())
        //    {
        //        return this.ErrorMessage(Resources.AdminController_ValidationErrorResult_ErrorMessage, htmlSummary.ToString());
        //    }

        //    return this.Content(htmlSummary.ToString());
        //}

        ///// <summary>
        ///// 成功消息展示。
        ///// </summary>
        ///// <param name="message">操作成功的消息。</param>
        ///// <param name="url">假如提供了url参数，则操作成功之后要跳转到的url，否则不跳转。</param>
        ///// <returns>返回成功的消息。</returns>
        //protected ActionResult SuccessMessage(string message, string url = "")
        //{
        //    return new MessageResult(MessageType.Success, message, string.Empty, url);
        //}

        ///// <summary>
        ///// 失败消息展示。
        ///// </summary>
        ///// <param name="message">操作失败的简单消息。</param>
        ///// <param name="detailsMessage">操作发生错误的时，用户自定义的详细信息。</param>
        ///// <param name="url">假如提供了url参数，则操作失败之后要跳转到的url，否则不跳转。</param>
        ///// <returns>返回失败的消息。</returns>
        //protected ActionResult ErrorMessage(string message, string detailsMessage = "", string url = "")
        //{
        //    return new MessageResult(MessageType.Error, message, detailsMessage, url);
        //}

        ///// <summary>
        ///// 异常消息展示。
        ///// </summary>
        ///// <param name="exception">异常对象。</param>
        ///// <param name="message">假如提供了errorMessage参数，则操作失败的时候会显示该参数，否则系统默认提供简单异常信息。</param>
        ///// <returns>返回失败的消息。</returns>
        //protected ActionResult ErrorMessage(Exception exception, string message = "")
        //{
        //    Check.NotNull(exception, "exception");

        //    return new MessageResult(MessageType.Error, message, exception.GetBaseException().Message.Replace(Environment.NewLine, string.Empty));
        //}

        ///// <summary>
        ///// 警告消息展示。
        ///// </summary>
        ///// <param name="message">警告操作的简单消息。</param>
        ///// <param name="detailsMessage">警告操作的详细消息。</param>
        ///// <param name="url">操作失败之后要跳转到的url。</param>
        ///// <returns>返回警告的消息。</returns>
        //protected ActionResult WarningMessage(string message, string detailsMessage = "", string url = "")
        //{
        //    return new MessageResult(MessageType.Warning, message, detailsMessage, url);
        //}

        ///// <summary>
        ///// 消息展示。
        ///// </summary>
        ///// <param name="isSuccess">操作是否成功。</param>
        ///// <param name="successMessage">成功的消息。</param>
        ///// <param name="errorMessage">错误的消息。</param>
        ///// <param name="url">要跳转到的url。</param>
        ///// <param name="errorRedirect">控制出错是否要跳转。</param>
        ///// <returns>成功或是失败的消息。</returns>
        //protected MessageResult Message(bool isSuccess, string successMessage, string errorMessage, string url = "", bool errorRedirect = false)
        //{
        //    if (isSuccess)
        //    {
        //        return new MessageResult(MessageType.Success, successMessage, string.Empty, url);
        //    }

        //    if (errorRedirect)
        //    {
        //        return new MessageResult(MessageType.Error, errorMessage, string.Empty, url);
        //    }

        //    return new MessageResult(MessageType.Error, errorMessage);
        //}

        ///// <summary>
        ///// 消息展示。
        ///// </summary>
        ///// <param name="isSuccess">操作是否成功。</param>
        ///// <param name="successMessage">成功消息。</param>
        ///// <param name="errorMessage">失败的消息。</param>
        ///// <param name="successUrl">操作成功转向的url。</param>
        ///// <param name="errorUrl">操作失败转向的url。</param>
        ///// <returns>成功或是失败的消息。</returns>
        //protected MessageResult Message(bool isSuccess, string successMessage, string errorMessage, string successUrl, string errorUrl)
        //{
        //    if (isSuccess)
        //    {
        //        return new MessageResult(MessageType.Success, successMessage, string.Empty, successUrl);
        //    }

        //    return new MessageResult(MessageType.Error, errorMessage, string.Empty, errorUrl);
        //}

        /// <summary>
        /// 在调用操作方法后调用。
        /// </summary>
        /// <param name="filterContext">有关当前请求和操作的信息。</param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(DataChangedAttribute), true).Any())
            {
                this.OnDataChangedActionExecuted(filterContext);
            }
        }

        /// <summary>
        /// 当执行数据改变操作后触发的事件。
        /// </summary>
        /// <param name="filterContext">操作上下文。</param>
        protected virtual void OnDataChangedActionExecuted(ActionExecutedContext filterContext)
        {
        }

        /// <summary>
        /// 调用JsonNetResult对输入的Object进行json格式的转化。
        /// </summary>
        /// <param name="data">一个需要序列化的对象。</param>
        /// <returns>返回Json格式的数据。</returns>
        protected JsonNetResult JsonNet(object data)
        {
            var jsonNetResult = new JsonNetResult { Data = data };
            return jsonNetResult;
        }

        /// <summary>
        /// 调用JsonNetResult对输入的Object进行json格式的转化。
        /// </summary>
        /// <param name="data">一个需要序列化的对象。</param>
        /// <param name="settings">设置返回Json数据格式的参数。</param>
        /// <returns>返回Json格式的数据。</returns>
        protected JsonNetResult JsonNet(object data, JsonSerializerSettings settings)
        {
            var jsonNetResult = new JsonNetResult { Data = data, Settings = settings };
            return jsonNetResult;
        }
    }
}