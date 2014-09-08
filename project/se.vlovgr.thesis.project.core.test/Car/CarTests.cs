using NUnit.Framework;

namespace se.vlovgr.thesis.project.core.test.Car
{
    [TestFixture]
    public class CarTests
    {
        [Test]
        public void TestGetId()
        {
            var vehicle = new core.Car(1, 50, "Description");
            Assert.That(vehicle.GetId(), Is.EqualTo(1));
        }

        [Test]
        public void TestGetFuelCapacity()
        {
            var vehicle = new core.Car(1, 50, "Description");
            Assert.That(vehicle.GetFuelCapacity(), Is.EqualTo(50));
        }

        [Test]
        public void TestGetDescription()
        {
            var vehicle = new core.Car(1, 50, "Description");
            Assert.That(vehicle.GetDescription(), Is.EqualTo("Description"));
        }
    }
}