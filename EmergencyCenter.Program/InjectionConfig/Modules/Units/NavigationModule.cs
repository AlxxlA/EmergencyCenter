using Autofac;
using EmergencyCenter.Units.Contracts.Navigation;
using EmergencyCenter.Units.Navigation;

namespace EmergencyCenter.Program.InjectionConfig.Modules.Units
{
    public class NavigationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Map>().As<IMap>().SingleInstance();
            builder.RegisterType<PathFinder>().As<IPathFinder>().SingleInstance();
        }
    }
}
