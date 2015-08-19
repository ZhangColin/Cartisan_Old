﻿namespace Cartisan.Web.Mvc.Results {
    public class HttpNotFoundResult: HttpStatusCodeResult {
        public HttpNotFoundResult() : this(null) { }

        public HttpNotFoundResult(string statusDescription) : base(404, statusDescription) { }
    }
}