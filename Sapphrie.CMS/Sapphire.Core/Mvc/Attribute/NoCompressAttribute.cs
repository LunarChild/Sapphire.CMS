using System;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 不启用压缩特性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
    public sealed class NoCompressAttribute : Attribute
    {
    }
}