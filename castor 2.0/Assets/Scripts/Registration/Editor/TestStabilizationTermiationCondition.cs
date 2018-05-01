using NUnit.Framework;
using Registration;

namespace Tests.Registration
{
    [TestFixture]
    public class StabilizationTerminationConditionTests
    {

        [Test]
        public void TestInsufficientData()
        {
            StabilizationTermiationCondition condition = new StabilizationTermiationCondition(5, 0.5);

            Assert.IsFalse(condition.ErrorHasStabilized(0.4f));
            Assert.IsFalse(condition.ErrorHasStabilized(0.3f));
            Assert.IsFalse(condition.ErrorHasStabilized(0.2f));
            Assert.IsFalse(condition.ErrorHasStabilized(0.1f));
        }

        [Test]
        public void TestSDTooHighData()
        {
            StabilizationTermiationCondition condition = new StabilizationTermiationCondition(5, 0.05);

            Assert.IsFalse(condition.ErrorHasStabilized(0.4f));
            Assert.IsFalse(condition.ErrorHasStabilized(0.3f));
            Assert.IsFalse(condition.ErrorHasStabilized(0.2f));
            Assert.IsFalse(condition.ErrorHasStabilized(0.1f));
            Assert.IsFalse(condition.ErrorHasStabilized(0.2f));
            Assert.IsFalse(condition.ErrorHasStabilized(0.3f));
        }


        [Test]
        public void TestSDTooLowData()
        {
            StabilizationTermiationCondition condition = new StabilizationTermiationCondition(5, 0.05);

            condition.ErrorHasStabilized(0.0010f);
            condition.ErrorHasStabilized(0.0011f);
            condition.ErrorHasStabilized(0.0009f);
            condition.ErrorHasStabilized(0.0010f);
            Assert.IsTrue(condition.ErrorHasStabilized(0.0012f));
        }
    }
}