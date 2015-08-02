using System.Collections.Generic;

namespace Cartisan.Utility {
    public static class StringExtensions {
        public static string Repeat(this string source, int quantity) {
            return source.Repeat("", quantity);
        }

        public static string Repeat(this string source, string separator, int quantity) {
            var strs = new List<string>(quantity);
            for (var i = 0; i < quantity; i++) {
                strs.Add(source);
            }
            return string.Join(separator, strs);
        }
    }
}