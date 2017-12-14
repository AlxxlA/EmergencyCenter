using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmergencyCenter.InputOutput;
using EmergencyCenter.Units.Map;

namespace JustTestProgram
{
    class Test
    {
        static void Main()
        {
            string path = "text.txt";

            Map map = new Map("map.txt");

            var consoleWriter = new ConsoleWriter();

            consoleWriter.WriteLine(map);

        }
    }
}
