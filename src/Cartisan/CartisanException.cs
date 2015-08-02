using System;
using System.Runtime.Serialization;

namespace Cartisan {
    [Serializable]
    public class CartisanException: ApplicationException {
        public CartisanException() {}
        public CartisanException(string message): base(message) {}
        public CartisanException(string message, Exception innerException): base(message, innerException) {}
        protected CartisanException(SerializationInfo info, StreamingContext context): base(info, context) {}
    }
}