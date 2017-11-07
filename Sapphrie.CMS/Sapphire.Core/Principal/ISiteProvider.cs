using System.Collections.Generic;
using Sapphire.Core.Provider;
using Sapphire.Core.Web;

namespace Sapphire.Core.Principal
{
    /// <summary>
    /// 站点管理员提供者。
    /// </summary>
    public interface ISiteProvider
    {
        /// <summary>
        /// 验证当前管理员是否有当前站点的权限。
        /// </summary>
        /// <returns>管理员状态。</returns>
        SiteUserIdentity SiteAuthorize();

        /// <summary>
        /// 根据管理员ID验证管理员是否有指定站点的管理权限。
        /// </summary>
        /// <param name="administratorId">管理员Id。</param>
        /// <param name="adminGroupIds">管理员组Id数组。</param>
        /// <param name="siteId">站点ID。</param>
        /// <returns>验证状态。</returns>
        bool SiteAuthorize(int administratorId, int[] adminGroupIds, int siteId);

        /// <summary>
        /// 获取当前站点标识符。
        /// </summary>
        /// <param name="siteId">站点Id。</param>
        /// <returns>站点标识符。</returns>
        string GetSiteIdentifier(int siteId);

        /// <summary>
        /// 根据模块、控制器、动作得到权限集Id数组。
        /// </summary>
        /// <param name="module">模块。</param>
        /// <param name="controller">控制器。</param>
        /// <param name="action">动作。</param>
        /// <returns>权限集Id数组。</returns>
        int[] GetSitePermissionSetIds(string module, string controller, string action);

        /// <summary>
        /// 根据角色Id获取角色的所有权限数据。
        /// </summary>
        /// <param name="roleIds">角色Id数组。</param>
        /// <returns>角色的所有权限数据。</returns>
        IList<IPermission> GetRolePermissions(int[] roleIds);

        /// <summary>
        /// 获得角色拥有的管理权限的站点信息。
        /// </summary>
        /// <returns>站点信息集合。</returns>
        IList<SiteInfo> GetSiteInfos();

        /// <summary>
        /// 判断当前站点是否在设置的站点规则范围内。
        /// </summary>
        /// <param name="includeSiteIds">包含的站点Id</param>
        /// <param name="excludeSiteIds">排除的站点Id</param>
        /// <returns>验证结果</returns>
        bool SiteEnable(string includeSiteIds, string excludeSiteIds);
    }
}