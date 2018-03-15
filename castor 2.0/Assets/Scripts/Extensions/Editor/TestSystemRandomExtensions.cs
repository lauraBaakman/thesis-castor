using NUnit.Framework;

namespace Tests.Extensions
{
    [TestFixture]
    public class SystemRandomExtensionTests
    {
        [Test, Repeat(100)]
        public void TestNextInRange()
        {
            System.Random random = new System.Random();
            float a = random.NextInRange(0.3f, 0.5f);
            float b = random.NextInRange(0.3f, 0.5f);

            Assert.AreNotEqual(a, b);
            Assert.That(a, Is.InRange(0.3f, 0.5f));
            Assert.That(b, Is.InRange(0.3f, 0.5f));
        }

        [Test]
        public void TestNextInRange_ExceptionIsThrown()
        {
            Assert.Throws(typeof(System.ArgumentException), new TestDelegate(TestNextInRange_ExceptionIsThrown_Helper));
        }

        public void TestNextInRange_ExceptionIsThrown_Helper()
        {
            System.Random random = new System.Random();
            random.NextInRange(0.5f, 0.3f);
        }
    }
}