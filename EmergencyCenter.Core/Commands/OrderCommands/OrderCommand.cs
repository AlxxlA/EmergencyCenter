using System;
using System.Collections.Generic;
using EmergencyCenter.Core.Contracts;
using EmergencyCenter.Validation;

namespace EmergencyCenter.Core.Commands.OrderCommands
{
    public abstract class OrderCommand : Command
    {
        private const string InvalidArgumentsMessage = "Invalid person id args.";

        protected OrderCommand(ICommandCenter commandCenter, IValidator validator)
            : base(commandCenter, validator)
        {
        }

        protected int TargetPersonId { get; private set; }

        protected void ParseParameters(IList<string> parameters)
        {
            //args: target_id
            try
            {
                this.TargetPersonId = int.Parse(parameters[0]);
            }
            catch (Exception)
            {
                throw new ArgumentException(InvalidArgumentsMessage);
            }
        }
    }
}
