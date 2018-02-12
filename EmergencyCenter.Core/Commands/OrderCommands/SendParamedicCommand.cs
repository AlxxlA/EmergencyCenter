using System.Collections.Generic;
using EmergencyCenter.Core.Contracts;
using EmergencyCenter.Validation;

namespace EmergencyCenter.Core.Commands.OrderCommands
{
    public class SendParamedicCommand : OrderCommand
    {
        private const string ParamedicIsSendedMessage = "Paramedic is on the way.";

        public SendParamedicCommand(ICommandCenter commandCenter, IValidator validator)
            : base(commandCenter, validator)
        {
        }

        public override string Execute(IList<string> parameters)
        {
            this.ParseParameters(parameters);

            var targetPerson = this.CommandCenter.Persons.Find(p => p.Id == this.TargetPersonId);

            this.CommandCenter.SendParamedicToMission(targetPerson);

            return ParamedicIsSendedMessage;
        }
    }
}
