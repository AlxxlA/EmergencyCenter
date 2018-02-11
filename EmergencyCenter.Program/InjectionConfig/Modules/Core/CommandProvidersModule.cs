using Autofac;
using EmergencyCenter.Core.CommandProviders;
using EmergencyCenter.Core.Contracts.CommandProviders;

namespace EmergencyCenter.Program.InjectionConfig.Modules.Core
{
    public class CommandProvidersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommandParser>().As<ICommandParser>().SingleInstance();
            builder.RegisterType<CommandProcessor>().As<ICommandProcessor>().SingleInstance();
        }
    }
}
