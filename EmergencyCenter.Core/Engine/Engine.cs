using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using EmergencyCenter.Core.Contracts;
using EmergencyCenter.Core.Contracts.CommandProviders;
using EmergencyCenter.Core.Contracts.Engine;
using EmergencyCenter.InputOutput.Contracts;
using EmergencyCenter.Validation;

namespace EmergencyCenter.Core.Engine
{
    public class Engine : IEngine
    {
        private const string CommandCenterCannotBeNullMessage = "Command center cannot be null.";
        private const string CommandParserCannotBeNullMessage = "Command parser cannot be null.";
        private const string CommandProcessorCannnotBeNullMessage = "Command processor cannot be null.";
        private const string ReaderCannnotBeNullMessage = "Reader cannot be null.";
        private const string WriterCannnotBeNullMessage = "Writer cannot be null.";
        private const string ValidatorCannnotBeNullMessage = "Validator cannot be null.";
        private const string StopReadCommandsMessage = "stop";
        private const string TerminateProgramMessage = "terminate";

        private readonly ICommandCenter commandCenter;
        private readonly ICommandParser commandParser;
        private readonly ICommandProcessor commandProcessor;
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IValidator validator;

        public Engine(ICommandCenter commandCenter, ICommandParser commandParser, ICommandProcessor commandProcessor,
            IReader reader, IWriter writer, IValidator validator)
        {
            this.validator = validator ?? throw new ArgumentNullException(ValidatorCannnotBeNullMessage);

            this.validator.ValidateNull(commandCenter, CommandCenterCannotBeNullMessage);
            this.validator.ValidateNull(commandParser, CommandParserCannotBeNullMessage);
            this.validator.ValidateNull(commandProcessor, CommandProcessorCannnotBeNullMessage);
            this.validator.ValidateNull(reader, ReaderCannnotBeNullMessage);
            this.validator.ValidateNull(writer, WriterCannnotBeNullMessage);

            this.commandCenter = commandCenter;
            this.commandParser = commandParser;
            this.commandProcessor = commandProcessor;
            this.reader = reader;
            this.writer = writer;
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
                        foreach (var line in this.ReadCommand())
                        {
                            // stop reading command
                            if (line == StopReadCommandsMessage)
                            {
                                break;
                            }
                            // stop program execution
                            if (line == TerminateProgramMessage || line == null)
                            {
                                return;
                            }

                            var command = this.commandParser.ParseCommand(line);

                            var parameters = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Skip(1).ToList();

                            reports.Add(this.commandProcessor.ProcessCommand(command, parameters));
                        }
                    }
                }

                this.commandCenter.UpdateUnits();
                reports.AddRange(this.commandCenter.Reports());

                this.PrintReports(reports);
                Thread.Sleep(500);
            }
        }

        private IEnumerable<string> ReadCommand()
        {
            foreach (var line in this.reader.ReadLine())
            {
                yield return line;
            }

            yield return null;
        }

        private void PrintReports(IEnumerable<string> reports)
        {
            this.validator.ValidateNull(reports, "Reports cannot be null.");

            foreach (var report in reports)
            {
                this.writer.WriteLine(report);
            }
        }
    }
}
