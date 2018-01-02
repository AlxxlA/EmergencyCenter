using System;
using System.Collections.Generic;
using System.Threading;
using EmergencyCenter.Core.Factories;
using EmergencyCenter.InputOutput;
using EmergencyCenter.InputOutput.Contracts;
using EmergencyCenter.Units;
using EmergencyCenter.Units.Characters.Enums;
using EmergencyCenter.Units.Maps;

namespace EmergencyCenter.Core.Engine
{
    public class Engine
    {
        private const string InputFileName = @"...\...\...\input.txt";
        private const string OutputFileName = @"...\...\...\output.txt";
        private const string MapFileName = @"...\...\...\Map.txt";
        private static Engine instance;
        private readonly ICharacterFactory factory;
        //  private readonly IReader consoleReader;
        private readonly IWriter consoleWriter;
        private readonly IReader fileReader;
        private readonly IWriter fileWriter;

        private readonly Map map;
        private readonly CommandCenter commandCenter;

        private Engine()
        {
            this.factory = new CharacterFactory();
            // this.consoleReader = new ConsoleReader();
            this.consoleWriter = new ConsoleWriter();
            this.fileReader = new FileReader(InputFileName);
            this.fileWriter = new FileWriter(OutputFileName);
            this.map = new Map(MapFileName);
            this.commandCenter = new CommandCenter(this.map);
        }

        public static Engine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Engine();
                }

                return instance;
            }
        }

        public void Run()
        {
            // execute commandExecutor.Update(), when key combination is pressed (Ctrl + F1) - read command and execute it
            while (true)
            {
                var reports = new List<string>();

                // key is pressed
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    while (Console.KeyAvailable)
                    {
                        Console.ReadKey(true);
                    }

                    // check is there key combination is Ctrl + F1
                    if (keyInfo.Modifiers == ConsoleModifiers.Control && keyInfo.Key == ConsoleKey.F1)
                    {
                        // read next command
                        foreach (var command in this.ReadCommand())
                        {
                            // stop reading command
                            if (command.Name == Command.EndCommandName)
                            {
                                break;
                            }
                            // stop program execution
                            if (command.Name == Command.TerminateCommandName)
                            {
                                return;
                            }

                            reports.Add(this.ProcessCommand(command));
                        }
                    }
                }

                this.commandCenter.UpdateUnits();
                reports.AddRange(this.commandCenter.Reports());

                this.PrintReports(reports);
                Thread.Sleep(500);
            }
        }

        private IEnumerable<Command> ReadCommand()
        {
            foreach (var line in this.fileReader.ReadLine())
            {
                yield return Command.Parse(line);
            }

            yield return null;
        }

        private string ProcessCommand(Command command)
        {
            switch (command.Name)
            {
                case Command.AddPolicemanCommand:
                    return this.AddPoliceman(command);
                case Command.AddParamedicCommand:
                    return this.AddParamedic(command);
                case Command.AddCitizenCommand:
                    return this.AddCitizen(command);
                case Command.AddCriminalCommand:
                    return this.AddCriminal(command);
                case Command.SendPolicemanCommand:
                    return this.SendPoliceman(command);
                case Command.SendParamedicCommand:
                    return this.SendParamedic(command);
                case Command.InjuredPerson:
                    return this.InjurePerson(command);
                default: return "Invalid command.";
            }
        }

        private string SendPoliceman(Command command)
        {
            //args: targetId
            int id;
            try
            {
                id = int.Parse(command.CommandArgs[0]);
            }
            catch (Exception e)
            {
                return "Invalid send  policeman command args.";
            }

            var target = this.commandCenter.ReturnCharacterById(id);

            if (target == null)
            {
                return "No person with given id found.";
            }

            try
            {
                this.commandCenter.SendPolicemanToMission(target);
                return "Police is on the way.";
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
        }

        private string SendParamedic(Command command)
        {
            //args: targetId
            int id;
            try
            {
                id = int.Parse(command.CommandArgs[0]);
            }
            catch (Exception e)
            {
                return "Invalid send  paramedic command args.";
            }

            var target = this.commandCenter.ReturnCharacterById(id);

            if (target == null)
            {
                return "No person with given id found.";
            }

            try
            {
                this.commandCenter.SendParamedicToMission(target);
                return "paramedic is on the way.";
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
        }

        private string InjurePerson(Command command)
        {
            //args: targetId injuryType
            int id;
            InjuryType injury;

            try
            {
                id = int.Parse(command.CommandArgs[0]);
                string injuryStr = command.CommandArgs[1].ToLower();
                switch (injuryStr)
                {
                    case "bruse":
                        injury = InjuryType.Bruise;
                        break;
                    case "wound":
                        injury = InjuryType.Wound;
                        break;
                    case "fracture":
                        injury = InjuryType.LargeFracture;
                        break;
                    case "none":
                        injury = InjuryType.None;
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
            catch (Exception e)
            {
                return "Invalid injure person command args.";
            }

            var target = this.commandCenter.ReturnCharacterById(id);

            if (target == null)
            {
                return "No person with given id found.";
            }
            target.Injury = injury;

            return $"Target was injured with {injury}";
        }

        private string AddPoliceman(Command command)
        {
            //args: name health strength x y station_x station_y
            try
            {
                string name = command.CommandArgs[0];
                int health = int.Parse(command.CommandArgs[1]);
                int strength = int.Parse(command.CommandArgs[2]);
                int x = int.Parse(command.CommandArgs[3]);
                int y = int.Parse(command.CommandArgs[4]);
                int stationX = int.Parse(command.CommandArgs[5]);
                int stationY = int.Parse(command.CommandArgs[6]);


                var policeman = this.factory.CreatePoliceman(name, health, strength, x, y, stationX, stationY, this.map);

                this.commandCenter.AddCharacter(policeman);

                return $"Policeman {name} added successfully.";
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
            catch (Exception e)
            {
                return "Invalid Add policeman args.";
            }
        }

        private string AddParamedic(Command command)
        {
            //args: name health strength x y station_x station_y
            try
            {
                string name = command.CommandArgs[0];
                int health = int.Parse(command.CommandArgs[1]);
                int strength = int.Parse(command.CommandArgs[2]);
                int x = int.Parse(command.CommandArgs[3]);
                int y = int.Parse(command.CommandArgs[4]);
                int stationX = int.Parse(command.CommandArgs[5]);
                int stationY = int.Parse(command.CommandArgs[6]);


                var paramedic = this.factory.CreateParamedic(name, health, strength, x, y, stationX, stationY, this.map);

                this.commandCenter.AddCharacter(paramedic);

                return $"Paramedic {name} added successfully.";
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
            catch (Exception e)
            {
                return "Invalid Add paramedic args.";
            }
        }

        private string AddCitizen(Command command)
        {
            //args: name health strength x y
            try
            {
                string name = command.CommandArgs[0];
                int health = int.Parse(command.CommandArgs[1]);
                int strength = int.Parse(command.CommandArgs[2]);
                int x = int.Parse(command.CommandArgs[3]);
                int y = int.Parse(command.CommandArgs[4]);

                var citizen = this.factory.CreateCitizen(name, health, strength, x, y, this.map);

                this.commandCenter.AddCharacter(citizen);

                return $"Citizen {name} added successfully.";
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
            catch (Exception e)
            {
                return "Invalid Add citizen args.";
            }
        }

        private string AddCriminal(Command command)
        {
            //args: name health strength x y
            try
            {
                string name = command.CommandArgs[0];
                int health = int.Parse(command.CommandArgs[1]);
                int strength = int.Parse(command.CommandArgs[2]);
                int x = int.Parse(command.CommandArgs[3]);
                int y = int.Parse(command.CommandArgs[4]);

                var criminal = this.factory.CreateCriminal(name, health, strength, x, y, this.map);

                this.commandCenter.AddCharacter(criminal);

                return $"Criminal {name} added successfully.";
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
            catch (Exception e)
            {
                return "Invalid Add criminal args.";
            }
        }

        private void PrintReports(IEnumerable<string> reports)
        {
            foreach (var report in reports)
            {
                this.consoleWriter.WriteLine(report);
                this.fileWriter.WriteLine(report);
            }
        }
    }
}