using System.Collections.Generic;
using Sapphire.Core.Principal;
using Sapphire.Core.Provider;

namespace Sapphire.Core.Web
{
    /// <summary>
    /// 站点操作类。
    /// </summary>
    public static class SiteManager
    {
        /// <summary>
        /// 验证当前管理员是否有当前站点的权限。
        /// </summary>
        /// <returns>管理员状态。</returns>
        public static SiteUserIdentity SiteAuthorize()
        {
            return GlobalSiteProvider.Current.SiteAuthorize();
        }

        /// <summary>
        /// 根据管理员ID验证管理员是否有指定站点的管理权限。
        /// </summary>
        /// <param name="administratorId">管理员Id。</param>
        /// <param name="adminGroupIds">管理员组Id数组。</param>
        /// <param name="siteId">站点ID。</param>
        /// <returns>验证状态。</returns>
        public static bool SiteAuthorize(int administratorId, int[] adminGroupIds, int siteId)
        {
            return GlobalSiteProvider.Current.SiteAuthorize(administratorId, adminGroupIds, siteId);
        }

        /// <summary>
        /// 获取当前站点标识符。
        /// </summary>
        /// <param name="siteId">站点Id。</param>
        /// <returns>站点标识符。</returns>
        public static string GetSiteIdentifier(int siteId)
        {
            return GlobalSiteProvider.Current.GetSiteIdentifier(siteId);
        }

        /// <summary>
        /// 根据模块、控制器、动作得到权限集Id数组。
        /// </summary>
        /// <param name="module">模块。</param>
        /// <param name="controller">控制器。</param>
        /// <param name="action">动作。</param>
        /// <returns>权限集Id数组。</returns>
        public static int[] GetSitePermissionSetIds(string module, string controller, string action)
        {
            return GlobalSiteProvider.Current.GetSitePermissionSetIds(module, controller, action);
        }

        /// <summary>
        /// 根据角色Id获取角色的权限列表。
        /// </summary>
        /// <param name="roleIdArray">角色Id数组。</param>
        /// <returns>权限列表。</returns>
        public static IList<IPermission> GetRolePermission(int[] roleIdArray)
        {
            return GlobalSiteProvider.Current.GetRolePermissions(roleIdArray);
        }

        /// <summary>
        /// 获得角色拥有的管理权限的站点信息。
        /// </summary>
        /// <returns>站点信息集合。</returns>
        public static IList<SiteInfo> GetSiteInfos()
        {
            return GlobalSiteProvider.Current.GetSiteInfos();
        }

        /// <summary>
        /// 判断当前站点是否在设置的站点规则范围内。
        /// </summary>
        /// <param name="includeSiteIds">包含的站点Id</param>
        /// <param name="excludeSiteIds">排除的站点Id</param>
        /// <returns>验证结果</returns>
        public static bool SiteEnable(string includeSiteIds, string excludeSiteIds)
        {
            return GlobalSiteProvider.Current.SiteEnable(includeSiteIds, excludeSiteIds);
        }
    }
}