using System;
using EmergencyCenter.Core.CommandProviders;
using EmergencyCenter.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EmergencyCenter.Tests.Core.CommandProviders.CommandProcessorTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Create_An_Object_When_Valid_Parameter_Is_Passed()
        {
            var validatorMock = new Mock<IValidator>();

            var commandProcessor = new CommandProcessor(validatorMock.Object);

            Assert.IsNotNull(commandProcessor);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_Called_With_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new CommandProcessor(null));
        }
    }
}
