using System;

namespace Sapphire.Core.Principal
{
    /// <summary>
    /// 管理员信息接口。
    /// </summary>
    public interface IAdministrator
    {
        /// <summary>
        /// 管理员Id。
        /// </summary>
        int AdministratorId { get; set; }

        /// <summary>
        /// 管理员名。
        /// </summary>
        string AdministratorName { get; set; }

        /// <summary>
        /// 管理员密码。
        /// </summary>
        string AdministratorPassword { get; set; }

        /// <summary>
        /// 是否允许多人登录。
        /// </summary>
        bool EnableMultiLogin { get; set; }

        /// <summary>
        /// 随机密码。
        /// </summary>
        string RandomPassword { get; set; }

        /// <summary>
        /// 登录次数。
        /// </summary>
        int LoginTimes { get; set; }

        /// <summary>
        /// 最近登录IP。
        /// </summary>
        string LastLoginIP { get; set; }

        /// <summary>
        /// 最近登录时间。
        /// </summary>
        DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 上次退出时间。
        /// </summary>
        DateTime? LastLogoutTime { get; set; }

        /// <summary>
        /// 上次修改密码的时间。
        /// </summary>
        DateTime? LastModifyPasswordTime { get; set; }

        /// <summary>
        /// 是否锁定。
        /// </summary>
        bool Locked { get; set; }

        /// <summary>
        /// 是否允许自己修改密码。
        /// true是可以修改。
        /// </summary>
        bool EnablePasswordReset { get; set; }
    }
}