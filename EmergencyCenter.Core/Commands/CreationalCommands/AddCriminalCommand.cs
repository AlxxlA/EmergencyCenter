using System.Collections.Generic;
using EmergencyCenter.Core.Contracts;
using EmergencyCenter.Core.Contracts.Commands;
using EmergencyCenter.Core.Factories;

namespace EmergencyCenter.Core.Commands.CreationalCommands
{
    public class AddCriminalCommand : CreationalCommand, ICommand
    {
        private const string AddedSuccessfullyMessage = "Criminal {0} added successfully.";

        public AddCriminalCommand(ICommandCenter commandCenter, ICharacterFactory characterFactory)
            : base(commandCenter, characterFactory)
        {
        }

        public override string Execute(IList<string> parameters)
        {
            base.ValidateParameters(parameters);

            //args: name health strength x y
            base.ParseParameters(parameters);

            var criminal = this.CharacterFactory.CreateCriminal(this.Name, this.Health, this.Strength,
                this.PositionX, this.PositionY, this.CommandCenter.Map);

            this.CommandCenter.AddCharacter(criminal);

            return string.Format(AddedSuccessfullyMessage, this.Name);
        }
    }
}
