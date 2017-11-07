using System.Web.Mvc;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 动态元数据设置接口。
    /// </summary>
    public interface IDynamicMetadataSetting
    {
        /// <summary>
        /// 设置模型元数据。
        /// </summary>
        /// <param name="modelMetadata">模型元数据对象。</param>
        void SetModelMetadata(ModelMetadata modelMetadata);
    }
}