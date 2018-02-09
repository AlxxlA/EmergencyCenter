using System;
using Autofac;
using EmergencyCenter.Core.Contracts.Commands;
using EmergencyCenter.Core.Contracts.Factories;

namespace EmergencyCenter.Core.Factories
{
    public class CommandFactory : ICommandFactory
    {
        private const string ContainerCannotBeNullMessage = "Container cannot be null.";

        private readonly IComponentContext container;

        public CommandFactory(IComponentContext container)
        {
            this.container = container ?? throw new ArgumentNullException(ContainerCannotBeNullMessage);
        }

        public ICommand Create(string commandName)
        {
            return this.container.ResolveNamed<ICommand>(commandName);
        }
    }
}
