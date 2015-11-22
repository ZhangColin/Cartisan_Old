using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Compilation;
using System.Web.Http;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Integration.WebApi;
using Cartisan.CommandProcessor;
using Cartisan.Configuration;
using Cartisan.DependencyInjection;
using Cartisan.EntityFramework;
using Cartisan.Repository;
using Cartisan.Web.WebApi;

namespace Cartisan.Autofac {
    public static class WebApiAutofacConfig {
        private static string assemblySkipLoadingPattern =
           "^System|^mscorlib|^Microsoft|^CppCodeProvider|^VJSharpCodeProvider|^WebDev|^Castle|^Iesi|^log4net|^NHibernate|^nunit|^TestDriven|^MbUnit|^Rhino|^QuickGraph|^TestFu|^Telerik|^ComponentArt|^MvcContrib|^AjaxControlToolkit|^Antlr3|^Remotion|^Recaptcha";
        private static string assemblyRestrictToLoadingPattern = ".*";

        public static void Initialize() {
            ContainerBuilder builder = new ContainerBuilder();


            Assembly[] assemblies =
                BuildManager.GetReferencedAssemblies().Cast<Assembly>().Where(assembly => assembly.FullName.StartsWith("Cartisan")).ToArray();
            builder.RegisterIDependency(assemblies);

            builder.RegisterType<DefaultCommandBus>().As<ICommandBus>();
            //                        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerHttpRequest();

            //            try {
            //assemblies.SelectMany(assembly => assembly.GetTypes())
            //                .Where(type => type.IsAssignableFrom(typeof (IModule)) && type.IsClass)
            //                .ForEach(type => {
            //                    IModule module = (IModule) Activator.CreateInstance(type);
            //                    module.Load(builder);
            //                });
            //            }
            //            catch(Exception ex) {
            //                
            //                throw;
            //            }

//            builder.RegisterType<UserContext>().InstancePerHttpRequest();
            builder.RegisterAssemblyModules(assemblies);

            var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies().Where(assembly=>assembly.FullName.StartsWith("Cartisan")).SelectMany(assembly=>assembly.GetTypes()).Where(type=>
                type.IsPublic && !type.IsAbstract && type.IsClass && typeof(DbContext).IsAssignableFrom(type));

            foreach(var dbContextType in dbContextTypes) {
                var entityTypes = dbContextType.Assembly.GetTypes().Where(type => type.BaseType != null && type.BaseType.IsGenericType
                    && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>))
                    .Select(configType=>configType.BaseType.GenericTypeArguments[0]);

                foreach(var entityType in entityTypes) {
                    var implementationType = typeof(EntityFrameworkRepository<>).MakeGenericType(entityType);
                    var defineType = typeof(IRepository<>).MakeGenericType(entityType);
                    builder.RegisterType(implementationType).As(defineType)
                        .WithParameter(new ResolvedParameter((pi, ctx)=>true, (pi, ctx)=>ctx.Resolve(dbContextType)));

                }
            }


            //            builder.RegisterControllers(assemblies);
            builder.RegisterApiControllers(assemblies);

            IContainer container = builder.Build();
//            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
//            ServiceLocator.Resolver = new MvcResolver();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            ServiceLocator.Resolver = new ApiResolver();
        }

        private static bool Matches(string assemblyFullName) {
            return !Matches(assemblyFullName, assemblySkipLoadingPattern)
                   && Matches(assemblyFullName, assemblyRestrictToLoadingPattern);
        }

        private static bool Matches(string assemblyFullName, string pattern) {
            return Regex.IsMatch(assemblyFullName, pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }
    }
    
}