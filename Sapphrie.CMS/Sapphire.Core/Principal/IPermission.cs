namespace Sapphire.Core.Principal
{
    /// <summary>
    /// 权限。
    /// </summary>
    public interface IPermission
    {
        /// <summary>
        /// 主键Id。
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// 权限集Id。
        /// </summary>
        int PermissionSetId { get; set; }

        /// <summary>
        /// 模块名称。
        /// </summary>
        string Area { get; set; }

        /// <summary>
        /// 控制器名称。
        /// </summary>
        string Controller { get; set; }

        /// <summary>
        /// Action名称或者权限码。
        /// </summary>
        string Purview { get; set; }
    }
}