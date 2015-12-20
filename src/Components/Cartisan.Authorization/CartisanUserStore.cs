using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Cartisan.Authorization {
    public class CartisanUserStore: 
        IUserStore<CartisanUser, long>, 
        IUserLockoutStore<CartisanUser, long>, 
        IUserPasswordStore<CartisanUser, long>, 
        IUserTwoFactorStore<CartisanUser, long> {
        #region IUserStore
        public void Dispose() {
        }

        public Task CreateAsync(CartisanUser user) {
            return Task.FromResult(0);
        }

        public Task UpdateAsync(CartisanUser user) {
            return Task.FromResult(0);
        }

        public Task DeleteAsync(CartisanUser user) {
            return Task.FromResult(0);
        }

        public Task<CartisanUser> FindByIdAsync(long userId) {
            return Task.FromResult(new CartisanUser() {
                Id = userId,
                UserName = "Test"
            });
        }

        public Task<CartisanUser> FindByNameAsync(string userName) {
            return Task.FromResult(new CartisanUser() {
                Id = 1,
                UserName = userName
            });
        }

        #endregion

        #region IUserLockoutStore

        public Task<DateTimeOffset> GetLockoutEndDateAsync(CartisanUser user) {
            return Task.FromResult(DateTimeOffset.Now);
        }

        public Task SetLockoutEndDateAsync(CartisanUser user, DateTimeOffset lockoutEnd) {
            return Task.FromResult(0);
        }

        public Task<int> IncrementAccessFailedCountAsync(CartisanUser user) {
            return Task.FromResult(0);
        }

        public Task ResetAccessFailedCountAsync(CartisanUser user) {
            return Task.FromResult(0);
        }

        public Task<int> GetAccessFailedCountAsync(CartisanUser user) {
            return Task.FromResult(0);
        }

        public Task<bool> GetLockoutEnabledAsync(CartisanUser user) {
            return Task.FromResult(false);
        }

        public Task SetLockoutEnabledAsync(CartisanUser user, bool enabled) {
            return Task.FromResult(0);
        }

        #endregion


        #region IUserPasswordStore
        public Task SetPasswordHashAsync(CartisanUser user, string passwordHash) {
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(CartisanUser user) {
            return Task.FromResult("Test");
        }

        public Task<bool> HasPasswordAsync(CartisanUser user) {
            return Task.FromResult(true);
        }

        #endregion

        #region IUserTwoFactorStore

        public Task SetTwoFactorEnabledAsync(CartisanUser user, bool enabled) {
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(CartisanUser user) {
            return Task.FromResult(false);
        }

        #endregion
    }
}