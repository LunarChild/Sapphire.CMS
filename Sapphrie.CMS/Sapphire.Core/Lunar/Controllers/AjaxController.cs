using System.Collections.Generic;
using System.Web.Mvc;

namespace Sapphire.Core.Lunar
{
    /// <summary>
    /// Ajax控制器。
    /// </summary>
    public class AjaxController : Controller
    {
        /// <summary>
        /// Ajax调用标签。
        /// </summary>
        /// <param name="moduleName">模块名称。</param>
        /// <param name="labelName">标签名称。</param>
        /// <param name="labelParams">标签参数。</param>
        /// <returns>返回标签模板。</returns>
        public ActionResult AjaxLabel(string moduleName, string labelName, string labelParams)
        {
            if (!string.IsNullOrEmpty(moduleName))
            {
                this.RouteData.DataTokens["area"] = moduleName;
            }

            var dictionary = this.CreateAjaxLabelParamValue(labelParams);

            if (dictionary != null)
            {
                this.ViewData[EngineHelper.LabelParamsKey] = dictionary;
            }

            return this.PartialView(labelName);
        }

        /// <summary>
        /// 构建Ajax调用标签参数。
        /// </summary>
        /// <param name="labelParams">标签参数。</param>
        /// <returns>返回标签参数字典。</returns>
        public IDictionary<string, object> CreateAjaxLabelParamValue(string labelParams)
        {
            if (string.IsNullOrEmpty(labelParams))
            {
                return null;
            }

            IDictionary<string, object> dictionary = new Dictionary<string, object>();

            var parms = labelParams.Split(',');

            foreach (var param in parms)
            {
                var labelParam = param.Trim();
                var index = labelParam.IndexOf('=');
                var key = labelParam.Substring(0, index).Trim();
                object value = labelParam.Substring(index + 1, labelParam.Length - (index + 1)).Trim();
                dictionary.Add(key, value);
            }

            return dictionary;
        }
    }
}