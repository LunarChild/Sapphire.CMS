namespace Sapphire.Core.Config
{
    /// <summary>
    /// 邮件配置。
    /// </summary>
    public static class MailConfig
    {
        /// <summary>
        /// 邮件配置实例。
        /// </summary>
        public static IMailConfig Instance
        {
            get
            {
                return GlobalConfigProvider.Current.GetMailConfig();
            }
        }
    }
}