using System;
using System.Collections.Generic;
using System.Threading;
using EmergencyCenter.Core.Commands;
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

        private readonly ICharacterFactory factory;
        //  private readonly IReader consoleReader;
        //  private readonly IWriter consoleWriter;
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly Map map;
        private readonly CommandCenter commandCenter;

        private Engine(ICharacterFactory factory)
        {
            this.factory = new CharacterFactory();
            // this.consoleReader = new ConsoleReader();
            this.consoleWriter = new ConsoleWriter();
            this.fileReader = new FileReader(InputFileName);
            this.fileWriter = new FileWriter(OutputFileName);
            this.map = new Map(MapFileName);
            this.commandCenter = new CommandCenter(this.map);
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