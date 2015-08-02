//using System.Net;
//using System.Text;
//using System.Web.Mvc;
//using Cartisan.Web.Mvc.Result;
//
//namespace Cartisan.Web.Mvc.Filter {
//    public class AjaxErrorHandleAttribute: HandleErrorAttribute {
//        public override void OnException(ExceptionContext filterContext) {
//            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest()) {
//                var errorMessage = filterContext.Exception.Message;
//                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
//
//                filterContext.Result = new JsonNetResult() {
//                    Data = new ResponseResult() {
//                        Success = false,
//                        Status = "",
//                        Message = errorMessage
//                    },
//                    ContentEncoding = Encoding.UTF8,
//                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
//                };
//
//                filterContext.ExceptionHandled = true;
//            }
//            else {
//                base.OnException(filterContext);
//            }
//        }
//    }
//}