using NUnit.Framework;

namespace se.vlovgr.thesis.project.core.test.Vehicle
{
    [TestFixture]
    public class VehicleTests
    {
        [Test]
        public void TestGetId()
        {
            var vehicle = new core.Vehicle(1);
            Assert.That(vehicle.GetId(), Is.EqualTo(1));
        }
    }
}