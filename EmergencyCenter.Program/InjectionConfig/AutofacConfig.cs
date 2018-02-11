using System.Reflection;
using Autofac;

namespace EmergencyCenter.Program.InjectionConfig
{
    public class AutofacConfig
    {
        public IContainer Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            return container;
        }
    }
}
