using System;
using System.Linq;
using System.Runtime.Caching;
using System.Text.RegularExpressions;
using Cartisan.Extensions;

namespace Cartisan.Caching {
    /// <summary>
    /// 使用System.Runtime.Caching.MemoryCache实现的本机缓存
    /// </summary>
    /// <remarks>仅能在.net framework4.0及以上版本使用</remarks>
    public class RuntimeMemoryCache: ICache {
        protected ObjectCache Cache {
            get {
                return MemoryCache.Default;
            }
        }

        public void Set(string cacheKey, object value, TimeSpan timeSpan) {
            if (string.IsNullOrEmpty(cacheKey) || value == null) {
                return;
            }
            this.Cache.Set(cacheKey, value, DateTimeOffset.Now.Add(timeSpan));
        }

        public object Get(string cacheKey) {
            return this.Cache[cacheKey];
        }

        public bool Contains(string cacheKey) {
            return Cache.Contains(cacheKey);
        }

        public void Remove(string cacheKey) {
            this.Cache.Remove(cacheKey);
        }

        public void RemoveByPattern(string pattern) {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            this.Cache.Select(item => item.Key).Where(key => regex.IsMatch(key)).ForEach(Remove);
        }

        public void Clear() {
            this.Cache.Select(c => c.Key).ForEach(Remove);
        }
    }
}