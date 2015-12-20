using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Cartisan.Authorization {
    public class CartisanClaimsIdentityFactory<TUser>: ClaimsIdentityFactory<TUser> where TUser: class, IUser<string> {
        internal const string IdentityProviderClaimType = "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider";
        internal const string DefaultIdentityProviderClaimValue = "ASP.NET Identity";

        public CartisanClaimsIdentityFactory() {
            UserIdClaimType = ClaimTypes.NameIdentifier;
            UserNameClaimType = ClaimsIdentity.DefaultNameClaimType;

            LastLoginTimeType = "LoginTime";
        }

        public async override Task<ClaimsIdentity> CreateAsync(UserManager<TUser, string> manager, TUser user, string authenticationType) {
            var id = new ClaimsIdentity(authenticationType, UserNameClaimType, null);

            id.AddClaim(new Claim(UserIdClaimType, user.Id.ToString(), ClaimValueTypes.String));
            id.AddClaim(new Claim(UserNameClaimType, user.UserName, ClaimValueTypes.String));
            id.AddClaim(new Claim(IdentityProviderClaimType, DefaultIdentityProviderClaimValue, ClaimValueTypes.String));
            id.AddClaim(new Claim(LastLoginTimeType, DateTime.Now.ToString(), ClaimValueTypes.String));

            if (manager.SupportsUserClaim) {
                id.AddClaims(await manager.GetClaimsAsync(user.Id));
            }

            return id;
        }

        public string LastLoginTimeType { get; set; }
    }
}