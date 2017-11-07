//using System;
//using System.Diagnostics;
//using System.Web.Mvc;
//using System.Web.Mvc.Html;
//using Sapphire.Core.Mvc;
//using Sapphire.Core.CommonHelper;

//namespace Sapphire.Core.Lunar
//{
//    /// <summary>
//    /// 标签呈现扩展方法类。
//    /// </summary>
//    public static class PartialExtensions
//    {
//        /// <summary>
//        /// 以 HTML 编码字符串的形式呈现指定的标签。
//        /// </summary>
//        /// <param name="powerHelper">此方法扩展的 Power 帮助器实例。</param>
//        /// <param name="partialViewName">要呈现的标签的名称。</param>
//        /// <returns>以 HTML 编码字符串形式呈现的标签。</returns>
//        public static MvcHtmlString Partial(this PowerHelper powerHelper, string partialViewName)
//        {
//            return powerHelper.Partial(null, partialViewName, null);
//        }

//        /// <summary>
//        /// 以 HTML 编码字符串的形式呈现指定的标签。
//        /// </summary>
//        /// <param name="powerHelper">此方法扩展的 Power 帮助器实例。</param>
//        /// <param name="partialViewName">要呈现的标签的名称。</param>
//        /// <param name="parameters">用于标签的匿名参数对。</param>
//        /// <returns>以 HTML 编码字符串形式呈现的标签。</returns>
//        public static MvcHtmlString Partial(this PowerHelper powerHelper, string partialViewName, object parameters)
//        {
//            return powerHelper.Partial(null, partialViewName, parameters);
//        }

//        /// <summary>
//        /// 以 HTML 编码字符串的形式呈现指定的标签。
//        /// </summary>
//        /// <param name="powerHelper">此方法扩展的 Power 帮助器实例。</param>
//        /// <param name="moduleName">模块名称。</param>
//        /// <param name="partialViewName">要呈现的标签的名称。</param>
//        /// <returns>以 HTML 编码字符串形式呈现的标签。</returns>
//        public static MvcHtmlString Partial(this PowerHelper powerHelper, string moduleName, string partialViewName)
//        {
//            return powerHelper.Partial(moduleName, partialViewName, null);
//        }

//        /// <summary>
//        /// 以 HTML 编码字符串的形式呈现指定的标签。
//        /// </summary>
//        /// <param name="powerHelper">此方法扩展的 Power 帮助器实例。</param>
//        /// <param name="moduleName">模块名称。</param>
//        /// <param name="partialViewName">要呈现的标签的名称。</param>
//        /// <param name="parameters">用于标签的匿名参数对象。</param>
//        /// <returns>以 HTML 编码字符串形式呈现的标签。</returns>
//        public static MvcHtmlString Partial(this PowerHelper powerHelper, string moduleName, string partialViewName, object parameters)
//        {
//            try
//            {
//                Check.NotNull(partialViewName, "partialViewName");

//                var dynamicParameters = DynamicParameterDictionary.ParseDynamicParameters(parameters);

//                powerHelper.HtmlHelper.ViewData[EngineHelper.ViewParametersKey] = dynamicParameters;

//                var originalArea = string.Empty;
//                if (!string.IsNullOrEmpty(moduleName))
//                {
//                    originalArea = powerHelper.SetArea(powerHelper.HtmlHelper, moduleName);
//                }

//                var mvcHtmlString = powerHelper.HtmlHelper.Partial(partialViewName, null, powerHelper.HtmlHelper.ViewData);

//                if (!string.IsNullOrEmpty(originalArea))
//                {
//                    powerHelper.HtmlHelper.ViewContext.RouteData.DataTokens["area"] = originalArea;
//                }

//                return mvcHtmlString;
//            }
//            catch (Exception e)
//            {
//                Debug.Print(e.Message);

//                //return MvcHtmlString.Create("调用“" + partialViewName + "”失败！");

//                return MvcHtmlString.Create(e.Message);
//            }
//        }
//    }
//}