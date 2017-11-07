//using System;
//using System.IO.Compression;
//using System.Web.Mvc;
//using Sapphire.Core.Utilities;

//namespace Sapphire.Core.Mvc
//{
//    /// <summary>
//    /// 文件压缩特性。
//    /// </summary>
//    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
//    public sealed class CompressFilterAttribute : ActionFilterAttribute
//    {
//        private bool isEnableCompression = true;

//        /// <summary>
//        /// Action方法执行前执行的方法。
//        /// </summary>
//        /// <param name="filterContext">ActionExecutingContext 对象。</param>
//        public override void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            Check.NotNull(filterContext, "filterContext");

//            if (filterContext.ActionDescriptor.IsDefined(typeof(NoCompressAttribute), false) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(NoCompressAttribute), false))
//            {
//                this.isEnableCompression = false;
//            }
//        }

//        /// <summary>
//        /// 返回结果集之后执行的方法。
//        /// </summary>
//        /// <param name="filterContext">结果执行上下文。</param>
//        public override void OnResultExecuted(ResultExecutedContext filterContext)
//        {
//            Check.NotNull(filterContext, "filterContext");

//            if (filterContext.HttpContext.Request.IsAjaxRequest())
//            {
//                return;
//            }

//            if (filterContext.Exception != null)
//            {
//                return;
//            }

//            if (this.isEnableCompression)
//            {
//                var request = filterContext.HttpContext.Request;
//                var response = filterContext.HttpContext.Response;
//                var acceptEncoding = request.Headers["Accept-Encoding"];

//                if (acceptEncoding == null)
//                {
//                    return;
//                }

//                if (acceptEncoding.ToLower().Contains("gzip"))
//                {
//                    response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
//                    response.AppendHeader("Content-Encoding", "gzip");
//                }
//                else if (acceptEncoding.ToLower().Contains("deflate"))
//                {
//                    response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
//                    response.AppendHeader("Content-Encoding", "deflate");
//                }
//            }
//        }
//    }
//}