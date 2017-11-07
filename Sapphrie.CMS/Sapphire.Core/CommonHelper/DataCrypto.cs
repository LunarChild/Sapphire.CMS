using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Sapphire.Core.CommonHelper
{
    /// <summary>
    /// 数据加密、解密类。
    /// </summary>
    public static class DataCrypto
    {
        /// <summary>
        /// 对输入字符串进行MD5加密，返回小写形式的加密字符串，字符串为32字符的十六进制格式。
        /// </summary>
        /// <param name="input">待加密的字符串。</param>
        /// <returns>加密后的字符串。</returns>
        public static string Md5(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            using (var md5 = new MD5CryptoServiceProvider())
            {
                var data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(data).Replace("-", string.Empty).ToLower(CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        /// 旧版中密码哈希值保存为16位，在新版中采用32位保存。
        /// 将需验证的密码哈希值分别与密码明文MD5加密后的32位字符串以及密码MD5加密后从8位开始取16位的字符串进行比较。
        /// 两个条件满足其一，则验证通过。
        /// </summary>
        /// <param name="hashValue">需要对比的密码哈希值。</param>
        /// <param name="plaintext">密码明文。</param>
        /// <returns>如果验证正确，则为 true；否则为 false。</returns>
        public static bool ValidateMd5(this string hashValue, string plaintext)
        {
            var encryptedValue = plaintext.Md5();
            return (string.Compare(hashValue, encryptedValue, StringComparison.Ordinal) == 0) || (string.Compare(hashValue, encryptedValue.Substring(8, 16), StringComparison.Ordinal) == 0);
        }

        /// <summary>
        /// 暂时处理，待改进。
        /// 对输入字符串进行SHA1加密，返回小写形式的加密字符串，字符串为40字符的十六进制格式。
        /// </summary>
        /// <param name="input">待加密的字符串。</param>
        /// <returns>加密后的字符串。</returns>
        public static string Sha1(this string input)
        {
            var algorithm = SHA1.Create();
            var data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            return data.Aggregate(string.Empty, (current, t) => current + t.ToString("x2").ToUpperInvariant());
        }
    }
}