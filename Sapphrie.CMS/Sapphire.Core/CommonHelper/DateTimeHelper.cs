//using Sapphire.Core.Annotations;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;

//namespace Sapphire.Core.CommonHelper
//{
//    /// <summary>
//    /// 日期格式合法性检测。
//    /// </summary>
//    public class DateTimeHelper
//    {
//        /// <summary>
//        /// 是否为时间型字符串
//        /// </summary>
//        /// <param name="value">时间字符串(15:00)</param>
//        /// <returns></returns>
//        public static bool IsTime(string value)
//        {
//            return new Regex(RegexPattern.TimePath).Match(value).Success;
//        }
//        ///  <summary>
//        /// 是否为日期型字符串
//        /// </summary>
//        /// <param name="value">日期字符串(2019-09-14)</param>
//        /// <returns></returns>
//        public static bool IsDate(string value)
//        {           
//            return new Regex(RegexPattern.DatePath).Match(value.Replace('/', '-')).Success;
//        }
//        ///  <summary>
//        /// 是否为日期时间型字符串
//        /// </summary>
//        /// <param name="value">日期字符串(2019-09-14 16:00)</param>
//        /// <returns></returns>
//        public static bool IsDateTime(string value)
//        {
//            return new Regex(RegexPattern.DateTimePath).Match(value.Replace('/', '-')).Success;
//        }
//    }
//}
