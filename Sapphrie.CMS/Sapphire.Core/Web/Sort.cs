namespace Sapphire.Core.Web
{
    /// <summary>
    /// 简单排序模型。
    /// </summary>
    public class Sort
    {
        /// <summary>
        /// 编号。
        /// </summary>
        public int SortId { get; set; }

        /// <summary>
        /// 显示名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序。
        /// </summary>
        public int Order { get; set; }
    }
}
