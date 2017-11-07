//using System.Web.Mvc;
//using Sapphire.Core.Config;
//using Sapphire.Core.Properties;

//namespace Sapphire.Core.Mvc
//{
//    /// <summary>
//    /// 配置控制器基类。
//    /// </summary>
//    /// <typeparam name="TConfig">配置信息类。</typeparam>
//    public abstract class ConfigController<TConfig> : AdminBaseController
//        where TConfig : BaseConfig<TConfig>, new()
//    {
//        /// <summary>
//        /// 配置控制器基类构造函数。
//        /// </summary>
//        protected ConfigController()
//        {
//            this.ModelDisplayName = this.GetModelDisplayName(typeof(TConfig));
//            this.ViewBag.ModelDisplayName = this.ModelDisplayName;
//        }

//        /// <summary>
//        /// 模型显示名称。
//        /// </summary>
//        protected string ModelDisplayName { get; private set; }

//        /// <summary>
//        /// 呈现配置页模板。
//        /// </summary>
//        /// <returns>操作方法结果。</returns>
//        public virtual ActionResult Config()
//        {
//            if (this.ViewBag.PageTitle == null)
//            {
//                this.ViewBag.PageTitle = this.ModelDisplayName;
//            }

//            return this.View(DefaultViewName.ConfigViewName, BaseConfig<TConfig>.Instance);
//        }

//        /// <summary>
//        /// 保存配置文件，返回保存后的配置页模板。
//        /// </summary>
//        /// <param name="config">配置信息对象。</param>
//        /// <returns>操作方法结果。</returns>
//        /// <remarks>
//        /// 返回成功信息，并跳转到配置页模板。
//        /// </remarks>
//        [HttpPost]
//        [ValidateInput(false)]
//        public virtual ActionResult Config(TConfig config)
//        {
//            config.Save();
//            var currentUrl = string.Empty;
//            if (this.Request.Url != null)
//            {
//                currentUrl = this.Request.Url.ToString();
//            }

//            return this.SuccessMessage(string.Format(Resources.ConfigController_SaveSuccessMessage, this.ModelDisplayName), currentUrl);
//        }
//    }
//}