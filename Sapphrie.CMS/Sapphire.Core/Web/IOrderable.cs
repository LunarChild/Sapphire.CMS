using System;

namespace Sapphire.Core.Web
{
    /// <summary>
    /// 定义一种特定于类型的通用排序字段，通过此字段对其实例进行排序。
    /// </summary>
    public interface IOrderable
    {
        /// <summary>
        /// 排序。
        /// </summary>
        int Order { get; set; }

        /// <summary>
        /// 比较对象。
        /// </summary>
        /// <param name="orderObject">排序对象。</param>
        /// <returns>返回结果。</returns>
        bool ComparisonObject(IOrderable orderObject);
    }
}