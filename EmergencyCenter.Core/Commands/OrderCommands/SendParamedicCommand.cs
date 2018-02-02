using System.Collections.Generic;
using EmergencyCenter.Core.Contracts;
using EmergencyCenter.Core.Contracts.Commands;

namespace EmergencyCenter.Core.Commands.OrderCommands
{
    public class SendParamedicCommand : OrderCommand, ICommand
    {
        private const string ParamedicIsSendedMessage = "Paramedic is on the way.";

        public SendParamedicCommand(ICommandCenter commandCenter)
            : base(commandCenter)
        {
        }

        public override string Execute(IList<string> parameters)
        {
            base.ParseParameters(parameters);

            var targetPerson = this.CommandCenter.ReturnCharacterById(this.TargetPersonId);

            this.CommandCenter.SendParamedicToMission(targetPerson);

            return ParamedicIsSendedMessage;
        }
    }
}
