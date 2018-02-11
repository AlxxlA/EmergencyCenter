using Autofac;
using EmergencyCenter.Core.Contracts.Data;
using EmergencyCenter.Core.Data;
using EmergencyCenter.Units.Contracts.Characters;

namespace EmergencyCenter.Program.InjectionConfig.Modules.Core
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PersonDatabase<IPerson>>().As<IDatabase<IPerson>>().SingleInstance();
            builder.RegisterType<PersonDatabase<IParamedic>>().As<IDatabase<IParamedic>>().SingleInstance();
            builder.RegisterType<PersonDatabase<IPoliceman>>().As<IDatabase<IPoliceman>>().SingleInstance();
            builder.RegisterType<PersonDatabase<ICitizen>>().As<IDatabase<ICitizen>>().SingleInstance();
            builder.RegisterType<PersonDatabase<ICriminal>>().As<IDatabase<ICriminal>>().SingleInstance();
        }
    }
}
