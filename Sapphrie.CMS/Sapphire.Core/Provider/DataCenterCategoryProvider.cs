using System.Collections.Generic;
using System.Web.Mvc;

namespace Sapphire.Core.Provider
{
    /// <summary>
    /// 数据中心分类提供者。
    /// </summary>
    public class DataCenterCategoryProvider
    {
        private static readonly Dictionary<string, IDataCenterCategoryProvider> DataCenterCategoryProviderDictionary = new Dictionary<string, IDataCenterCategoryProvider>();

        /// <summary>
        /// 添加数据中心分类提供者。
        /// </summary>
        /// <param name="dataCenterCategoryProvider">数据中心分类提供者实例。</param>
        public static void AddDataCenterCategoryProvider(IDataCenterCategoryProvider dataCenterCategoryProvider)
        {
            DataCenterCategoryProviderDictionary.Add(dataCenterCategoryProvider.DataCenterCategoryProviderName, dataCenterCategoryProvider);
        }

        /// <summary>
        /// 通过提供者名称获取索引提供者实例。
        /// </summary>
        /// <param name="providerName">提供者名称。</param>
        /// <returns>返回索引提供者实例。</returns>
        public static IDataCenterCategoryProvider GetDataCenterCategoryProvider(string providerName)
        {
            return DataCenterCategoryProviderDictionary[providerName];
        }

        /// <summary>
        /// 获取数据中心提供者数据源。
        /// </summary>
        /// <returns>返回数据中心提供者数据源。</returns>
        public static IEnumerable<SelectListItem> GetDataCenterCategoryProviderDataSource()
        {
            foreach (var provider in DataCenterCategoryProviderDictionary)
            {
                var list = provider.Value.GetMoldName();
                foreach (var item in list)
                {
                    yield return new SelectListItem { Value = item.Value, Text = item.Text, Group = new SelectListGroup { Name = provider.Value.DataCenterCategoryProviderDisplayName } };
                }
            }
        }

        /// <summary>
        /// 获取站点下所订阅的节点名称集合。
        /// </summary>
        /// <param name="nodes">节点编号集合。</param>
        /// <param name="curProviderName">当前提供者名称。</param>
        /// <returns>分类各站点下所订阅的节点名称集合。</returns>
        public static Dictionary<string, List<string>> GetSubscriptionNodesNameList(List<int> nodes, string curProviderName)
        {
            var list = GetDataCenterCategoryProvider(curProviderName).GetSubscriptionNode(nodes);
            return list;
        }
    }
}