using System;
using NUnit.Framework;
using UnityEngine;
namespace Tests.Experiment
{
	[TestFixture]
	public class Test_RunResult
	{
		private float precision = 0.000001f;

		StatisticsComputer.RunResult result;

		[SetUp]
		public void SetUp()
		{
			result = new StatisticsComputer.RunResult(
				objPath: "",
				expectedRotation: Quaternion.Euler(new Vector3(15, 30, -45)),
				expectedTranslation: new Vector3(+1.0f, +1.2f, -2.0f)
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
	}
}