using System.Collections.Generic;

namespace Sapphire.Core.Web
{
    /// <summary>
    /// 带子集合的树节点模型。
    /// </summary>
    public class TreeSetNode
    {
        /// <summary>
        /// 初始化 TreeSetNode 类的新实例。
        /// </summary>
        public TreeSetNode()
        {
            this.Children = new List<TreeSetNode>();
        }

        /// <summary>
        /// 节点id。
        /// </summary>
        public int TreeSetNodeId { get; set; }

        /// <summary>
        /// 节点显示名。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 节点是否选中。
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// 节点子集合。
        /// </summary>
        public IList<TreeSetNode> Children { get; set; }

        /// <summary>
        /// 节点图标。
        /// </summary>
        public string IconSkin { get; set; }
    }
}