using System;
using System.Collections.Generic;
using System.Threading;
using EmergencyCenter.InputOutput;
using EmergencyCenter.InputOutput.Contracts;

namespace EmergencyCenter.Core.Engine
{
    public class Engine
    {
        private const string InputFileName = "input.txt";
        private const string OutputFileName = "result.txt";
        private static Engine instance;
        private readonly CommandCenter commandCenter;
        private readonly IReader consoleReader;
        private readonly IWriter consoleWriter;
        private readonly IReader fileReader;
        private readonly IWriter fileWriter;

        private Engine()
        {
            this.commandCenter = new CommandCenter();
            this.consoleReader = new ConsoleReader();
            this.consoleWriter = new ConsoleWriter();
            this.fileReader = new FileReader(InputFileName);
            this.fileWriter = new FileWriter(OutputFileName);
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
            // execute commandCenter.Update(), when key combination is pressed (Ctrl + F1) - read command and execute it
            while (true)
            {
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
                            // finish program execution
                            if (command.Name == Command.EndCommandName)
                            {
                                // TODO: add end report 
                                return;
                            }

                            Report report = this.ProcessCommand(command);
                            this.PrintReport(report);
                            break;
                        }
                    }
                }

                this.commandCenter.UpdateUnits();
                Thread.Sleep(1500);
            }
        }

        private IEnumerable<Command> ReadCommand()
        {
            foreach (var line in this.consoleReader.ReadLine())
            {
                yield return Command.Parse(line);
            }

            yield return null;
        }

        private Report ProcessCommand(Command command)
        {
            var report = this.commandCenter.ExecuteCommand(command);
            return report;
        }

        private void PrintReport(Report report)
        {
            this.consoleWriter.WriteLine(report);
            this.fileWriter.WriteLine(report);
        }
    }
}