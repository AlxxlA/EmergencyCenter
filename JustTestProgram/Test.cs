using System;
using EmergencyCenter.Units.Characters;
using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Contracts.Navigation;
using EmergencyCenter.Units.Navigation;

namespace JustTestProgram
{
    class Test
    {
        static void Main()
        {
            IMap map = new Map(
                @"C:\Users\Alexander\source\repos\TelerikAcademy\EmergencyCenter\JustTestProgram\bin\Debug\Map.txt");

            var route = MapUtils.FindShortestRoute(map, new Position(0, 0), new Position(6, 2));

            Console.WriteLine(map);

            Console.WriteLine();
            Console.WriteLine(route);

            //var route = MapUtils.FindShortestRoute(map, new Position(0, 1), new Position(6, 5));
            //Console.WriteLine(route.positions.Count);
            //foreach (var position in route.positions)
            //{
            //    Console.WriteLine(position);
            //}

            //var paramedic = new Paramedic("Pesho", 100, 100, new Position(0, 0), map, new Position(0, 0));
            //var patient = new Citizen("Gosho tupoto", 100, 100, new Position(0, 7), map) { Injury = InjuryType.Wound };

            //var route = MapUtils.FindShortestRoute(map, paramedic.Position, patient.Position);

            //paramedic.StartMission(route, patient);
            //while (paramedic.IsOnMission)
            //{
            //    patient.Update();
            //    paramedic.Update();
            //    var report = paramedic.MakeReport();
            //    Console.WriteLine(patient.Health);
            //    if (report != null)
            //    {
            //        Console.WriteLine(report);
            //    }
            //}

        }
    }
}