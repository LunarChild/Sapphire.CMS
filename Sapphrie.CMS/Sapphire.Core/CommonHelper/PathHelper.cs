using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Sapphire.Core.Config;
using Sapphire.Core.Mvc;

namespace Sapphire.Core.CommonHelper
{
    /// <summary>
    /// 路径助手类。
    /// </summary>
    public class PathHelper
    {
        /// <summary>
        /// 后台目录。
        /// </summary>
        public const string ManagePath = "~/Admin/";

        /// <summary>
        /// 后台目录替换符。
        /// </summary>
        public const string ManagePathReplaceSymbol = "!/";

        /// <summary>
        /// 上传目录替换符。
        /// </summary>
        public const string UploadPathReplaceSymbol = "$/";

        /// <summary>
        /// 上传虚拟目录。
        /// </summary>
        public static readonly string UploadVirtualPath = string.Format("~/{0}/", GlobalUploadConfig.Instance.UploadDirectory);

        /// <summary>
        /// 生成内容路径。
        /// </summary>
        /// <param name="path">路径。</param>
        /// <returns>返回内容路径。</returns>
        public static string GetContentUrl(string path)
        {
            //Check.NotEmpty(path, "path");
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

            // 验证path是否外部地址
            var regex = new Regex(@"^ (https ?| ftp):\/\/ (((([a - z] |\d | -|\.| _ | ~|[\u00A0-\uD7FF\uF900 -\uFDCF\uFDF0 -\uFFEF])| (%[\da - f]{ 2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/ (([a - z] |\d | -|\.| _ | ~|[\u00A0-\uD7FF\uF900 -\uFDCF\uFDF0 -\uFFEF])| (%[\da - f]{ 2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF] |\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$", RegexOptions.IgnoreCase);
            var match = regex.Match(path);
            var isUploadPath = false;

            // 外部地址直接输出
            if (match.Success)
            {
                return path;
            }

            if (path.StartsWith(ManagePathReplaceSymbol))
            {
                path = path.Replace(ManagePathReplaceSymbol, ManagePath);
            }
            else if (path.StartsWith(UploadPathReplaceSymbol))
            {
                isUploadPath = true;
                path = path.Replace(UploadPathReplaceSymbol, UploadVirtualPath);
            }
            else if (path.Trim().StartsWith("{") && path.Trim().EndsWith("}"))
            {
                var jsonString = JsonConvert.DeserializeObject<SapphireRouteData>(path);
                path = urlHelper.RouteUrl(jsonString.RouteName, jsonString.RouteValues);
            }

            if (path != null)
            {
                if (!path.StartsWith("~/") && !path.StartsWith("/"))
                {
                    path = "~/" + path;
                }

                var parameterIndex = path.LastIndexOf("?", StringComparison.Ordinal);

                if (parameterIndex > 0)
                {
                    string leftPart = path.Substring(0, parameterIndex);
                    string queryPart = path.Substring(parameterIndex);
                    var list = new List<string>();
                    foreach (var item in queryPart.Split('&'))
                    {
                        var stringBuilder = new StringBuilder();
                        var splitItem = item.Split('=');
                        stringBuilder.Append(splitItem[0].ToLower());
                        stringBuilder.Append("=");
                        stringBuilder.Append(splitItem[1]);
                        list.Add(stringBuilder.ToString());
                    }

                    path = leftPart.ToLower() + string.Join("&", list);
                }
                else
                {
                    path = path.ToLower();
                }

                path = urlHelper.Content(path);
            }

            return isUploadPath ? GlobalUploadConfig.Instance.UploadPathPerfix + path : path;
        }

        /// <summary>
        /// 替换文本字符串里面的上传路径。
        /// </summary>
        /// <param name="body">需要替换的文本。</param>
        /// <returns>替换后的文本。</returns>
        public static string ReplaceUploadPath(string body)
        {
            if (!string.IsNullOrEmpty(body))
            {
                var uploadPath = VirtualPathUtility.ToAbsolute(UploadVirtualPath);
                return Regex.Replace(body, "((src|href)\\s*=\\s*(\"|\'))\\$\\/", "$1" + GlobalUploadConfig.Instance.UploadPathPerfix + uploadPath);
            }

            return body;
        }

        /// <summary>
        /// 获取上传目录。
        /// </summary>
        /// <param name="path">需要替换的目录。</param>
        /// <returns>替换后的目录。</returns>
        public static string GetUploadPath(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                var uploadPath = VirtualPathUtility.ToAbsolute(UploadVirtualPath);
                return Regex.Replace(path, "^\\$", GlobalUploadConfig.Instance.UploadPathPerfix + uploadPath);
            }

            return path;
        }
    }
}