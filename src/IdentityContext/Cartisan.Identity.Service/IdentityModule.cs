using System.Data.Entity;
using Autofac;
using AutoMapper;
using Cartisan.CommandProcessor;
using Cartisan.EntityFramework;
using Cartisan.Identity.Repository;
using Cartisan.Identity.Service.Commands;
using Cartisan.Repository;

namespace Cartisan.Identity.Service {
    public class IdentityModule: Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType<IdentityMapperProfile>().As<Profile>().SingleInstance();

            builder.RegisterType<IdentityContext>()
                .As<ContextBase>()
                .WithParameter("connectionString", "cartisanConnectionString");
            builder.RegisterGeneric(typeof(EntityFrameworkRepository<>)).As(typeof(IRepository<>));
                //.UsingConstructor(()=>ServiceLocator.GetService<IdentityContext>());

            builder.RegisterType<AccountService>().As<IAccountService>().InstancePerRequest();

            builder.RegisterType<AccountProcessor>().As<ICommandHandler<AddUser>>();
            builder.RegisterType<AccountProcessor>().As<ICommandHandler<UpdateUser>>();
            builder.RegisterType<AccountProcessor>().As<ICommandHandler<DeleteUser>>();
        }
    }
}