using System;
using System.Collections.Generic;
using EmergencyCenter.Core.Commands.OrderCommands;
using EmergencyCenter.Core.Contracts;
using EmergencyCenter.Units.Contracts.Characters;
using EmergencyCenter.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EmergencyCenter.Tests.Core.Commands.OrderCommands.SendParamedicTests
{
    [TestClass]
    public class Execute_Should
    {
        [TestMethod]
        public void Return_Success_Execution_Message_When_Called_With_Valid_Parameter()
        {
            var commnadCenterMock = new Mock<ICommandCenter>();
            var validatorMock = new Mock<IValidator>();
            var personMock = new Mock<IPerson>();
            var parametersMock = new Mock<IList<string>>();

            validatorMock.Setup(v => v.ValidateNull(null, It.IsAny<string>())).Throws<ArgumentNullException>();

            commnadCenterMock.Setup(c => c.Persons.Find(It.IsAny<Predicate<IPerson>>())).Returns(personMock.Object);

            parametersMock.Setup(l => l[It.IsAny<int>()]).Returns("0");

            var command = new SendParamedicCommand(commnadCenterMock.Object, validatorMock.Object);
            var succsessMessge = "Paramedic is on the way.";

            var returnedMessage = command.Execute(parametersMock.Object);

            Assert.AreEqual(succsessMessge, returnedMessage);
        }

        [TestMethod]
        public void Throw_ArgumentException_When_Called_With_Null()
        {
            var commnadCenterMock = new Mock<ICommandCenter>();
            var validatorMock = new Mock<IValidator>();

            validatorMock.Setup(v => v.ValidateNull(null, It.IsAny<string>())).Throws<ArgumentNullException>();

            var command = new SendParamedicCommand(commnadCenterMock.Object, validatorMock.Object);

            Assert.ThrowsException<ArgumentException>(() => command.Execute(null));
        }

        [TestMethod]
        public void Return_Success_Execution_Message_When_Called_With_Invalid_First_Parameter()
        {
            var commnadCenterMock = new Mock<ICommandCenter>();
            var validatorMock = new Mock<IValidator>();
            var parameters = new List<string>() { "invalid" };

            validatorMock.Setup(v => v.ValidateNull(null, It.IsAny<string>())).Throws<ArgumentNullException>();

            var command = new SendParamedicCommand(commnadCenterMock.Object, validatorMock.Object);

            Assert.ThrowsException<ArgumentException>(() => command.Execute(parameters));
        }

        [TestMethod]
        public void Call_CommandCenter_Persons_Find_Once()
        {
            var commnadCenterMock = new Mock<ICommandCenter>();
            var validatorMock = new Mock<IValidator>();
            var parametersMock = new Mock<IList<string>>();

            validatorMock.Setup(v => v.ValidateNull(null, It.IsAny<string>())).Throws<ArgumentNullException>();
            parametersMock.Setup(l => l[It.IsAny<int>()]).Returns("0");

            var calls = 0;
            commnadCenterMock.Setup(c => c.Persons.Find(It.IsAny<Predicate<IPerson>>())).Callback(() => calls++);


            var command = new SendParamedicCommand(commnadCenterMock.Object, validatorMock.Object);
            command.Execute(parametersMock.Object);

            Assert.AreEqual(1, calls);
        }

        [TestMethod]
        public void Call_CommandCenter_SendParamedic_Once()
        {
            var commnadCenterMock = new Mock<ICommandCenter>();
            var validatorMock = new Mock<IValidator>();
            var personMock = new Mock<IPerson>();
            var parametersMock = new Mock<IList<string>>();

            validatorMock.Setup(v => v.ValidateNull(null, It.IsAny<string>())).Throws<ArgumentNullException>();
            parametersMock.Setup(l => l[It.IsAny<int>()]).Returns("0");
            commnadCenterMock.Setup(c => c.Persons.Find(It.IsAny<Predicate<IPerson>>())).Returns(personMock.Object);

            var calls = 0;

            commnadCenterMock.Setup(c => c.SendParamedicToMission(It.IsAny<IPerson>())).Callback(() => calls++);

            var command = new SendParamedicCommand(commnadCenterMock.Object, validatorMock.Object);
            command.Execute(parametersMock.Object);

            Assert.AreEqual(1, calls);
        }

        [TestMethod]
        public void Call_CommandCenter_SendParamedic_Once_With_Right_Parameter()
        {
            var commnadCenterMock = new Mock<ICommandCenter>();
            var validatorMock = new Mock<IValidator>();
            var personMock = new Mock<IPerson>();
            var parametersMock = new Mock<IList<string>>();

            validatorMock.Setup(v => v.ValidateNull(null, It.IsAny<string>())).Throws<ArgumentNullException>();
            parametersMock.Setup(l => l[It.IsAny<int>()]).Returns("0");
            commnadCenterMock.Setup(c => c.Persons.Find(It.IsAny<Predicate<IPerson>>())).Returns(personMock.Object);

            var calls = 0;

            commnadCenterMock.Setup(c => c.SendParamedicToMission(personMock.Object)).Callback(() => calls++);

            var command = new SendParamedicCommand(commnadCenterMock.Object, validatorMock.Object);
            command.Execute(parametersMock.Object);

            Assert.AreEqual(1, calls);
        }

    }
}
