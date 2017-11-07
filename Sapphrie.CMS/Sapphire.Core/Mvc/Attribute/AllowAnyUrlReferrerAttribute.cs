using System;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 允许任意Url来访页访问。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class AllowAnyUrlReferrerAttribute : Attribute
    {
    }
}