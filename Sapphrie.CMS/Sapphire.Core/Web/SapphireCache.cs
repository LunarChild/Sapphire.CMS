using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;

namespace Sapphire.Core.Web
{
    /// <summary>
    /// 通用缓存类。
    /// </summary>
    public static class SapphrieCache
    {
        /// <summary>
        /// 当前请求上下文的缓存对象。
        /// </summary>
        private static readonly Cache SystemCache = InitCache();

        /// <summary>
        /// 获取存储在缓存中的项数。
        /// </summary>
        public static int Count
        {
            get
            {
                return SystemCache.Count;
            }
        }

        /// <summary>
        /// 从缓存中移除指定项。
        /// </summary>
        /// <param name="key">要移除的缓存项的标识符。</param>
        public static void Remove(string key)
        {
            SystemCache.Remove(key);
        }

        /// <summary>
        /// 清除所有的缓存。
        /// </summary>
        public static void Clear()
        {
            var enumerator = SystemCache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Remove(enumerator.Key.ToString());
            }
        }

        /// <summary>
        /// 通过正则表达式按照一定规则清除所有的缓存。
        /// </summary>
        /// <param name="pattern">正则表达式，区分大小写。</param>
        public static void Clear(string pattern)
        {
            var enumerator = SystemCache.GetEnumerator();
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.ExplicitCapture);
            while (enumerator.MoveNext())
            {
                var key = enumerator.Key.ToString();
                if (regex.IsMatch(key))
                {
                    Remove(key);
                }
            }
        }

        /// <summary>
        /// 根据键名获取被缓存的数据对象，未找到该键时为委托方法值。
        /// </summary>
        /// <typeparam name="T">缓存对象的返回类型。</typeparam>
        /// <param name="key">缓存键。</param>
        /// <param name="getValue">方法返回值。</param>
        /// <param name="dependencies">缓存依赖项。</param>
        /// <param name="minutes">缓存时间（分钟）。</param>
        /// <param name="onRemoveCallback">在从缓存中移除对象时将调用的委托（如果提供）。</param>
        /// <param name="isAbsoluteExpiration">
        /// 是否为绝对过期时间。true表示：所插入对象将过期并被从缓存中移除的时间。false表示：最后一次访问所插入对象时与该对象过期时之间的时间间隔。如果该值等效于
        /// minutes 分钟，则对象在最后一次被访问 minutes 分钟之后将过期并被从缓存中移除。
        /// </param>
        /// <param name="priority">指定 Cache 对象中存储的项的相对优先级。</param>
        /// <returns>返回检索到的缓存项，未找到该键时为func()。</returns>
        public static T Get<T>(
            string key,
            Func<T> getValue,
            CacheDependency dependencies = null,
            int minutes = int.MaxValue,
            CacheItemRemovedCallback onRemoveCallback = null,
            bool isAbsoluteExpiration = true,
            CacheItemPriority priority = CacheItemPriority.Default)
        {
            var value = SystemCache.Get(key);
            if (value == null)
            {
                value = getValue();
                if (isAbsoluteExpiration)
                {
                    SystemCache.Insert(key, value, dependencies, DateTime.UtcNow.AddMinutes(minutes), Cache.NoSlidingExpiration, priority, onRemoveCallback);
                }
                else
                {
                    SystemCache.Insert(key, value, dependencies, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(minutes), priority, onRemoveCallback);
                }
            }

            return (T)value;
        }

        /// <summary>
        /// 根据键名设置被缓存的数据对象，未找到该键时为委托方法值。
        /// </summary>
        /// <typeparam name="T">缓存对象的返回类型。</typeparam>
        /// <param name="key">缓存键。</param>
        /// <param name="getValue">方法返回值。</param>
        /// <param name="dependencies">缓存依赖项。</param>
        /// <param name="minutes">缓存时间（分钟）。</param>
        /// <param name="onRemoveCallback">在从缓存中移除对象时将调用的委托（如果提供）。</param>
        /// <param name="isAbsoluteExpiration">
        /// 是否为绝对过期时间。true表示：所插入对象将过期并被从缓存中移除的时间。false表示：最后一次访问所插入对象时与该对象过期时之间的时间间隔。如果该值等效于
        /// minutes 分钟，则对象在最后一次被访问 minutes 分钟之后将过期并被从缓存中移除。
        /// </param>
        /// <param name="priority">指定 Cache 对象中存储的项的相对优先级。</param>
        public static void Set<T>(
            string key,
            Func<T> getValue,
            CacheDependency dependencies = null,
            int minutes = int.MaxValue,
            CacheItemRemovedCallback onRemoveCallback = null,
            bool isAbsoluteExpiration = true,
            CacheItemPriority priority = CacheItemPriority.Default)
        {
            var value = getValue();
            if (isAbsoluteExpiration)
            {
                SystemCache.Insert(key, value, dependencies, DateTime.Now.AddMinutes(minutes), Cache.NoSlidingExpiration, priority, onRemoveCallback);
            }
            else
            {
                SystemCache.Insert(key, value, dependencies, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(minutes), priority, onRemoveCallback);
            }
        }

        /// <summary>
        /// 初始化缓存对象。
        /// </summary>
        /// <returns>缓存对象。</returns>
        private static Cache InitCache()
        {
            return HttpContext.Current != null ? HttpContext.Current.Cache : HttpRuntime.Cache;
        }
    }
}