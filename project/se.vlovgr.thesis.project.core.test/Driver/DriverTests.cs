using System.Runtime.InteropServices;
using NUnit.Framework;

namespace se.vlovgr.thesis.project.core.test.Driver
{
    [TestFixture]
    public class DriverTests
    {
        private core.Driver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new core.Driver("Name");
        }

        [Test]
        public void TestGetName()
        {
            Assert.That(driver.GetName(), Is.EqualTo("Name"));
        }

        [Test]
        public void TestDrive()
        {
            var car = new core.Car(0, 0, "car");
            Assert.That(driver.Drive(car), Is.EqualTo("Name drives car"));
        }
    }
}