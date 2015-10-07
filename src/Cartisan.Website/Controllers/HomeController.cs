using System.Web.Mvc;

namespace Cartisan.Website.Controllers {
    public class HomeController: Controller {
        public ActionResult Index() {
            return Content("HomeIndex");
        }
    }
}