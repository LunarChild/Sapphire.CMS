using System;
using System.Collections.Generic;
using System.Linq;

namespace Sapphire.Core.CommonHelper
{
    /// <summary>
    /// IEnumerable扩展。
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// 通过使用指定的比较条件进行比较生成两个序列的差集。
        /// </summary>
        /// <typeparam name="T">输入序列中的元素的类型。</typeparam>
        /// <typeparam name="TKey">输入比较条件的类型。</typeparam>
        /// <param name="first">一个 System.Collections.Generic.IEnumerable&lt;T&gt;，将返回其也不在 second 中的元素。</param>
        /// <param name="second">一个 System.Collections.Generic.IEnumerable&lt;T&gt;，如果它的元素也出现在第一个序列中，则将导致从返回的序列中移除这些元素。</param>
        /// <param name="getKey">比较条件。</param>
        /// <returns>包含两个序列元素的差集的序列。</returns>
        public static IEnumerable<T> Except<T, TKey>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, TKey> getKey)
        {
            return from item in first
                   join otherItem in second on getKey(item) equals getKey(otherItem) into tempItems
                   from temp in tempItems.DefaultIfEmpty()
                   where ReferenceEquals(null, temp) || temp.Equals(default(T))
                   select item;
        }

        /// <summary>
        /// 查找序列中相应元素对应的位置。
        /// </summary>
        /// <typeparam name="T">输入序列中的元素的类型。</typeparam>
        /// <param name="enumerable">查找元素的序列。</param>
        /// <param name="element">用于在序列中匹配位置的元素。</param>
        /// <returns>返回元素在序列中的位置，如果查找不到该元素则返回-1。</returns>
        public static int IndexOf<T>(this IEnumerable<T> enumerable, T element)
        {
            var i = 1;
            IEqualityComparer<T> comparer = EqualityComparer<T>.Default;
            foreach (var currentElement in enumerable)
            {
                if (comparer.Equals(currentElement, element))
                {
                    return i;
                }

                i++;
            }

            return -1;
        }
    }
}