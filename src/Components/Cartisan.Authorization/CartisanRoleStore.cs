using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Cartisan.Authorization {
    public class CartisanRoleStore: IRoleStore<CartisanRole, long> {
        public void Dispose() {
        }

        public Task CreateAsync(CartisanRole role) {
            return Task.FromResult(0);
        }

        public Task UpdateAsync(CartisanRole role) {
            return Task.FromResult(0);
        }

        public Task DeleteAsync(CartisanRole role) {
            return Task.FromResult(0);
        }

        public Task<CartisanRole> FindByIdAsync(long roleId) {
            return Task.FromResult(new CartisanRole() {
                Id = roleId,
                Name = "Test"
            });
        }

        public Task<CartisanRole> FindByNameAsync(string roleName) {
            return Task.FromResult(new CartisanRole() {
                Id = 1,
                Name = roleName
            });
        }
    }
}