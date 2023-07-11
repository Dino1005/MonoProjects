using Autofac;
using Autofac.Integration.WebApi;
using Projects.Repository;
using Projects.Repository.Common;
using Projects.Service;
using System.Reflection;
using System.Web.Http;

namespace Projects.WebApi.App_Start
{
    public class ContainerConfig
    {
        public static void Configure()
        {
            var config = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterType<Database>().SingleInstance();

            IContainer container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}