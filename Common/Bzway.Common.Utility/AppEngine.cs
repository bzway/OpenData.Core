using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Bzway.Common.Utility
{
    public class AppEngine
    {
        private static object lockObj = new object();
        private static AppEngine app;
        public static AppEngine Current
        {
            get
            {
                if (app == null)
                {
                    lock (lockObj)
                    {
                        if (app != null)
                        {
                            return app;
                        }
                        app = new AppEngine();
                        return app;
                    }
                }
                return app;
            }
        }

        private ContainerBuilder containerBuilder = new ContainerBuilder();
        private IContainer container;
        public AutofacServiceProvider Build(IServiceCollection services)
        {
            containerBuilder.Populate(services);
            container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }
        public AppEngine Register<I, T>(string Name = "")
        {
            if (string.IsNullOrEmpty(Name))
            {
                this.containerBuilder.RegisterType<T>().As<I>();
            }
            else
            {
                this.containerBuilder.RegisterType<T>().Named<I>(Name).As<I>();
            }
            return this;
        }
        public I Get<I>(string Name = "")
        {
            if (string.IsNullOrEmpty(Name))
            {
                return this.container.Resolve<I>();
            }
            return this.container.ResolveNamed<I>(Name);
        }
    }
}