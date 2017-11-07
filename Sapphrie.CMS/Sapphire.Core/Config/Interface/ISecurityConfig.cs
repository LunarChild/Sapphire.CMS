namespace Sapphire.Core.Config
{
    /// <summary>
    /// 网站安全配置接口。
    /// </summary>
    public interface ISecurityConfig
    {
        /// <summary>
        /// 管理目录。
        /// </summary>
        string ManagePath { get; set; }

        /// <summary>
        /// 管理员密码哈希值。
        /// </summary>
        string AdministratorPasswordHashCode { get; set; }

        /// <summary>
        /// 管理员身份验证票过期时间。
        /// </summary>
        int TicketTime { get; set; }

        /// <summary>
        /// 是否启用后台管理认证码。
        /// </summary>
        bool EnableSiteManageCode { get; set; }

        /// <summary>
        /// 管理认证码。
        /// </summary>
        string SiteManageCode { get; set; }

        /// <summary>
        /// 是否启用来访页验证。
        /// </summary>
        bool EnableValidateUrlReferrer { get; set; }

        /// <summary>
        /// 是否使用软键盘输入密码。
        /// </summary>
        bool EnableSoftKeyBoardInput { get; set; }

        /// <summary>
        /// 是否对数据库连接字符串加密。
        /// </summary>
        bool EnableConnectionStringProtect { get; set; }

        /// <summary>
        /// 是否启用小写URL模式。
        /// </summary>
        bool EnableLowerUrl { get; set; }

        /// <summary>
        /// 维护说明。
        /// </summary>
        string ServiceInstraction { get; set; }
    }
}