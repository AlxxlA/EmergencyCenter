using System.Linq;
using EmergencyCenter.Core.Data;
using EmergencyCenter.Units.Contracts.Characters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmergencyCenter.Tests.Core.Data.PersonDataTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Return_Object_When_Called()
        {
            var data = new PersonDatabase<IPerson>();

            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void Create_Empty_Collection_When_Called()
        {
            var data = new PersonDatabase<IPerson>();

            Assert.AreEqual(0, data.Count);
        }

        [TestMethod]
        public void Create_Empty_Collection_From_Given_Type()
        {
            var data = new PersonDatabase<IPerson>();

            CollectionAssert.AllItemsAreInstancesOfType(data.ToList(), typeof(IPerson));
        }
    }
}
