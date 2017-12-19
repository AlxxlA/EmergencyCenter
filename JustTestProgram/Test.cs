using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmergencyCenter.InputOutput;
using EmergencyCenter.Units.Characters;
using EmergencyCenter.Units.Map;

namespace JustTestProgram
{
    class Test
    {
        static void Main()
        {
            Map map = new Map(
                @"C:\Users\Alexander\source\repos\TelerikAcademy\EmergencyCenter\JustTestProgram\bin\Debug\Map.txt");

            Console.WriteLine(map);

             var route  = MapUtils.FindShortestRoute(map, new Position(0, 0), new Position(8, 0));
            Console.WriteLine(route.Positions.Count);
            foreach (var position in route.Positions)
            {
                Console.WriteLine(position);
            }

        }
        
    }
}
