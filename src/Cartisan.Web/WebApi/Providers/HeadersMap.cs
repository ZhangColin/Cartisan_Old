using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace Cartisan.Web.WebApi.Providers {
    public class HeadersMap {
        private Dictionary<string, string> _headersCollection;

        public HeadersMap(HttpHeaders headers) {
            _headersCollection = headers.ToDictionary(
                x => x.Key.ToLower().Replace("-", string.Empty),
                x => string.Join(",", x.Value));
        }

        public string this[string header] {
            get {
                string key = header.ToLower();
                return _headersCollection.ContainsKey(key) ? _headersCollection[key] : null;
            }
        }

        public bool ContainsHeader(string header) {
            return this[header] != null;
        }
    }
}