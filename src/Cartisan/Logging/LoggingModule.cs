//using System.Collections.Generic;
//using Autofac;
//using Autofac.Core;
//using Cartisan.Logging.Log4Net;
//
//namespace Cartisan.Logging {
//    public class LoggingModule: Module {
//        protected override void Load(ContainerBuilder builder) {
//            builder.RegisterType<Log4NetLoggerFactory>().As<ILoggerFactory>().InstancePerLifetimeScope();
//
//            builder.Register(CreateLogger).As<ILogger>().InstancePerDependency();
//        }
//
//        private static ILogger CreateLogger(IComponentContext context, IEnumerable<Parameter> parameters) {
//            var loggerFactory = context.Resolve<ILoggerFactory>();
//
//            return loggerFactory.CreateLogger("Cartisan");
//        }
//    }
//}