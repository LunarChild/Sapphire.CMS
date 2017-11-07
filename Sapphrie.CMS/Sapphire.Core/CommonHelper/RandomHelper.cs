using System;
using System.Text;

namespace Sapphire.Core.CommonHelper
{
    /// <summary>
    /// 随机数类。
    /// </summary>
    public static class RandomHelper
    {
        private static readonly Random Rand = new Random(unchecked((int)DateTime.Now.Ticks));

        // UNDONE [2013-09-18]  http://stackoverflow.com/questions/1344221/how-can-i-generate-random-8-character-alphanumeric-strings-in-c 此贴中有讨论随机数生成的方法

        /// <summary>
        /// 获取指定长度和字符的随机字符串。
        /// 通过调用 Random 类的 Next() 方法，先获得一个大于或等于 0 而小于 pwdchars 长度的整数。
        /// 以该数作为索引值，从可用字符串中随机取字符，以指定的密码长度为循环次数。
        /// 依次连接取得的字符，最后即得到所需的随机密码串了。
        /// </summary>
        /// <param name="chars">随机字符串里包含的字符。</param>
        /// <param name="length">随机字符串的长度。</param>
        /// <returns>随机产生的字符串。</returns>
        public static string GetRandomString(string chars, int length)
        {
            var randomString = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                var randNum = Rand.Next(chars.Length);
                randomString.Append(chars[randNum]);
            }

            return randomString.ToString();
        }

        /// <summary>
        /// 获取指定长度的随机字符串。
        /// </summary>
        /// <param name="length">随机字符串的长度。</param>
        /// <returns>随机产生的字符串。</returns>
        public static string GetRandomString(int length)
        {
            return GetRandomString("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_*", length);
        }

        /// <summary>
        /// 获取指定长度的无符号随机字符串。
        /// </summary>
        /// <param name="length">随机字符串的长度。</param>
        /// <returns>随机产生无符号的字符串。</returns>
        public static string GetNoSymbolRandomString(int length)
        {
            return GetRandomString("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length);
        }
    }
}