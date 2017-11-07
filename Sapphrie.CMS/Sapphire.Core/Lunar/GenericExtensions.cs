//using Sapphire.Core.Mvc;

//namespace Sapphire.Core.Power
//{
//    /// <summary>
//    /// 模板通用扩展函数。
//    /// </summary>
//    public static class GenericExtensions
//    {
//        /// <summary>
//        /// 对比两个值是否相等。
//        /// </summary>
//        /// <typeparam name="T">比较数据类型。</typeparam>
//        /// <param name="powerHelper">此方法扩展的 Power 帮助器实例。</param>
//        /// <param name="value1">第一个值。</param>
//        /// <param name="value2">第二个值。</param>
//        /// <param name="equalString">两值相等时返回的字符串。</param>
//        /// <returns>相等则返回指定字符串，不相等则返回空字符串。</returns>
//        public static string ValueCompare<T>(this PowerHelper powerHelper, T value1, T value2, string equalString)
//        {
//            return powerHelper.ValueCompare(value1, value2, equalString, string.Empty);
//        }

//        /// <summary>
//        /// 对比两个值是否相等。
//        /// </summary>
//        /// <typeparam name="T">比较数据类型。</typeparam>
//        /// <param name="powerHelper">此方法扩展的 Power 帮助器实例。</param>
//        /// <param name="value1">第一个值。</param>
//        /// <param name="value2">第二个值。</param>
//        /// <param name="equalString">两值相等时返回的字符串。</param>
//        /// <param name="notEqualString">两值不相等时返回的字符串。</param>
//        /// <returns>返回指定字符串。</returns>
//        public static string ValueCompare<T>(this PowerHelper powerHelper, T value1, T value2, string equalString, string notEqualString)
//        {
//            if (typeof(T) == typeof(string))
//            {
//                var stringValue1 = value1.ToString().ToLower();
//                var stringValue2 = value2.ToString().ToLower();
//                return stringValue1.Equals(stringValue2) ? equalString : notEqualString;
//            }

//            return value1.Equals(value2) ? equalString : notEqualString;
//        }
//    }
//}