using System.Collections.Generic;

namespace Sapphire.Core.Web
{
    /// <summary>
    /// 包装树类。
    /// </summary>
    public class PackageTree
    {
        /// <summary>
        /// RightClickFunName。
        /// </summary>
        public string RightClickFunName { get; set; }

        /// <summary>
        /// ClickFunName。
        /// </summary>
        public string ClickFunName { get; set; }

        /// <summary>
        /// RightMenu。
        /// </summary>
        public IList<TreeToolMenu> RightMenu { get; set; }

        /// <summary>
        /// PaneMenu。
        /// </summary>
        public IList<TreeToolMenu> PaneMenu { get; set; }
    }
}