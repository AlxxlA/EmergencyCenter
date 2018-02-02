using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmergencyCenter.Core.Commands;
using EmergencyCenter.Core.Contracts.Commands;

namespace EmergencyCenter.Core.CommandProviders
{
    public class CommandParser : ICommandParser
    {
        public ICommand ParseCommand(string line)
        {
            var commandArgs = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            string commandName = commandArgs[0];
            commandArgs.RemoveAt(0);

            return new Command(commandName, commandArgs);
        }
    }
}
