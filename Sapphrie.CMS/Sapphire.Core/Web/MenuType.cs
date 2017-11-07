using System.ComponentModel.DataAnnotations;

namespace Sapphire.Core.Web
{
    /// <summary>
    /// SiteMap菜单类型。
    /// </summary>
    public enum MenuType
    {
        /// <summary>
        /// 平台。
        /// </summary>
        [Display(Name = "平台")]
        Platform = -1,

        /// <summary>
        /// 全站。
        /// </summary>
        [Display(Name = "全站")]
        AllSite = 1,

        /// <summary>
        /// 主站。
        /// </summary>
        [Display(Name = "主站")]
        MainSite = 0,
    }
}