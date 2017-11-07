using System.Text;
using System.Web.Mvc;

namespace Sapphire.Core.Lunar
{
    /// <summary>
    /// 字符串帮助类。
    /// </summary>
    public class SapphireStringHelper
    {
        /// <summary>
        /// 获取固定长度文字（全角字符算两个）。
        /// </summary>
        /// <param name="originalString">原始字串。</param>
        /// <param name="length">输出长度。</param>
        /// <param name="substring">超长后的标识字符，为none时不输出。</param>
        /// <returns>格式后的字符。</returns>
        public MvcHtmlString CutText(string originalString, int length, string substring)
        {
            return MvcHtmlString.Create(this.SubString(originalString, length, substring));
        }

        /// <summary>
        /// 截取字符串。
        /// </summary>
        /// <param name="demand">要截取的字符串。</param>
        /// <param name="length">截取长度。</param>
        /// <param name="substitute">替换字符串。</param>
        /// <returns>截取后的字符串。</returns>
        private string SubString(string demand, int length, string substitute)
        {
            if (string.IsNullOrEmpty(demand))
            {
                return string.Empty;
            }

            if (Encoding.GetEncoding("GB2312").GetByteCount(demand) > length)
            {
                var ascii = new ASCIIEncoding();
                length = length - Encoding.GetEncoding("GB2312").GetByteCount(substitute);
                var factualLength = 0;
                var sb = new StringBuilder();
                var s = ascii.GetBytes(demand);
                for (var i = 0; i < s.Length; i++)
                {
                    // 判断是否为汉字或全角符号
                    if (s[i] == 63)
                    {
                        factualLength += 2;
                    }
                    else
                    {
                        factualLength += 1;
                    }

                    if (factualLength > length)
                    {
                        break;
                    }

                    sb.Append(demand.Substring(i, 1));
                }

                sb.Append(substitute);
                return sb.ToString();
            }

            return demand;
        }
    }
}