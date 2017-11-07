//using System;
//using System.Collections.Generic;
//using System.Web.Mvc;
//using System.Web.WebPages;
//using Sapphire.Core.Web;

//namespace Sapphire.Core.Mvc
//{
//    /// <summary>
//    /// 角色权限验证器。
//    /// </summary>
//    public abstract class RoleActionPermissionAttribute : FilterAttribute, IAuthorizationFilter
//    {
//        private const string NoneParameterMessage = "没有任何参数值。";

//        /// <summary>
//        /// 验证上下文。
//        /// </summary>
//        private AuthorizationContext filterContext;

//        /// <summary>
//        /// 构造函数。
//        /// </summary>
//        /// <param name="parameterName"> 要进行验证的参数名称。</param>
//        protected RoleActionPermissionAttribute(string parameterName)
//        {
//            this.ParameterName = parameterName;
//        }

//        /// <summary>
//        /// 控制器名称。
//        /// </summary>
//        public string ControllerName { get; set; }

//        /// <summary>
//        /// 要验证的权限码。
//        /// </summary>
//        public string Purview { get; set; }

//        /// <summary>
//        /// 要进行验证的参数名称。
//        /// </summary>
//        protected string ParameterName { get; set; }

//        /// <summary>
//        /// 验证。
//        /// </summary>
//        /// <param name="context">筛选器上下文。</param>
//        public void OnAuthorization(AuthorizationContext context)
//        {
//            if (string.IsNullOrEmpty(this.Purview))
//            {
//                this.Purview = context.ActionDescriptor.ActionName;
//                if (this.Purview.Equals("Create", StringComparison.CurrentCultureIgnoreCase) || this.Purview.Equals("Modify", StringComparison.CurrentCultureIgnoreCase))
//                {
//                    this.Purview = "Edit";
//                }
//            }

//            var authorize = SiteManager.SiteAuthorize();

//            //TODO 缺少判断超管。
//            if (authorize == SiteUserIdentity.SupperSiteAdmin)
//            {
//                return;
//            }

//            this.filterContext = context;
//            this.ControllerName = context.ActionDescriptor.ControllerDescriptor.ControllerName;

//            // 非路由匹配的参数，需要在Request里取值。
//            var parameterValue = context.RouteData.Values[this.ParameterName] ?? context.HttpContext.Request[this.ParameterName];

//            if (parameterValue == null)
//            {
//                this.HandleUnauthorizedRequest(NoneParameterMessage);
//                return;
//            }

//            var parameter = parameterValue.ToString();

//            var parameters = parameter.Split(',');
//            if (parameters.Length > 1)
//            {
//                ICollection<int> ids = new List<int>();
//                foreach (var id in parameters)
//                {
//                    if (id.AsInt() > 0)
//                    {
//                        ids.Add(id.AsInt());
//                    }
//                }

//                this.AuthorizationCollection(ids);
//                return;
//            }

//            int idParameter;

//            if (int.TryParse(parameter, out idParameter))
//            {
//                this.Authorization(idParameter);
//            }
//        }

//        /// <summary>
//        /// 验证集合数据。
//        /// </summary>
//        /// <param name="ids">id集合。</param>
//        protected abstract void AuthorizationCollection(ICollection<int> ids);

//        /// <summary>
//        /// 验证数据。
//        /// </summary>
//        /// <param name="id">id。</param>
//        protected abstract void Authorization(int id);

//        /// <summary>
//        /// 处理未授权请求。
//        /// </summary>
//        /// <param name="message">错误信息。</param>
//        protected virtual void HandleUnauthorizedRequest(string message = null)
//        {
//            if (string.IsNullOrEmpty(message))
//            {
//                message = "没有相关的操作权限";
//            }

//            if (this.filterContext.HttpContext.Request.IsAjaxRequest())
//            {
//                this.filterContext.HttpContext.Response.StatusCode = 403;
//                this.filterContext.Result = this.filterContext.Result ?? new MessageResult(MessageType.Error, message);
//            }
//            else
//            {
//                this.filterContext.Result = this.filterContext.Result ?? new ErrorResult(message);
//            }
//        }
//    }
//}