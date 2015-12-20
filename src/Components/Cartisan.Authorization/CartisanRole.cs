using Microsoft.AspNet.Identity;

namespace Cartisan.Authorization {
    public class CartisanRole: IRole<long> {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}