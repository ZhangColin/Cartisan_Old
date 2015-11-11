﻿using System;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Cartisan.Web.Authentication;
using Cartisan.Website.Models.Account;
using Microsoft.Owin.Security;

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
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            // 这不会计入到为执行帐户锁定而统计的登录失败次数中
            // 若要在多次输入错误密码的情况下触发帐户锁定，请更改为 shouldLockout: true
            //            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            //            switch (result) {
            //                case SignInStatus.Success:
            //                    return RedirectToLocal(returnUrl);
            //                case SignInStatus.LockedOut:
            //                    return View("Lockout");
            //                case SignInStatus.RequiresVerification:
            //                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
            //                case SignInStatus.Failure:
            //                default:
            //                    ModelState.AddModelError("", "无效的登录尝试。");
            //                    return View(model);
            //            }
            HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties {
                IsPersistent = true
            }, new ClaimsIdentity(new[] {
                new Claim("User", model.Username), new Claim("Password", model.Password)
            }));

            return Redirect(returnUrl);
        }
    }
}