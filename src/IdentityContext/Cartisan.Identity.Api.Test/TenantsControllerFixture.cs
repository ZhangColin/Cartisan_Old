using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Cartisan.Identity.Api.Controllers;
using Cartisan.Identity.Domain.Models;
using Xunit;
using Xunit.Abstractions;

namespace Cartisan.Identity.Api.Test {
    public class TenantsControllerFixture {
        private readonly ITestOutputHelper _output;
        public TenantsControllerFixture(ITestOutputHelper output) {
            _output = output;
        }

        [Fact]
        public void GetTenant() {
            

            //_output.WriteLine(task.Result.ToJson());
        }
    }
}