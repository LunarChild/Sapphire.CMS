using System;
namespace Sapphire.Core.CommonHelper
{
    // UNDONE [2013-11-15]  研究契约式编程Contract

    /// <summary>
    /// 方法参数通用检查类。
    /// </summary>
    internal class Check
    {
        public static T NotNull<T>(T value, string parameterName) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        public static T? NotNull<T>(T? value, string parameterName) where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        public static string NotEmpty(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(string.Format(parameterName));
            }

            return value;
        }
    }
}