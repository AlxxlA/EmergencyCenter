using System;
using System.Collections.Generic;
using EmergencyCenter.Core.CommandProviders;
using EmergencyCenter.Core.Contracts.Commands;
using EmergencyCenter.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EmergencyCenter.Tests.Core.CommandProviders.CommandProcessorTests
{
    [TestClass]
    public class ProcessCommand_Should
    {
        [TestMethod]
        public void Return_String_When_Called_With_Valid_Parameters()
        {
            var validatorMock = new Mock<IValidator>();
            var commandMock = new Mock<ICommand>();

            validatorMock.Setup(v => v.ValidateNull(null, It.IsAny<string>())).Throws<ArgumentNullException>();

            var expectedMessage = "Success";
            commandMock.Setup(c => c.Execute(It.IsAny<IList<string>>())).Returns(expectedMessage);

            var commandProcessor = new CommandProcessor(validatorMock.Object);

            var returnedMessage = commandProcessor.ProcessCommand(commandMock.Object, new List<string>());

            Assert.AreEqual(expectedMessage, returnedMessage);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_First_Parameter_Is_Null()
        {
            var validatorMock = new Mock<IValidator>();

            validatorMock.Setup(v => v.ValidateNull(null, It.IsAny<string>())).Throws<ArgumentNullException>();

            var commandProcessor = new CommandProcessor(validatorMock.Object);

            Assert.ThrowsException<ArgumentNullException>(() => commandProcessor.ProcessCommand(null, new List<string>()));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_Second_Parameter_Is_Null()
        {
            var validatorMock = new Mock<IValidator>();
            var commandMock = new Mock<ICommand>();

            validatorMock.Setup(v => v.ValidateNull(null, It.IsAny<string>())).Throws<ArgumentNullException>();

            var commandProcessor = new CommandProcessor(validatorMock.Object);

            Assert.ThrowsException<ArgumentNullException>(() => commandProcessor.ProcessCommand(commandMock.Object, null));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_Both_Parameters_Are_Null()
        {
            var validatorMock = new Mock<IValidator>();

            validatorMock.Setup(v => v.ValidateNull(null, It.IsAny<string>())).Throws<ArgumentNullException>();

            var commandProcessor = new CommandProcessor(validatorMock.Object);

            Assert.ThrowsException<ArgumentNullException>(() => commandProcessor.ProcessCommand(null, null));
        }

        [TestMethod]
        public void Call_Command_Execute_Method_Onece_With_Valid_Parameter()
        {
            var validatorMock = new Mock<IValidator>();
            var commandMock = new Mock<ICommand>();

            validatorMock.Setup(v => v.ValidateNull(null, It.IsAny<string>())).Throws<ArgumentNullException>();

            var parameters = new Mock<IList<string>>();

            var calls = 0;

            commandMock.Setup(c => c.Execute(parameters.Object)).Callback(() => calls++);

            var commandProcessor = new CommandProcessor(validatorMock.Object);
            commandProcessor.ProcessCommand(commandMock.Object, parameters.Object);

            Assert.AreEqual(1, calls);
        }

        [TestMethod]
        public void Call_Command_Execute_Method_Only_Once()
        {
            var validatorMock = new Mock<IValidator>();
            var commandMock = new Mock<ICommand>();

            validatorMock.Setup(v => v.ValidateNull(null, It.IsAny<string>())).Throws<ArgumentNullException>();

            var parameters = new Mock<IList<string>>();

            var calls = 0;

            commandMock.Setup(c => c.Execute(It.IsAny<IList<string>>())).Callback(() => calls++);

            var commandProcessor = new CommandProcessor(validatorMock.Object);
            commandProcessor.ProcessCommand(commandMock.Object, parameters.Object);

            Assert.AreEqual(1, calls);
        }
    }
}
