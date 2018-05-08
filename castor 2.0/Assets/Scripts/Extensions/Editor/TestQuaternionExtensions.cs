using UnityEngine;
using NUnit.Framework;

namespace Tests.Extensions
{
	[TestFixture]
	public class QuaternionExtensionTests
	{
		float precision = 0.0001f;

		[Test]
		public void TestExtractEulerXAngle()
		{
			float expected = Random.value;

			Quaternion rotation = Quaternion.identity;
			rotation.eulerAngles = new Vector3(expected, Random.value, Random.value);

			float actual = rotation.ExtractEulerXAngle();

			Assert.That(actual, Is.EqualTo(expected).Within(precision));
		}

		[Test]
		public void TestExtractEulerYAngle()
		{
			float expected = Random.value;

			Quaternion rotation = Quaternion.identity;
			rotation.eulerAngles = new Vector3(Random.value, expected, Random.value);

			float actual = rotation.ExtractEulerYAngle();

			Assert.That(actual, Is.EqualTo(expected).Within(precision));
		}

		[Test]
		public void TestExtractEulerZAngle()
		{
			float expected = Random.value;

			Quaternion rotation = Quaternion.identity;
			rotation.eulerAngles = new Vector3(Random.value, Random.value, expected);

			float actual = rotation.ExtractEulerZAngle();

			Assert.That(actual, Is.EqualTo(expected).Within(precision));
		}

		[Test, TestCaseSource("DotCases")]
		public void TestDot(Quaternion lhs, Quaternion rhs, float expected)
		{
			float actual = lhs.Dot(rhs);

			Assert.That(actual, Is.EqualTo(expected).Within(precision));
		}

		static object[] DotCases = {
			new object[]{
				new Quaternion(+276.922984960890e-003f, +46.1713906311539e-003f, +97.1317812358475e-003f, +823.457828327293e-003f),
				new Quaternion(+694.828622975817e-003f, +317.099480060861e-003f, +950.222048838355e-003f, +34.4460805029088e-003f),
				327.716595092112e-003f
			},
			new object[]{
				new Quaternion(+43.8744359656398e+000f, +38.1558457093008e+000f, +76.5516788149002e+000f, +79.5199901137063e+000f),
				new Quaternion(+18.6872604554379e+000f, +48.9764395788231e+000f, +44.5586200710899e+000f, +64.6313010111265e+000f),
				11.2391480737438e+003f
			},
			new object[]{
				new Quaternion(+43.8744359656398e+000f, +38.1558457093008e+000f, +76.5516788149002e+000f, +79.5199901137063e+000f),
				new Quaternion(+0.00000000000000e+000f, +0.00000000000000e+000f, +0.00000000000000e+000f, +0.00000000000000e+000f),
				0.00000000000000e+000f
			},
			new object[]{
				new Quaternion(+15.5098003973841e+000f, -33.7388264805369e+000f, -38.1002318441623e+000f, -163.594801785705e-003f),
				new Quaternion(+45.9743958516081e+000f, -15.9614273333867e+000f, +8.52677509797774e+000f, -27.6188060508863e+000f),
				931.219715217067e+000f
			},
		};
	}
}