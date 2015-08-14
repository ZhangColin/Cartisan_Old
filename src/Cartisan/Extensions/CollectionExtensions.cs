using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cartisan.Extensions {
    public static class CollectionExtensions {
        /// <summary>
        /// 尝试移除指定项
        /// </summary>
        /// <param name="hashtable"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool TryRemove(this Hashtable hashtable, object key) {
            try {
                hashtable.Remove(key);
                return true;
            }
            catch (Exception) {
                return false;
            }
        }



        /// <summary>
        /// 如果为Null，返回一个空集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> source) {
            return source ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// 为每一个执行指定的操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action) {
            foreach (T element in source.OrEmptyIfNull())
                action(element);
            return source;
        }

        /// <summary>
        /// 添加一组对象到集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="initial"></param>
        /// <param name="collection"></param>
        public static void AddRange<T>(this IList<T> initial, IEnumerable<T> collection) {
            AssertionConcern.NotNull(initial, "initial is null");

            if (collection == null) {
                return;
            }

            foreach (T value in collection) {
                initial.Add(value);
            }
        }

        /// <summary>
        /// 返回第一个符合条件的对象的位置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this IEnumerable<T> collection, Func<T, bool> predicate) {
            int index = 0;
            foreach (T value in collection) {
                if (predicate(value)) {
                    return index;
                }
                index++;
            }

            return -1;
        }
    }
}