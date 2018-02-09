using System;
using EmergencyCenter.InputOutput;
using EmergencyCenter.Units.Characters;
using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Contracts.Navigation;
using EmergencyCenter.Units.Navigation;
using EmergencyCenter.Validation;

namespace JustTestProgram
{
    class Test
    {
        static void Main()
        {
            var validator = new Validator();
            var fileReader = new FileReader(@"C:\Users\Alexander\source\repos\TelerikAcademy\EmergencyCenter\JustTestProgram\bin\Debug\Map.txt", validator);
            IMap map = new Map(fileReader, validator);

            var pathFinder = new PathFinder();

            var paramedic = new Paramedic("Pesho", 100, 100, new Position(0, 0), map, new Position(0, 0), pathFinder);
            var patient = new Citizen("Gosho tupoto", 100, 100, new Position(0, 7), map) { Injury = InjuryType.LargeFracture };

            var route = pathFinder.FindShortestRoute(map, paramedic.Position, patient.Position);

            paramedic.StartMission(route, patient);
            while (paramedic.IsOnMission)
            {
                patient.Update();
                paramedic.Update();
                var report = paramedic.MakeReport();
                Console.WriteLine(patient.Health);
                if (report != null)
                {
                    Console.WriteLine(report);
                }
            }

        }
    }

    class ValidationTest
    {
        public string Name { get; set; }
    }
}