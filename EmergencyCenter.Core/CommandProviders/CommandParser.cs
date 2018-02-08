using System;
using System.Linq;
using EmergencyCenter.Core.Contracts.Commands;
using EmergencyCenter.Core.Contracts.Factories;
using EmergencyCenter.Validation;

namespace EmergencyCenter.Core.CommandProviders
{
    public class CommandParser : ICommandParser
    {
        private const string CommandFactoryCannotBeNullMessage = "Command factory cannot be null.";
        private const string LineCannotBeNullOrEmptyMessage = "Command line cannot be null or empty.";

        private readonly ICommandFactory commandFactory;

        public CommandParser(ICommandFactory commandFactory)
        {
            Validator.ValidateNull(commandFactory, CommandFactoryCannotBeNullMessage);
            this.commandFactory = commandFactory;
        }

        public ICommand ParseCommand(string line)
        {
            Validator.ValidateStringNullOrEmpty(line, LineCannotBeNullOrEmptyMessage);

            var commandArgs = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            string commandName = commandArgs[0];

            var command = this.commandFactory.Create(commandName);

            return command;
        }
    }
}
