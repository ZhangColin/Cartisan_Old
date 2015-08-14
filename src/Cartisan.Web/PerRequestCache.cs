using System;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Cartisan.Caching;
using Cartisan.Extensions;

namespace Cartisan.Web {
    /// <summary>
    /// Http请求内缓存（短期缓存）
    /// </summary>
    public class PerRequestCache: ICache {
        private readonly HttpContextBase _context;

        public PerRequestCache(HttpContextBase context) {
            _context = context;
        }

        private IDictionary GetItems() {
            if (_context != null) {
                return _context.Items;
            }

            return null;
        }

        public void Set(string cacheKey, object value, TimeSpan timeSpan) {
            if (string.IsNullOrEmpty(cacheKey) || value == null) {
                return;
            }

            var items = GetItems();
            if (items == null) {
                return;
            }

            if (items.Contains(cacheKey)) {
                items[cacheKey] = value;
            }
            else {
                items.Add(cacheKey, value);
            }
        }

        public object Get(string cacheKey) {
            var items = GetItems();
            if (items == null || !items.Contains(cacheKey)) {
                return null;
            }

            return items[cacheKey];
        }

        public bool Contains(string cacheKey) {
            var items = GetItems();
            return items != null && items.Contains(cacheKey);
        }

        public void Remove(string cacheKey) {
            var items = GetItems();
            if (items == null) {
                return;
            }
            items.Remove(cacheKey);
        }

        public void RemoveByPattern(string pattern) {
            var items = GetItems();
            if (items == null) {
                return;
            }
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            items.Keys.Cast<string>().Where(key => regex.IsMatch(key)).ForEach(Remove);
        }

        public void Clear() {
            var items = GetItems();
            if (items == null) {
                return;
            }

            items.Keys.Cast<string>().ForEach(Remove);
        }
    }
}