using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 动态元数据助手。
    /// </summary>
    public static class DynamicMetadataHelper
    {
        /// <summary>
        /// 根据模型获取元数据对应的键。
        /// </summary>
        /// <typeparam name="TModel">模型类型。</typeparam>
        /// <returns>元数据对应的键。</returns>
        public static string GetMetadataKeyForType<TModel>()
        {
            return GetMetadataKey(typeof(TModel), null);
        }

        /// <summary>
        /// 根据属性获取元数据对应的键。
        /// </summary>
        /// <typeparam name="TModel">要获取属性的模型。</typeparam>
        /// <typeparam name="TProperty">绑定元数据的属性。</typeparam>
        /// <param name="expression">属性表达式。</param>
        /// <returns>元数据对应的键。</returns>
        public static string GetMetadataKeyForProperty<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            string propertyName = null;
            if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                var memberExpression = (MemberExpression)expression.Body;
                propertyName = memberExpression.Member is PropertyInfo ? memberExpression.Member.Name : null;
            }

            return GetMetadataKey(typeof(TModel), propertyName);
        }

        /// <summary>
        /// 根据当前名称获取元数据对应的键。
        /// </summary>
        /// <param name="type">类型。</param>
        /// <param name="propertyName">属性名称。</param>
        /// <returns>元数据对应的键。</returns>
        public static string GetMetadataKey(Type type, string propertyName)
        {
            const string MetadataKey = "DynamicMetadata";

            var modelNames = type.Name.Split('_');
            string modelName = modelNames.Length > 1 ? modelNames[0] : type.Name;

            if (string.IsNullOrEmpty(propertyName))
            {
                return string.Format("{0}_{1}", modelName, MetadataKey);
            }

            return string.Format("{0}_{1}_{2}", modelName, propertyName, MetadataKey);
        }
    }
}