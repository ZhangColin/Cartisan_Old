using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Cartisan.Website {
    public class CartisanUserStore: IUserStore<CartisanUser> {
        public void Dispose() {
            throw new System.NotImplementedException();
        }

        public Task CreateAsync(CartisanUser user) {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(CartisanUser user) {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(CartisanUser user) {
            throw new System.NotImplementedException();
        }

        public Task<CartisanUser> FindByIdAsync(string userId) {
            throw new System.NotImplementedException();
        }

        public Task<CartisanUser> FindByNameAsync(string userName) {
            throw new System.NotImplementedException();
        }
    }
}