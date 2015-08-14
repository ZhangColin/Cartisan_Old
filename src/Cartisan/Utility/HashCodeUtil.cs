using System.Collections.Generic;
using System.Linq;

namespace Cartisan.Utility {
    public static class HashCodeUtil {
        public static int CombineHashCodes(IEnumerable<object> objects) {
            unchecked {
                return objects.Aggregate(17, (current, obj) => current * 23 + (obj != null ? obj.GetHashCode() : 0));
            }
        }
    }
}