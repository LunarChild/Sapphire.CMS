using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapphire.Core.CommonHelper
{
    /// <summary>
    ///
    /// </summary>
    public class BoolHelper
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="value"></param>
       /// <returns></returns>
        public static string IsBool(string value)
        {
            if (value == "0")
            {
                return "false";
            }
            else if (value == "1")
            {
                return "true";
            }
            else
            {
                return value;
            }
        }
    }
}
