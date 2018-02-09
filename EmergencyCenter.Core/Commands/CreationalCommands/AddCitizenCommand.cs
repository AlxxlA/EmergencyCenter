using System.Collections.Generic;
using EmergencyCenter.Core.Contracts;
using EmergencyCenter.Core.Contracts.Factories;
using EmergencyCenter.Validation;

namespace EmergencyCenter.Core.Commands.CreationalCommands
{
    public class AddCitizenCommand : CreationalCommand
    {
        private const string AddedSuccessfullyMessage = "Citizen {0} added successfully.";

        public AddCitizenCommand(ICommandCenter commandCenter, ICharacterFactory characterFactory, IValidator validator)
            : base(commandCenter, characterFactory, validator)
        {
        }

        public override string Execute(IList<string> parameters)
        {
            this.ValidateParameters(parameters);

            //args: name health strength x y
            this.ParseParameters(parameters);

            var citizen = this.CharacterFactory.CreateCitizen(this.Name, this.Health, this.Strength,
                this.PositionX, this.PositionY, this.CommandCenter.Map);

            this.CommandCenter.AddCharacter(citizen);

            return string.Format(AddedSuccessfullyMessage, this.Name);
        }
    }
}
