using System.Collections.Generic;

namespace Sapphire.Core.Principal
{
    /// <summary>
    /// 管理员提供者。
    /// </summary>
    public interface IManagerProvider
    {
        /// <summary>
        /// 根据管理员名称获取管理员信息。
        /// </summary>
        /// <param name="administratorName">管理员名称。</param>
        /// <returns>返回管理员实体。</returns>
        IAdministrator GetAdministratorByAdminName(string administratorName);

        /// <summary>
        /// 根据管理员名称获取角色Id数组。
        /// </summary>
        /// <param name="administratorName">管理员名称。</param>
        /// <returns>角色Id数组。</returns>
        int[] GetRoleIds(string administratorName);

        /// <summary>
        /// 根据管理员名称获取站点权限集Id数组。
        /// </summary>
        /// <param name="administratorName">管理员名称。</param>
        /// <returns>权限集Id数组。</returns>
        int[] GetSitePermissionSetIds(string administratorName);

        /// <summary>
        /// 根据管理员名称获取有权限的站点Id数组。
        /// </summary>
        /// <param name="administratorName">管理员名称。</param>
        /// <returns>角色Id数组。</returns>
        int[] GetSiteIds(string administratorName);

        /// <summary>
        /// 根据模块、控制器、动作得到角色Id数组。
        /// </summary>
        /// <param name="module">模块。</param>
        /// <param name="controller">控制器。</param>
        /// <param name="action">动作。</param>
        /// <returns>角色Id数组。</returns>
        int[] GetRoleIds(string module, string controller, string action);

        /// <summary>
        /// 根据权限集Id获取权限集的所有权限数据。
        /// </summary>
        /// <param name="permissionSetIds">权限集Id数组。</param>
        /// <returns>权限集的所有权限数据。</returns>
        IList<IPermission> GetRolePermissions(int[] permissionSetIds);
    }
}