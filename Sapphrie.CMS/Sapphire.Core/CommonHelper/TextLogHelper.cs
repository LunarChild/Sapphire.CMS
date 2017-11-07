using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapphire.Core.CommonHelper
{
    /// <summary>
    /// 
    /// </summary>
    public class TextLogHelper
    {
        private static object loker = new object();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="filePrefix"></param>
        public static void WriteLog(string msg, string filePrefix = "")
        {
            string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format(@"Log\{0}\", DateTime.Now.ToString("yyyyMM")));
            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }
            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            if (!string.IsNullOrEmpty(filePrefix))
                fileName = filePrefix + fileName;
            lock (loker)
            {
                using (StreamWriter sw = new StreamWriter(logDir + fileName, true))
                {
                    sw.WriteLine(DateTime.Now.ToString() + ":" + msg);
                    sw.WriteLine("---------------------------------------------------------");
                    sw.Close();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        /// <param name="filePrefix"></param>
        public static void WriteLog(string msg, Exception ex, string filePrefix = "")
        {
            if (ex != null)
            {
                msg += ex.Message + ", Exception=>" + ex.StackTrace;
            }
            if (ex.InnerException != null)
            {
                msg += ex.InnerException.Message + ", InnerException=>" + ex.InnerException.StackTrace;
            }
            string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format(@"Error\{0}\", DateTime.Now.ToString("yyyyMM")));
            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }
            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            if (!string.IsNullOrEmpty(filePrefix))
                fileName = filePrefix + fileName;
            lock (loker)
            {
                using (StreamWriter sw = new StreamWriter(logDir + fileName, true))
                {
                    sw.WriteLine(DateTime.Now.ToString() + ":" + msg + "，" + ex.StackTrace);
                    sw.WriteLine("---------------------------------------------------------");
                    sw.Close();
                }
            }
        }
    }
}