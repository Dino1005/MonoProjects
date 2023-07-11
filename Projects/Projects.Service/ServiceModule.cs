using Projects.Service.Common;
using Autofac;

namespace Projects.Service
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<AdvertisementService>().As<IAdvertisementService>();
        }
    }
}
