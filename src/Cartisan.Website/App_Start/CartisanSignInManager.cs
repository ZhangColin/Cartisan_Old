﻿using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Cartisan.Website {
    public class CartisanSignInManager: SignInManager<CartisanUser, string> {
        public CartisanSignInManager(UserManager<CartisanUser, string> userManager, IAuthenticationManager authenticationManager): base(userManager, authenticationManager) {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(CartisanUser user) {
            return user.GenerateUserIdentityAsync((CartisanUserManager)UserManager);
        }

        public static CartisanSignInManager Create(IdentityFactoryOptions<CartisanSignInManager> options, IOwinContext context) {
            return new CartisanSignInManager(context.GetUserManager<CartisanUserManager>(), context.Authentication);
        }
    }
}