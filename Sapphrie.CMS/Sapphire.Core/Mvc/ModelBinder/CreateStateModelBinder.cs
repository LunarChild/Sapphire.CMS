//using System;
//using System.ComponentModel;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using Sapphire.Core.Annotations;
//using Sapphire.Core.Utilities;
//using Sapphire.Core.Web;

//namespace Sapphire.Core.Mvc
//{
//    //代码疑难[戴培任][2015-06-01] 这里的模型绑定，出于什么考虑需要判断验证这么多的属性设置，还有是否匹配指定的前缀？
//    /// <summary>
//    /// 创建状态模型绑定。
//    /// </summary>
//    public class CreateStateModelBinder : DefaultModelBinder
//    {
//        /// <summary>
//        /// 绑定属性。
//        /// </summary>
//        /// <param name="controllerContext">运行控制器的上下文。</param>
//        /// <param name="bindingContext">绑定模型的上下文。</param>
//        /// <param name="propertyDescriptor">描述要绑定的属性。</param>
//        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor)
//        {
//            Check.NotNull(controllerContext, "controllerContext");

//            Check.NotNull(propertyDescriptor, "propertyDescriptor");

//            controllerContext.Controller.ViewBag.EditState = EditState.CreateState;
//            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);

//            if (propertyDescriptor.Attributes[typeof(CurrentDateAttribute)] != null)
//            {
//                this.SetProperty(controllerContext, bindingContext, propertyDescriptor, DateTime.Today);
//            }
//            else if (propertyDescriptor.Attributes[typeof(CurrentDateTimeAttribute)] != null)
//            {
//                this.SetProperty(controllerContext, bindingContext, propertyDescriptor, DateTime.Now);
//            }
//            else if (propertyDescriptor.Attributes[typeof(CurrentAdminAttribute)] != null)
//            {
//                this.SetProperty(controllerContext, bindingContext, propertyDescriptor, SiteContext.Current.Admin.AdministratorName);
//            }
//            else if (propertyDescriptor.Attributes[typeof(ClientIPAddressAttribute)] != null)
//            {
//                this.SetProperty(controllerContext, bindingContext, propertyDescriptor, HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
//            }
//            else if (propertyDescriptor.Attributes[typeof(PasswordAttribute)] != null)
//            {
//                var passwordValue = controllerContext.HttpContext.Request[propertyDescriptor.Name];
//                var passwordHash = passwordValue.Md5();
//                this.SetProperty(controllerContext, bindingContext, propertyDescriptor, passwordHash);
//            }
//            else if (propertyDescriptor.Attributes[typeof(GuidAttribute)] != null)
//            {
//                this.SetProperty(controllerContext, bindingContext, propertyDescriptor, Guid.NewGuid());
//            }
//            else if (propertyDescriptor.Attributes[typeof(PropertyBindAttribute)] != null)
//            {
//                var modelBindAttr = propertyDescriptor.Attributes[typeof(PropertyBindAttribute)] as PropertyBindAttribute;
//                if (modelBindAttr != null)
//                {
//                    modelBindAttr.BindProperty(controllerContext, bindingContext, propertyDescriptor);
//                }
//            }
//        }

//        /// <summary>
//        /// 模型绑定之后。
//        /// </summary>
//        /// <param name="controllerContext">运行控制器的上下文。</param>
//        /// <param name="bindingContext">绑定模型的上下文。</param>
//        protected override void OnModelUpdated(ControllerContext controllerContext, ModelBindingContext bindingContext)
//        {
//            base.OnModelUpdated(controllerContext, bindingContext);
//            var properties = this.GetModelProperties(controllerContext, bindingContext);
//            foreach (PropertyDescriptor property in properties)
//            {
//                if (property.Attributes[typeof(IgnoreValidationAttribute)] != null)
//                {
//                    bindingContext.ModelState.Keys.ToList().ForEach(
//                        m =>
//                        {
//                            if (this.IsPrefixMatch(property.Name, m))
//                            {
//                                bindingContext.ModelState.Remove(m);
//                            }
//                        });
//                }
//            }
//        }

//        /// <summary>
//        /// 当前字符串是否匹配到指定的前缀。
//        /// </summary>
//        /// <param name="prefix">指定的前缀。</param>
//        /// <param name="testString">指定字符串。</param>
//        /// <returns>匹配到结果。</returns>
//        private bool IsPrefixMatch(string prefix, string testString)
//        {
//            if (testString == null)
//            {
//                return false;
//            }

//            if (prefix.Length == 0)
//            {
//                return true;
//            }

//            if (prefix.Length > testString.Length)
//            {
//                return false;
//            }

//            if (!testString.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
//            {
//                return false;
//            }

//            if (testString.Length == prefix.Length)
//            {
//                return true;
//            }

//            switch (testString[prefix.Length])
//            {
//                case '.':
//                case '[':
//                    return true;

//                default:
//                    return false;
//            }
//        }
//    }
//}