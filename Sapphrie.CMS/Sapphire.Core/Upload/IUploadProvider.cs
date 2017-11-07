using System.Collections.Specialized;

namespace Sapphire.Core.Upload
{
    /// <summary>
    /// 上传提供者接口。
    /// </summary>
    public interface IUploadProvider
    {
        /// <summary>
        /// 获取上传提供者的键。
        /// </summary>
        /// <returns>上传提供者的键。</returns>
        string GetUploadProviderKey();

        /// <summary>
        /// 解析上传路径规则。
        /// </summary>
        /// <param name="uploadPathRule">上传路径规则。</param>
        /// <param name="file">PowerHttpFile对象实例。</param>
        /// <param name="uploadRuleKeys">替换规则占位符时需要使用到的参数集合。</param>
        /// <returns>解析后的上传路径（已追加上传文件扩展名）。</returns>
        string ResolveUploadPath(string uploadPathRule, PowerHttpFile file, NameValueCollection uploadRuleKeys);
    }
}