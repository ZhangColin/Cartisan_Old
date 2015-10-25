using System.Data.Entity.ModelConfiguration;
using Cartisan.Identity.Domain.Models;

namespace Cartisan.Identity.Repository.Mappings {
    public class UserAccountMap: EntityTypeConfiguration<UserAccount> {
        public UserAccountMap() {
            this.ToTable("ID_UserAccounts").HasKey(u => u.Id);

            Property(u => u.Id);
            Property(u => u.UserGuid).IsRequired();
            Property(u => u.UserName).HasMaxLength(64);
            Property(u => u.Password).HasMaxLength(128);
            Property(u => u.PasswordSalt).HasMaxLength(32);
            Property(u => u.Email).HasMaxLength(64);
            Property(u => u.IsEmailVerified);
            Property(u => u.Mobile).HasMaxLength(64);
            Property(u => u.IsMobileVerified);
            Property(u => u.TrueName).HasMaxLength(64);
            Property(u => u.NickName).HasMaxLength(64);
            Property(u => u.IsActive);
            Property(u => u.IsDeleted);
            Property(u => u.Created);
        }
    }
}