using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Cartisan.AutoMapper;
using Cartisan.DependencyInjection;
using Cartisan.EntityFramework;
using Cartisan.Identity.Domain.Models;
using Cartisan.Repository;
using Dapper;
using MySql.Data.MySqlClient;
using Cartisan.Identity.Contract.Dtos;

namespace Cartisan.Identity.Service {
    public class AccountService: IAccountService {
        private readonly IRepository<UserAccount> _userRepository;
        public AccountService(IRepository<UserAccount> userRepository) {
            _userRepository = userRepository;
        }

        public AccountDto Get(Guid userId) {
            //DbProviderFactory fac = DbProviderFactories.GetFactory("MySql.Data.MySqlClient");
            
            using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["cartisanConnectionString"].ConnectionString)) {
                return connection.Query<AccountDto>("select * from Identity_UserAccounts where userId = @userId", new { userId })
                    .FirstOrDefault();
            }
            //return _userRepository.Get(userId).MapTo<AccountDto>();
        }

        public IList<AccountDto> GetAll() {
            var userAccounts = _userRepository.All().ToList();
            return userAccounts.Select(account => account.MapTo<AccountDto>()).ToList();
        }

        public AccountDto CreateAccount(string userName, string password, string email, string mobile, string trueName, string nickName) {
            throw new NotImplementedException();
        }
        


        //        public void Add(string name) {
//            _userRepository.Add(new UserAccount() {
//                Name = name
//            });
//        }
//
//        public void Update(Guid userId, string name) {
//            var userAccount = _userRepository.Get(userId);
//            userAccount.Name = name;
//
//            _userRepository.Save(userAccount);
//        }
//
//        public void Remove(Guid userId) {
//            _userRepository.Remove(userId);
//        }
    }
}