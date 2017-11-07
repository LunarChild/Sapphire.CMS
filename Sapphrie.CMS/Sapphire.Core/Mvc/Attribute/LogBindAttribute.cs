using System;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 日志模型参数绑定特性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class LogBindAttribute : Attribute
    {
        /// <summary>
        /// 构造LogBindAttribute类的新实例。
        /// </summary>
        public LogBindAttribute()
            : this(true)
        {
        }

        /// <summary>
        /// 通过指定是否启用绑定构造LogBindAttribute类的新实例。
        /// </summary>
        /// <param name="enable">是否启用绑定。</param>
        public LogBindAttribute(bool enable)
        {
            this.Enable = enable;
        }

        /// <summary>
        /// 启用绑定。
        /// </summary>
        public bool Enable { get; private set; }

        /// <summary>
        /// 获取或设置不允许绑定的属性名称的列表（各属性名称之间用英文逗号分隔）。
        /// </summary>
        public string Exclude { get; set; }

        /// <summary>
        /// 获取或设置允许绑定的属性名称的列表（各属性名称之间用英文逗号分隔）。
        /// </summary>
        public string Include { get; set; }
    }
}