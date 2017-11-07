//using System;

//namespace Sapphire.Core.Web
//{
//    /// <summary>
//    /// 缓存模型类型数据。
//    /// </summary>
//    public static class CachedModelTypeData
//    {
//        /// <summary>
//        /// 模型类型数据缓存键。
//        /// </summary>
//        private const string ModelTypeDataCacheKey = "Sapphire::ModelTypeData::";

//        /// <summary>
//        /// 获取模型类型数据。
//        /// </summary>
//        /// <param name="type">类型。</param>
//        /// <returns>返回模型类型数据。</returns>
//        internal static ModelTypeData GetModelTypeData(Type type)
//        {
//            return SapphrieCache.Get(ModelTypeDataCacheKey + type.Name + "::" + type.GUID, () => ModelTypeDataProvider.ReflectClass(type));
//        }
//    }
//}