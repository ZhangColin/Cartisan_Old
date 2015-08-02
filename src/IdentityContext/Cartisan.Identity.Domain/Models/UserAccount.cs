using System;
using Cartisan.Domain;

namespace Cartisan.Identity.Domain.Models {
    public class UserAccount: Entity<Guid> {
        public string Name { get; set; }

    }
}