using Autofac;
using Bzway.Data.Core;

namespace Bzway.Data.SQLServer
{
    public class DependencyRegistrar : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SQLServerDatabase>().As<IDatabase>().Named<IDatabase>("SQLServer");
            base.Load(builder);
        }
    }
}