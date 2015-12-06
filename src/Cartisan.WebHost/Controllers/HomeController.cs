using System.Web.Mvc;

namespace Cartisan.WebHost.Controllers {
    public class HomeController: Controller {
        public ActionResult Index() {
            return RedirectToAction("Index", "Help", new {area="HelpPage"});
        }
    }
}