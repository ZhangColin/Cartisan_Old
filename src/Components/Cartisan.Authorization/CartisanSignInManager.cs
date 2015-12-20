using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Cartisan.Authorization {
    public class CartisanSignInManager: SignInManager<CartisanUser, long> {
        public CartisanSignInManager(UserManager<CartisanUser, long> userManager, IAuthenticationManager authenticationManager): base(userManager, authenticationManager) {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(CartisanUser user) {
            return user.GenerateUserIdentityAsync((CartisanUserManager)UserManager, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public static CartisanSignInManager Create(IdentityFactoryOptions<CartisanSignInManager> options, IOwinContext context) {
            return new CartisanSignInManager(context.GetUserManager<CartisanUserManager>(), context.Authentication);
        }
    }
}