using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmergencyCenter.InputOutput;
using EmergencyCenter.Units.Characters;
using EmergencyCenter.Units.Maps;

namespace JustTestProgram
{
    class Test
    {
        static void Main()
        {
            Map map = new Map(
                @"C:\Users\Alexander\source\repos\TelerikAcademy\EmergencyCenter\JustTestProgram\bin\Debug\Map.txt");

            //Console.WriteLine(map);

            //var route = MapUtils.FindShortestRoute(map, new Position(0, 1), new Position(6, 5));
            //Console.WriteLine(route.Positions.Count);
            //foreach (var position in route.Positions)
            //{
            //    Console.WriteLine(position);
            //}

            //var policeman = new Policeman("Pesho", 100, 100, new Position(0, 0), map, new Position(0, 0));
            //var criminal = new Criminal("Gosho tupoto", 100, 100, new Position(6, 12), map);

            //var route = MapUtils.FindShortestRoute(map, policeman.Position, criminal.Position);

            //policeman.StartMission(route, criminal);
            //while (policeman.IsOnMission)
            //{
            //    policeman.Update();
            //    var report = policeman.MakeReport();
            //    Console.Write(report);
            //}

            //for (int i = 0; i < 20; i++)
            //{
            //    policeman.Update();
            //    Console.WriteLine(policeman.MakeReport());
            //}

        }

    }
}
