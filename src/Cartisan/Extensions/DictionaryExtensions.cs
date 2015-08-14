using System;
using System.Collections;
using System.Collections.Generic;

namespace Cartisan.Extensions {
    public static class DictionaryExtensions {
        /// <summary>
        /// 尝试移除指定项
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool TryRemove(this IDictionary collection, object key) {
            try {
                collection.Remove(key);
                return true;
            }
            catch (Exception) {
                return false;
            }
        }


        /// <summary>
        /// 尝试获取指定键的值，不存在返回默认值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) {
            return dictionary.TryGetValue(key, default(TValue));
        }

        /// <summary>
        /// 尝试获取指定键的值，不存在返回默认值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TValue TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
            TValue defaultValue) {
            TValue result;
            if (!dictionary.TryGetValue(key, out result)) {
                return defaultValue;
            }

            return result;
        }
    }
}