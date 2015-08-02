//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Dynamic;
//using System.Globalization;
//using System.IO;
//using System.Web.Mvc;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;
//
//namespace Cartisan.Web.Mvc.Provider {
//    public class JsonNetValueProviderFactory: ValueProviderFactory {
//        public override IValueProvider GetValueProvider(ControllerContext controllerContext) {
//            if (controllerContext == null) {
//                throw new ArgumentNullException("controllerContext");
//            }
//
//            if (!controllerContext.HttpContext.Request.ContentType.StartsWith("application/json",
//                StringComparison.OrdinalIgnoreCase)) {
//                return null;
//            }
//
//            object JSONObject;
//
//            using (StreamReader streamReader = new StreamReader(controllerContext.HttpContext.Request.InputStream)) {
//                using (JsonTextReader JSONReader = new JsonTextReader(streamReader)) {
//                    if (!JSONReader.Read()) {
//                        return null;
//                    }
//
//                    JsonSerializer JSONSerializer = new JsonSerializer();
//                    JSONSerializer.Converters.Add(new ExpandoObjectConverter());
//
//                    if (JSONReader.TokenType == JsonToken.StartArray) {
//                        JSONObject = JSONSerializer.Deserialize<List<ExpandoObject>>(JSONReader);
//                    }
//                    else {
//                        JSONObject = JSONSerializer.Deserialize<ExpandoObject>(JSONReader);
//                    }
//                }
//            }
//
//            Dictionary<string, object> backingStore =
//                    new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
//            AddToBackingStore(backingStore, string.Empty, JSONObject);
//
//            return new DictionaryValueProvider<object>(backingStore, CultureInfo.CurrentCulture);
//        }
//
//        private static void AddToBackingStore(Dictionary<string, object> backingStore, string prefix, object value) {
//            IDictionary<string, object> d = value as IDictionary<string, object>;
//            if (d != null) {
//                foreach (KeyValuePair<string, object> entry in d) {
//                    AddToBackingStore(backingStore, MakePropertyKey(prefix, entry.Key), entry.Value);
//                }
//                return;
//            }
//
//            IList l = value as IList;
//            if (l != null) {
//                for (int i = 0; i < l.Count; i++) {
//                    AddToBackingStore(backingStore, MakeArrayKey(prefix, i), l[i]);
//                }
//                return;
//            }
//            backingStore[prefix] = value;
//        }
//
//        private static string MakeArrayKey(string prefix, int index) {
//            return prefix + "[" + index.ToString(CultureInfo.InvariantCulture) + "]";
//        }
//
//        private static string MakePropertyKey(string prefix, string propertyName) {
//            return (string.IsNullOrEmpty(prefix)) ? propertyName : prefix + "." + propertyName;
//        }
//    }
//}