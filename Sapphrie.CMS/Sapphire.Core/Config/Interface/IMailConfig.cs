namespace Sapphire.Core.Config
{
    /// <summary>
    /// 邮箱配置信息接口。
    /// </summary>
    public interface IMailConfig
    {
        /// <summary>
        /// 是否Ssl加密。
        /// </summary>
        bool EnabledSsl { get; set; }

        /// <summary>
        /// 发件人信箱。
        /// </summary>
        string MailFrom { get; set; }

        /// <summary>
        /// 端口。
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// 验证类型。
        /// </summary>
        AuthenticationType AuthenticationType { get; set; }

        /// <summary>
        /// SMTP服务器地址。
        /// </summary>
        string MailServer { get; set; }

        /// <summary>
        /// SMTP登录用户名。
        /// </summary>
        string MailServerUserName { get; set; }

        /// <summary>
        /// SMTP登录密码。
        /// </summary>
        string MailServerPassWord { get; set; }
    }
}