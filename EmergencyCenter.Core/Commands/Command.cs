using System;
using System.Collections.Generic;
using EmergencyCenter.Core.Contracts;
using EmergencyCenter.Core.Contracts.Commands;
using EmergencyCenter.Validation;

namespace EmergencyCenter.Core.Commands
{
    public abstract class Command : ICommand
    {
        private const string CommandCenterCannotBeNullMessage = "CommandCenter cannot be null.";
        private const string ParametersCannotBeNullMessage = "Parameters cannot be null.";
        private const string ValidatorCannnotBeNullMessage = "Validator cannot be null.";

        protected Command(ICommandCenter commandCenter, IValidator validator)
        {
            this.Validator = validator ?? throw new ArgumentNullException(ValidatorCannnotBeNullMessage);

            this.Validator.ValidateNull(commandCenter, CommandCenterCannotBeNullMessage);
            this.CommandCenter = commandCenter;
        }

        protected ICommandCenter CommandCenter { get; }

        protected IValidator Validator { get; }

        public abstract string Execute(IList<string> parameters);

        protected virtual void ValidateParameters(IList<string> parameters)
        {
            this.Validator.ValidateNull(parameters, ParametersCannotBeNullMessage);
        }
    }
}
