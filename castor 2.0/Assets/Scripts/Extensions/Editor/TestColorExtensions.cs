using NUnit.Framework;
using UnityEngine;

namespace Tests.Extensions
{
	[TestFixture]
	public class ColorExtensionsTests
	{
		private System.Random randomGenerator;

		[SetUp]
		public void SetUp()
		{
			randomGenerator = new System.Random();
		}

		[Test, Repeat(100)]
		public void TestRandom()
		{
			float hueMax, hueMin;
			GenerateRandomRange(out hueMin, out hueMax);

			float saturationMax, saturationMin;
			GenerateRandomRange(out saturationMin, out saturationMax);

			float valueMax, valueMin;
			GenerateRandomRange(out valueMin, out valueMax);

			Color color = new Color().Random(
				hueMin, hueMax,
				saturationMin, saturationMax,
				valueMin, valueMax
			);

			float hue, saturation, val, alpha;
			Color.RGBToHSV(color, out hue, out saturation, out val);
			alpha = color.a;

			Assert.That(hue, Is.GreaterThanOrEqualTo(hueMin));
			Assert.That(hue, Is.LessThan(hueMax));

			Assert.That(saturation, Is.GreaterThanOrEqualTo(saturationMin));
			Assert.That(saturation, Is.LessThan(saturationMax));

			Assert.That(val, Is.GreaterThanOrEqualTo(valueMin));
			Assert.That(val, Is.LessThan(valueMax));

			Assert.That(alpha, Is.EqualTo(1.0f));
		}

		private void GenerateRandomRange(out float min, out float max)
		{
			min = (float)randomGenerator.NextDouble();
			do
			{
				max = (float)randomGenerator.NextDouble();
			} while (max < min);
		}
	}
}