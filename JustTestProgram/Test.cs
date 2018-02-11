using System;
using System.Collections.Generic;
using EmergencyCenter.Core.Factories;
using EmergencyCenter.InputOutput;
using EmergencyCenter.Units.Characters;
using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Contracts.Characters;
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

            var factory = new CharacterFactory();
            var paramedic = factory.CreateParamedic("Pesho", 100, 100, 0, 0, 0, 0, map, pathFinder);
            var patient = new Citizen("Gosho tupoto", 100, 100, new Position(0, 7), map) { Injury = InjuryType.LargeFracture };

            //var route = pathFinder.FindShortestRoute(map, paramedic.Position, patient.Position);

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

            IDatabase<IParamedic> db = new PersonDatabase<IParamedic>();


            db.Add(paramedic as IParamedic);

            var l = new Dictionary<int,string>();

            l.Add(1, "2");
            l.Add(2, "3");

            l.Remove(1);

            Console.WriteLine(string.Join(", ",l));
        }
    }

    interface IDatabase<T>
    {
        void Add(T element);

        

        void RemoveByCriteria(Predicate<T> match);
    }

    class PersonDatabase<T> : IDatabase<T> where T : IPerson
    {
        private List<T> persons = new List<T>();

        public void Add(T person)
        {
            if (person == null)
            {
                throw new ArgumentNullException("Person cannot be null.");
            }

            this.persons.Add(person);
        }

        public T First()
        {
            if (this.persons == null || this.persons.Count == 0)
            {
                return default(T);
            }

            return this.persons[0];
        }

        public void RemoveByCriteria(Predicate<T> match)
        {
            
        }
    }
}