using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Cartisan.DependencyInjection;

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
            var enumerable = GlobalConfiguration.Configuration.DependencyResolver.GetServices(typeof(TService));
            if(!enumerable.Any()) {
                return Enumerable.Empty<TService>();
            }
            return (IEnumerable<TService>)enumerable;
        }

        //        public object Resolve(Type serviceType) {
//            return GlobalConfiguration.Configuration.DependencyResolver.GetService(serviceType);
//        }
//
//        public TService Resolve<TService>() {
//            return (TService)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(TService));
//        }
//
//        public TService ResolveNamed<TService>(string serviceName) {
//            throw new NotImplementedException();
//        }
//
//        public TService ResolveKeyed<TService>(object serviceKey) {
//            throw new NotImplementedException();
//        }
    }
}