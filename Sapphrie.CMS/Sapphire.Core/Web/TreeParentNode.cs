namespace Sapphire.Core.Web
{
    /// <summary>
    /// 带父id的树节点模型。
    /// </summary>
    public class TreeParentNode
    {
        /// <summary>
        /// 节点Id。
        /// </summary>
        public int TreeParentNodeId { get; set; }

        /// <summary>
        /// 父id。
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 节点显示名。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 节点是否选中。
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// 是否叶子节点。
        /// </summary>
        public bool IsLeaf { get; set; }

        /// <summary>
        /// 节点图标。
        /// </summary>
        public string IconSkin { get; set; }

        /// <summary>
        /// 是否可用。
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 是否可见。
        /// </summary>
        public bool Visible { get; set; }
    }
}