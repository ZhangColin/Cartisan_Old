using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Cartisan.Website {
    public class CartisanRoleManager: RoleManager<CartisanRole> {
        public CartisanRoleManager(IRoleStore<CartisanRole, string> store): base(store) {}

        public static CartisanRoleManager Create(IdentityFactoryOptions<CartisanRoleManager> options, IOwinContext context) {
            var manager = new CartisanRoleManager(new CartisanRoleStore());

            return manager;
        }
    }
}