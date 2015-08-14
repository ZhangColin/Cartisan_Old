using System.Web;
using System.Web.Mvc;

namespace Cartisan.Web.Mvc.Results {
    public class MultipleResponseFormatsAttribute : ActionFilterAttribute {
        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            HttpRequestBase request = filterContext.HttpContext.Request;
            ViewResult viewResult = filterContext.Result as ViewResult;

            if (viewResult == null) {
                return;
            }

            if (request.IsAjaxRequest()) {
                filterContext.Result = new PartialViewResult() {
                    TempData = viewResult.TempData,
                    ViewData = viewResult.ViewData,
                    ViewName = viewResult.ViewName
                };
            }
            else if (request.IsJsonRequest()) {
                filterContext.Result = new JsonResult() {
                    Data = viewResult.Model,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }

    public static class JsonRequestExtensions {
        public static bool IsJsonRequest(this HttpRequestBase request) {
            return string.Equals(request["format"], "json");
        }
    }
}