using System;
using System.Collections.Generic;
using EmergencyCenter.Core.Contracts.CommandProviders;
using EmergencyCenter.Core.Contracts.Commands;
using EmergencyCenter.Validation;

namespace EmergencyCenter.Core.CommandProviders
{
    public class CommandProcessor : ICommandProcessor
    {
        private const string CommandCannotBeNullMessage = "Command cannot be null.";
        private const string ParametersCannotBeNullMessage = "Parameters cannot be null.";
        private const string ValidatorCannnotBeNullMessage = "Validator cannot be null.";

        private readonly IValidator validator;

        public CommandProcessor(IValidator validator)
        {
            this.validator = validator ?? throw new ArgumentNullException(ValidatorCannnotBeNullMessage);
        }

        public string ProcessCommand(ICommand command, IList<string> parameters)
        {
            this.validator.ValidateNull(command, CommandCannotBeNullMessage);
            this.validator.ValidateNull(parameters, ParametersCannotBeNullMessage);

            var result = command.Execute(parameters);

            return result;
        }
    }
}
