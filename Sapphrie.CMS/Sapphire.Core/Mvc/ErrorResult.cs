using System.Web.Mvc;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 错误页模板。
    /// </summary>
    public class ErrorResult : ViewResult
    {
        /// <summary>
        /// 创建一个表示错误页的模板。
        /// </summary>
        /// <param name="errorMessage">错误信息。</param>
        public ErrorResult(string errorMessage)
            : this(errorMessage, string.Empty)
        {
        }

        /// <summary>
        /// 通过错误信息、返回地址创建一个表示错误页的模板。
        /// </summary>
        /// <param name="errorMessage">错误信息。</param>
        /// <param name="returnUrl">返回地址。</param>
        public ErrorResult(string errorMessage, string returnUrl)
        {
            this.ErrorMessage = errorMessage;
            this.ViewName = DefaultViewName.ErrorViewName;
            this.ViewBag.ErrorMessage = errorMessage;
            this.ViewBag.ReturnUrl = returnUrl;
        }

        /// <summary>
        /// 错误消息。
        /// </summary>
        public string ErrorMessage { get; private set; }
    }
}