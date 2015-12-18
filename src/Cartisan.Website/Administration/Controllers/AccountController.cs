using System.Threading.Tasks;
using System.Web.Mvc;
using Cartisan.Admin.Models;
using Cartisan.Identity.Contract.Dtos;
using Cartisan.Repository;
using Flurl.Http;

namespace Cartisan.Admin.Controllers {
    public class AccountController: Cartisan.Web.Mvc.Controllers.ControllerBase {

        public AccountController() {}

        public ActionResult Index() {
            return View();
        }

        public async Task<JsonResult> GetAccounts() {
            Paginated<AccountDto> questionDtos = await "http://localhost:50217/api/accounts"
                .GetJsonAsync<Paginated<AccountDto>>();

            return Json(questionDtos, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> CreateAccount(CreateAccountViewModel accountViewModel) {

            return Json(accountViewModel);
        } 
    }
}