using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// Json格式输出结果。
    /// </summary>
    public class JsonNetResult : ActionResult
    {
        /// <summary>
        /// 需要转换成Json格式的数据。
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 用于设置Json序列化时的参数。
        /// </summary>
        public JsonSerializerSettings Settings { get; set; }

        /// <summary>
        /// 执行结果。
        /// </summary>
        /// <param name="context">用于执行结果的上下文。</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;
            //response.ContentType = "application/json";
            response.ContentType = "text/html";
            response.ContentEncoding = Encoding.UTF8;

            if (this.Data != null)
            {
                response.Write(JsonConvert.SerializeObject(this.Data, this.Settings));
            }
        }
    }
}