using System.Text.RegularExpressions;

namespace Sapphire.Core.Lunar
{
    /// <summary>
    /// 引擎助手。
    /// </summary>
    public static class EngineHelper
    {
        /// <summary>
        /// 标签参数键。
        /// </summary>
        public static readonly string LabelParamsKey = "Sapphire_LabelParams";

        /// <summary>
        /// 模板参数键。
        /// </summary>
        public static readonly string ViewParametersKey = "Sapphire_ViewParametersKey";

        /// <summary>
        /// 分页模型键。
        /// </summary>
        public static readonly string PageModelKey = "Sapphire_PageModel";

        /// <summary>
        /// 生成静态Html页标识。
        /// </summary>
        public static readonly string GenerateStaticHtmlFlagKey = "Sapphire_GenerateStaticHtml";

        /// <summary>
        /// 静态Html页路径。
        /// </summary>
        public static readonly string GenerateStaticHtmlFileDirectory = "StaticHtmlFile";

        /// <summary>
        /// 默认没有数据时的提示信息。
        /// </summary>
        public static readonly string DefaultNoDataMessage = "没有任何数据！";

        /// <summary>
        /// 默认列表超链接打开方式。
        /// </summary>
        public static readonly string DefaultListLinkOpenType = "_self";

        /// <summary>
        /// 默认列表标题长度。
        /// </summary>
        public static readonly int DefaultListTitleLength = 10;

        /// <summary>
        /// Ajax调用标签处理控制器。
        /// </summary>
        public static readonly string AjaxLabelController = "Ajax";

        /// <summary>
        /// Ajax调用标签处理操作。
        /// </summary>
        public static readonly string AjaxLabelAction = "AjaxLabel";

        /// <summary>
        /// Ajax调用标签处理路径。
        /// </summary>
        public static readonly string AjaxLabelPath = "/" + AjaxLabelController + "/" + AjaxLabelAction;

        /// <summary>
        /// SQL关键字过滤正则表达式。
        /// </summary>
        public static readonly Regex SqlKeywordRegex =
            new Regex(
                @"(SELECT|UPDATE|INSERT|DELETE|DECLARE|@|EXEC|DBCC|ALTER|DROP|CREATE|BACKUP|IF|ELSE|END|AND|OR|ADD|SET|OPEN|CLOSE|USE|BEGIN|RETUN|AS|GO|EXISTS|KILL|&)",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// 过滤SQL关键字。
        /// </summary>
        /// <param name="content">内容。</param>
        /// <returns>过滤后的内容。</returns>
        public static string FilterSqlKeyword(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return string.Empty;
            }

            var tryAgain = false;

            if (SqlKeywordRegex.IsMatch(content))
            {
                content = SqlKeywordRegex.Replace(content, string.Empty);
                tryAgain = true;
            }

            if (tryAgain)
            {
                FilterSqlKeyword(content);
            }

            return content;
        }
    }
}