//using System.Web.Mvc;
//using Sapphire.Core.Power;

//namespace Sapphire.Core.Mvc
//{
//    /// <summary>
//    /// 在强类型模板中呈现 HTML 的助手类。
//    /// </summary>
//    /// <typeparam name="TModel">模型对象。</typeparam>
//    public class PowerHelper<TModel> : PowerHelper
//    {
//        /// <summary>
//        /// 初始化 PowerHelper 类的新实例。
//        /// </summary>
//        /// <param name="viewContext">模板上下文。</param>
//        /// <param name="viewDataContainer">模板数据容器。</param>
//        /// <param name="htmlHelper">此方法扩展的HTML帮助器实例。</param>
//        public PowerHelper(ViewContext viewContext, IViewDataContainer viewDataContainer, HtmlHelper<TModel> htmlHelper)
//            : base(viewContext, viewDataContainer, htmlHelper)
//        {
//            this.HtmlHelper = htmlHelper;
//            this.Control = new PowerControlHelper<TModel>(this.HtmlHelper);
//        }

//        /// <summary>
//        /// 前台控件帮助类。
//        /// </summary>
//        public new PowerControlHelper<TModel> Control { get; private set; }

//        /// <summary>
//        /// HtmlHelper。
//        /// </summary>
//        internal new HtmlHelper<TModel> HtmlHelper { get; set; }
//    }
//}