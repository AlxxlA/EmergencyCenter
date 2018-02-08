using System.Collections.Generic;
using EmergencyCenter.Core.Contracts;
using EmergencyCenter.Core.Contracts.Commands;
using EmergencyCenter.Core.Contracts.Factories;

namespace EmergencyCenter.Core.Commands.CreationalCommands
{
    public class AddCitizenCommand : CreationalCommand, ICommand
    {
        private const string AddedSuccessfullyMessage = "Citizen {0} added successfully.";

        public AddCitizenCommand(ICommandCenter commandCenter, ICharacterFactory characterFactory)
            : base(commandCenter, characterFactory)
        {
        }

        public override string Execute(IList<string> parameters)
        {
            base.ValidateParameters(parameters);

            //args: name health strength x y
            base.ParseParameters(parameters);

            var citizen = this.CharacterFactory.CreateCitizen(this.Name, this.Health, this.Strength,
                this.PositionX, this.PositionY, this.CommandCenter.Map);

            this.CommandCenter.AddCharacter(citizen);

            return string.Format(AddedSuccessfullyMessage, this.Name);
        }
    }
}
