using System;
using System.Collections.Generic;
using Cartisan.Domain;

namespace Cartisan.EntityFramework.Tests.Infrastructure {
    public class Book: Entity<int> {
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public DateTime PublishedOn { get; set; }

        protected override IEnumerable<object> GetIdentityComponents() {
            throw new System.NotImplementedException();
        }
    }
}