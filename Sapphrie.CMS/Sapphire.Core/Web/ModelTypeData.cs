using System;
using System.Reflection;

namespace Sapphire.Core.Web
{
    /// <summary>
    /// 模型类型数据。
    /// </summary>
    public class ModelTypeData
    {
        /// <summary>
        /// 显示属性（被标记为DisplayColumn特性的属性）。
        /// </summary>
        public PropertyInfo DisplayColumnProperty { get; set; }

        /// <summary>
        /// Id属性（标记有Id特性的数据）。
        /// </summary>
        public PropertyInfo IdProperty { get; set; }

        /// <summary>
        /// 外键属性。
        /// </summary>
        public PropertyInfo SuperordinateIdProperty { get; set; }

        /// <summary>
        /// 获取显示属性的名称，当属性不存在时返回空字符串。
        /// </summary>
        /// <returns>返回属性名称。</returns>
        public string GetDisplayColumnName()
        {
            return this.DisplayColumnProperty == null ? string.Empty : this.DisplayColumnProperty.Name;
        }

        /// <summary>
        /// 获取显示属性值，当属性不存在时返回空字符串。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>返回显示属性值。</returns>
        public string GetDisplayColumnValue(object obj)
        {
            return this.DisplayColumnProperty == null ? string.Empty : this.DisplayColumnProperty.GetValue(obj) as string;
        }

        /// <summary>
        /// 获取Id属性的名称，当属性不存在时返回空字符串。
        /// </summary>
        /// <returns>返回Id属性名称。</returns>
        public string GetIdName()
        {
            return this.IdProperty == null ? string.Empty : this.IdProperty.Name;
        }

        /// <summary>
        /// 获取Id属性值，当属性不存在时返回0。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>返回Id属性值。</returns>
        public int GetIdValue(object obj)
        {
            return this.IdProperty == null ? 0 : Convert.ToInt32(this.IdProperty.GetValue(obj));
        }

        /// <summary>
        /// 获取外键Id属性名称，当属性不存在时返回空字符串。
        /// </summary>
        /// <returns>返回外键Id属性名称。</returns>
        public string GetSuperordinateIdName()
        {
            return this.SuperordinateIdProperty == null ? string.Empty : this.SuperordinateIdProperty.Name;
        }

        /// <summary>
        /// 获取外键Id属性值，当属性不存在时返回0。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>返回外键Id属性值。</returns>
        public int GetSuperordinateIdValue(object obj)
        {
            return this.SuperordinateIdProperty == null ? 0 : Convert.ToInt32(this.SuperordinateIdProperty.GetValue(obj));
        }

        /// <summary>
        /// 设置外键属性的值。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <param name="foreignKeyValue">外键属性值。</param>
        public void SetSuperordinateIdValue(object obj, int foreignKeyValue)
        {
            if (this.SuperordinateIdProperty != null)
            {
                this.SuperordinateIdProperty.SetValue(obj, foreignKeyValue);
            }
        }
    }
}