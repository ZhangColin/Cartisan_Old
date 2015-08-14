using System.Web.Http;
using System.Web.Http.Controllers;

namespace Cartisan.Web.WebApi.ModelBinders {
    public class FromHeaderAttribute : ParameterBindingAttribute {
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter) {
            return new HeaderValueParameterBinding(parameter);
        }
    }
}