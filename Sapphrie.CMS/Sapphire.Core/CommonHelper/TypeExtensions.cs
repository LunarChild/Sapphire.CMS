using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Sapphire.Core.CommonHelper
{
    /// <summary>
    /// 类型的扩展类
    /// </summary>
    public static class TypeExtensions
    {
        ///// <summary>
        ///// 获取字段的DisplayAttribute特性值
        ///// </summary>
        ///// <param name="type">类型。</param>
        ///// <param name="fieldName">字段名。</param>
        ///// <returns>返回字段的DisplayAttribute特性值</returns>
        //public static string GetDisplayName(this Type type, string fieldName)
        //{
        //    var displayAttribute = type.GetField(fieldName).GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
        //    return displayAttribute != null ? displayAttribute.Name : fieldName;
        //}
    }
}