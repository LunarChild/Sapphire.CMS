namespace Sapphire.Core.Web
{
    /// <summary>
    /// 树工具菜单类。
    /// </summary>
    public class TreeToolMenu
    {
        /// <summary>
        /// 生成元素Id。
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 生成元素显示文字。
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 生成元素样式类名。
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 客户端回调函数名。
        /// </summary>
        public string FunctionName { get; set; }
    }
}