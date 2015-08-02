//using System.Text;
//using Cartisan.Web.Mvc.Result;
//using Microsoft.AspNet.Mvc;
//
//namespace Cartisan.Web.Mvc.Controller {
//    public class BaseController: Microsoft.AspNet.Mvc.Controller {
//        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior) {
////            if (behavior == JsonRequestBehavior.DenyGet &&
////            string.Equals(this.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase)) {
////                //Call JsonResult to throw the same exception as JsonResult  
////                return new JsonResult();
////            }
//
//            return new JsonNetResult() {
//                Data = data,
//                ContentType = contentType,
//                ContentEncoding = contentEncoding,
//                JsonRequestBehavior = behavior
//            };  
//        }
//    }
//}