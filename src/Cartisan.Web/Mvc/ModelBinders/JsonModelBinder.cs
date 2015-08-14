using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Cartisan.Web.Mvc.ModelBinders {
    public class JsonModelBinder : DefaultModelBinder {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            string json = string.Empty;
            IValueProvider provider = bindingContext.ValueProvider;
            ValueProviderResult providerValue = provider.GetValue(bindingContext.ModelName);
            if (providerValue != null) {
                json = providerValue.AttemptedValue;
            }

            // 确保表达式字符串以JSON对象（{}）或数组([])表示
            if (Regex.IsMatch(json, @"^(\[.*\]|{.*})$")) {
                return new JavaScriptSerializer().Deserialize(json, bindingContext.ModelType);
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum |
        AttributeTargets.Interface | AttributeTargets.Parameter |
        AttributeTargets.Struct | AttributeTargets.Property,
        AllowMultiple = false, Inherited = false)]
    public class JsonModelBinderAttribute : CustomModelBinderAttribute {
        public override IModelBinder GetBinder() {
            return new JsonModelBinder();
        }
    }
}