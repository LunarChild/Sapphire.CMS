//using System;
//using System.Linq;
//using System.Text.RegularExpressions;
//using Sapphire.Core.Annotations;
//using Sapphire.Core.Config;
//using Sapphire.Core.Down;
//using Sapphire.Core.Properties;

//namespace Sapphire.Core.CommonHelper
//{
//    /// <summary>
//    /// 编辑器下载助手。
//    /// </summary>
//    public static class UeditorDownHelper
//    {
//        /// <summary>
//        /// 下载外链图片。
//        /// </summary>
//        /// <param name="model">模型对象。</param>
//        public static void DownImage(object model)
//        {
//            foreach (var property in model.GetType().GetProperties().Where(o => Attribute.IsDefined(o, typeof(UEditorAttribute))))
//            {
//                var value = property.GetValue(model);
//                if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
//                {
//                    property.SetValue(
//                        model,
//                        Regex.Replace(
//                            value.ToString(),
//                            Resources.Down_UeditorImage,
//                            o =>
//                            {
//                                if (!o.Value.Contains(SiteConfig.Instance.Domain))
//                                {
//                                    var result = DownProviders.Current.DownImage(o.Value);
//                                    if (result.IsSuccess)
//                                    {
//                                        return result.Path;
//                                    }
//                                }

//                                return o.Value;
//                            }));
//                }
//            }
//        }
//    }
//}