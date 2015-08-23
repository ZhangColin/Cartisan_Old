using System;
using System.Collections.Generic;
using Cartisan.Domain;

namespace Cartisan.EntityFramework.Tests.Infrastructure {
    public class Person: Entity<int>, IAggregateRoot {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        protected override IEnumerable<object> GetIdentityComponents() {
            throw new System.NotImplementedException();
        }
    }
}