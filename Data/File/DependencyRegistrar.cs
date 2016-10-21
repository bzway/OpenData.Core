using Autofac;
using Bzway.Data.Core;

namespace Bzway.Data.JsonFile
{
    public class DependencyRegistrar : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileDatabase>().As<IDatabase>().Named<IDatabase>("Default");
            base.Load(builder);
        }
    }
}
