using System;
using System.Collections.Generic;
using EmergencyCenter.Core.Contracts;
using EmergencyCenter.Core.Contracts.Commands;
using EmergencyCenter.Units.Characters.Enums;

namespace EmergencyCenter.Core.Commands.OrderCommands
{
    public class InjurePersonCommand : OrderCommand, ICommand
    {
        private const string InjuredPersonMessage = "Person was injured with {0}";
        private const string InvalidCommandArgs = "Invalid injure person command args.";

        public InjurePersonCommand(ICommandCenter commandCenter)
            : base(commandCenter)
        {
        }

        public override string Execute(IList<string> parameters)
        {
            base.ParseParameters(parameters);

            InjuryType injury;

            try
            {
                string injuryStr = parameters[1];

                injury = (InjuryType)Enum.Parse(typeof(InjuryType), injuryStr);
            }
            catch (Exception e)
            {
                return InvalidCommandArgs;
            }

            var person = this.CommandCenter.ReturnCharacterById(this.TargetPersonId);

            this.CommandCenter.InjurePerson(person, injury);

            return string.Format(InjuredPersonMessage, injury);
        }
    }
}
