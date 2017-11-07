namespace Sapphire.Core.Provider
{
    /// <summary>
    /// 信息公开数据提供者接口。
    /// </summary>
    public interface IPublicInformationProvider
    {
        /// <summary>
        /// 获取政府公开信息。
        /// </summary>
        /// <param name="contentId">内容Id。</param>
        /// <returns>政府公开信息。</returns>
        ContentInfo GetPublicInformation(int contentId);
    }
}