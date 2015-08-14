using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Cartisan.Web.Authentication;
using Cartisan.Web.Mvc.Results;
using Newtonsoft.Json;

namespace Cartisan.Web.Mvc.Filters {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CartisanAuthorizeAttribute: AuthorizeAttribute {
        protected override bool AuthorizeCore(HttpContextBase httpContext) {
            HttpCookie authCookie = httpContext.Request.Cookies.Get("CartisanAuth");
            if (authCookie == null || string.IsNullOrEmpty(authCookie.Value)) {
                return false;
            }

            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            if (ticket == null) {
                return false;
            }

            User user = JsonConvert.DeserializeObject<User>(ticket.UserData);
            return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext) {
            base.OnAuthorization(filterContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext) {
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest()) {
                filterContext.Result = new JsonNetResult();
            }
            else {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}