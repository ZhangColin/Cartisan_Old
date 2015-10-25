using System;

namespace Cartisan.Identity.Contract.Dtos {
    public class AccountDto {
        public long Id { get; set; }
        public Guid UserGuid { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }
        public bool IsEmailVerified { get; set; }
        public string Mobile { get; set; }
        public bool IsMobileVerified { get; set; }
        public string TrueName { get; set; }
        public string NickName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
    }
}