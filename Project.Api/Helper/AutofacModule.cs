using Autofac;
using Project.Data.Infrastructure;
using Project.Data.Repositories;
using Project.Service;
using Microsoft.AspNetCore.Http;

namespace Project.Startup
{
    public class AutofacModule : Module
    {
        public static void RegisterType(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerDependency();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(InfoCodeRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(InfoCodeService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerDependency();

            builder.RegisterType<IFormFile>()
                .AsImplementedInterfaces().InstancePerDependency();
        }
    }
}