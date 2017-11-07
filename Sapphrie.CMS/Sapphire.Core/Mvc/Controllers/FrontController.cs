using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 前台控制器基类。
    /// </summary>
    public class FrontController : Controller
    {
        /// <summary>
        /// 使用模板名称创建一个呈现指定IView对象的ViewResult对象。
        /// </summary>
        /// <param name="viewName">为相应呈现的模板。</param>
        /// <param name="model">模板呈现的模型。</param>
        /// <returns>指定模板名称的ViewResult对象。</returns>
        public ViewResult TemplateView(string viewName, object model)
        {
            if (viewName.StartsWith("/"))
            {
                viewName = "~/Views" + viewName;
            }
            else
            {
                viewName = "~/Views/" + viewName;
            }

            var virtualPathDisplayInfo = DisplayModeProvider.Instance.GetDisplayInfoForVirtualPath(
                viewName,
                this.ControllerContext.HttpContext,
                HostingEnvironment.VirtualPathProvider.FileExists,
                this.ControllerContext.DisplayMode);
            if (virtualPathDisplayInfo != null)
            {
                viewName = virtualPathDisplayInfo.FilePath;
            }

            return this.View(viewName, model);
        }

        /// <summary>
        /// 调用JsonNetResult对输入的Object进行json格式的转化。
        /// </summary>
        /// <param name="data">一个需要序列化的对象。</param>
        /// <returns>返回Json格式的数据。</returns>
        protected JsonNetResult JsonNet(object data)
        {
            return new JsonNetResult { Data = data };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ViewResult Message(string message)
        {
            return this.TemplateView("_Common/CRM/Shared/Message.cshtml",message);
        }
    }
}