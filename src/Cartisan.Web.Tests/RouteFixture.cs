using Xunit;

namespace Cartisan.Web.Tests {
    public class RouteFixture : HttpFixtureBase {
        [Fact]
        public void TestIncomingRoutes() {
            TestRouteMatch("~/Admin/Index", "Admin", "Index");
            TestRouteMatch("~/One/Two", "One", "Two");

            TestRouteFail("~/Admin/Index/Segment");
            TestRouteFail("~/Admin");
        }
    }
}