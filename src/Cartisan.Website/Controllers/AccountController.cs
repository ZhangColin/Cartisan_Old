using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using Cartisan.Authorization;
using Cartisan.Web.Mvc.Filters;
using Cartisan.Website.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Cartisan.Website.Controllers {
    [CartisanAuthorize]
    public class AccountController: Controller {

        private CartisanSignInManager _signInManager;
        private CartisanUserManager _userManager;

        public AccountController() {
        }

        public AccountController(CartisanUserManager userManager, CartisanSignInManager signInManager) {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public CartisanSignInManager SignInManager {
            get {
                return _signInManager ?? HttpContext.GetOwinContext().Get<CartisanSignInManager>();
            }
            private set {
                _signInManager = value;
            }
        }

        public CartisanUserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<CartisanUserManager>();
            }
            private set {
                _userManager = value;
            }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl = "") {
            if(string.IsNullOrWhiteSpace(returnUrl)) {
                returnUrl = Request.ApplicationPath;
            }

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl) {
            if(!ModelState.IsValid) {
                if(Request.IsAjaxRequest()) {
                    return Json(new {
                        success = false,
                        message = string.Join(" ", ModelState.Values.SelectMany(m => m.Errors).Select(err => err.ErrorMessage))
                    });
                }
                else{
                    return View(model);
                }
            }

            // 这不会计入到为执行帐户锁定而统计的登录失败次数中
            // 若要在多次输入错误密码的情况下触发帐户锁定，请更改为 shouldLockout: true
            var result =
                await
                    SignInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe,
                        shouldLockout: false);
            if(Request.IsAjaxRequest()) {
                switch (result) {
                    case SignInStatus.Success:
                        return Json(new {
                            success = true,
                            returnUrl
                        });
                    case SignInStatus.LockedOut:
                        return Json(new {
                            success = false,
                            message = "LockedOut"
                        });
                    case SignInStatus.RequiresVerification:
                        return Json(new {
                            success = true,
                            model.RememberMe,
                            returnUrl,
                            message="SendCode"
                        });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "无效的登录尝试。");
                        return Json(new {
                            success = false,
                            message = string.Join(" ", ModelState.Values.SelectMany(m=>m.Errors).Select(err=>err.ErrorMessage))
                        });
                }
            }
            else {
                switch (result) {
                    case SignInStatus.Success:
                        return RedirectToLocal(returnUrl);
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new {
                            ReturnUrl = returnUrl,
                            RememberMe = model.RememberMe
                        });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "无效的登录尝试。");
                        return View(model);
                }
            }
            
        }

//        [AllowAnonymous]
//        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe) {
//            // 要求用户已通过使用用户名/密码或外部登录名登录
//            if(!await SignInManager.HasBeenVerifiedAsync()) {
//                return View("Error");
//            }
//
//           
//        }

        [AllowAnonymous]
        public ActionResult Register() {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model) {
            if(ModelState.IsValid) {
                var user = new CartisanUser() {
                    UserName = model.UserName,
//                    Email = model.Email
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if(result.Succeeded) {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // 有关如何启用帐户确认和密码重置的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=320771
                    // 发送包含此链接的电子邮件
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "确认你的帐户", "请通过单击 <a href=\"" + callbackUrl + "\">這裏</a>来确认你的帐户");

                    return RedirectToAction("Index", "Home");
                }

                AddErrors(result);
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout() {
            SignInManager.AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }

        private ActionResult RedirectToLocal(string returnUrl) {
            if(Url.IsLocalUrl(returnUrl)) {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result) {
            foreach(var error in result.Errors) {
                ModelState.AddModelError("", error);
            }
        }
    }
}