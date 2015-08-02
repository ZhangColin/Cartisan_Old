namespace Cartisan.Utility {
    public class PasswordGenerator {
        public static string GetHashPassword(string password) {
            return password;
        }

        public static bool Equals(string password, string hashedPassword) {
            return string.Equals(GetHashPassword(password), hashedPassword);
        }
    }
}