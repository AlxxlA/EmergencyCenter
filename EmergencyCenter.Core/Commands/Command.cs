using System;
using System.Collections.Generic;
using EmergencyCenter.Core.Contracts;
using EmergencyCenter.Core.Contracts.Commands;
using EmergencyCenter.Validation;

namespace EmergencyCenter.Core.Commands
{
    public abstract class Command : ICommand
    {
        public const string EndCommandName = "Stop";
        public const string TerminateCommandName = "Terminate";
        public const string AddPolicemanCommand = "AddPoliceman"; // ready
        public const string AddParamedicCommand = "AddParamedic"; // ready
        public const string AddCitizenCommand = "AddCitizen"; // ready
        public const string AddCriminalCommand = "AddCriminal"; // ready
        public const string SendPolicemanCommand = "SendPoliceman"; // ready
        public const string SendParamedicCommand = "SendParamedic"; // ready
        public const string InjuredPerson = "InjuredPerson"; // ready

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
