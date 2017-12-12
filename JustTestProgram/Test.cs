using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmergencyCenter.Units.Map;

namespace JustTestProgram
{
    class Test
    {
        static void Main()
        {
           // string path = "text.txt";

            Map map = new Map("map.txt");

            Console.WriteLine(map);

        }

        public static IEnumerable<string> ReadCommand()
        {
            foreach (var line in ReadFromFile("text.txt"))
            {
                yield return line;
            }

            yield return null;
        }


        public static IEnumerable<string> ReadFromFile(string path)
        {
            using (var reader = new StreamReader(path))
            {
                var line = reader.ReadLine();

                while (!string.IsNullOrEmpty(line))
                {
                    yield return line;
                    line = reader.ReadLine();
                }

            }
        }

        public static IEnumerable<string> ReadFromConsole()
        {
            while (true)
            {
                yield return Console.ReadLine();
            }
        }


        static void WriteOnConsole(string line)
        {
            Console.WriteLine(line);
        }

        static void WriteOnFile(string path, string line)
        {
            //if (!Directory.Exists(path))
            //{
            //    Console.WriteLine("Does not exist");
            //    return;
            //}
            using (var writer = new StreamWriter(path, append: true))
            {
                writer.WriteLine(line);
            }
        }
    }
}
