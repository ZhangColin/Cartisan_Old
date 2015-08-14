using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Cartisan.Web.WebApi.Filters {
    public class LogErrorsAttribute : Attribute, IExceptionFilter {
        public bool AllowMultiple { get { return false; } }

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken) {
            Debug.WriteLine(string.Format("Exception Type: {0}", actionExecutedContext.Exception.GetType()));
            Debug.WriteLine(string.Format("Exception Message: {0}", actionExecutedContext.Exception.Message));

            return Task.FromResult<object>(null);
        }
    }
}