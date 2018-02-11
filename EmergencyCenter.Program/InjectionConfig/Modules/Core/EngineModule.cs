using Autofac;
using EmergencyCenter.Core.Contracts.Engine;
using EmergencyCenter.Core.Engine;

namespace EmergencyCenter.Program.InjectionConfig.Modules.Core
{
    public class EngineModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Engine>().As<IEngine>().SingleInstance();
        }
    }
}
