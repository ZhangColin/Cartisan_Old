using System.Data.Entity.ModelConfiguration;
using Cartisan.Identity.Domain.Models;

namespace Cartisan.Identity.Repository.Mappings {
    public class UserAccountMap: EntityTypeConfiguration<UserAccount> {
        public UserAccountMap() {
            this.ToTable("Identity_UserAccounts").HasKey(u => u.Id);

            Property(u => u.Id).HasColumnName("UserId");
            Property(u => u.Name).IsRequired().HasMaxLength(50);
        }
    }
}