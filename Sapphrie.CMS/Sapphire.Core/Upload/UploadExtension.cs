//using System.Web;
//using System.Web.Mvc;
//using System.Web.Mvc.Html;
//using Sapphire.Core.Annotations;

//namespace Sapphire.Core.Upload
//{
//    /// <summary>
//    /// 上传控件Html方法扩展类。
//    /// </summary>
//    public static class UploadExtension
//    {
//        /// <summary>
//        /// 呈现多文件上传控件。
//        /// </summary>
//        /// <param name="htmlHelper">HtmlHelper对象实例。</param>
//        /// <param name="fieldName">字段名称。</param>
//        /// <param name="uploadProviderKey">上传提供者key。</param>
//        /// <param name="uploadRuleKey">解析上传目录规则，文件名规则需要使用的关键字。</param>
//        /// <returns>返回多文件上传控件。</returns>
//        public static IHtmlString MultipleFileForEditor(
//            this HtmlHelper htmlHelper,
//            string fieldName,
//            string uploadProviderKey = GlobalUploadProvider.UploadProviderKey,
//            string uploadRuleKey = null)
//        {
//            var viewdata = new ViewDataDictionary
//            {
//                { "propertyName", fieldName },
//                { MetadataAdditionalKey.UploadRuleKey, uploadRuleKey },
//                { MetadataAdditionalKey.UploadProviderKey, uploadProviderKey }
//            };

//            return htmlHelper.Partial("EditorTemplates/MultipleFileUpload", viewdata);
//        }
//    }
//}