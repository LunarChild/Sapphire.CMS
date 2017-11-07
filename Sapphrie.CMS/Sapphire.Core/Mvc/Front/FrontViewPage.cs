//using System.Web;
//using System.Web.Mvc;
//using Sapphire.Core.Lunar;

//namespace Sapphire.Core.Mvc
//{
//    /// <summary>
//    /// 扩展表示呈现使用 ASP.NET Razor 语法的模板所需的属性和方法。
//    /// </summary>
//    public abstract class FrontViewPage : WebViewPage
//    {
//        private DynamicParameterDictionary dynamicParameterDictionary;

//        /// <summary>
//        /// 获取或设置 PowerHelper 对象，该对象用于呈现 HTML 元素。
//        /// </summary>
//        public PowerHelper Power { get; set; }

//        /// <summary>
//        /// 模板参数。
//        /// </summary>
//        public dynamic ViewParams
//        {
//            get
//            {
//                return this.ViewData[EngineHelper.ViewParametersKey];
//            }
//        }

//        /// <summary>
//        /// Url参数。
//        /// </summary>
//        public dynamic UrlParams
//        {
//            get
//            {
//                return this.dynamicParameterDictionary ?? (this.dynamicParameterDictionary = DynamicParameterDictionary.ParseDynamicParameters(HttpContext.Current.Request.QueryString));
//            }
//        }

//        /// <summary>
//        /// 初始化 Helper 类。
//        /// </summary>
//        public override void InitHelpers()
//        {
//            base.InitHelpers();
//            this.Power = new PowerHelper(this.ViewContext, this, this.Html);
//        }
//    }
//}