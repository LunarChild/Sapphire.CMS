﻿using System.Web.Mvc;

namespace Sapphire.Core.Lunar
{
    /// <summary>
    /// 前台控件帮助类。
    /// </summary>
    /// <typeparam name="TModel">模型。</typeparam>
    public class SapphireControlHelper<TModel> : SapphirControlHelper
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="htmlHelper">此方法扩展的HTML帮助器实例。</param>
        public SapphireControlHelper(HtmlHelper<TModel> htmlHelper)
            : base(htmlHelper)
        {
            this.Html = htmlHelper;
        }

        /// <summary>
        /// HtmlHelper。
        /// </summary>
        public new HtmlHelper<TModel> Html { get; set; }
    }
}