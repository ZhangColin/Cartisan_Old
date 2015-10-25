using System;
using Cartisan.Identity.Domain.Models;
using Cartisan.Repository;

namespace Cartisan.Identity.Service {
    public interface IIdentityApplicationService {
        Tenant GetTenant(string tenantId);
    }

    public class IdentityApplicationService: IIdentityApplicationService {
        private readonly IRepository<Tenant> _tenantRepository;
        public IdentityApplicationService(IRepository<Tenant> tenantRepository) {
            _tenantRepository = tenantRepository;
        }

        public Tenant GetTenant(string tenantId) {
            return this._tenantRepository.Get(new Guid(tenantId));
        }
    }
}