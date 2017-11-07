using System.Web.Mvc;

namespace Sapphire.Core.Lunar
{
    /// <summary>
    /// 前台控件帮助类。
    /// </summary>
    public class SapphirControlHelper
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="htmlHelper">此方法扩展的HTML帮助器实例。</param>
        public SapphirControlHelper(HtmlHelper htmlHelper)
        {
            this.Html = htmlHelper;
        }

        /// <summary>
        /// htmlHelp。
        /// </summary>
        public HtmlHelper Html { get; set; }
    }
}