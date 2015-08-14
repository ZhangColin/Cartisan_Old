//using System.Linq;
//using NUnit.Framework;
//
//namespace Cartisan.Tests {
//    [TestFixture]
//    public class PaginatedFixture {
//        [Test]
//        public void Paginated() {
//            Paginated<string> page = new Paginated<string>(Enumerable.Empty<string>(), 1, 10, 95);
//
//            Assert.AreEqual(1, page.PageIndex);
//            Assert.AreEqual(10, page.PageSize);
//            Assert.AreEqual(95, page.Total);
//            Assert.AreEqual(10, page.PageTotal);
//        }
//    }
//}