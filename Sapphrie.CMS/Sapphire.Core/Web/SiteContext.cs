using System;
using System.Collections.Specialized;
using System.Threading;
using System.Web;
using Sapphire.Core.Principal;

namespace Sapphire.Core.Web
{
    /// <summary>
    /// 封装个别 HTT P请求的所有 HTTP 的特定信息。
    /// </summary>
    public class SiteContext
    {
        private const string DataKey = "SapphireContextStore";

        private const string CurrentSiteIdKey = "SiteId";

        private const string CurrentSiteIdentifier = "SiteIdentifier";

        private readonly HttpContext httpContext;

        private readonly NameValueCollection queryString;

        private AdminPrincipal admin;

        private int siteId;

        private string siteIdentifier;

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="context">HttpContext。</param>
        /// <param name="includeQueryString">是否包含QueryString。</param>
        private SiteContext(HttpContext context, bool includeQueryString)
        {
            this.httpContext = context;
            this.queryString = includeQueryString ? new NameValueCollection(context.Request.QueryString) : null;
        }

        /// <summary>
        /// 从数据槽中返回一个实例化的上下文。
        /// </summary>
        public static SiteContext Current
        {
            get
            {
                var httpContext = HttpContext.Current;
                SiteContext context;
                if (httpContext != null)
                {
                    context = httpContext.Items[DataKey] as SiteContext;
                }
                else
                {
                    context = Thread.GetData(GetSlot()) as SiteContext;
                }

                if (context == null)
                {
                    context = new SiteContext(httpContext, true);
                    SaveContextToStore(context);
                }

                return context;
            }
        }

        /// <summary>
        /// HTTP请求上下文。
        /// </summary>
        public HttpContext Context
        {
            get
            {
                return this.httpContext;
            }
        }

        /// <summary>
        /// HTTP查询字符串变量集合。
        /// </summary>
        public NameValueCollection QueryString
        {
            get
            {
                return this.queryString;
            }
        }

        /// <summary>
        /// 检查HttpContext是否为null，如果 HttpContext == null，则返回false。
        /// </summary>
        public bool IsWebRequest
        {
            get
            {
                return this.Context != null;
            }
        }

        /// <summary>
        /// 是否平台。
        /// </summary>
        public bool IsPlatform { get; set; }

        /// <summary>
        /// 管理员主体。
        /// </summary>
        public AdminPrincipal Admin
        {
            get
            {
                return this.admin ?? (this.admin = new AdminPrincipal(new NoAuthenticateIdentity(), null));
            }

            set
            {
                this.admin = value;
            }
        }

        /// <summary>
        /// 当前站点Id。
        /// </summary>
        public int SiteId
        {
            get
            {
                return this.siteId == 0 ? this.siteId = Convert.ToInt32(HttpContext.Current.Request.RequestContext.RouteData.Values[CurrentSiteIdKey]) : this.siteId;
            }
        }

        /// <summary>
        /// 当前站点标识符。
        /// </summary>
        public string SiteIdentifier
        {
            get
            {
                if (string.IsNullOrEmpty(this.siteIdentifier))
                {
                    this.siteIdentifier = HttpContext.Current.Request.RequestContext.RouteData.Values[CurrentSiteIdentifier].ToString();
                }

                return this.siteIdentifier;
            }
        }

        /// <summary>
        /// 用HttpContext创建一个PEContext实例。
        /// 这个方法必需在一个HttpModule的Begin_Request中调用。
        /// </summary>
        /// <param name="context">HttpContext。</param>
        /// <returns>SiteContext。</returns>
        public static SiteContext Create(HttpContext context)
        {
            var currentContext = new SiteContext(context, true);
            SaveContextToStore(currentContext);
            return currentContext;
        }

        /// <summary>
        /// 释放内存槽中的数据。
        /// </summary>
        public static void Unload()
        {
            Thread.FreeNamedDataSlot(DataKey);
        }

        /// <summary>
        /// 存储上下文。
        /// </summary>
        /// <param name="context">上下文。</param>
        public static void SavePlatformState(SiteContext context)
        {
            context.Context.Items[DataKey] = context;
        }

        /// <summary>
        /// 存储上下文。
        /// </summary>
        /// <param name="context">上下文。</param>
        private static void SaveContextToStore(SiteContext context)
        {
            if (context.IsWebRequest)
            {
                context.Context.Items[DataKey] = context;
            }
            else
            {
                Thread.SetData(GetSlot(), context);
            }
        }

        /// <summary>
        /// 获取内存槽中的数据。
        /// </summary>
        /// <returns>内存槽中的数据。</returns>
        private static LocalDataStoreSlot GetSlot()
        {
            return Thread.GetNamedDataSlot(DataKey);
        }
    }
}