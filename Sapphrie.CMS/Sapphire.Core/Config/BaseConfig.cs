using System;
using System.IO;
using System.Text;
using System.Web.Caching;
using System.Xml;
using System.Xml.Serialization;
using Sapphire.Core.Web;
using Sapphire.Core.CommonHelper;

namespace Sapphire.Core.Config
{
    /// <summary>
    /// 配置信息基类。
    /// </summary>
    /// <typeparam name="T">配置信息类。</typeparam>
    public abstract class BaseConfig<T>
        where T : BaseConfig<T>, new()
    {
        private const string ConfigPath = "~/Config/";

        private const string ConfigCacheKey = "Sapphire::Config::";

        /// <summary>
        /// 实例化配置信息对象。
        /// </summary>
        /// <returns>配置信息对象。</returns>
        public static T Instance
        {
            get
            {
                return new T().InitConfig();
            }
        }

        /// <summary>
        /// 配置信息文件路径。
        /// </summary>
        protected virtual string ConfigFilePath
        {
            get
            {
                return ModulePathHelper.GetFilePath(GetConfigPath());
            }
        }

        /// <summary>
        /// 保存配置信息至配置文件。
        /// </summary>
        public void Save()
        {
            var config = this as T;
            if (config == null)
            {
                return;
            }

            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var xmlTextWriter = new XmlTextWriter(this.ConfigFilePath, Encoding.UTF8))
            {
                xmlTextWriter.Formatting = Formatting.Indented;
                var xmlNamespace = new XmlSerializerNamespaces();
                xmlNamespace.Add(string.Empty, string.Empty);
                xmlSerializer.Serialize(xmlTextWriter, config, xmlNamespace);
            }
        }

        /// <summary>
        /// 移除缓存时执行的回调方法。
        /// </summary>
        /// <param name="key">键。</param>
        /// <param name="value">值。</param>
        /// <param name="reason">缓存项移除原因。</param>
        protected virtual void OnCacheRemoveCallback(string key, object value, CacheItemRemovedReason reason)
        {
        }

        /// <summary>
        /// 获取配置信息文件路径。
        /// </summary>
        /// <returns>配置信息文件路径。</returns>
        private static string GetConfigPath()
        {
            var configName = typeof(T).Name;
            if (configName.EndsWith("config", StringComparison.OrdinalIgnoreCase))
            {
                configName = configName.Substring(0, configName.Length - 6);
            }

            return string.Concat(ConfigPath, configName, ".config");
        }

        /// <summary>
        /// 从配置文件中反序列化出配置对象。
        /// </summary>
        /// <param name="configType">配置对象类型。</param>
        /// <param name="filePath">配置文件路径。</param>
        /// <returns>配置对象。</returns>
        private static T Deserialize(Type configType, string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(GetConfigPath());
            }

            using (var xmlTextReader = new XmlTextReader(filePath))
            {
                var xmlSerializer = new XmlSerializer(configType);
                return (T)xmlSerializer.Deserialize(xmlTextReader);
            }
        }

        private T InitConfig()
        {
            var configType = typeof(T);
            var configCacheKey = string.Concat(ConfigCacheKey, configType.Name);

            return SapphrieCache.Get(configCacheKey, () => Deserialize(configType, this.ConfigFilePath), new CacheDependency(this.ConfigFilePath), int.MaxValue, this.OnCacheRemoveCallback);
        }
    }
}