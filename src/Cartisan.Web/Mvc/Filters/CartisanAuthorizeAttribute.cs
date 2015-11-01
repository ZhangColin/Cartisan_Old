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
        public string[] Permissions { get; set; }

        public bool RequireAllPermissions { get; set; }

        public CartisanAuthorizeAttribute(params  string [] permissions) {
            Permissions = permissions;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext) {
//            if(!base.AuthorizeCore(httpContext)) {
//                return false;
//            }

            try {
                HttpCookie authCookie = httpContext.Request.Cookies.Get("CartisanAuth");
                if (authCookie == null || string.IsNullOrEmpty(authCookie.Value)) {
                    return false;
                }

                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                if (ticket == null) {
                    return false;
                }

                User user = JsonConvert.DeserializeObject<User>(ticket.UserData);

                httpContext.Session["User"] = user;
                return true;
            }
            catch(Exception) {
                return false;
            }
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