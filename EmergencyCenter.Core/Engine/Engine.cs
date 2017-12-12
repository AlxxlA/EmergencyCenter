using System.Collections.Generic;
using EmergencyCenter.Core.Contracts;
using EmergencyCenter.Core.InputOutput;

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
            foreach (var command in this.ReadCommand())
            {
                if (command.Name == Command.EndCommandName)
                {
                    break;
                }

                Report report = this.ProcessCommand(command);
                this.PrintReport(report);
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
            throw new System.NotImplementedException();
        }

        private void PrintReport(Report report)
        {
            this.consoleWriter.WriteLine(report);
            this.fileWriter.WriteLine(report);
        }
    }
}