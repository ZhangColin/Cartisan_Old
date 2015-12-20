using Microsoft.AspNet.Identity;

namespace Cartisan.Authorization {
    public class CartisanPasswordHasher: IPasswordHasher {
        public string HashPassword(string password) {
            return password;
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword) {
            if (hashedPassword == providedPassword)
                return PasswordVerificationResult.Success;
            else
                return PasswordVerificationResult.Failed;
        }
    }
}