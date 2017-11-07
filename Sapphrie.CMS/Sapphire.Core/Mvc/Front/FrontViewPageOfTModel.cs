//using System.Web.Mvc;

//namespace Sapphire.Core.Mvc
//{
//    /// <summary>
//    /// 扩展表示呈现使用 ASP.NET Razor 语法的模板所需的属性和方法。
//    /// </summary>
//    /// <typeparam name="TModel">模板数据模型的类型。</typeparam>
//    public abstract class FrontViewPage<TModel> : FrontViewPage
//    {
//        private ViewDataDictionary<TModel> viewDataDictionary;

//        /// <summary>
//        /// 获取或设置 PowerHelper 对象，该对象用于呈现 HTML 元素。
//        /// </summary>
//        public new PowerHelper<TModel> Power { get; set; }

//        /// <summary>
//        /// 获取或设置 HtmlHelper 对象，该对象用于呈现 HTML 元素。
//        /// </summary>
//        public new HtmlHelper<TModel> Html { get; set; }

//        /// <summary>
//        /// 模型。
//        /// </summary>
//        public new TModel Model
//        {
//            get
//            {
//                return this.ViewData.Model;
//            }
//        }

//        /// <summary>
//        /// AjaxHelper。
//        /// </summary>
//        public new AjaxHelper<TModel> Ajax { get; set; }

//        /// <summary>
//        /// 模板数据。
//        /// </summary>
//        public new ViewDataDictionary<TModel> ViewData
//        {
//            get
//            {
//                if (this.viewDataDictionary == null)
//                {
//                    this.SetViewData(new ViewDataDictionary<TModel>());
//                }

//                return this.viewDataDictionary;
//            }

//            set
//            {
//                this.SetViewData(value);
//            }
//        }

//        /// <summary>
//        /// 初始化 Helper 类。
//        /// </summary>
//        public override void InitHelpers()
//        {
//            base.InitHelpers();
//            this.Ajax = new AjaxHelper<TModel>(this.ViewContext, this);
//            this.Html = new HtmlHelper<TModel>(this.ViewContext, this);
//            this.Power = new PowerHelper<TModel>(this.ViewContext, this, this.Html);
//        }

//        /// <summary>
//        /// 设置模板数据。
//        /// </summary>
//        /// <param name="viewData">模板数据。</param>
//        protected override void SetViewData(ViewDataDictionary viewData)
//        {
//            this.viewDataDictionary = new ViewDataDictionary<TModel>(viewData);

//            base.SetViewData(this.viewDataDictionary);
//        }
//    }
//}