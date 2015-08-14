using System.Globalization;
using System.Web.Http.ValueProviders;

namespace Cartisan.Web.WebApi.Providers {
    public class HeaderValueProvider : IValueProvider {
        private readonly HeadersMap _headers;

        public HeaderValueProvider(HeadersMap headers) {
            _headers = headers;
        }

        public bool ContainsPrefix(string prefix) {
            return false;
        }

        public ValueProviderResult GetValue(string key) {
            string value = _headers[key];
            return value == null ? null : new ValueProviderResult(value, value, CultureInfo.InvariantCulture);
        }
    }
}