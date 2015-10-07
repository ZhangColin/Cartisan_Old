using System.Web.Mvc;

namespace Cartisan.Admin.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult Operating(string message, string backUrl, string operationUrl, string confirmInfo) {
            ViewBag.Message = message ?? "正在执行，请耐心等待……";
            ViewBag.ConfirmInfo = confirmInfo ?? "";
            ViewBag.OperationUrl = operationUrl ?? "javascript: window.history.go(-1)";
            ViewBag.BackUrl = backUrl ?? "javascript: window.history.go(-1)";
            return View();
        }

        public ActionResult Success(string message, string backUrl) {
            ViewBag.Message = message ?? "执行成功。";
            ViewBag.BackUrl = backUrl ?? "javascript: window.history.go(-1)";
            return View();
        }

        public ActionResult Error(string message, string operationUrl, string backUrl) {
            ViewBag.Message = message ?? "执行失败。";
            ViewBag.OperationUrl = operationUrl ?? "javascript: window.history.go(-1)";
            ViewBag.BackUrl = backUrl ?? "javascript: window.history.go(-1)";
            return View();
        }
    }
}