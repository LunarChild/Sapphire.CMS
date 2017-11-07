//using System;
//using System.Data.Entity.Infrastructure;
//using System.Globalization;
//using System.Linq;
//using System.Web.Caching;
//using System.Web.Mvc;
//using log4net;
//using Sapphire.Core.Config;
//using Sapphire.Core.Utilities;

//namespace Sapphire.Core.Mvc
//{
//    /// <summary>
//    /// 表示一个自定义特性，该特性用于处理由操作方法引发的异常和过滤异常。
//    /// </summary>
//    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
//    public sealed class CustomHandleErrorAttribute : FilterAttribute, IExceptionFilter
//    {
//        private readonly object typeId = new object();

//        private Type exceptionType = typeof(Exception);

//        /// <summary>
//        /// 获取或设置用于显示异常信息的母版模板。
//        /// </summary>
//        public string Master { get; set; }

//        /// <summary>
//        /// 模板。
//        /// </summary>
//        public string View { get; set; }

//        /// <summary>
//        /// 获取或设置异常的类型。
//        /// </summary>
//        public Type ExceptionType
//        {
//            get
//            {
//                return this.exceptionType;
//            }

//            set
//            {
//                Check.NotNull(value, "value");

//                if (!typeof(Exception).IsAssignableFrom(value))
//                {
//                    throw new ArgumentException(
//                        string.Format(CultureInfo.CurrentCulture, "Looks up a localized string similar to The type &apos;{0}&apos; does not inherit from Exception..", value.FullName));
//                }

//                this.exceptionType = value;
//            }
//        }

//        /// <summary>
//        /// 类型Id。
//        /// </summary>
//        public override object TypeId
//        {
//            get
//            {
//                return this.typeId;
//            }
//        }

//        /// <summary>
//        /// 出现异常时执行。
//        /// </summary>
//        /// <param name="filterContext">操作筛选器上下文。</param>
//        public void OnException(ExceptionContext filterContext)
//        {
//            Check.NotNull(filterContext, "filterContext");
//            if (filterContext.IsChildAction)
//            {
//                return;
//            }

//            var isConcurrencyException = filterContext.Exception.GetType() == typeof(DbUpdateConcurrencyException);
//            if (isConcurrencyException)
//            {
//                if (filterContext.HttpContext.Request.IsAjaxRequest())
//                {
//                    var ex = (DbUpdateConcurrencyException)filterContext.Exception;
//                    var entry = ex.Entries.Single();
//                    var databaseEntry = entry.GetDatabaseValues();
//                    filterContext.Result = new MessageResult(MessageType.Error, databaseEntry == null ? "数据已经被删除。" : "数据已经被修改过，请刷新数据。");
//                }
//            }
//            else
//            {
//                var controllerName = (string)filterContext.RouteData.Values["controller"];
//                var actionName = (string)filterContext.RouteData.Values["action"];
//                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

//                //记录日志。
//                this.Log(filterContext);

//                //根据用户配置跳转至详情页面或友好页面。
//                var logConfig = ExceptionConfig.Instance;

//                if (filterContext.HttpContext.Request.IsAjaxRequest())
//                {
//                    filterContext.Result = logConfig.ErrorMessageType == 0
//                                               ? new MessageResult(MessageType.Error, "服务器错误")
//                                               : new MessageResult(MessageType.Error, "服务器错误", filterContext.Exception.Message);
//                }
//                else
//                {
//                    filterContext.HttpContext.Response.Clear();
//                    filterContext.Result = new ViewResult
//                    {
//                        //读取用户配置 显示友好页还是详细错误页。
//                        ViewName = (!string.IsNullOrEmpty(this.View)) ? this.View : logConfig.ErrorMessageType == 0 ? DefaultViewName.FriendlyViewName : DefaultViewName.ErrorViewName,
//                        MasterName = this.Master,
//                        ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
//                        TempData = filterContext.Controller.TempData
//                    };
//                    filterContext.HttpContext.Response.StatusCode = 500;
//                    filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
//                }
//            }

//            filterContext.ExceptionHandled = true;
//        }

//        /// <summary>
//        /// 获取一个hashcode。
//        /// </summary>
//        /// <param name="filterContext">filterContext。</param>
//        /// <returns>返回一个hashcode。</returns>
//        private static string GetHashCode(ExceptionContext filterContext)
//        {
//            var exception = filterContext.Exception;
//            var stackTrace = exception.StackTrace;
//            stackTrace = stackTrace.Length > 500 ? stackTrace.Substring(0, 500) : stackTrace;
//            var hashCode = (filterContext.HttpContext.Request.RawUrl + exception.Message + stackTrace).GetHashCode().ToString(CultureInfo.InvariantCulture);
//            return hashCode;
//        }

//        /// <summary>
//        /// 记录日志。
//        /// </summary>
//        /// <param name="filterContext">异常上下文。</param>
//        private void Log(ExceptionContext filterContext)
//        {
//            var logConfig = ExceptionConfig.Instance;
//            if (!logConfig.LogEnable)
//            {
//                return;
//            }

//            var exception = filterContext.Exception;

//            // 对指定异常之外的异常不记录。
//            if (!this.ExceptionType.IsInstanceOfType(exception))
//            {
//                return;
//            }

//            // 对action里面的可空参数异常不记录。
//            var argumentException = exception as ArgumentException;
//            if (argumentException != null && argumentException.ParamName == "parameters")
//            {
//                return;
//            }

//            var request = filterContext.HttpContext.Request;

//            //行号所在的index。
//            int lineIndex = exception.StackTrace.IndexOf("行号", StringComparison.Ordinal);
//            //位置所在的index  不需要显示位置所以+2。
//            int sourceIndex = exception.StackTrace.IndexOf("位置", StringComparison.Ordinal) + 2;

//            string sourceFile = exception.StackTrace.Substring(sourceIndex, lineIndex + 8 - sourceIndex);

//            GlobalContext.Properties["RawUrl"] = request.RawUrl;
//            GlobalContext.Properties["SourceFile"] = sourceFile;
//            GlobalContext.Properties["StackTrace"] = exception.StackTrace;
//            GlobalContext.Properties["UserHostAddress"] = request.UserHostAddress;

//            var log = LogManager.GetLogger("FileLogger");

//            //如果已经记录相同原因引发的异常，则根据用户配置来确定多久记录一次 。
//            var timeSpan = logConfig.RecordTimeSpan;

//            //如果用户设置不重复记录 则加入缓存。
//            if (timeSpan > 0)
//            {
//                var cache = filterContext.HttpContext.Cache;
//                var hashCode = GetHashCode(filterContext);
//                if (cache[hashCode] != null)
//                {
//                    return;
//                }

//                //把异常信息加入缓存，失效时间为 当前时间+timespan。
//                log.Error(exception.Message);
//                cache.Insert(hashCode, DateTime.UtcNow, null, DateTime.UtcNow.AddHours(timeSpan), Cache.NoSlidingExpiration);
//            }
//            else
//            {
//                log.Error(exception.Message);
//            }
//        }
//    }
//}