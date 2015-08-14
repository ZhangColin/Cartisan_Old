﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Cartisan.Web.WebApi.MessageHandlers {
    public class CacheableEntity {
        public string ResourceKey { get; private set; }
        public EntityTagHeaderValue EntityTag { get; set; }
        public DateTimeOffset LastModified { get; set; }

        public CacheableEntity(string resourceKey) {
            ResourceKey = resourceKey;
        }

        public bool IsValid(DateTimeOffset modifiedSince) {
            var lastModified = LastModified.UtcDateTime;
            return (lastModified.AddSeconds(-1) < modifiedSince.UtcDateTime);
        }
    }

    public class HttpCachingHandler : DelegatingHandler {
        private static ConcurrentDictionary<string, CacheableEntity> _eTagCacheDictionary =
            new ConcurrentDictionary<string, CacheableEntity>();
        public ICollection<Func<string, string[]>> CacheInvalidationStore = new Collection<Func<string, string[]>>();
        private readonly string[] _varyHeaders;

        public HttpCachingHandler(params string[] varyHeaders) {
            _varyHeaders = varyHeaders;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            var resourceKey = GetResourceKey(request.RequestUri, _varyHeaders, request);
            CacheableEntity cacheableEntity = null;
            var cacheControlHeader = new CacheControlHeaderValue() {
                Private = true,
                MustRevalidate = true,
                MaxAge = TimeSpan.FromSeconds(0)
            };

            if (request.Method == HttpMethod.Get) {
                var eTags = request.Headers.IfNoneMatch;
                var modifiedSince = request.Headers.IfModifiedSince;
                var anyETagsFromTehClientExist = eTags.Any();
                var doWeHaveAnyCacheableEntityForTheRequest =
                    _eTagCacheDictionary.TryGetValue(resourceKey, out cacheableEntity);

                if (anyETagsFromTehClientExist) {
                    if (doWeHaveAnyCacheableEntityForTheRequest) {
                        if (eTags.Any(x => x.Tag == cacheableEntity.EntityTag.Tag)) {
                            var cacheResponse = request.CreateResponse(HttpStatusCode.NotModified);
                            cacheResponse.Headers.CacheControl = cacheControlHeader;
                            return cacheResponse;
                        }
                    }
                }
                else if (modifiedSince.HasValue) {
                    if (doWeHaveAnyCacheableEntityForTheRequest) {
                        if (cacheableEntity.IsValid(modifiedSince.Value)) {
                            var cacheResponse = request.CreateResponse(HttpStatusCode.NotModified);
                            cacheResponse.Headers.CacheControl = cacheControlHeader;
                            return cacheResponse;
                        }
                    }
                }
            }

            HttpResponseMessage response;
            try {
                response = await base.SendAsync(request, cancellationToken);
            }
            catch (Exception ex) {
                response = request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

            if (response.IsSuccessStatusCode) {
                if (request.Method == HttpMethod.Get
                    && !_eTagCacheDictionary.TryGetValue(resourceKey, out cacheableEntity)) {
                    cacheableEntity = new CacheableEntity(resourceKey);
                    cacheableEntity.EntityTag = GenerateETag();
                    cacheableEntity.LastModified = DateTimeOffset.Now;

                    _eTagCacheDictionary.AddOrUpdate(resourceKey, cacheableEntity, (k, e) => cacheableEntity);
                }

                if (request.Method == HttpMethod.Put ||
                    request.Method == HttpMethod.Post ||
                    request.Method == HttpMethod.Delete) {
                    HashSet<string> invalidCaches = new HashSet<string>();
                    invalidCaches.Add(GetRequestUri(request.RequestUri));

                    CacheInvalidationStore.ToList().ForEach(func => func(GetRequestUri(request.RequestUri))
                        .ToList().ForEach(uri => invalidCaches.Add(uri)));

                    invalidCaches.ToList().ForEach(invalidCacheUri => {
                        var cacheEntityKeys = _eTagCacheDictionary.Keys.Where(
                            x => x.StartsWith(string.Format("{0}:", invalidCacheUri),
                                StringComparison.InvariantCultureIgnoreCase));

                        cacheEntityKeys.ToList().ForEach(key => {
                            if (!string.IsNullOrEmpty(key)) {
                                CacheableEntity outVal = null;
                                _eTagCacheDictionary.TryRemove(key, out outVal);
                            }
                        });
                    });


                }
                else if (request.Method == HttpMethod.Get) {
                    response.Headers.CacheControl = cacheControlHeader;
                    response.Headers.ETag = cacheableEntity.EntityTag;
                    response.Content.Headers.LastModified = cacheableEntity.LastModified;

                    _varyHeaders.ToList().ForEach(varyHeader => response.Headers.Vary.Add(varyHeader));
                }

            }

            return response;
        }

        private string GetResourceKey(Uri uri, string[] varyHeaders, HttpRequestMessage request) {
            return GetResourceKey(GetRequestUri(uri), varyHeaders, request);
        }

        private string GetResourceKey(string trimedRequestUri, string[] varyHeaders, HttpRequestMessage request) {
            var requestedVaryHeaderValuePairs = request.Headers
                .Where(x => varyHeaders.Contains(x.Key))
                .Select(x => string.Format("{0}:{1}", x.Key, string.Join(";", x.Value)));
            return string.Format("{0}:{1}", trimedRequestUri, string.Join("_", requestedVaryHeaderValuePairs))
                .ToLower(CultureInfo.InvariantCulture);
        }

        private EntityTagHeaderValue GenerateETag() {
            var eTag = string.Concat("\"", Guid.NewGuid().ToString("N"), "\"");
            return new EntityTagHeaderValue(eTag);
        }

        private string GetRequestUri(Uri requestUri) {
            return string.Concat(requestUri.LocalPath.TrimEnd('/'), requestUri.Query)
                .ToLower(CultureInfo.InvariantCulture);
        }
    }
}