using System;
using System.Collections.Generic;
using Cartisan.Domain;

namespace Cartisan.Identity.Domain.Models {
    public class Tenant: Entity<Guid>, IAggregateRoot {
        protected override IEnumerable<object> GetIdentityComponents() {
            throw new NotImplementedException();
        }
    }
}