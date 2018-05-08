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
			StabilizationTermiationCondition condition = new StabilizationTermiationCondition(5, 0.00005);

			condition.ErrorHasStabilized(0.0424007f);
			condition.ErrorHasStabilized(0.0424006f);
			condition.ErrorHasStabilized(0.0424005f);
			condition.ErrorHasStabilized(0.0424008f);
			Assert.IsTrue(condition.ErrorHasStabilized(0.0424012f));
		}
	}
}