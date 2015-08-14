using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Cartisan.Web.WebApi.Filters {
    public class EnableETagAttribute : ActionFilterAttribute {
        private static ConcurrentDictionary<string, EntityTagHeaderValue> _etags =
            new ConcurrentDictionary<string, EntityTagHeaderValue>();

        public override void OnActionExecuting(HttpActionContext actionContext) {
            var request = actionContext.Request;

            if (request.Method == HttpMethod.Get) {
                var key = GetKey(request);
                ICollection<EntityTagHeaderValue> etagsFromClient = request.Headers.IfNoneMatch;

                if (etagsFromClient.Any()) {
                    EntityTagHeaderValue etag = null;
                    if (_etags.TryGetValue(key, out etag) && etagsFromClient.Any(t => t.Tag == etag.Tag)) {
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.NotModified);
                        SetCacheControl(actionContext.Response);
                    }
                }
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext) {
            var request = actionExecutedContext.Request;
            var key = GetKey(request);

            EntityTagHeaderValue etag;

            if (!_etags.TryGetValue(key, out etag) || request.Method == HttpMethod.Put || request.Method == HttpMethod.Post) {
                etag = new EntityTagHeaderValue("\"" + Guid.NewGuid() + "\"");
                _etags.AddOrUpdate(key, etag, (k, val) => etag);
            }

            actionExecutedContext.Response.Headers.ETag = etag;
            SetCacheControl(actionExecutedContext.Response);
        }

        private string GetKey(HttpRequestMessage request) {
            return request.RequestUri.ToString();
        }

        private void SetCacheControl(HttpResponseMessage response) {
            response.Headers.CacheControl = new CacheControlHeaderValue() {
                MaxAge = TimeSpan.FromSeconds(6),
                MustRevalidate = true,
                Private = true
            };
        }
    }
}