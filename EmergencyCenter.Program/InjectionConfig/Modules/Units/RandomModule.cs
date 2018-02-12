using Autofac;
using EmergencyCenter.Units.Contracts.Random;
using EmergencyCenter.Units.Random;

namespace EmergencyCenter.Program.InjectionConfig.Modules.Units
{
    public class RandomModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RandomGenerator>().As<IRandomGenerator>();
        }
    }
}
