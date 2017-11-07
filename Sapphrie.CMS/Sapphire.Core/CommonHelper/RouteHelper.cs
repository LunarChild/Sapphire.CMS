using System.Text.RegularExpressions;

namespace Sapphire.Core.CommonHelper
{
    /// <summary>
    /// 路由助手。
    /// </summary>
    public static class RouteHelper
    {
        /// <summary>
        /// 将路由URL模式转为Regex对象实例。
        /// </summary>
        /// <param name="url">路由URL模式。</param>
        /// <returns>Regex对象实例。</returns>
        public static Regex CreateRegex(string url)
        {
            url = url.Replace("/", @"\/?");
            url = url.Replace(".", @"\.?");
            url = url.Replace("-", @"\-?");
            url = url.Replace("{", @"(?<");
            url = url.Replace("}", @">([a-zA-Z0-9_]*))");

            return new Regex("^" + url + "$", RegexOptions.IgnoreCase);
        }
    }
}