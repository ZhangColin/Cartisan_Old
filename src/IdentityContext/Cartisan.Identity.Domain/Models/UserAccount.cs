﻿using System;
using System.Collections.Generic;
using Cartisan.Domain;

namespace Cartisan.Identity.Domain.Models {
    public class UserAccount: Entity<Guid>, IAggregateRoot {
        public string Name { get; set; }

        protected override IEnumerable<object> GetIdentityComponents() {
            throw new NotImplementedException();
        }
    }
}