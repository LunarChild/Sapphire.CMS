using System.Web.Mvc;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 成功页模板。
    /// </summary>
    public class SuccessResult : ViewResult
    {
        /// <summary>
        /// 创建一个表示成功页的模板。
        /// </summary>
        /// <param name="successMessage">成功信息。</param>
        public SuccessResult(string successMessage)
            : this(successMessage, string.Empty)
        {
        }

        /// <summary>
        /// 通过成功信息、返回地址创建一个表示成功页的模板。
        /// </summary>
        /// <param name="successMessage">成功信息。</param>
        /// <param name="returnUrl">返回地址。</param>
        public SuccessResult(string successMessage, string returnUrl)
        {
            this.SuccessMessage = successMessage;
            this.ViewName = DefaultViewName.SuccessViewName;
            this.ViewBag.SuccessMessage = successMessage;
            this.ViewBag.ReturnUrl = returnUrl;
        }

        /// <summary>
        /// 成功消息。
        /// </summary>
        public string SuccessMessage { get; private set; }
    }
}