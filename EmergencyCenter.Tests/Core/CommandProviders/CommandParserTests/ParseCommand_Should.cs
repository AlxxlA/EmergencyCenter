using System;
using EmergencyCenter.Core.CommandProviders;
using EmergencyCenter.Core.Contracts.Commands;
using EmergencyCenter.Core.Contracts.Factories;
using EmergencyCenter.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EmergencyCenter.Tests.Core.CommandProviders.CommandParserTests
{
    [TestClass]
    public class ParseCommand_Should
    {
        [TestMethod]
        public void Return_ICommand_When_Valid_Command_With_No_Parameters_Is_Passed()
        {
            var commandFactoyMock = new Mock<ICommandFactory>();
            var validatorMock = new Mock<IValidator>();
            var commandMock = new Mock<ICommand>();

            validatorMock.Setup(v => v.ValidateNull(null, It.IsAny<string>())).Throws<ArgumentNullException>();
            validatorMock.Setup(v => v.ValidateStringNullOrEmpty(It.Is<string>(s => string.IsNullOrEmpty(s)), It.IsAny<string>()))
                .Throws<ArgumentException>();

            commandFactoyMock.Setup(f => f.Create(It.IsAny<string>())).Returns(commandMock.Object);

            var commandParser = new CommandParser(commandFactoyMock.Object, validatorMock.Object);
            var commandName = "somecommand";

            var commandReturned = commandParser.ParseCommand(commandName);

            Assert.AreEqual(commandMock.Object, commandReturned);
        }

        [TestMethod]
        public void Return_ICommand_When_Valid_Command_With_Parameters_Is_Passed()
        {
            var commandFactoyMock = new Mock<ICommandFactory>();
            var validatorMock = new Mock<IValidator>();
            var commandMock = new Mock<ICommand>();

            validatorMock.Setup(v => v.ValidateNull(null, It.IsAny<string>())).Throws<ArgumentNullException>();
            validatorMock.Setup(v => v.ValidateStringNullOrEmpty(It.Is<string>(s => string.IsNullOrEmpty(s)), It.IsAny<string>()))
                .Throws<ArgumentException>();

            var commandFull = "somecommand param1  param2 param3";
            var commandName = "somecommand";

            commandFactoyMock.Setup(f => f.Create(commandName)).Returns(commandMock.Object);

            var commandParser = new CommandParser(commandFactoyMock.Object, validatorMock.Object);

            var commandReturned = commandParser.ParseCommand(commandFull);

            Assert.AreEqual(commandMock.Object, commandReturned);
        }

        [TestMethod]
        public void Throw_ArgumentException_When_Called_With_Null()
        {
            var commandFactoyMock = new Mock<ICommandFactory>();
            var validatorMock = new Mock<IValidator>();

            validatorMock.Setup(v => v.ValidateNull(null, It.IsAny<string>())).Throws<ArgumentNullException>();
            validatorMock.Setup(v => v.ValidateStringNullOrEmpty(It.Is<string>(s => string.IsNullOrEmpty(s)), It.IsAny<string>()))
               .Throws<ArgumentException>();

            var commandParser = new CommandParser(commandFactoyMock.Object, validatorMock.Object);

            Assert.ThrowsException<ArgumentException>(() => commandParser.ParseCommand(null));
        }

        [TestMethod]
        public void Throw_ArgumentException_When_Called_With_Empty_String()
        {
            var commandFactoyMock = new Mock<ICommandFactory>();
            var validatorMock = new Mock<IValidator>();

            validatorMock.Setup(v => v.ValidateNull(null, It.IsAny<string>())).Throws<ArgumentNullException>();
            validatorMock.Setup(v => v.ValidateStringNullOrEmpty(It.Is<string>(s => string.IsNullOrEmpty(s)), It.IsAny<string>()))
                .Throws<ArgumentException>();

            var commandParser = new CommandParser(commandFactoyMock.Object, validatorMock.Object);

            Assert.ThrowsException<ArgumentException>(() => commandParser.ParseCommand(string.Empty));
        }

        [TestMethod]
        public void Validator_ValidateStringNullOrEmpty_Is_Called_Ones_With_Passed_Parameter()
        {
            var commandFactoyMock = new Mock<ICommandFactory>();
            var validatorMock = new Mock<IValidator>();

            var commandLine = "somecommand param1";

            int calls = 0;

            validatorMock.Setup(v => v.ValidateNull(null, It.IsAny<string>())).Throws<ArgumentNullException>();
            validatorMock.Setup(v => v.ValidateStringNullOrEmpty(commandLine, It.IsAny<string>())).Callback(() => calls++);

            var commandParser = new CommandParser(commandFactoyMock.Object, validatorMock.Object);
            commandParser.ParseCommand(commandLine);

            Assert.AreEqual(1, calls);
        }

        [TestMethod]
        public void CommandFactory_Create_Is_Called_Once_With_Valid_Parameter()
        {
            var commandFactoyMock = new Mock<ICommandFactory>();
            var validatorMock = new Mock<IValidator>();

            var commandLine = "somecommand param1";
            var commandName = "somecommand";

            int calls = 0;

            validatorMock.Setup(v => v.ValidateNull(null, It.IsAny<string>())).Throws<ArgumentNullException>();
            validatorMock.Setup(v => v.ValidateStringNullOrEmpty(It.Is<string>(s => string.IsNullOrEmpty(s)), It.IsAny<string>()))
                .Throws<ArgumentException>();

            commandFactoyMock.Setup(f => f.Create(commandName)).Callback(() => calls++);

            var commandParser = new CommandParser(commandFactoyMock.Object, validatorMock.Object);
            commandParser.ParseCommand(commandLine);

            Assert.AreEqual(1, calls);
        }
    }
}
