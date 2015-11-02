using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Cartisan.Web.Authentication;
using Cartisan.Website.Models.Account;

namespace Cartisan.Website.Controllers {
    public class AccountController: Controller {
        public ActionResult Login(string returnUrl = "") {
            if(string.IsNullOrWhiteSpace(returnUrl)) {
                returnUrl = Request.ApplicationPath;
            }

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginModel, string returnUrl = "") {

            if(!ModelState.IsValid) {
                throw new Exception("表单填写错误。");
            }

            User user = new User() {
                UserName = loginModel.Username,
                UserId = 1
            };

            var userData = user.ToString();
            var authTicket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now, DateTime.Now.AddDays(7), true,
                userData);
            var authCookie = new HttpCookie(ConfigurationManager.AppSettings["CartisanAuth"]) {
                Value = FormsAuthentication.Encrypt(authTicket)
            };

            Response.Cookies.Add(authCookie);

            return Redirect(returnUrl);

            //            return Json(new {
            //                Success = true,
            //                TargetUrl = returnUrl
            //            });
        }
    }
}