using System.Collections.Generic;
using EmergencyCenter.Core.Contracts;
using EmergencyCenter.Core.Contracts.Factories;
using EmergencyCenter.Units.Contracts.Characters;
using EmergencyCenter.Validation;

namespace EmergencyCenter.Core.Commands.CreationalCommands
{
    public class AddCriminalCommand : CreationalCommand
    {
        private const string AddedSuccessfullyMessage = "Criminal {0} added successfully.";

        public AddCriminalCommand(ICommandCenter commandCenter, ICharacterFactory characterFactory, IValidator validator)
            : base(commandCenter, characterFactory, validator)
        {
        }

        public override string Execute(IList<string> parameters)
        {
            this.ValidateParameters(parameters);

            //args: name health strength x y
            this.ParseParameters(parameters);

            var criminal = this.CharacterFactory.CreateCriminal(this.Name, this.Health, this.Strength,
                this.PositionX, this.PositionY, this.CommandCenter.Map);

            this.CommandCenter.Criminals.Add(criminal as ICriminal);
            this.CommandCenter.Persons.Add(criminal);

            return string.Format(AddedSuccessfullyMessage, this.Name);
        }
    }
}
