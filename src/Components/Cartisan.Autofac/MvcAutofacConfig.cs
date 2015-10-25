using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Compilation;
using System.Web.Http;
using Autofac;
using Cartisan.CommandProcessor;
using Cartisan.DependencyInjection;
using Cartisan.Web.WebApi;
using System.Web.Mvc;
using Cartisan.Web.Mvc;
using Autofac.Integration.Mvc;

namespace Cartisan.Autofac {
    public static class MvcAutofacConfig {
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


            builder.RegisterControllers(assemblies);

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            ServiceLocator.Resolver = new MvcResolver();
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