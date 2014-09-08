using NUnit.Framework;

namespace se.vlovgr.thesis.project.core.test.FueledVehicle
{
    [TestFixture]
    public class FueledVehicleTests
    {
        [Test]
        public void TestGetId()
        {
            var vehicle = new core.FueledVehicle(1, 50);
            Assert.That(vehicle.GetId(), Is.EqualTo(1));
        }

        [Test]
        public void TestGetFuelCapacity()
        {
            var vehicle = new core.FueledVehicle(1, 50);
            Assert.That(vehicle.GetFuelCapacity(), Is.EqualTo(50));
        }
    }
}