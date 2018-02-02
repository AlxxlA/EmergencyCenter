using System;
using System.Collections.Generic;
using EmergencyCenter.Core.Contracts;
using EmergencyCenter.Core.Contracts.Commands;
using EmergencyCenter.Core.Factories;

namespace EmergencyCenter.Core.Commands.CreationalCommands
{
    public class AddParamedicCommand : CreationalCommand, ICommand
    {
        private const string AddedSuccessfullyMessage = "Paramedic {0} added successfully.";
        private const string InvalidAddParamedicArgumentsMessage = "Invalid Add paramedic args.";

        public AddParamedicCommand(ICommandCenter commandCenter, ICharacterFactory characterFactory)
            : base(commandCenter, characterFactory)
        {
        }

        public override string Execute(IList<string> parameters)
        {
            base.ValidateParameters(parameters);

            //args: name health strength x y station_x station_y
            base.ParseParameters(parameters);
            int stationX;
            int stationY;

            try
            {
                stationX = int.Parse(parameters[5]);
                stationY = int.Parse(parameters[6]);
            }
            catch (Exception e)
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
