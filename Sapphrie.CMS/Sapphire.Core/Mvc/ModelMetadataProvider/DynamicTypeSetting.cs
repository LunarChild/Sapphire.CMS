using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 动态类型元数据设置。
    /// </summary>
    public class DynamicTypeSetting : IDynamicMetadataSetting
    {
        /// <summary>
        /// 脏读列表。
        /// </summary>
        private readonly List<string> dirtyList = new List<string>();

        /// <summary>
        /// 显示名称。
        /// </summary>
        private string displayName;

        /// <summary>
        /// 显示名称。
        /// </summary>
        public string DisplayName
        {
            get
            {
                return this.displayName;
            }

            set
            {
                this.displayName = value;
                this.dirtyList.Add(GetPropertyName(() => this.DisplayName));
            }
        }

        /// <summary>
        /// 设置模型元数据。
        /// </summary>
        /// <param name="modelMetadata">模型元数据对象。</param>
        public void SetModelMetadata(ModelMetadata modelMetadata)
        {
            if (this.IsDirty(() => this.DisplayName))
            {
                modelMetadata.DisplayName = this.DisplayName;
            }
        }

        /// <summary>
        /// 获取属性名的字符串形式。
        /// </summary>
        /// <typeparam name="T">类或对象的类型。</typeparam>
        /// <param name="expression">表达式树。</param>
        /// <returns>返回属性名的字符串形式。</returns>
        private static string GetPropertyName<T>(Expression<Func<T>> expression)
        {
            var body = (MemberExpression)expression.Body;
            return body.Member.Name;
        }

        /// <summary>
        /// 数据值是否已被修改。
        /// </summary>
        /// <typeparam name="T">属性。</typeparam>
        /// <param name="expression">表达式树。</param>
        /// <returns>是否已被修改。</returns>
        private bool IsDirty<T>(Expression<Func<T>> expression)
        {
            return this.dirtyList.Contains(GetPropertyName(expression));
        }
    }
}