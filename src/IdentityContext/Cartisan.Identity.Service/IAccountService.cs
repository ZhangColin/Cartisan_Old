using System;
using System.Collections.Generic;
using Cartisan.Identity.Contract.Dtos;

namespace Cartisan.Identity.Service {
    public interface IAccountService {
        AccountDto Get(Guid userId);
        IList<AccountDto> GetAll();

//        void Add(string name);
//        void Update(Guid userId, string name);
//        void Remove(Guid userId);
    }
}