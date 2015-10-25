using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Cartisan.Autofac;
using Cartisan.AutoMapper;

namespace Cartisan.WebHost
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebApiAutofacConfig.Initialize();
            AutoMapperConfig.Initialize();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
