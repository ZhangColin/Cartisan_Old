//using Autofac;
//using Cartisan.DependencyInjection;
//
//namespace Cartisan.Caching {
//    public class CacheModule: Module {
//        protected override void Load(ContainerBuilder builder) {
//            builder.RegisterType<RuntimeMemoryCache>().As<ICache>();
//            builder.Register((context, parameters) => new DefaultCacheService(context.Resolve<ICache>(), 1))
//                .As<ICacheService>()
//                .SingleInstance();
//        }
//    }
//}