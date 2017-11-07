using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 动态属性元数据设置。
    /// </summary>
    public class DynamicPropertySetting : IDynamicMetadataSetting
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
        /// 是否在显示页显示。
        /// </summary>
        private bool showForDisplay;

        /// <summary>
        /// 是否在编辑页显示。
        /// </summary>
        private bool showForEdit;

        /// <summary>
        /// 是否必填。
        /// </summary>
        private bool isRequired;

        /// <summary>
        /// 模型的其他元数据的字典。
        /// </summary>
        private Dictionary<string, object> additionalValues = new Dictionary<string, object>();

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
        /// 是否在显示页显示。
        /// </summary>
        public bool ShowForDisplay
        {
            get
            {
                return this.showForDisplay;
            }

            set
            {
                this.showForDisplay = value;
                this.dirtyList.Add(GetPropertyName(() => this.ShowForDisplay));
            }
        }

        /// <summary>
        /// 是否在编辑页显示。
        /// </summary>
        public bool ShowForEdit
        {
            get
            {
                return this.showForEdit;
            }

            set
            {
                this.showForEdit = value;
                this.dirtyList.Add(GetPropertyName(() => this.ShowForEdit));
            }
        }

        /// <summary>
        /// 是否必填。
        /// </summary>
        public bool IsRequired
        {
            get
            {
                return this.isRequired;
            }

            set
            {
                this.isRequired = value;
                this.dirtyList.Add(GetPropertyName(() => this.IsRequired));
            }
        }

        /// <summary>
        /// 模型的其他元数据的字典。
        /// </summary>
        public Dictionary<string, object> AdditionalValues
        {
            get
            {
                return this.additionalValues;
            }

            set
            {
                this.additionalValues = value;
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

            if (this.IsDirty(() => this.ShowForDisplay))
            {
                modelMetadata.ShowForDisplay = this.ShowForDisplay;
            }

            if (this.IsDirty(() => this.ShowForEdit))
            {
                modelMetadata.ShowForEdit = this.ShowForEdit;
            }

            if (this.IsDirty(() => this.IsRequired))
            {
                modelMetadata.IsRequired = this.IsRequired;
            }

            if (this.AdditionalValues.Any())
            {
                foreach (var item in this.AdditionalValues)
                {
                    modelMetadata.AdditionalValues[item.Key] = item.Value;
                }
            }
        }

        /// <summary>
        /// 添加模型的其他元数据的附加值。
        /// </summary>
        /// <param name="additionalValueKey">附加值的键。</param>
        /// <param name="additionalValue">附加值。</param>
        /// <returns>返回动态属性元数据设置。</returns>
        public DynamicPropertySetting AdditionalValue(string additionalValueKey, object additionalValue)
        {
            this.AdditionalValues[additionalValueKey] = additionalValue;
            return this;
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