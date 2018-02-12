using System;
using System.Collections.Generic;
using EmergencyCenter.Core.Contracts;
using EmergencyCenter.Core.Contracts.Factories;
using EmergencyCenter.Units.Contracts.Random;
using EmergencyCenter.Validation;

namespace EmergencyCenter.Core.Commands.CreationalCommands
{
    public abstract class CreationalCommand : Command
    {
        private const string FactoryCannotBeNullMessage = "CharacterFactory cannot be null.";
        protected const string InvalidArgumentsMessage = "Invalid Add Person args.";

        protected CreationalCommand(ICommandCenter commandCenter, ICharacterFactory characterFactory, IValidator validator, IRandomGenerator random)
            : base(commandCenter, validator)
        {
            this.Validator.ValidateNull(characterFactory, FactoryCannotBeNullMessage);
            this.CharacterFactory = characterFactory;
            this.Random = random;
        }

        protected ICharacterFactory CharacterFactory { get; }


        protected string Name { get; private set; }

        protected int Health { get; private set; }

        protected int Strength { get; private set; }

        protected int PositionX { get; private set; }

        protected int PositionY { get; private set; }

        protected IRandomGenerator Random { get; }

        protected void ParseParameters(IList<string> parameters)
        {
            //args: name health strength x y
            try
            {
                this.Name = parameters[0];
                this.Health = int.Parse(parameters[1]);
                this.Strength = int.Parse(parameters[2]);
                this.PositionX = int.Parse(parameters[3]);
                this.PositionY = int.Parse(parameters[4]);
            }
            catch (Exception)
            {
                throw new ArgumentException(InvalidArgumentsMessage);
            }
        }
    }
}
