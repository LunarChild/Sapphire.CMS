namespace Sapphire.Core.Provider
{
    /// <summary>
    /// 全局数据提供者。
    /// </summary>
    public static class GlobalProvider
    {
        /// <summary>
        /// 内容管理数据提供者。
        /// </summary>
        public static IContentManageProvider ContentManage { get; set; }

        /// <summary>
        /// 信息公开数据提供者。
        /// </summary>
        public static IPublicInformationProvider PublicInformation { get; set; }
    }
}