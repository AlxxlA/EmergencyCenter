using System;
using EmergencyCenter.Core.CommandProviders;
using EmergencyCenter.Core.Contracts.Factories;
using EmergencyCenter.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EmergencyCenter.Tests.Core.CommandProviders.CommandParserTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Create_An_Object_When_Valid_Parameters_Passed()
        {
            var commandFactoyMock = new Mock<ICommandFactory>();
            var validatorMock = new Mock<IValidator>();

            validatorMock.Setup(v => v.ValidateNull(null, It.IsAny<string>())).Throws<ArgumentNullException>();

            var commandParser = new CommandParser(commandFactoyMock.Object, validatorMock.Object);

            Assert.IsNotNull(commandParser);
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_First_Parameter_Is_Null()
        {
            var validatorMock = new Mock<IValidator>();

            validatorMock.Setup(v => v.ValidateNull(null, It.IsAny<string>())).Throws<ArgumentNullException>();

            Assert.ThrowsException<ArgumentNullException>(() => new CommandParser(null, validatorMock.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Second_Parameter_Is_Null()
        {
            var commandFactoyMock = new Mock<ICommandFactory>();

            Assert.ThrowsException<ArgumentNullException>(() => new CommandParser(commandFactoyMock.Object, null));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Both_Parameters_Are_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new CommandParser(null, null));
        }
    }
}
