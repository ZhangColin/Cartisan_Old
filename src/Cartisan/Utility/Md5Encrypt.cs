using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Cartisan.Utility {
    public static class Md5Encrypt {
        public static string Md5EncryptPassword(string data) {
//            var encoding = new UTF8Encoding();
//            var bytes = encoding.GetBytes(data);
//            var hashed = MD5.Create().ComputeHash(bytes);
//            return Encoding.UTF8.GetString(hashed);

            return string.Join("", MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(data)).Select(d => d.ToString("x2")));
        }
    }
}