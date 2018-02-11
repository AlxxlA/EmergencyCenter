using Autofac;
using EmergencyCenter.Core.Contracts.Factories;
using EmergencyCenter.Core.Factories;

namespace EmergencyCenter.Program.InjectionConfig.Modules.Core
{
    public class FactoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CharacterFactory>().As<ICharacterFactory>().SingleInstance();
            builder.RegisterType<CommandFactory>().As<ICommandFactory>().SingleInstance();
        }
    }
}
