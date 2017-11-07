using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 注入模型元数据。
    /// </summary>
    public interface IInjectModelMetadata
    {
        /// <summary>
        /// 获取需要注入的模型元数据列表。
        /// </summary>
        /// <param name="containerType">容器的类型。</param>
        /// <returns>模型元数据列表。</returns>
        List<ModelMetadata> InjectModelMetadatas(Type containerType);
    }
}