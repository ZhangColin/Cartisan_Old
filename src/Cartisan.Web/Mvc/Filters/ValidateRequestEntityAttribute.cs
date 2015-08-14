//using System.Linq;
//using System.Web.Mvc;
//using Camela.Web.Framework.Exception;
//
//namespace Camela.Web.Framework.Mvc.Filter {
//    public class ValidateRequestEntityAttribute: ActionFilterAttribute {
//        public override void OnActionExecuting(ActionExecutingContext filterContext) {
//            var modelState = filterContext.Controller.ViewData.ModelState;
//
//            if(!modelState.IsValid) {
//                string errorMessages = string.Join("<br/>",
//                    modelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
//
//                throw new ValidateFailureException(errorMessages);
//            }
//        }
//    }
//}