using System.Web.Mvc;

namespace Cartisan.Web.Mvc.Results {
    public class PermanentRedirectResult: ViewResult {
        public string Url { get; set; }

        public PermanentRedirectResult(string url) {
            AssertionConcern.NotEmpty(url, "url is null or empty");

            this.Url = url;
        }

        public override void ExecuteResult(ControllerContext context) {
            AssertionConcern.NotNull(context, "context is null.");

            context.HttpContext.Response.StatusCode = 301;
            context.HttpContext.Response.RedirectLocation = Url;
            context.HttpContext.Response.End();
        }
    }
}