using NUnit.Framework;

namespace se.vlovgr.thesis.project.core.test.Bicycle
{
    [TestFixture]
    public class BicycleTests
    {
        [Test]
        public void TestGetId()
        {
            var vehicle = new core.Bicycle(1, "Description");
            Assert.That(vehicle.GetId(), Is.EqualTo(1));
        }

        [Test]
        public void TestGetDescription()
        {
            var vehicle = new core.Bicycle(1, "Description");
            Assert.That(vehicle.GetDescription(), Is.EqualTo("Description"));
        }
    }
}