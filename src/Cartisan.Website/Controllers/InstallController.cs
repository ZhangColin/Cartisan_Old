using System.Web.Mvc;

namespace Cartisan.Website.Controllers {
    public class InstallController: Controller {
        public InstallController() {}

        public ActionResult Index() {
            return Content("Test");
        }
    }
}