using System.Collections.Generic;
using EmergencyCenter.Core.Contracts;
using EmergencyCenter.Validation;

namespace EmergencyCenter.Core.Commands.OrderCommands
{
    public class SendPolicemanCommand : OrderCommand
    {
        private const string PoliceIsSendedMessage = "Police is on the way.";

        public SendPolicemanCommand(ICommandCenter commandCenter, IValidator validator)
            : base(commandCenter, validator)
        {
        }

        public override string Execute(IList<string> parameters)
        {
            this.ParseParameters(parameters);

            var targetPerson = this.CommandCenter.Persons.ReturnByCriteria(p => p.Id == this.TargetPersonId);

            this.CommandCenter.SendPolicemanToMission(targetPerson);

            return PoliceIsSendedMessage;
        }
    }
}
