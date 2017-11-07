using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace Sapphire.Core.CommonHelper
{
    /// <summary>
    /// 数据源助手。
    /// </summary>
    public static class DataSourceHelper
    {
        private const string Suffix = "_DataSource";

        /// <summary>
        /// 根据属性获取数据源对应的ViewData键。
        /// </summary>
        /// <typeparam name="TModel">要获取属性的模型。</typeparam>
        /// <typeparam name="TProperty">绑定数据源的属性。</typeparam>
        /// <param name="expression">属性表达式。</param>
        /// <returns>数据源名称。</returns>
        public static string GetDataSourceViewDataKey<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            string propertyName = null;
            if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                var memberExpression = (MemberExpression)expression.Body;
                propertyName = memberExpression.Member is PropertyInfo ? memberExpression.Member.Name : null;
            }

            return GetDataSourceViewDataKey(propertyName);
        }

        /// <summary>
        /// 根据当前名称获取数据源对应的ViewData键。
        /// </summary>
        /// <param name="name">数据源名称。</param>
        /// <returns>符合数据源规则的名称。</returns>
        public static string GetDataSourceViewDataKey(string name)
        {
            return name + Suffix;
        }

        /// <summary>
        /// 将数据源名称设置到ModelMetaData中。
        /// </summary>
        /// <param name="metadata">模型元数据。</param>
        /// <param name="dataSourceName">数据源名称，如果名称为空将会使用ModelMetaData中的属性名称作为数据源名称。</param>
        /// <param name="key">数据源名字在元数据字典中的key值，默认为“DataSource”。</param>
        public static void IntoModelMetadata(ModelMetadata metadata, string dataSourceName, string key = "DataSource")
        {
            if (string.IsNullOrWhiteSpace(dataSourceName))
            {
                dataSourceName = metadata.PropertyName;
            }

            var dataSourceViewDataKey = GetDataSourceViewDataKey(dataSourceName);

            metadata.AdditionalValues.Add(key, dataSourceViewDataKey);
        }
    }
}