using System;
using System.Web.Mvc;
using Cartisan.Web;

namespace Cartisan.Admin.Controllers {
    public class ToolController : Controller {
        public ActionResult SystemTool() {
            return View();
        }

        public ActionResult Restart() {
            return RedirectToAction("Operating", "Home", new {
                message = "正在执行系统重启操作……",
                backUrl = Url.Action("SystemTool"),
                operationUrl = Url.Action("_Restart"),
                confirmInfo = "你确定要重启系统吗？"
            });
        }

        public ActionResult _Restart() {
            try {
                string successUrl = Url.Action("Success", "Home", new {
                    message = "系统重启成功。",
                    backUrl = Url.Action("SystemTool")
                });

                HostEnvironment.RestartAppDomain();

                return Redirect(successUrl);
            }
            catch (Exception) {
                return RedirectToAction("Error", "Home", new {
                    message = "系统重启失败。",
                    backUrl = Url.Action("SystemTool"),
                    operationUrl = Url.Action("Operating", "Home", new {
                        message = "正在执行系统重启操作……",
                        backUrl = Url.Action("SystemTool"),
                        operationUrl = Url.Action("_Restart")
                    })
                });
            }
        }
    }
}