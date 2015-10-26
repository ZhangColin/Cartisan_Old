using AutoMapper;
using Cartisan.Identity.Contract.Dtos;
using Cartisan.Identity.Domain.Models;

namespace Cartisan.Identity.Service {
    public class IdentityMapperProfile: Profile {
//        public override string ProfileName {
//            get {
//                return "IdentityMapperProfile";
//            }
//        }

        protected override void Configure() {
            Mapper.CreateMap<UserAccount, AccountDto>();
        }
    }
}