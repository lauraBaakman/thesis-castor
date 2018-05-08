using NUnit.Framework;
using UnityEngine;
using Utils;

namespace Tests.Utils
{
	[TestFixture]
	public class VectorUtilsTests
	{
		float precision = 0.0005f;

		[Test]
		public void Test_HomogeneousCoordinate()
		{
			Vector3 input = new Vector3(1, 2, 3);

			Vector4 expected = new Vector4(1, 2, 3, 1);
			Vector4 actual = VectorUtils.HomogeneousCoordinate(input);

			for (int i = 0; i < 4; i++) Assert.That(actual[i], Is.EqualTo(expected[i]).Within(precision));
		}

		[Test, TestCaseSource("CrossCases")]
		public void Test_Cross(Vector4 lhs, Vector4 rhs, Vector4 expected)
		{
			Vector4 actual = VectorUtils.Cross(lhs, rhs);

			for (int i = 0; i < 4; i++) Assert.That(actual[i], Is.EqualTo(expected[i]).Within(precision));
		}

		static object[] CrossCases = {
			new object[] {
				new Vector4(+530.797553008973e-003f, +779.167230102011e-003f, +934.010684229183e-003f, +129.906208473730e-003f),
				new Vector4(+568.823660872193e-003f, +469.390641058206e-003f, +11.9020695012414e-003f, +337.122644398882e-003f),
				new Vector4(-429.142171299786e-003f, +524.969787329984e-003f, -194.057352579263e-003f, +0.00000000000000e+000f),
			},
			new object[] {
				new Vector4(+16.2182308193243e+000f, +79.4284540683907e+000f, +31.1215042044805e+000f, +52.8533135506213e+000f),
				new Vector4(+16.5648729499781e+000f, +60.1981941401637e+000f, +26.2971284540144e+000f, +65.4079098476782e+000f),
				new Vector4(+215.281907505019e+000f, +89.0308640068021e+000f, -339.414042784394e+000f, +0.00000000000000e+000f),
			},
			new object[] {
				new Vector4(+189.214503140008e-003f, +248.151592823709e-003f, -49.4584014975022e-003f, -416.178622003067e-003f),
				new Vector4(-271.023031283181e-003f, +413.337361501670e-003f, -347.621981030777e-003f, +325.816977489547e-003f),
				new Vector4(-65.8199431142528e-003f, +79.1794863175574e-003f, +145.464220390572e-003f, +0.00000000000000e+000f),
			},
			new object[] {
				new Vector4(+3.83424352600571e+000f, +49.6134716626885e+000f, -42.1824471246816e+000f, -5.73217302245537e+000f),
				new Vector4(-39.3347229819416e+000f, +46.1898080855054e+000f, -49.5365775865933e+000f, +27.4910464711502e+000f),
				new Vector4(-509.282451092995e+000f, +1.84917017426162e+003f, +2.12863513664356e+003f, +0.00000000000000e+000f),
			},
		};

		public void Test_ScaleWithScalar()
		{
			Vector4 vector = new Vector4(2, 3, 4, 5);
			float scale = 2.5f;

			Vector4 expected = new Vector4(5, 7.5f, 10, 12.5f);

			Vector4 actual = VectorUtils.MultiplyWithScalar(vector, scale);

			for (int i = 0; i < 4; i++) Assert.That(actual[i], Is.EqualTo(expected[i]).Within(precision));
		}
	}
}