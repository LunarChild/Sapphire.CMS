namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 权限验证实体。
    /// </summary>
    public class AuthorizePurviewModel
    {
        /// <summary>
        /// 动作。
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 控制器。
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 模块名。
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 跳过验证。
        /// </summary>
        public bool SkipAuthorize { get; set; }

        /// <summary>
        /// 用于验证的模块。
        /// </summary>
        public string AuthorizeArea { get; set; }

        /// <summary>
        /// 用于验证的控制器。
        /// </summary>
        public string AuthorizeController { get; set; }

        /// <summary>
        /// 用于验证的Action。
        /// </summary>
        public string AuthorizeAction { get; set; }
    }
}