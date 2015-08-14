using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Cartisan.DependencyInjection;

namespace Cartisan.Web.Mvc {
    public class CartisanMvcDependencyResolver: IDependencyResolver {
        public object GetService(Type serviceType) {
            return ServiceLocator.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return (IEnumerable<object>)ServiceLocator.GetService(typeof(IEnumerable<>).MakeGenericType(serviceType));
        }
    }
}