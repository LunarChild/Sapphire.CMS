//using System;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Reflection;
//using Sapphire.Core.Annotations;

//namespace Sapphire.Core.Web
//{
//    /// <summary>
//    /// 模型类型数据提供者。
//    /// </summary>
//    internal class ModelTypeDataProvider
//    {
//        /// <summary>
//        /// 反射类型获取模型类型数据。。
//        /// </summary>
//        /// <param name="type">类型。</param>
//        /// <returns>返回模型类型数据。</returns>
//        internal static ModelTypeData ReflectClass(Type type)
//        {
//            var modelTypeData = new ModelTypeData();
//            var properties = type.GetProperties();

//            // 获取DisplayColumn对应的属性。
//            var displayColumnAttribute = type.GetCustomAttribute(typeof(DisplayColumnAttribute), true) as DisplayColumnAttribute;
//            if (displayColumnAttribute != null)
//            {
//                var displayColumnPropertyName = displayColumnAttribute.DisplayColumn;
//                modelTypeData.DisplayColumnProperty = properties.FirstOrDefault(x => x.Name == displayColumnPropertyName);
//            }

//            // 获取Id属性。
//            foreach (var property in properties)
//            {
//                var idAttribute = property.GetCustomAttributes(typeof(IdAttribute), true).FirstOrDefault();
//                if (idAttribute != null)
//                {
//                    modelTypeData.IdProperty = property;
//                    break;
//                }
//            }

//            var superordinateIdProperty = properties.FirstOrDefault(x => x.IsDefined(typeof(SuperordinateIdAttribute), true));
//            if (superordinateIdProperty != null)
//            {
//                modelTypeData.SuperordinateIdProperty = superordinateIdProperty;
//            }

//            return modelTypeData;
//        }
//    }
//}