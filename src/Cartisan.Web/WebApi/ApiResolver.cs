using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using Cartisan.IoC;

namespace Cartisan.Web.WebApi {
    public class ApiResolver: IResolver {
        public object GetService(Type serviceType) {
            return GlobalConfiguration.Configuration.DependencyResolver.GetService(serviceType);
        }

        public TService GetService<TService>() {
            return (TService)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(TService));
        }

        public IEnumerable GetServices(Type serviceType) {
            return GlobalConfiguration.Configuration.DependencyResolver.GetServices(serviceType);
        }

        public IEnumerable<TService> GetServices<TService>() {
            return (IEnumerable<TService>)GlobalConfiguration.Configuration.DependencyResolver.GetServices(typeof(TService));
        }
    }
}