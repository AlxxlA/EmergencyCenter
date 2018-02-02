using System.Collections.Generic;
using EmergencyCenter.Core.Contracts;
using EmergencyCenter.Core.Contracts.Commands;

namespace EmergencyCenter.Core.Commands.OrderCommands
{
    public class SendPolicemanCommand : OrderCommand, ICommand
    {
        private const string PoliceIsSendedMessage = "Police is on the way.";

        public SendPolicemanCommand(ICommandCenter commandCenter)
            : base(commandCenter)
        {
        }

        public override string Execute(IList<string> parameters)
        {
            base.ParseParameters(parameters);

            var targetPerson = this.CommandCenter.ReturnCharacterById(this.TargetPersonId);

            this.CommandCenter.SendPolicemanToMission(targetPerson);

            return PoliceIsSendedMessage;
        }
    }
}
