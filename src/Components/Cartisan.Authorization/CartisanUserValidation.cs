using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Cartisan.Authorization {
    public class CartisanUserValidation: IIdentityValidator<CartisanUser> {
        public Task<IdentityResult> ValidateAsync(CartisanUser item) {
            throw new System.NotImplementedException();

//            if (item.UserName.ToLower().Contains("bad"))
//                return Task.FromResult(IdentityResult.Failed("UserName cannot contain bad"));
//            else if (item.HomeTown.ToLower().Contains("unknown"))
//                return Task.FromResult(IdentityResult.Failed("HomeTown cannot contain unknown city"));
//            else
//                return Task.FromResult(IdentityResult.Success);
        }
    }
}