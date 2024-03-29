using UnityEngine;
using NUnit.Framework;

using Utils;

namespace Tests.Utils
{
	[TestFixture]
	public class ColorGeneratorTests
	{

		[SetUp]
		public void Init()
		{
			ColorGenerator.Instance.Reset();
		}

		[Test]
		public void TestGetNextColor()
		{
			TestNextColor(new Color(0.55f, 0.00f, 0.20f));
			TestNextColor(new Color(0.29f, 0.22f, 0.57f));
			TestNextColor(new Color(0.93f, 0.33f, 0.20f));
			TestNextColor(new Color(1.00f, 1.00f, 0.70f));
			TestNextColor(new Color(0.61f, 0.85f, 0.58f));
			TestNextColor(new Color(0.16f, 0.45f, 0.69f));
			TestNextColor(new Color(0.78f, 0.16f, 0.25f));
			TestNextColor(new Color(0.98f, 0.62f, 0.31f));
			TestNextColor(new Color(0.88f, 0.96f, 0.53f));
			TestNextColor(new Color(0.99f, 0.85f, 0.47f));
			TestNextColor(new Color(0.34f, 0.72f, 0.58f));
			TestNextColor(new Color(0.55f, 0.00f, 0.20f));
		}

		private void TestNextColor(Color expected)
		{
			Color actual = ColorGenerator.Instance.GetNextColor();
			Assert.That(actual, Is.EqualTo(expected));
		}
	}

	[TestFixture]
	public class HSVColorTests
	{
		[Test]
		public void TestHClamping_validValue()
		{
			HSVColor actual = new HSVColor(0.5f, 0.5f, 0.5f);
			actual.Hue = 0.7f;

			HSVColor expected = new HSVColor(0.7f, 0.5f, 0.5f);

			Assert.That(actual.Hue, Is.EqualTo(expected.Hue));
			Assert.That(actual.Saturation, Is.EqualTo(expected.Saturation));
			Assert.That(actual.Value, Is.EqualTo(expected.Value));
		}

		[Test]
		public void TestHClamping_TooLowValue()
		{
			HSVColor actual = new HSVColor(0.5f, 0.5f, 0.5f);
			actual.Hue = -0.7f;

			HSVColor expected = new HSVColor(0.0f, 0.5f, 0.5f);

			Assert.That(actual.Hue, Is.EqualTo(expected.Hue));
			Assert.That(actual.Saturation, Is.EqualTo(expected.Saturation));
			Assert.That(actual.Value, Is.EqualTo(expected.Value));
		}

		[Test]
		public void TestHClamping_TooHighValue()
		{
			HSVColor actual = new HSVColor(0.5f, 0.5f, 0.5f);
			actual.Hue = 2.7f;

			HSVColor expected = new HSVColor(1.0f, 0.5f, 0.5f);

			Assert.That(actual.Hue, Is.EqualTo(expected.Hue));
			Assert.That(actual.Saturation, Is.EqualTo(expected.Saturation));
			Assert.That(actual.Value, Is.EqualTo(expected.Value));
		}

		[Test]
		public void TestSClamping_validValue()
		{
			HSVColor actual = new HSVColor(0.5f, 0.5f, 0.5f);
			actual.Saturation = 0.7f;

			HSVColor expected = new HSVColor(0.5f, 0.7f, 0.5f);

			Assert.That(actual.Hue, Is.EqualTo(expected.Hue));
			Assert.That(actual.Saturation, Is.EqualTo(expected.Saturation));
			Assert.That(actual.Value, Is.EqualTo(expected.Value));
		}

		[Test]
		public void TestSClamping_TooLowValue()
		{
			HSVColor actual = new HSVColor(0.5f, 0.5f, 0.5f);
			actual.Saturation = -0.7f;

			HSVColor expected = new HSVColor(0.5f, 0.0f, 0.5f);

			Assert.That(actual.Hue, Is.EqualTo(expected.Hue));
			Assert.That(actual.Saturation, Is.EqualTo(expected.Saturation));
			Assert.That(actual.Value, Is.EqualTo(expected.Value));
		}

		[Test]
		public void TestSClamping_TooHighValue()
		{
			HSVColor actual = new HSVColor(0.5f, 0.5f, 0.5f);
			actual.Saturation = 2.7f;

			HSVColor expected = new HSVColor(0.5f, 1.0f, 0.5f);

			Assert.That(actual.Hue, Is.EqualTo(expected.Hue));
			Assert.That(actual.Saturation, Is.EqualTo(expected.Saturation));
			Assert.That(actual.Value, Is.EqualTo(expected.Value));
		}

		[Test]
		public void TestVClamping_validValue()
		{
			HSVColor actual = new HSVColor(0.5f, 0.5f, 0.5f);
			actual.Value = 0.7f;

			HSVColor expected = new HSVColor(0.5f, 0.5f, 0.7f);

			Assert.That(actual.Hue, Is.EqualTo(expected.Hue));
			Assert.That(actual.Saturation, Is.EqualTo(expected.Saturation));
			Assert.That(actual.Value, Is.EqualTo(expected.Value));
		}

		[Test]
		public void TestVClamping_TooLowValue()
		{
			HSVColor actual = new HSVColor(0.5f, 0.5f, 0.5f);
			actual.Value = -0.7f;

			HSVColor expected = new HSVColor(0.5f, 0.5f, 0.0f);

			Assert.That(actual.Hue, Is.EqualTo(expected.Hue));
			Assert.That(actual.Saturation, Is.EqualTo(expected.Saturation));
			Assert.That(actual.Value, Is.EqualTo(expected.Value));
		}

		[Test]
		public void TestVClamping_TooHighValue()
		{
			HSVColor actual = new HSVColor(0.5f, 0.5f, 0.5f);
			actual.Value = 2.7f;

			HSVColor expected = new HSVColor(0.5f, 0.5f, 1.0f);

			Assert.That(actual.Hue, Is.EqualTo(expected.Hue));
			Assert.That(actual.Saturation, Is.EqualTo(expected.Saturation));
			Assert.That(actual.Value, Is.EqualTo(expected.Value));
		}
	}

}