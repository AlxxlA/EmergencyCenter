using Autofac;
using EmergencyCenter.Core;
using EmergencyCenter.Core.Contracts;

namespace EmergencyCenter.Program.InjectionConfig.Modules.Core
{
    public class CommandCenterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommandCenter>().As<ICommandCenter>().SingleInstance();
        }
    }
}
