using System;
using System.Linq;
using EmergencyCenter.Core.Data;
using EmergencyCenter.Units.Contracts.Characters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EmergencyCenter.Tests.Core.Data.PersonDataTests
{
    [TestClass]
    public class Add_Should
    {
        [TestMethod]
        public void Add_Given_Element()
        {
            var data = new PersonDatabase<IPerson>();

            var personMock = new Mock<IPerson>();

            data.Add(personMock.Object);

            Assert.AreEqual(personMock.Object, data.ToList()[0]);
        }
    }
}
