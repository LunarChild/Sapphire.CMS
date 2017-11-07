using System;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 标识数据发生改变的特性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DataChangedAttribute : Attribute
    {
    }
}