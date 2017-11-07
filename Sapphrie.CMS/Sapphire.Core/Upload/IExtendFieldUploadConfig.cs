namespace Sapphire.Core.Upload
{
    /// <summary>
    /// 扩展字段上传配置接口
    /// </summary>
    public interface IExtendFieldUploadConfig
    {
        /// <summary>
        /// 获取扩展字段上传配置
        /// </summary>
        /// <param name="fieldId">扩展字段Id。</param>
        /// <returns>扩展字段上传配置。</returns>
        IUploadConfig GetUploadConfig(int fieldId);
    }
}
