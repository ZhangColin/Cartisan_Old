using System.Web.Mvc;

namespace Cartisan.Website.Controllers {
    public class CartisanAppController: Controller {
        public ActionResult Load(string viewUrl) {
            if(!viewUrl.StartsWith("~")) {
                viewUrl = "~" + viewUrl;
            }

            return View(viewUrl);
        }
    }
}