//using System.Web.Mvc;
//
//namespace Camela.Web.Framework.Mvc.Filter {
//    public class UnifiedJsonResponseAttribute: ActionFilterAttribute {
//        public override void OnResultExecuting(ResultExecutingContext filterContext) {
//            if(filterContext.Result is JsonResult) {
//                var result = filterContext.Result as JsonResult;
//                var data = result.Data;
//
//                ResponseResult responseResult = new ResponseResult {
//                    Success = true,
//                    Status = ResultState.Success,
//                    Data = data
//                };
//
//                result.Data = responseResult;
//            }
//        }
//    }
//}