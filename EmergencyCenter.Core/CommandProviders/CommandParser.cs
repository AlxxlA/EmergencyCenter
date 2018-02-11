using System;
using System.Linq;
using EmergencyCenter.Core.Contracts.CommandProviders;
using EmergencyCenter.Core.Contracts.Commands;
using EmergencyCenter.Core.Contracts.Factories;
using EmergencyCenter.Validation;

namespace EmergencyCenter.Core.CommandProviders
{
    public class CommandParser : ICommandParser
    {
        private const string CommandFactoryCannotBeNullMessage = "Command factory cannot be null.";
        private const string LineCannotBeNullOrEmptyMessage = "Command line cannot be null or empty.";
        private const string ValidatorCannnotBeNullMessage = "Validator cannot be null.";

        private readonly ICommandFactory commandFactory;
        private readonly IValidator validator;

        public CommandParser(ICommandFactory commandFactory, IValidator validator)
        {
            this.validator = validator ?? throw new ArgumentNullException(ValidatorCannnotBeNullMessage);

            this.validator.ValidateNull(commandFactory, CommandFactoryCannotBeNullMessage);
            this.commandFactory = commandFactory;
            this.validator = validator;
        }

        public ICommand ParseCommand(string line)
        {
            this.validator.ValidateStringNullOrEmpty(line, LineCannotBeNullOrEmptyMessage);

            var commandArgs = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            string commandName = commandArgs[0];

            var command = this.commandFactory.Create(commandName);

            return command;
        }
    }
}
