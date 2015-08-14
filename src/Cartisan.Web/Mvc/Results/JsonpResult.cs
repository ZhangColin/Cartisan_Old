using System;
using System.Web.Mvc;
using Cartisan.Extensions;

namespace Cartisan.Web.Mvc.Results {
    public class JsonpResult: JsonNetResult {
        private const String JsonpCallbackName = "callback";

        protected override string GetJsonString(ControllerContext context, object data) {
            if (context.HttpContext.Request[JsonpCallbackName] == null) {
                return data.ToJson();
            }
            else {
                return string.Format("{0}({1})", context.HttpContext.Request[JsonpCallbackName], data.ToJson());
            }
        }
    }
}