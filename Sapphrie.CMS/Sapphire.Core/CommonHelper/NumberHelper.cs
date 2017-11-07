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
//    public class NumberHelper
//    {
//        /// <summary>
//        /// 用于判断数字类型的ID，用逗号分开的，比如"1,2,3"
//        /// </summary>
//        /// <param name="value">时间字符串(15:00)</param>
//        /// <returns></returns>
//        public static bool IsNumberPlusComma(string value)
//        {
//            return new Regex(RegexPattern.NumberPlusComma).Match(value).Success;
//        }
//    }
//}
