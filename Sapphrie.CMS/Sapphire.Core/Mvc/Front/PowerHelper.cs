//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Routing;
//using Sapphire.Core.Power;
//using Sapphire.Core.Utilities;

//namespace Sapphire.Core.Mvc
//{
//    /// <summary>
//    /// 在模板中呈现 HTML 的助手类。
//    /// </summary>
//    public class PowerHelper
//    {
//        /// <summary>
//        /// 分页模型字段。
//        /// </summary>
//        private PageModel pageModel;

//        /// <summary>
//        /// 初始化 PowerHelper 类的新实例。
//        /// </summary>
//        /// <param name="viewContext">模板上下文。</param>
//        /// <param name="viewDataContainer">模板数据容器。</param>
//        /// <param name="htmlHelper">此方法扩展的HTML帮助器实例。</param>
//        public PowerHelper(ViewContext viewContext, IViewDataContainer viewDataContainer, HtmlHelper htmlHelper)
//            : this(viewContext, viewDataContainer, RouteTable.Routes, htmlHelper)
//        {
//        }

//        /// <summary>
//        /// 模板引擎。
//        /// </summary>
//        /// <param name="viewContext">模板上下文。</param>
//        /// <param name="viewDataContainer">模板数据容器。</param>
//        /// <param name="routeCollection">路由集合。</param>
//        /// <param name="htmlHelper">此方法扩展的HTML帮助器实例。</param>
//        public PowerHelper(ViewContext viewContext, IViewDataContainer viewDataContainer, RouteCollection routeCollection, HtmlHelper htmlHelper)
//        {
//            Check.NotNull(viewContext, "viewContext");

//            Check.NotNull(viewDataContainer, "viewDataContainer");

//            Check.NotNull(routeCollection, "routeCollection");

//            this.ViewContext = viewContext;
//            this.ViewDataContainer = viewDataContainer;
//            this.RouteCollection = routeCollection;
//            this.HtmlHelper = htmlHelper;
//            this.Url = new PowerUrlHelper();
//            this.String = new PowerStringHelper();
//            this.Route = new SapphireRouteHelper();
//            this.Control = new PowerControlHelper(this.HtmlHelper);
//        }

//        /// <summary>
//        /// 分页模型属性。
//        /// </summary>
//        public PageModel PageModel
//        {
//            get
//            {
//                return this.pageModel ?? (this.pageModel = HttpContext.Current.Items[EngineHelper.PageModelKey] as PageModel);
//            }
//        }

//        /// <summary>
//        /// 字符串帮助类。
//        /// </summary>
//        public PowerStringHelper String { get; private set; }

//        /// <summary>
//        /// 前台控件帮助类。
//        /// </summary>
//        public PowerControlHelper Control { get; private set; }

//        /// <summary>
//        /// 路径帮助类。
//        /// </summary>
//        public PowerUrlHelper Url { get; private set; }

//        /// <summary>
//        /// 路由帮助类。
//        /// </summary>
//        public SapphireRouteHelper Route { get; private set; }

//        /// <summary>
//        /// HtmlHelper。
//        /// </summary>
//        internal HtmlHelper HtmlHelper { get; private set; }

//        /// <summary>
//        /// 模板上下文。
//        /// </summary>
//        internal ViewContext ViewContext { get; private set; }

//        /// <summary>
//        /// 模板数据。
//        /// </summary>
//        internal ViewDataDictionary ViewData
//        {
//            get
//            {
//                return this.ViewDataContainer.ViewData;
//            }
//        }

//        /// <summary>
//        /// 路由集合。
//        /// </summary>
//        protected RouteCollection RouteCollection { get; private set; }

//        /// <summary>
//        /// 模板数据容器。
//        /// </summary>
//        protected IViewDataContainer ViewDataContainer { get; private set; }

//        /// <summary>
//        /// Ajax调用标签。
//        /// </summary>
//        /// <param name="moduleName">模块名称（调用本模块中标签时，输入null）。</param>
//        /// <param name="partialViewName">标签名称。</param>
//        /// <param name="updateTargetId">更新目标元素Id。</param>
//        /// <param name="parameters">标签参数。</param>
//        /// <returns>返回Jquery Ajax调用函数。</returns>
//        public MvcHtmlString AjaxLabel(string moduleName, string partialViewName, string updateTargetId, params string[] parameters)
//        {
//            if (string.IsNullOrEmpty(partialViewName) || string.IsNullOrEmpty(updateTargetId))
//            {
//                return new MvcHtmlString("Ajax调用标签失败，请确保参数正确！");
//            }

//            var sb = new StringBuilder();
//            sb.Append("<script type=\"text/javascript\">");
//            sb.Append("$.ajax({");
//            sb.Append("url: '" + EngineHelper.AjaxLabelPath + "', ");
//            sb.Append("data: {moduleName: \"" + moduleName + "\", labelName: \"" + partialViewName + "\", labelparameters: \"" + this.ParametersArrayToString(parameters) + "\"},");
//            sb.Append("success: function (response) {");
//            sb.Append("$('#" + updateTargetId + "').html(response);");
//            sb.Append("}");
//            sb.Append(",error: function (response){");
//            sb.Append("alert(\"Ajax调用" + partialViewName + "出现错误！\");");
//            sb.Append("}");
//            sb.Append("})");
//            sb.Append("</script>");

//            return new MvcHtmlString(sb.ToString());
//        }

//        /// <summary>
//        /// Ajax调用标签，自定义回调函数。
//        /// </summary>
//        /// <param name="moduleName">模块名称（调用本模块中标签时，输入null）。</param>
//        /// <param name="partialViewName">标签名称。</param>
//        /// <param name="beforeSendFunction">发送请求前回调函数（不需要绑定发送请求前回调函数时，输入null）。</param>
//        /// <param name="successFunction">调用成功回调函数。</param>
//        /// <param name="errorFunction">调用失败回调函数（不需要绑定失败回调函数时，输入null）。</param>
//        /// <param name="parameters">标签参数。</param>
//        /// <returns>返回Jquery Ajax调用函数。</returns>
//        public MvcHtmlString AjaxLabelCallBack(string moduleName, string partialViewName, string beforeSendFunction, string successFunction, string errorFunction, params string[] parameters)
//        {
//            if (string.IsNullOrEmpty(partialViewName) || string.IsNullOrEmpty(successFunction))
//            {
//                return new MvcHtmlString("Ajax调用标签失败，请确保参数正确！");
//            }

//            var sb = new StringBuilder();
//            sb.Append("<script type=\"text/javascript\">");
//            sb.Append("$.ajax({");
//            sb.Append("url: '" + EngineHelper.AjaxLabelPath + "', ");
//            sb.Append("data: {moduleName: \"" + moduleName + "\", labelName: \"" + partialViewName + "\", labelparameters: \"" + this.ParametersArrayToString(parameters) + "\"},");

//            if (!string.IsNullOrEmpty(beforeSendFunction))
//            {
//                sb.Append("beforeSend: function (request) {");
//                sb.Append(beforeSendFunction + " (request); ");
//                sb.Append("},");
//            }

//            sb.Append("success: function (response) {");
//            sb.Append(successFunction + " (response); ");
//            sb.Append("}");
//            sb.Append(",error: function (response){");

//            if (string.IsNullOrEmpty(errorFunction))
//            {
//                sb.Append("alert(\"Ajax调用" + partialViewName + "出现错误！\");");
//            }
//            else
//            {
//                sb.Append(errorFunction + " (response);");
//            }

//            sb.Append("}");
//            sb.Append("})");
//            sb.Append("</script>");

//            return new MvcHtmlString(sb.ToString());
//        }

//        /// <summary>
//        /// 使用Sql语句查询，获取数据表。
//        /// </summary>
//        /// <param name="queryString">Sql语句。</param>
//        /// <param name="parameters">查询参数。</param>
//        /// <returns>返回数据表。</returns>
//        public DataTable GetDataBySql(string queryString, Parameters parameters)
//        {
//            return this.QueryDataTable(queryString, parameters);
//        }

//        /// <summary>
//        /// 使用Sql语句查询，获取标量值。
//        /// </summary>
//        /// <param name="queryString">Sql语句。</param>
//        /// <param name="parameters">查询参数。</param>
//        /// <returns>返回标量值。</returns>
//        public object GetScalarBySql(string queryString, Parameters parameters)
//        {
//            return this.QueryScalar(queryString, parameters);
//        }

//        /// <summary>
//        /// 获取分页数据。
//        /// </summary>
//        /// <param name="pageIndex">当前页。</param>
//        /// <param name="strColumn1">查询字段1，多个字段之间用“,”分隔。</param>
//        /// <param name="tableName">表名，多个表名之间用“,”分隔。</param>
//        /// <param name="sortColumn">排序字段，排序字段后可以跟排序方式“ASC”，“DESC”。</param>
//        /// <returns>返回分页数据。</returns>
//        public DataTable GetDataForPage(int pageIndex, string strColumn1, string tableName, string sortColumn)
//        {
//            return this.GetDataForPage(pageIndex, strColumn1, tableName, sortColumn, string.Empty, null);
//        }

//        /// <summary>
//        /// 获取分页数据。
//        /// </summary>
//        /// <param name="pageIndex">当前页。</param>
//        /// <param name="strColumn1">查询字段1，多个字段之间用“,”分隔。</param>
//        /// <param name="tableName">表名，多个表名之间用“,”分隔。</param>
//        /// <param name="sortColumn">排序字段，排序字段后可以跟排序方式“ASC”，“DESC”。</param>
//        /// <param name="filter">条件语句。</param>
//        /// <param name="parameters">查询参数。</param>
//        /// <returns>返回分页数据。</returns>
//        public DataTable GetDataForPage(int pageIndex, string strColumn1, string tableName, string sortColumn, string filter, Parameters parameters)
//        {
//            return this.GetDataForPage(pageIndex, strColumn1, string.Empty, tableName, sortColumn, filter, parameters);
//        }

//        /// <summary>
//        /// 获取分页数据。
//        /// </summary>
//        /// <param name="pageIndex">当前页。</param>
//        /// <param name="strColumn1">查询字段1，多个字段之间用“,”分隔。</param>
//        /// <param name="strColumn2">查询字段2，多个字段之间用“,”分隔。</param>
//        /// <param name="tableName">表名，多个表名之间用“,”分隔。</param>
//        /// <param name="sortColumn">排序字段，排序字段后可以跟排序方式“ASC”，“DESC”。</param>
//        /// <param name="filter">条件语句。</param>
//        /// <param name="parameters">查询参数。</param>
//        /// <returns>返回分页数据。</returns>
//        public DataTable GetDataForPage(int pageIndex, string strColumn1, string strColumn2, string tableName, string sortColumn, string filter, Parameters parameters)
//        {
//            return this.GetDataForPage(pageIndex, 10, strColumn1, strColumn2, tableName, sortColumn, filter, parameters);
//        }

//        /// <summary>
//        /// 获取分页数据。
//        /// </summary>
//        /// <param name="pageIndex">当前页。</param>
//        /// <param name="pageSize">每页显示数量。</param>
//        /// <param name="strColumn1">查询字段1，多个字段之间用“,”分隔。</param>
//        /// <param name="tableName">表名，多个表名之间用“,”分隔。</param>
//        /// <param name="sortColumn">排序字段，排序字段后可以跟排序方式“ASC”，“DESC”。</param>
//        /// <returns>返回分页数据。</returns>
//        public DataTable GetDataForPage(int pageIndex, int pageSize, string strColumn1, string tableName, string sortColumn)
//        {
//            return this.GetDataForPage(pageIndex, pageSize, strColumn1, tableName, sortColumn, string.Empty, null);
//        }

//        /// <summary>
//        /// 获取分页数据。
//        /// </summary>
//        /// <param name="pageIndex">当前页。</param>
//        /// <param name="pageSize">每页显示数量。</param>
//        /// <param name="strColumn1">查询字段1，多个字段之间用“,”分隔。</param>
//        /// <param name="tableName">表名，多个表名之间用“,”分隔。</param>
//        /// <param name="sortColumn">排序字段，排序字段后可以跟排序方式“ASC”，“DESC”。</param>
//        /// <param name="filter">条件语句。</param>
//        /// <param name="parameters">查询参数。</param>
//        /// <returns>返回分页数据。</returns>
//        public DataTable GetDataForPage(int pageIndex, int pageSize, string strColumn1, string tableName, string sortColumn, string filter, Parameters parameters)
//        {
//            return this.GetDataForPage(pageIndex, pageSize, strColumn1, string.Empty, tableName, sortColumn, filter, parameters);
//        }

//        /// <summary>
//        /// 获取分页数据。
//        /// </summary>
//        /// <param name="pageIndex">当前页。</param>
//        /// <param name="pageSize">每页显示数量。</param>
//        /// <param name="strColumn1">查询字段1，多个字段之间用“,”分隔。</param>
//        /// <param name="strColumn2">查询字段2，多个字段之间用“,”分隔。</param>
//        /// <param name="tableName">表名，多个表名之间用“,”分隔。</param>
//        /// <param name="sortColumn">排序字段，排序字段后可以跟排序方式“ASC”，“DESC”。</param>
//        /// <param name="filter">条件语句。</param>
//        /// <param name="parameters">查询参数。</param>
//        /// <returns>返回分页数据。</returns>
//        public DataTable GetDataForPage(int pageIndex, int pageSize, string strColumn1, string strColumn2, string tableName, string sortColumn, string filter, Parameters parameters)
//        {
//            if (pageIndex <= 0)
//            {
//                pageIndex = 1;
//            }

//            if (pageSize <= 0)
//            {
//                pageSize = 10;
//            }

//            if (string.IsNullOrEmpty(strColumn2))
//            {
//                strColumn2 = strColumn1;
//            }

//            var startRows = ((pageIndex - 1) * pageSize) + 1;
//            var endRows = pageIndex * pageSize;

//            var queryString = new StringBuilder();
//            queryString.Append("SELECT ");
//            queryString.Append(strColumn1);
//            queryString.Append(" FROM (SELECT ROW_NUMBER() OVER (ORDER BY ");
//            queryString.Append(sortColumn);
//            queryString.Append(" ) AS RowNo, ");
//            queryString.Append(strColumn2);
//            queryString.Append(" FROM ");
//            queryString.Append(tableName);
//            queryString.Append(" ");
//            queryString.Append(filter);
//            queryString.Append(" ) AS A WHERE RowNo BETWEEN ");
//            queryString.Append(startRows);
//            queryString.Append(" AND ");
//            queryString.Append(endRows);

//            var countString = new StringBuilder();
//            countString.Append("SELECT Count(*) FROM ");
//            countString.Append(tableName);
//            countString.Append(" ");
//            countString.Append(filter);

//            var dataTable = this.QueryDataTable(queryString.ToString(), parameters);

//            var dataCount = this.QueryScalar(countString.ToString(), parameters);

//            if (dataCount != null)
//            {
//                var sessionPageModel = new PageModel { DataCount = (int)dataCount, PageSize = pageSize, PageIndex = pageIndex };

//                var httpSessionStateBase = this.ViewContext.HttpContext.Session;
//                if (httpSessionStateBase != null)
//                {
//                    httpSessionStateBase.Add(EngineHelper.PageModelKey, sessionPageModel);
//                }
//            }

//            return dataTable;
//        }

//        /// <summary>
//        /// 获取上传目录。
//        /// </summary>
//        /// <returns>返回上传目录。</returns>
//        public string UploadDir()
//        {
//            return this.GetUploadPath();
//        }

//        /// <summary>
//        /// 转换查询字符串为路由数据字典。
//        /// </summary>
//        /// <param name="queryString">查询字符串。</param>
//        /// <returns>返回路由数据字典。</returns>
//        public RouteValueDictionary ParseQueryStringToRouteValues(string queryString)
//        {
//            var parsed = HttpUtility.ParseQueryString(queryString);
//            var queryStringDictionary = parsed.AllKeys.ToDictionary(k => k, k => (object)parsed[k]);
//            return new RouteValueDictionary(queryStringDictionary);
//        }

//        /// <summary>
//        /// 构建标签参数。
//        /// </summary>
//        /// <param name="parameters">参数数组。</param>
//        /// <returns>返回参数字典。</returns>
//        public ViewDataDictionary CreateLabelParameter(params string[] parameters)
//        {
//            var viewData = new ViewDataDictionary();
//            foreach (var parameter in parameters)
//            {
//                var index = parameter.IndexOf('=');
//                var key = parameter.Substring(0, index);
//                object value = parameter.Substring(index + 1, parameter.Length - (index + 1));
//                viewData.Add(key, value);
//            }

//            return viewData;
//        }

//        /// <summary>
//        /// 设置新区域名称并返回原区域名称。
//        /// </summary>
//        /// <param name="htmlHelper">此方法扩展的HTML帮助器实例。</param>
//        /// <param name="moduleName">模块名称。</param>
//        /// <returns>返回区域名称。</returns>
//        internal string SetArea(HtmlHelper htmlHelper, string moduleName)
//        {
//            var area = string.Empty;
//            if (this.HtmlHelper.ViewContext.RouteData.DataTokens["area"] != null)
//            {
//                area = this.HtmlHelper.ViewContext.RouteData.DataTokens["area"].ToString();
//                htmlHelper.ViewContext.RouteData.DataTokens["area"] = moduleName;
//            }
//            else
//            {
//                htmlHelper.ViewContext.RouteData.DataTokens.Add("area", moduleName);
//            }

//            return area;
//        }

//        /// <summary>
//        /// 参数数组转换为字符串。
//        /// </summary>
//        /// <param name="parameters">参数数组。</param>
//        /// <returns>参数字符串。</returns>
//        private string ParametersArrayToString(params string[] parameters)
//        {
//            var sb = new StringBuilder();
//            foreach (var parameter in parameters)
//            {
//                sb.Append(parameter);
//                sb.Append(",");
//            }

//            return sb.ToString().TrimEnd(',');
//        }

//        /// <summary>
//        /// 查询数据库，返回DataTable。
//        /// </summary>
//        /// <param name="queryString">Sql语句。</param>
//        /// <param name="parameters">查询参数。</param>
//        /// <returns>返回DataTable。</returns>
//        private DataTable QueryDataTable(string queryString, Parameters parameters)
//        {
//            SqlConnection connection;
//            using (connection = new SqlConnection(this.GetConnectionString()))
//            {
//                var sqlCommand = new SqlCommand();
//                this.InitCommand(sqlCommand, connection, queryString, parameters);
//                var adapter = new SqlDataAdapter(sqlCommand);
//                var dataSet = new DataSet();
//                adapter.Fill(dataSet, "DataBySql");
//                sqlCommand.Parameters.Clear();
//                return dataSet.Tables[0];
//            }
//        }

//        /// <summary>
//        /// 查询数据库，返回标量值。
//        /// </summary>
//        /// <param name="queryString">Sql语句。</param>
//        /// <param name="parameters">查询参数。</param>
//        /// <returns>返回标量值。</returns>
//        private object QueryScalar(string queryString, Parameters parameters)
//        {
//            SqlConnection connection;
//            using (connection = new SqlConnection(this.GetConnectionString()))
//            {
//                var sqlCommand = new SqlCommand();
//                this.InitCommand(sqlCommand, connection, queryString, parameters);
//                var obj = sqlCommand.ExecuteScalar();
//                sqlCommand.Parameters.Clear();
//                return obj;
//            }
//        }

//        /// <summary>
//        /// 获取数据库连接字符串。
//        /// </summary>
//        /// <returns>返回数据库连接字符串。</returns>
//        private string GetConnectionString()
//        {
//            var dbContext = new TemplateEngineDbContext();
//            return dbContext.Database.Connection.ConnectionString;
//        }

//        /// <summary>
//        /// 初始化命令对象。
//        /// </summary>
//        /// <param name="command">命令对象。</param>
//        /// <param name="connection">数据库连接对象。</param>
//        /// <param name="queryString">查询字符串。</param>
//        /// <param name="parameters">查询参数。</param>
//        private void InitCommand(SqlCommand command, SqlConnection connection, string queryString, Parameters parameters)
//        {
//            if (connection.State != ConnectionState.Open)
//            {
//                connection.Open();
//            }

//            command.Connection = connection;
//            command.CommandText = queryString;
//            command.CommandType = CommandType.Text;

//            if (parameters != null)
//            {
//                var sqlParameterAdapter = new SqlParameterAdapter();
//                sqlParameterAdapter.ConvertParameter(command, parameters);
//            }
//        }

//        /// <summary>
//        /// 获取上传文件夹路径。
//        /// </summary>
//        /// <returns>返回上传文件夹路径。</returns>
//        private string GetUploadPath()
//        {
//            const string UploadDir = "UploadFiles";
//            var virtualPath = HttpContext.Current.Request.ApplicationPath;
//            var uploadPath = VirtualPathUtility.AppendTrailingSlash(virtualPath) + VirtualPathUtility.AppendTrailingSlash(UploadDir);
//            return uploadPath;
//        }

//        /// <summary>
//        /// 去除html返回文字
//        /// </summary>
//        /// <param name="html">html</param>
//        /// <param name="length">长度</param>
//        /// <param name="substring">截断</param>
//        /// <returns>纯文字</returns>
//        public string GetNotHtml(string html, int length = 100, string substring = "...")
//        {
//            if (!string.IsNullOrWhiteSpace(html))
//            { 
//                //去除所有脚本，中间部分也删除
//                html = Regex.Replace(html, @"<script[^>]*?>.*?</script>", string.Empty, RegexOptions.IgnoreCase);

//                //去除所有样式，中间部分也删除
//                html = Regex.Replace(html, @"<style[^>]*?>.*?</style>", string.Empty, RegexOptions.IgnoreCase);

//                //去除table里面的所有内容
//                html = Regex.Replace(html, @"<table[^>]*?>.*?</table>", string.Empty, RegexOptions.IgnoreCase);

//                //去除所有的标签
//                html = Regex.Replace(html, @"<[^>]*>", string.Empty, RegexOptions.IgnoreCase);

//                //去除所有的转义标签
//                html = Regex.Replace(html, @"&[a-z]*;", string.Empty, RegexOptions.IgnoreCase);

//                length = html.Length < length ? html.Length : length; //防止超出
//                return html.Substring(0, length) + substring;
//            }

//            return string.Empty;
//        }
//    }
//}