using Autofac;
using Bzway.Data.Core;

namespace Bzway.Data.Mongo
{

    public class DependencyRegistrar : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MongoDatabase>().As<IDatabase>().Named<IDatabase>("MongoDB");
            base.Load(builder);
        }
    }
}
