using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Cartisan.AutoMapper;
using Cartisan.Identity.Domain.Models;
using Cartisan.Identity.Service.Dtos;
using Cartisan.Repository;
using Dapper;
using MySql.Data.MySqlClient;

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
            return _userRepository.All().ToList().Select(account => account.MapTo<AccountDto>()).ToList();
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