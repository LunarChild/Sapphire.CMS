using System.Web.Mvc;

namespace Sapphire.Core.Mvc
{
    /// <summary>
    /// 模型元数据扩展类。
    /// </summary>
    public static class ModelMetadataExtensions
    {
        /// <summary>
        /// 获取模型元数据的附加数据值。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="modelMetadata">模型元数据。</param>
        /// <param name="key">附加数据键值。</param>
        /// <param name="defaultValue">默认值。</param>
        /// <returns>模型元数据的附加数据值。</returns>
        public static T GetAdditionalValue<T>(this ModelMetadata modelMetadata, string key, T defaultValue)
        {
            return modelMetadata.AdditionalValues.ContainsKey(key) ? (T)modelMetadata.AdditionalValues[key] : defaultValue;
        }

        /// <summary>
        /// 获取模型元数据的附加数据值。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="modelMetadata">模型元数据。</param>
        /// <param name="key">附加数据键值。</param>
        /// <returns>模型元数据的附加数据值。</returns>
        public static T GetAdditionalValue<T>(this ModelMetadata modelMetadata, string key)
        {
            T value;
            if (modelMetadata.AdditionalValues.ContainsKey(key))
            {
                value = (T)modelMetadata.AdditionalValues[key];
            }
            else
            {
                value = default(T);
            }

            return value;
        }
    }
}