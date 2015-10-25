using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cartisan.Identity.Domain.Models;

namespace Cartisan.Identity.Api.Controllers {
    public class TenantsController: ApiController {
        public IHttpActionResult GetTenant(Guid tenantId) {
            Tenant tenant = new Tenant() {Id = tenantId};
            //            if(userId==null) {
            //                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            //            }
            //            UserAccount userAccount = db.UserAccounts.Find(userId);
            if(tenant==null) {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.NotFound, "");
            }

            return Ok(tenant);
        }
    }
}