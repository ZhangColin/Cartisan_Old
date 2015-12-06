﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Cartisan.Identity.Api.Models;
using Cartisan.Identity.Contract.Dtos;
using Cartisan.Identity.Service;
using Cartisan.Identity.Service.Commands;
using Cartisan.Repository;

namespace Cartisan.Identity.Api.Controllers {
    [RoutePrefix("api/Accounts")]
    public class AccountsController : ApiController
    {
        private readonly IAccountService _accountService;
//        private readonly ICommandBus _commandBus;
//        private readonly ICommandHandler<AddUser> _addUserCommand;

        public AccountsController(IAccountService accountService) {
            _accountService = accountService;
        }

        //        public AccountsController(IAccountService accountService, ICommandBus commandBus, ICommandHandler<AddUser> addUserCommand) {
//            _accountService = accountService;
//            _commandBus = commandBus;
//            _addUserCommand = addUserCommand;
//        }

        // GET: api/Account
        public async Task<Paginated<AccountDto>> GetAccounts() {

            return await Task.FromResult<Paginated<AccountDto>>(new Paginated<AccountDto>(_accountService.GetAll(), 1,10, 100));
        }

        // GET: api/Account/5
        public AccountDto GetAccount(Guid userId)
        {
//            if(userId==null) {
//                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
//            }
//            UserAccount userAccount = db.UserAccounts.Find(userId);
//            if(userAccount==null) {
//                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.NotFound, "");
//            }

//            return Ok(userAccount);
            return _accountService.Get(userId);
        }

        // POST: api/Account
        public void PostAccount(AddUser addUser) {
//            _addUserCommand.Execute(addUser);
//            _commandBus.Submit(addUser);
        }

        // PUT: api/Account/5
        public void PutAccount(UpdateUser updateUser) {
//            _commandBus.Submit(updateUser);
        }

        // DELETE: api/Account/5
        public void DeleteAccount(DeleteUser deleteUser) {
//            _commandBus.Submit(deleteUser);
        }



        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterByEmail model) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            

            return Ok();
        }
    }
}
