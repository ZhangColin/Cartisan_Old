//using System;
//using System.Web.Mvc;
//
//namespace Cartisan.Web.Mvc.Filter {
//    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
//    public class CamelaAuthorizeAttribute: AuthorizeAttribute {
//        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext) {
//            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest()) {
////                var result = new ResponseResult {
////                    Success = false,
////                    State = ResultState.Unauthorized,
////                    Message = "用户未登录或Session过期"
////                };
////
////                filterContext.Result = new CustomJsonResult {
////                    Data = result
////                };
//            }
//            else {
//                base.HandleUnauthorizedRequest(filterContext);
//            }
//        }
//    }
//}