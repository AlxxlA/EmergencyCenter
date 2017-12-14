using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EmergencyCenter.InputOutput;
using EmergencyCenter.Units.Map;

namespace JustTestProgram
{
    class Test
    {
        public static event EventHandler CountdownCompleted;
        static void Main()
        {
            string path = "text.txt";

            //Map map = new Map("map.txt");

            //var consoleWriter = new ConsoleWriter();

            //consoleWriter.WriteLine(map);

            int counter = 1;

            var fileReader = new ConsoleReader();

            while (true)
            {

                if (Console.KeyAvailable)
                {

                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    while (Console.KeyAvailable)
                    {
                        Console.ReadKey(true);
                    }

                    if (keyInfo.Modifiers == ConsoleModifiers.Control && keyInfo.Key == ConsoleKey.F1)
                    {
                        foreach (var command in fileReader.ReadLine())
                        {
                            if (command == "end")
                            {
                                return;
                            }
                            Console.WriteLine(command);
                            break;
                        }
                    }
                }

                Console.WriteLine("Patrul: " + counter);

                Thread.Sleep(1500);

                counter++;

            }

        }

        public static string Method(string str)
        {
            return "commmand: " + str;
        }
    }
}
