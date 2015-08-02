using System;
using System.Collections.Generic;
using System.Linq;

namespace Cartisan.Extensions {
    public static class CollectionExtension {
        public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> source) {
            return source ?? Enumerable.Empty<T>();
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action) {
            foreach (T element in source.OrEmptyIfNull()) {
                action(element);
            }

            return source;
        }  
    }
}