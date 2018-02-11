using Autofac;
using EmergencyCenter.Core.Commands.CreationalCommands;
using EmergencyCenter.Core.Commands.OrderCommands;
using EmergencyCenter.Core.Contracts.Commands;

namespace EmergencyCenter.Program.InjectionConfig.Modules.Core
{
    public class CommandsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // creational commands
            builder.RegisterType<AddPolicemanCommand>().Named<ICommand>("addpoliceman");
            builder.RegisterType<AddParamedicCommand>().Named<ICommand>("addparamedic");
            builder.RegisterType<AddCitizenCommand>().Named<ICommand>("addcitizen");
            builder.RegisterType<AddCriminalCommand>().Named<ICommand>("addcriminal");

            // order commands
            builder.RegisterType<SendPolicemanCommand>().Named<ICommand>("sendpoliceman");
            builder.RegisterType<SendParamedicCommand>().Named<ICommand>("sendparamedic");
            builder.RegisterType<InjurePersonCommand>().Named<ICommand>("injuredperson");
        }
    }
}
