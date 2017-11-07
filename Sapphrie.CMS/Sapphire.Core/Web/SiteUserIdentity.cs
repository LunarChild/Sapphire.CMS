using System.ComponentModel.DataAnnotations;

namespace Sapphire.Core.Web
{
    /// <summary>
    /// 站点管理员身份。
    /// </summary>
    public enum SiteUserIdentity
    {
        /// <summary>
        /// 站点超级管理员。
        /// </summary>
        [Display(Name = "站点超级管理员")]
        SupperSiteAdmin = 0,

        /// <summary>
        /// 普通管理员。
        /// </summary>
        [Display(Name = "普通管理员")]
        SiteAdmin = 1,

        /// <summary>
        /// 未认证用户。
        /// </summary>
        [Display(Name = "未认证用户")]
        Unauthorized = -1,
    }
}