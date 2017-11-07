using System.Collections.Generic;
using System.Web.Mvc;

namespace Sapphire.Core.Provider
{
    /// <summary>
    /// 数据中心分类提供者接口。
    /// </summary>
    public interface IDataCenterCategoryProvider
    {
        /// <summary>
        /// 数据中心分类名称提供者名称。
        /// </summary>
        string DataCenterCategoryProviderName { get; }

        /// <summary>
        /// 数据中心分类提供者显示名称。
        /// </summary>
        string DataCenterCategoryProviderDisplayName { get; }

        /// <summary>
        /// 获得当前模块的模型名称集合。
        /// </summary>
        /// <returns>当前模块的模型名称集合。</returns>
        IEnumerable<SelectListItem> GetMoldName();

        /// <summary>
        /// 获取分类各站点下所订阅的节点名称集合。
        /// </summary>
        /// <param name="nodeIds">节点编号集合。</param>
        /// <returns>分类各站点下所订阅的节点名称集合。</returns>
        Dictionary<string, List<string>> GetSubscriptionNode(List<int> nodeIds);
    }
}