using System;
using System.Text;
using System.Web.Mvc;
using Cartisan.Web.Mvc.Extensions;
using Cartisan.Web.Mvc.Filters;
using Cartisan.Web.Mvc.Results;

namespace Cartisan.Web.Mvc.Controllers {
//    [Compress]
    [HandleError]
    public abstract class ControllerBase: Controller {
        protected virtual PermanentRedirectResult PermanentRedirect(string url) {
            return new PermanentRedirectResult(url);
        }

        protected override void OnException(ExceptionContext filterContext) {
            if (filterContext.Exception is InvalidOperationException) {
                filterContext.SwitchToErrorView();
            }
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior) {
            if (behavior == JsonRequestBehavior.DenyGet &&
            string.Equals(this.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase)) {
                //Call JsonResult to throw the same exception as JsonResult  
                return new JsonResult();
            }

            return new JsonNetResult() {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
    }
}