using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cartisan.Extensions {
    public static class JsonExtension {
        public static string ToJson(this object obj, bool useCamelCasePropertyName = true,
            Formatting formatting = Formatting.Indented) {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            if (useCamelCasePropertyName) {
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }
            else {
                settings.ContractResolver = new DefaultContractResolver();
            }
            return JsonConvert.SerializeObject(obj, formatting, settings);
        } 
    }
}