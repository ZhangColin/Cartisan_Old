using Microsoft.AspNet.Identity;

namespace Cartisan.Website {
    public class CartisanRole: IRole<string> {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}