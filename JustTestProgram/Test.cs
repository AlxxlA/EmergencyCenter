using System;
using System.Collections.Generic;
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
using EmergencyCenter.Units.Characters.Enums;
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

            var paramedic = new Paramedic("Pesho", 100, 100, new Position(0, 0), map, new Position(0, 0));
            var patient = new Citizen("Gosho tupoto", 100, 100, new Position(0, 16), map) { Injury = InjuryType.Bruise };

            var route = MapUtils.FindShortestRoute(map, paramedic.Position, patient.Position);

            paramedic.StartMission(route, patient);
            while (paramedic.IsOnMission)
            {
                paramedic.Update();
                patient.Update();
                var report = paramedic.MakeReport();
                Console.WriteLine(patient.Health);
                Console.Write(report);
            }

        }
    }
}