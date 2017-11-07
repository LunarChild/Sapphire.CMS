using System;
using System.ComponentModel;

namespace Sapphire.Core.CommonHelper
{
    /// <summary>
    /// 数据类型转换帮助类。
    /// </summary>
    public static class ConvertTypeHelper
    {
        /// <summary>
        /// 将string类型转换成任意基本类型。
        /// </summary>
        /// <param name="str">待转换的字符串。</param>
        /// <param name="type">转换到的数据类型。</param>
        /// <returns>转换后的数据。</returns>
        public static object Convert(string str, Type type)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            if (type == null)
            {
                return str;
            }

            if (type.IsArray)
            {
                var elementType = type.GetElementType();
                var strs = str.Split(new[] { ';' });
                var array = Array.CreateInstance(elementType, strs.Length);
                for (int i = 0, c = strs.Length; i < c; ++i)
                {
                    array.SetValue(ConvertSimpleType(strs[i], elementType), i);
                }

                return array;
            }

            return ConvertSimpleType(str, type);
        }

        private static object ConvertSimpleType(object value, Type destinationType)
        {
            object returnValue;
            if ((value == null) || destinationType.IsInstanceOfType(value))
            {
                return value;
            }

            var str = value as string;
            if ((str != null) && (str.Length == 0))
            {
                return null;
            }

            var converter = TypeDescriptor.GetConverter(destinationType);
            var flag = converter.CanConvertFrom(value.GetType());
            if (!flag)
            {
                converter = TypeDescriptor.GetConverter(value.GetType());
            }

            if (!flag && !converter.CanConvertTo(destinationType))
            {
                throw new InvalidOperationException("无法转换成类型：" + value + "==>" + destinationType);
            }

            try
            {
                returnValue = flag ? converter.ConvertFrom(null, null, value) : converter.ConvertTo(null, null, value, destinationType);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("类型转换出错：" + value + "==>" + destinationType, e);
            }

            return returnValue;
        }
    }
}