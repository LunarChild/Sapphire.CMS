using System.Collections.Generic;
using Sapphire.Core.Principal;

namespace Sapphire.Core.Web
{
    /// <summary>
    /// 管理员操作类。
    /// </summary>
    public static class Manager
    {
        /// <summary>
        /// 根据管理员名获取管理员信息。
        /// </summary>
        /// <param name="administratorName">管理员名。</param>
        /// <returns>管理员信息。</returns>
        public static IAdministrator GetAdministratorByAdminName(string administratorName)
        {
            return GlobalManagerProvider.Current.GetAdministratorByAdminName(administratorName);
        }

        /// <summary>
        /// 根据管理员名称获取角色Id数组。
        /// </summary>
        /// <param name="administratorName">管理员名。</param>
        /// <returns>角色Id数组。</returns>
        public static int[] GetRoleIds(string administratorName)
        {
            return GlobalManagerProvider.Current.GetRoleIds(administratorName);
        }

        /// <summary>
        /// 根据管理员名称获取站点权限集Id数组。
        /// </summary>
        /// <param name="administratorName">管理员名。</param>
        /// <returns>权限集Id数组。</returns>
        public static int[] GetSitePermissionSetIds(string administratorName)
        {
            return GlobalManagerProvider.Current.GetSitePermissionSetIds(administratorName);
        }

        /// <summary>
        /// 根据管理员名称获取有权限的站点Id数组。
        /// </summary>
        /// <param name="administratorName">管理员名。</param>
        /// <returns>角色Id数组。</returns>
        public static int[] GetSiteIds(string administratorName)
        {
            return GlobalManagerProvider.Current.GetSiteIds(administratorName);
        }

        //Issue [08-20]  GetRoleIds 的方法名要修改的更贴合功能名。
        /// <summary>
        /// 根据模块、控制器、动作得到角色Id数组。
        /// </summary>
        /// <param name="module">模块。</param>
        /// <param name="controller">控制器。</param>
        /// <param name="action">动作。</param>
        /// <returns>角色Id数组。</returns>
        public static int[] GetRoleIds(string module, string controller, string action)
        {
            return GlobalManagerProvider.Current.GetRoleIds(module, controller, action);
        }

        /// <summary>
        /// 根据权限集Id获取权限集的权限列表。
        /// </summary>
        /// <param name="permissionSetIds">权限集Id数组。</param>
        /// <returns>权限列表。</returns>
        public static IList<IPermission> GetRolePermission(int[] permissionSetIds)
        {
            return GlobalManagerProvider.Current.GetRolePermissions(permissionSetIds);
        }
    }
}