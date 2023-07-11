using Autofac;
using Projects.Repository.Common;

namespace Projects.Repository
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountRepository>().As<IAccountRepository>();
            builder.RegisterType<AdvertisementRepository>().As<IAdvertisementRepository>();
        }
    }
}
