using AutoMapper;
using Cartisan.Identity.Domain.Models;
using Cartisan.Identity.Service.Dtos;

namespace Cartisan.Identity.Api {
    public class IdentityMapperProfile: Profile {
        public override string ProfileName {
            get {
                return "IdentityMapperProfile";
            }
        }

        protected override void Configure() {
            Mapper.CreateMap<UserAccount, AccountDto>().ForMember(dto => dto.UserId, cfg => cfg.MapFrom(ua => ua.Id));
        }
    }
}