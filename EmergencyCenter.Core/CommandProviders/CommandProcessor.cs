using System.Collections.Generic;
using EmergencyCenter.Core.Contracts.Commands;
using EmergencyCenter.Validation;

namespace EmergencyCenter.Core.CommandProviders
{
    public class CommandProcessor : ICommandProcessor
    {
        private const string CommandCannotBeNullMessage = "Command cannot be null.";
        private const string ParametersCannotBeNullMessage = "Parameters cannot be null.";

        public string ProcessCommand(ICommand command, IList<string> parameters)
        {
            Validator.ValidateNull(command, CommandCannotBeNullMessage);
            Validator.ValidateNull(parameters, ParametersCannotBeNullMessage);

            var result = command.Execute(parameters);

            return result;
        }
    }
}
