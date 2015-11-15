﻿using System.Web.Mvc;
using Cartisan.Web.Mvc.Filters;

namespace Cartisan.Website.Controllers {
    public class HomeController: Controller {
        public ActionResult Index() {
            return View();
        }

        [CartisanAuthorize]
        public ActionResult Angular() {
            return View("~/App/views/layout/layout.cshtml");
        }
    }
}