using System.Collections.Generic;
using System.Linq;
using Cartisan.Utility;

namespace Cartisan.Domain {
    public abstract class ValueObject<T> where T: ValueObject<T> {
        protected abstract IEnumerable<object> GetAttributesToIncludeInEqualityCheck();
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object other) {
            return Equals(other as T);
        }

        public bool Equals(T other) {
            if(other == null) {
                return false;
            }

            return GetAttributesToIncludeInEqualityCheck().SequenceEqual(other.GetAttributesToIncludeInEqualityCheck());
        }

        public static bool operator ==(ValueObject<T> left, ValueObject<T> right) {
            return Equals(left, right);
        }

        public static bool operator !=(ValueObject<T> left, ValueObject<T> right) {
            return !(left == right);
        }

        public override int GetHashCode() {
            return HashCodeUtil.CombineHashCodes(this.GetEqualityComponents());
            //            int hash = 17;
            //            foreach(var obj in GetAttributesToIncludeInEqualityCheck()) {
            //                hash = hash * 31 + (obj == null ? 0 : obj.GetHashCode());
            //            }
            //            return hash;
        }
    }
}