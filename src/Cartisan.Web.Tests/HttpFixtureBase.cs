using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using Xunit;

namespace Cartisan.Web.Tests {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.MapRoute("MyRoute", "{controller}/{action}");
        }
    }
    public class HttpFixtureBase {
        public HttpContextBase CreateHttpContext(string targetUrl = null, string httpMethod = "GET") {
            // Create the mock request
            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);

            // Create the mock response
            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

            // Create the mock context, using the request and response
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            // return the mocked context
            return mockContext.Object;
        }

        public void TestRouteMatch(string url, string controller, string action, object routeProperties = null,
            string httpMethod = "GET") {
            // Arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData result = routes.GetRouteData(CreateHttpContext(url, httpMethod));

            // Assert
            Assert.NotNull(result);
            Assert.True(TestIncomingRouteResult(result, controller, action, routeProperties));
        }

        private bool TestIncomingRouteResult(RouteData routeResult, string controller, string action,
            object propertySet = null) {
            Func<object, object, bool> valCompare =
                (v1, v2) => StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;

            bool result = valCompare(routeResult.Values["controller"], controller)
                          && valCompare(routeResult.Values["action"], action);

            if (propertySet != null) {
                PropertyInfo[] propertyInfos = propertySet.GetType().GetProperties();
                foreach (PropertyInfo pi in propertyInfos) {
                    if (!(routeResult.Values.ContainsKey(pi.Name)
                        && valCompare(routeResult.Values[pi.Name], pi.GetValue(propertySet, null)))) {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        public void TestRouteFail(string url) {
            // Arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData result = routes.GetRouteData(CreateHttpContext(url));

            // Assert
            Assert.True(result == null || result.Route == null);
        }
    }
}