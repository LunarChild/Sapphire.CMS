using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Sapphire.Core.HostService
{
    /// <summary>
    /// 从配置文件读取数据并添加usings，设置继承子句，指定layout名称。
    /// </summary>
    /// <remarks>
    /// 修改子模块中的模板，使它们像正常的模板一样工作。包括以下几项修改：
    /// <list type="bullet">
    ///     <item>包含 @model 指令。</item>
    ///     <item>添加 @inherits 指令。</item>
    ///     <item>添加丢失的@using语句（MVC和ASP.NET依赖）。</item>
    /// </list>
    /// <para>在<see cref="ViewFileProvider"/>中加载这个模板修正器。</para>
    /// </remarks>
    public class ExternalViewFixer : IExternalViewFixer
    {
        /// <summary>
        /// 初始化<see cref="ExternalViewFixer"/>类的一个新实例。
        /// </summary>
        public ExternalViewFixer()
        {
            this.WebViewPageClassName = "Sapphire.Web.MvcContrib.GriffinWebViewPage";
            this.LayoutPath = null;
        }

        /// <summary>
        /// 需要继承的模板基类。
        /// </summary>
        public string WebViewPageClassName { get; set; }

        /// <summary>
        /// 获取或者设置layout文件的相对路径。
        /// </summary>
        /// <value>默认使用_ViewStart中指定的。</value>
        public string LayoutPath { get; set; }

        /// <summary>
        /// 修改模板。
        /// </summary>
        /// <param name="virtualPath">模板的路径 Path to view </param>
        /// <param name="stream">包含原始模板的数据流。</param>
        /// <param name="webConfigStream">包含web.config文件的数据流。</param>
        /// <returns>返回代表修改后的内容的数据流。</returns>
        public Stream CorrectView(string virtualPath, Stream stream, Stream webConfigStream)
        {
            var reader = new StreamReader(stream, Encoding.UTF8);
            var view = reader.ReadToEnd();
            stream.Close();

            var namespaces = new List<string>();
            if (webConfigStream != null)
            {
                var xmlDocument = XDocument.Load(webConfigStream);
                if (xmlDocument.Root != null)
                {
                    namespaces = xmlDocument.Root.Descendants("namespaces").Descendants().Select(x => x.Attribute("namespace").Value).ToList();
                    this.WebViewPageClassName = xmlDocument.Root.Descendants("system.web.webPages.razor").Descendants("pages").Select(x => x.Attribute("pageBaseType").Value).FirstOrDefault();
                }
            }

            var ourStream = new MemoryStream();
            var writer = new StreamWriter(ourStream, Encoding.UTF8);
            var modelString = string.Empty;
            var modelPos = view.IndexOf("@model", StringComparison.Ordinal);
            if (modelPos != -1)
            {
                writer.Write(view.Substring(0, modelPos));
                var modelEndPos = view.IndexOfAny(new[] { '\r', '\n' }, modelPos);
                modelString = view.Substring(modelPos, modelEndPos - modelPos);
                view = view.Remove(0, modelEndPos);
            }

            if (namespaces.Count > 0)
            {
                foreach (var namespaceConfig in namespaces)
                {
                    writer.WriteLine("@using " + namespaceConfig);
                }
            }
            else
            {
                writer.WriteLine("@using System.Web.Mvc");
                writer.WriteLine("@using System.Web.Mvc.Ajax");
                writer.WriteLine("@using System.Web.Mvc.Html");
                writer.WriteLine("@using System.Web.Routing");
            }

            var basePrefix = "@inherits " + this.WebViewPageClassName;

            if (virtualPath.ToLower().Contains("_viewstart"))
            {
                writer.WriteLine("@inherits System.Web.WebPages.StartPage");
            }
            else if (modelString == "@model object")
            {
                writer.WriteLine(basePrefix + "<dynamic>");
            }
            else if (!string.IsNullOrEmpty(modelString))
            {
                writer.WriteLine(basePrefix + "<" + modelString.Substring(7) + ">");
            }
            else
            {
                writer.WriteLine(basePrefix + "<dynamic>");
            }

            // 标签不能有layout
            if (!string.IsNullOrEmpty(this.LayoutPath) && !virtualPath.Contains("/_"))
            {
                writer.WriteLine("@{{ Layout = \"{0}\"; }}", this.LayoutPath);
            }

            writer.Write(view);
            writer.Flush();
            ourStream.Position = 0;
            return ourStream;
        }
    }
}