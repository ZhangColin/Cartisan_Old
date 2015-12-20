using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Cartisan.Autofac;
using Cartisan.AutoMapper;
using Cartisan.Web.Data;
using Cartisan.Web.Mvc.Themes;

namespace Cartisan.Website {
    public class MvcApplication: System.Web.HttpApplication {
        protected void Application_Start() {
            MvcAutofacConfig.Initialize();
            AutoMapperConfig.Initialize();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ThemeableRazorViewEngine());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest(object sender, EventArgs e) {
//            if(!DataSettingsHelper.DatabaseIsInstalled()) {
//                string installUrl = string.Format("{0}/install",
//                    "http://" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"]);
//
//                if(!this.Request.RawUrl.Contains("install")) {
//                    this.Response.Redirect(installUrl);
//                }
//                
//            }
        }
    }
}
