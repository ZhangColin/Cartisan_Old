//namespace Cartisan.Caching {
//    /// <summary>
//    /// 用于连接Memcached的分布式缓存
//    /// </summary>
//    public class MemcachedCache: ICache {
//        private MemcachedClient _cache = new MemcachedClient();
//
//        public void Set(string cacheKey, object value, TimeSpan timeSpan) {
//            this._cache.Store(StoreMode.Set, cacheKey, value, DateTime.Now.Add(timeSpan));
//        }
//
//        public object Get(string cacheKey) {
//            return this._cache.Get(cacheKey);
//        }
//
//        public bool Contains(string cacheKey) {
//            return this._cache.Get(cacheKey) != null;
//        }
//
//        public void Remove(string cacheKey) {
//            this._cache.Remove(cacheKey);
//        }
//
//        public void RemoveByPattern(string pattern) {
//            throw new NotImplementedException();
//        }
//
//        public void Clear() {
//            this._cache.FlushAll();
//        }
//    }
//}