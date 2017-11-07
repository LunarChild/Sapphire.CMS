namespace Sapphire.Core.Provider
{
    /// <summary>
    /// 内容管理数据提供者接口。
    /// </summary>
    public interface IContentManageProvider
    {
        /// <summary>
        /// 获取文章信息。
        /// </summary>
        /// <param name="contentId">内容Id。</param>
        /// <returns>文章信息。</returns>
        ContentInfo GetArticle(int contentId);

        /// <summary>
        /// 获取图片信息。
        /// </summary>
        /// <param name="contentId">内容Id。</param>
        /// <returns>图片信息。</returns>
        ContentInfo GetPhoto(int contentId);

        /// <summary>
        /// 获取视频信息。
        /// </summary>
        /// <param name="contentId">内容Id。</param>
        /// <returns>视频信息。</returns>
        ContentInfo GetVideo(int contentId);

        /// <summary>
        /// 获取站点名称。
        /// </summary>
        /// <param name="siteId">站点Id。</param>
        /// <returns>站点名称。</returns>
        string GetSiteName(int siteId);
    }
}