using System;
using System.Collections.Generic;
using EmergencyCenter.Core.Contracts;
using EmergencyCenter.Core.Contracts.Factories;
using EmergencyCenter.Validation;

namespace EmergencyCenter.Core.Commands.CreationalCommands
{
    public class AddParamedicCommand : CreationalCommand
    {
        private const string AddedSuccessfullyMessage = "Paramedic {0} added successfully.";
        private const string InvalidAddParamedicArgumentsMessage = "Invalid Add paramedic args.";

        public AddParamedicCommand(ICommandCenter commandCenter, ICharacterFactory characterFactory, IValidator validator)
            : base(commandCenter, characterFactory, validator)
        {
        }

        public override string Execute(IList<string> parameters)
        {
            this.ValidateParameters(parameters);

            //args: name health strength x y station_x station_y
            this.ParseParameters(parameters);
            int stationX;
            int stationY;

            try
            {
                stationX = int.Parse(parameters[5]);
                stationY = int.Parse(parameters[6]);
            }
            catch (Exception)
            {
                throw new ArgumentException(InvalidAddParamedicArgumentsMessage);
            }

            var paramedic = this.CharacterFactory.CreateParamedic(this.Name, this.Health, this.Strength,
                this.PositionX, this.PositionY, stationX, stationY, this.CommandCenter.Map);

            this.CommandCenter.AddCharacter(paramedic);

            return string.Format(AddedSuccessfullyMessage, this.Name);
        }
    }
}
