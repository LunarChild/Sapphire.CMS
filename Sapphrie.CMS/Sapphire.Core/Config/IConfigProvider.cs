using Sapphire.Core.Upload;

namespace Sapphire.Core.Config
{
    /// <summary>
    /// 全局配置提供者接口。
    /// </summary>
    public interface IConfigProvider
    {
        /// <summary>
        /// 获取网站安全配置。
        /// </summary>
        /// <returns>网站安全配置。</returns>
        ISecurityConfig GetSecurityConfig();

        /// <summary>
        /// 获取网站信息配置。
        /// </summary>
        /// <returns>网站信息配置。</returns>
        ISiteConfig GetSiteConfig();

        /// <summary>
        /// 获取异常配置信息。
        /// </summary>
        /// <returns>异常配置信息。</returns>
        IExceptionConfig GetExceptionConfig();

        /// <summary>
        /// 获取邮件配置信息。
        /// </summary>
        /// <returns>邮件配置信息。</returns>
        IMailConfig GetMailConfig();

        /// <summary>
        /// 获取网站用户配置。
        /// </summary>
        /// <returns>网站用户配置。</returns>
        IUserConfig GetUserConfig();

        /// <summary>
        /// 获取全局上传配置。
        /// </summary>
        /// <returns>全局上传配置。</returns>
        IGlobalUploadConfig GetGlobalUploadConfig();

        /// <summary>
        /// 获取水印配置。
        /// </summary>
        /// <returns>水印配置。</returns>
        IWatermarkConfig GetWatermarkConfig();
        /// <summary>
        /// 获取水印配置。
        /// </summary>
        /// <returns>水印配置。</returns>
        IThumbnailConfig GetThumbnailConfig();
    }
}