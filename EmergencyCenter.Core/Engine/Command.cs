using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyCenter.Core.Engine
{
    public class Command
    {
        public const string EndCommandName = "End";
        public const string TerminateCommandName = "Terminate";

        public string Name { get; set; }

        public static Command Parse(string line)
        {
            return new Command();
        }
    }
}
