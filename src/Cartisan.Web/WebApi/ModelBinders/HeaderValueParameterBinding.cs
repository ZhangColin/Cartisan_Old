using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.ValueProviders;
using Cartisan.Web.WebApi.Providers;

namespace Cartisan.Web.WebApi.ModelBinders {
    public class HeaderValueParameterBinding : HttpParameterBinding {
        private HeaderValueProviderFactory _factory;

        public HeaderValueParameterBinding(HttpParameterDescriptor descriptor) : base(descriptor) {
            _factory = new HeaderValueProviderFactory();
        }

        public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext context,
            CancellationToken cancellationToken) {
            IValueProvider valueProvider = _factory.GetValueProvider(context);
            if (valueProvider != null) {
                ValueProviderResult result = valueProvider.GetValue(Descriptor.ParameterName);
                if (result != null) {
                    SetValue(context, result.RawValue);
                }
            }
            return Task.FromResult<object>(null);
        }
    }
}