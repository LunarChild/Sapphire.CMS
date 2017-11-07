//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Sapphire.Core.CommonHelper
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    public static class EnumExtensions
//    {

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="em"></param>
//        /// <returns></returns>
//        public static string GetDisplayName(this Enum em)
//        {
//            Type type = em.GetType();
//            var d1 = type.GetField(Enum.GetName(type,em));
//            var d2 = d1.GetCustomAttributes(typeof(DisplayAttribute), false);
//            var d3 = d2.FirstOrDefault();
//            var displayAttribute = d3 as DisplayAttribute;
//            return displayAttribute != null ? displayAttribute.Name : "";
//        }
//    }
//}
