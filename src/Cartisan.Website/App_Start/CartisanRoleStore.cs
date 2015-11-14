using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Cartisan.Website {
    public class CartisanRoleStore: IRoleStore<CartisanRole, string> {
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

        public Task<CartisanRole> FindByIdAsync(string roleId) {
            return Task.FromResult(new CartisanRole() {
                Id = roleId,
                Name = "Test"
            });
        }

        public Task<CartisanRole> FindByNameAsync(string roleName) {
            return Task.FromResult(new CartisanRole() {
                Id = Guid.NewGuid().ToString(),
                Name = roleName
            });
        }
    }
}