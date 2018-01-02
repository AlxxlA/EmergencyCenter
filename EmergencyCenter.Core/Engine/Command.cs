using System;
using System.Collections.Generic;
using System.Linq;

namespace EmergencyCenter.Core.Engine
{
    public class Command
    {
        public const string EndCommandName = "Stop";
        public const string TerminateCommandName = "Terminate";
        public const string AddPolicemanCommand = "AddPoliceman";
        public const string AddParamedicCommand = "AddParamedic";
        public const string AddCitizenCommand = "AddCitizen";
        public const string AddCriminalCommand = "AddCriminal";
        public const string SendPolicemanCommand = "SendPoliceman";
        public const string SendParamedicCommand = "SendParamedic";
        public const string InjuredPerson = "InjuredPerson";

        private Command(string name, IList<string> commandArgs)
        {
            this.Name = name;
            this.CommandArgs = commandArgs;
        }

        public string Name { get; }

        public IList<string> CommandArgs { get; }

        public static Command Parse(string line)
        {
            var commandArgs = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            string commandName = commandArgs[0];
            commandArgs.RemoveAt(0);

            return new Command(commandName, commandArgs);
        }
    }
}