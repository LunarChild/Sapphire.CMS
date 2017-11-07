using System.ComponentModel.DataAnnotations;

namespace Sapphire.Core.Config
{
    /// <summary>
    /// 邮箱验证类型。
    /// </summary>
    public enum AuthenticationType
    {
        /// <summary>
        /// 无。
        /// </summary>
        [Display(Name = "无")]
        None = 0,

        /// <summary>
        /// 基本。
        /// </summary>
        [Display(Name = "基本")]
        Basic = 1,

        /// <summary>
        /// NTLM (Windows 身份验证)。
        /// </summary>
        [Display(Name = "NTLM (Windows 身份验证)")]
        Ntlm = 2
    }
}