using System;
using System.Collections.Generic;
using Cartisan.Domain;

namespace Cartisan.Identity.Domain.Models {
    public class UserAccount: Entity<long>, IAggregateRoot {
        public virtual Guid UserGuid { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string PasswordSalt { get; set; }
        public virtual string Email { get; set; }
        public virtual bool IsEmailVerified { get; set; }
        public virtual string Mobile { get; set; }
        public virtual bool IsMobileVerified { get; set; }
        public virtual string TrueName { get; set; }
        public virtual string NickName { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime Created { get; set; }

        protected override IEnumerable<object> GetIdentityComponents() {
            throw new NotImplementedException();
        }
    }
}