using System;
using System.Collections.Generic;
using EmergencyCenter.Core.Contracts;
using EmergencyCenter.Core.Contracts.Factories;
using EmergencyCenter.Units.Contracts.Characters;
using EmergencyCenter.Units.Contracts.Random;
using EmergencyCenter.Validation;

namespace EmergencyCenter.Core.Commands.CreationalCommands
{
    public class AddPolicemanCommand : CreationalCommand
    {
        private const string AddedSuccessfullyMessage = "Policeman {0} added successfully.";
        private const string InvalidAddPoliceArgumentsMessage = "Invalid Add policeman args.";

        public AddPolicemanCommand(ICommandCenter commandCenter, ICharacterFactory characterFactory, IValidator validator, IRandomGenerator random)
            : base(commandCenter, characterFactory, validator, random)
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
                throw new ArgumentException(InvalidAddPoliceArgumentsMessage);
            }

            var policeman = this.CharacterFactory.CreatePoliceman(this.Name, this.Health, this.Strength,
                this.PositionX, this.PositionY, stationX, stationY, this.CommandCenter.Map, this.CommandCenter.PathFinder, this.Random);

            this.CommandCenter.Policemans.Add(policeman as IPoliceman);
            this.CommandCenter.Persons.Add(policeman);

            return string.Format(AddedSuccessfullyMessage, this.Name);
        }
    }
}
