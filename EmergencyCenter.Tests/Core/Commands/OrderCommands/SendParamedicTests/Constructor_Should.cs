using System;
using EmergencyCenter.Core.Commands.OrderCommands;
using EmergencyCenter.Core.Contracts;
using EmergencyCenter.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EmergencyCenter.Tests.Core.Commands.OrderCommands.SendParamedicTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Return_Object_When_Called_With_Valid_Parameters()
        {
            var commnadCenterMock = new Mock<ICommandCenter>();
            var validatorMock = new Mock<IValidator>();

            var commnad = new SendParamedicCommand(commnadCenterMock.Object, validatorMock.Object);

            Assert.IsNotNull(commnad);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_First_Parameter_Is_Null()
        {
            var validatorMock = new Mock<IValidator>();

            validatorMock.Setup(v => v.ValidateNull(null, It.IsAny<string>())).Throws<ArgumentNullException>();

            Assert.ThrowsException<ArgumentNullException>(() => new SendParamedicCommand(null, validatorMock.Object));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_Second_Parameter_Is_Null()
        {
            var commnadCenterMock = new Mock<ICommandCenter>();

            Assert.ThrowsException<ArgumentNullException>(() => new SendParamedicCommand(commnadCenterMock.Object, null));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_Both_Parameters_Are_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new SendParamedicCommand(null, null));
        }
    }
}
