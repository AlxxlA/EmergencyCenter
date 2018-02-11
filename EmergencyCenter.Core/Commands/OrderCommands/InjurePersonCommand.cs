using System;
using System.Collections.Generic;
using EmergencyCenter.Core.Contracts;
using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Validation;

namespace EmergencyCenter.Core.Commands.OrderCommands
{
    public class InjurePersonCommand : OrderCommand
    {
        private const string InjuredPersonMessage = "Person was injured with {0}";
        private const string InvalidCommandArgs = "Invalid injure person command args.";

        public InjurePersonCommand(ICommandCenter commandCenter, IValidator validator)
            : base(commandCenter, validator)
        {
        }

        public override string Execute(IList<string> parameters)
        {
            this.ParseParameters(parameters);

            InjuryType injury;

            try
            {
                string injuryStr = parameters[1];

                injury = (InjuryType)Enum.Parse(typeof(InjuryType), injuryStr);
            }
            catch (Exception)
            {
                return InvalidCommandArgs;
            }

            var person = this.CommandCenter.Persons.ReturnByCriteria(p => p.Id == this.TargetPersonId);

            this.CommandCenter.InjurePerson(person, injury);

            return string.Format(InjuredPersonMessage, injury);
        }
    }
}
