namespace Sapphire.Core.Config
{
    /// <summary>
    /// 网站会员配置接口。
    /// </summary>
    public interface IUserConfig
    {
        /// <summary>
        /// 允许会员注册。
        /// </summary>
        bool EnableRegister { get; set; }

        /// <summary>
        /// 注册服务条款和声明。
        /// </summary>
        string Protocol { get; set; }

        /// <summary>
        /// 会员名最少字符数。
        /// </summary>
        int UserNameLimit { get; set; }

        /// <summary>
        /// 会员名最多字符数。
        /// </summary>
        int UserNameMax { get; set; }

        /// <summary>
        /// 禁止注册的会员名。
        /// </summary>
        string UserNameRegisterDisabled { get; set; }

        /// <summary>
        /// 注册需要邮件验证。
        /// </summary>
        bool EmailCheckRegister { get; set; }

        /// <summary>
        /// 注册时验证邮件内容。
        /// </summary>
        string EmailOfRegisterCheck { get; set; }

        /// <summary>
        /// 注册成功后所属会员组。
        /// </summary>
        string UserGroup { get; set; }

        /// <summary>
        /// 发送注册信息到会员邮箱。
        /// </summary>
        bool SendMail { get; set; }

        /// <summary>
        /// 注册信息邮件内容。
        /// </summary>
        string MailContent { get; set; }

        /// <summary>
        /// 登录时启用验证码功能。
        /// </summary>
        bool EnableCheckCodeOfLogin { get; set; }
    }
}