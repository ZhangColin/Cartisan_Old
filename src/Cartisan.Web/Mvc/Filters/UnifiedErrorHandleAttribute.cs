//using System.Net;
//using System.Text;
//using System.Web.Mvc;
//using Camela.Web.Framework.Mvc.Result;
//
//namespace Cartisan.Web.Mvc.Filter {
//    public class UnifiedErrorHandleAttribute: HandleErrorAttribute {
//        public override void OnException(ExceptionContext filterContext) {
//            var errorMessage = filterContext.Exception.Message;
//            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
//
//            var status = filterContext.Exception is UnauthorizedException ? ResultState.Unauthorized :
//                filterContext.Exception is ValidateFailureException ? ResultState.ValidateFailure :
//                    filterContext.Exception is RuntimeFailureException ? ResultState.RuntimeFailure :
//                        ResultState.Exception;
//
//            filterContext.Result = new JsonNetResult() {
//                Data = new ResponseResult() {
//                    Success = false,
//                    Status = status,
//                    Message = errorMessage
//                },
//                ContentEncoding = Encoding.UTF8,
//                JsonRequestBehavior = JsonRequestBehavior.AllowGet
//            };
//
//            filterContext.ExceptionHandled = true;
//        }
//    }
//}