using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Cartisan.Authorization {
    public class CartisanRoleManager: RoleManager<CartisanRole, long> {
        public CartisanRoleManager(IRoleStore<CartisanRole, long> store): base(store) {}

        public static CartisanRoleManager Create(IdentityFactoryOptions<CartisanRoleManager> options, IOwinContext context) {
            var manager = new CartisanRoleManager(new CartisanRoleStore());

            return manager;
        }
    }
}