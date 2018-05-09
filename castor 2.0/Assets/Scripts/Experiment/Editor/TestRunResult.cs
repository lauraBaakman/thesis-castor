using System;
using NUnit.Framework;
using UnityEngine;
namespace Tests.Experiment
{
	[TestFixture]
	public class Test_RunResult
	{
		private float precision = 0.0001f;

		StatisticsComputer.RunResult result;

		[SetUp]
		public void SetUp()
		{
			result = new StatisticsComputer.RunResult(
				objPath: "",
				expectedRotation: Quaternion.Euler(new Vector3(15, 30, -45)),
				expectedTranslation: new Vector3(+1.0f, +1.2f, -2.0f),
				runData: StatisticsComputer.RunData.RunDataForTests()
			);
		}

		[Test, TestCaseSource("TranslationCases")]
		public void TestComputeTranslationDifference(Vector3 actualTranslation, float expected)
		{
			result.ActualTranslation = actualTranslation;
			float actual = result.TranslationError;

			Assert.That(actual, Is.EqualTo(expected).Within(precision));
		}

		static object[] TranslationCases = {
			new object[] {
				new Vector3(+1.1f, +1.0f, -1.5f),
				0.273861278752583f
			},
			new object[] {
				new Vector3(+1.0f, +1.2f, -2.0f),
				0.0f
			},
			new object[] {
				new Vector3(+4.0f, +0.0f, -1.4f),
				1.643167672515498f
			}
		};

		[Test]
		public void TestTranslationDifferenceGetterNotSet()
		{
			Assert.Throws<ArgumentException>(
				delegate { float error = result.TranslationError; }
			);
		}

		[Test]
		public void TestRotationDifferenceGetterNotSet()
		{
			Assert.Throws<ArgumentException>(
				delegate { Vector3 error = result.RotationError; }
			);
		}

		[Test, TestCaseSource("RotationCases")]
		public void TestComputeRotationDifference(Quaternion actualRotation, Vector3 expected)
		{
			result.ActualRotation = actualRotation;
			Vector3 actual = result.RotationError;

			Assert.That(actual.x, Is.EqualTo(expected.x).Within(precision));
			Assert.That(actual.y, Is.EqualTo(expected.y).Within(precision));
			Assert.That(actual.z, Is.EqualTo(expected.z).Within(precision));
		}

		static object[] RotationCases = {
			new object[] {
				Quaternion.Euler(new Vector3(15, 30, -45)),
				new Vector3(0, 0, 0)
			},
			new object[] {
				Quaternion.Euler(new Vector3(20, 25, -55)),
				new Vector3(-05.0f, +05.0f, +10.0f)
			},
			new object[] {
				Quaternion.Euler(new Vector3(-345f, +765f, -235f)),
				new Vector3(000f, -015f, -170f)
			},
		};

		// Use to test a method that is private.
		//[TestCase(-360, +000)]
		//[TestCase(+360, +000)]
		//[TestCase(-720, +000)]
		//[TestCase(-735, -015)]
		//[TestCase(+735, +015)]
		//[TestCase(+190, -170)]
		//[TestCase(-190, +170)]
		//[TestCase(+200, -160)]
		//[TestCase(-200, +160)]
		//[TestCase(+180, +180)]
		//[TestCase(-180, +180)]
		//public void TestNormalizeAngle(float input, float expected)
		//{
		//	float actual = StatisticsComputer.RunResult.NormalizeAngle(input);
		//	Assert.That(actual, Is.EqualTo(expected).Within(precision));
		//}

		//[Test]
		//public void TestNormalizeAngle_Range(
		//	[Random(-1080, +1080, 50)] float angle)
		//{
		//	float actual = StatisticsComputer.RunResult.NormalizeAngle(angle);
		//	Assert.That(actual, Is.InRange(-179, 180));
		//}
	}
}