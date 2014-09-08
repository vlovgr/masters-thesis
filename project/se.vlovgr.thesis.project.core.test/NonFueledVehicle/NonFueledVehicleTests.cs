using NUnit.Framework;

namespace se.vlovgr.thesis.project.core.test.NonFueledVehicle
{
    [TestFixture]
    public class NonFueledVehicleTests
    {
        [Test]
        public void TestGetId()
        {
            var vehicle = new core.NonFueledVehicle(1);
            Assert.That(vehicle.GetId(), Is.EqualTo(1));
        }
    }
}