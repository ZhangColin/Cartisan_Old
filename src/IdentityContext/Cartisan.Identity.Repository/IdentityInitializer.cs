using System.Data.Entity;

namespace Cartisan.Identity.Repository {
    public class IdentityInitializer: DropCreateDatabaseIfModelChanges<IdentityContext> {
        protected override void Seed(IdentityContext context) {
            //            context.UserAccounts.Add(new UserAccount() {
            //                Name = "Test1"
            //            });
            //            context.UserAccounts.Add(new UserAccount() {
            //                Name = "Test2"
            //            });
            //            context.SaveChanges();
        }
    }
}