using System.Data.Entity;

namespace Cartisan.EntityFramework.Tests.Infrastructure {
    public class PeopleContext: ContextBase {
        public IDbSet<Person> People { get; set; }
        public IDbSet<Book> Books { get; set; }  
    }
}