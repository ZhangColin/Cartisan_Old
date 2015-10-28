using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Cartisan.Identity.Contract.Dtos;
using Cartisan.Identity.Service;
using Flurl.Http;

namespace Cartisan.Admin.Controllers {
    public class AccountController: Controller {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService  accountService) {
            _accountService = accountService;
        }

        public ActionResult Index() {
            return View();
        }

//        [HttpPost]
//        public async Task<JsonResult> AccountList(int draw, int start, int length) {
        public async Task<JsonResult> AccountList() {
            List<AccountDto> accountDtos = await "http://localhost:50217/api/accounts".GetAsync().ReceiveJson<List<AccountDto>>();

            return Json(new {
                draw = 1,
                recordsTotal = 10,
                recordsFiltered = 10,
                data = accountDtos
            }, JsonRequestBehavior.AllowGet);
        }
    }
}