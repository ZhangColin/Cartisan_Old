using Autofac;
using AutoMapper;
using Cartisan.EntityFramework;
using Cartisan.QuestionAnswer.Domain.Services;
using Cartisan.QuestionAnswer.Repository;
using Cartisan.Repository;

namespace Cartisan.QuestionAnswer.Service {
    public class QuestionAnswerModule: Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType<QuestionAnswerMapperProfile>().As<Profile>().SingleInstance();

            builder.RegisterType<QuestionAnswerContext>()
//                .As<ContextBase>()
                .WithParameter("connectionString", "cartisanConnectionString");
//            builder.RegisterGeneric(typeof(EntityFrameworkRepository<>)).As(typeof(IRepository<>));

            builder.RegisterType<AcceptService>().SingleInstance();
            builder.RegisterType<QuestionAnswerApplicationService>()
                .As<IQuestionAnswerApplicationService>()
                .InstancePerRequest();


        }
    }
}