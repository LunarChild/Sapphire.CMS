using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Sapphire.Core.Web;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 左侧树帮助类。
    /// </summary>
    public class TreeHelper
    {
        private readonly IList<TreeToolMenu> rightMenu = new List<TreeToolMenu>();

        private readonly IList<TreeToolMenu> paneMenu = new List<TreeToolMenu>();

        /// <summary>
        /// 增加右键菜单项。
        /// </summary>
        /// <param name="id">id。</param>
        /// <param name="label">label。</param>
        /// <param name="functionName">functionName。</param>
        /// <param name="className">className。</param>
        /// <returns>右键菜单项。</returns>
        public TreeHelper AddrightMenuItem(string id, string label, string functionName, string className = null)
        {
            this.rightMenu.Add(this.AddItem(id, label, className, functionName));
            return this;
        }

        /// <summary>
        /// 增加工具菜单项。
        /// </summary>
        /// <param name="id">元素id。</param>
        /// <param name="title">元素显示文字。</param>
        /// <param name="functionName">客户端回调函数名。</param>
        /// <param name="className">元素css样式类名。</param>
        /// <returns>工具菜单项。</returns>
        public TreeHelper AddpaneMenuItem(string id, string title, string functionName, string className = null)
        {
            this.paneMenu.Add(this.AddItem(id, title, className, functionName));
            return this;
        }

        /// <summary>
        /// 生成树数据。
        /// </summary>
        /// <param name="treeNodeslist">树节点列表。</param>
        /// <param name="rightClickFunName">节点右键点击事件回调函数名。</param>
        /// <param name="clickFunName">节点点击事件回调函数名。</param>
        /// <returns>树数据。</returns>
        public string GetTree(IEnumerable treeNodeslist, string rightClickFunName = null, string clickFunName = null)
        {
            var packageTree = new PackageTree { ClickFunName = clickFunName, RightClickFunName = rightClickFunName, PaneMenu = this.paneMenu, RightMenu = this.rightMenu };
            var tree = new { treeNodes = treeNodeslist, packageTree };
            return JsonConvert.SerializeObject(tree);
        }

        /// <summary>
        /// 生成树数据。
        /// </summary>
        /// <param name="treeNodeslist">树节点列表。</param>
        /// <param name="packageTree">包装树实体。</param>
        /// <returns>树数据。</returns>
        public string GetTree(IEnumerable treeNodeslist, PackageTree packageTree)
        {
            var tree = new { treeNodes = treeNodeslist, packageTree };
            return JsonConvert.SerializeObject(tree);
        }

        private TreeToolMenu AddItem(string id, string label, string className, string functionName)
        {
            var item = new TreeToolMenu { Id = id, Label = label, ClassName = className, FunctionName = functionName };
            return item;
        }
    }
}