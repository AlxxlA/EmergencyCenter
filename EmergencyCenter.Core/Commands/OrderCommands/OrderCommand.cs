using System;
using System.Collections.Generic;
using EmergencyCenter.Core.Contracts;
using EmergencyCenter.Core.Contracts.Commands;
using EmergencyCenter.Core.Factories;

namespace EmergencyCenter.Core.Commands.OrderCommands
{
    public abstract class OrderCommand : Command, ICommand
    {
        private const string InvalidArgumentsMessage = "Invalid person id args.";

        protected OrderCommand(ICommandCenter commandCenter)
            : base(commandCenter)
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
            catch (Exception e)
            {
                throw new ArgumentException(InvalidArgumentsMessage);
            }
        }
    }
}
