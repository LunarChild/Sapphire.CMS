using System;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 允许任意角色访问。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class AllowAnyRoleAttribute : Attribute
    {
    }
}