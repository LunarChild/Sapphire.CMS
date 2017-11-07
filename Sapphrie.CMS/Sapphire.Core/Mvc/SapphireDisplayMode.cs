using System.Web.WebPages;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 初始化显示模式。
    /// </summary>
    public class SapphireDisplayMode : DefaultDisplayMode
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="displayModeId">显示模式Id。</param>
        public SapphireDisplayMode(string displayModeId)
            : base(displayModeId)
        {
        }

        /// <summary>
        /// 改变虚拟路径。
        /// </summary>
        /// <param name="virtualPath">虚拟路径。</param>
        /// <param name="suffix">后缀。</param>
        /// <returns>改变之后的路径。</returns>
        protected override string TransformPath(string virtualPath, string suffix)
        {
            if (string.IsNullOrEmpty(suffix))
            {
                return virtualPath;
            }

            return virtualPath.Replace("/Views/", "/Views." + suffix + "/");
        }
    }
}