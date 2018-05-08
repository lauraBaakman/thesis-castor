using System.Collections.Generic;
using NUnit.Framework;
using Registration;
using UnityEngine;
using Utils;
using Registration.Error;

namespace Tests.Registration.TransformFinders
{

	[TestFixture]
	public class IGDTransformFinderTests
	{
		float precision = 0.001f;

		private static readonly List<Point> modelPoints = new List<Point> {
			new Point(new Vector3(-1, 2, +3)),
			new Point(new Vector3(+5, 3, +4)),
			new Point(new Vector3(-2, 8, +2)),
			new Point(new Vector3(+7, 9, -1)),
		};

		IGDTransformFinder transformFinder;

		[SetUp]
		public void SetUp()
		{
			IGDTransformFinder.Configuration config = new IGDTransformFinder.Configuration(
				learningRate: 0.001f,
				convergenceError: 0.00001f,
				maxNumIterations: 50,
				errorMetric: new WheelerIterativeError());

			transformFinder = new IGDTransformFinder(config);
		}

		[Test, TestCaseSource("FindTransformCases")]
		public void FindTransformTest(CorrespondenceCollection correspondences, Matrix4x4 expected)
		{
			Matrix4x4 actual = transformFinder.FindTransform(correspondences);

			for (int i = 0; i < 4; i++)
				for (int j = 0; j < 4; j++)
					Assert.That(actual[i, j], Is.EqualTo(expected[i, j]).Within(precision));
		}

		private static object[] FindTransformCases =
		{
            // static points are not transform w.r.t. model points, only noise added
            new object[] {
				new CorrespondenceCollection(
					modelPoints,
					new List<Point>{
						new Point(new Vector3(-1.00025091976231e+000f, +2.00090142861282e+000f, +3.00046398788362e+000f)),
						new Point(new Vector3(+5.00019731696839e+000f, +2.99931203728088e+000f, +3.99931198904067e+000f)),
						new Point(new Vector3(-2.00088383277566e+000f, +8.00073235229155e+000f, +2.00020223002349e+000f)),
						new Point(new Vector3(+7.00041614515559e+000f, +8.99904116898859e+000f, -999.060180295676e-003f)),
					}
				),
				Matrix4x4.identity
			},
            // static points are translated w.r.t. to model points.
            new object[] {
				new CorrespondenceCollection(
					modelPoints,
					new List<Point>{
						new Point(new Vector3(1.99974908023769e+000f, 6.00090142861282e+000f, 10.0004639878836e+000f)),
						new Point(new Vector3(8.00019731696839e+000f, 6.99931203728088e+000f, 10.9993119890407e+000f)),
						new Point(new Vector3(999.116167224336e-003f, 12.0007323522915e+000f, 9.00020223002349e+000f)),
						new Point(new Vector3(10.0004161451556e+000f, 12.9990411689886e+000f, 6.00093981970432e+000f)),
					}
				),
				new Matrix4x4(
					column0: new Vector4(+999.999815484386e-003f, +369.930439839439e-006f, -481.853362910449e-006f, 0),
					column1: new Vector4(-370.655256081534e-006f, +999.998798785045e-003f, -1.50500602923799e-003f, 0),
					column2: new Vector4(+481.296036558626e-006f, +1.50518435302250e-003f, +999.998751386315e-003f, 0),
					column3: new Vector4(+16.6265046195479e-003f, +22.1409173161353e-003f, +38.8354427880211e-003f, 1)
				)
			},
            // static points are rotated w.r.t. to model points.
            new object[] {
				new CorrespondenceCollection(
					modelPoints,
					new List<Point>{
						new Point(new Vector3(-855.480506516037e-003f, +787.202430508499e-003f, +3.55719185045041e+000f)),
						new Point(new Vector3(+4.38853756969794e+000f, +4.01585369985757e+000f, +3.82159702538470e+000f)),
						new Point(new Vector3(-4.22519442915076e+000f, +5.87432378619885e+000f, +4.43372494372237e+000f)),
						new Point(new Vector3(+2.66159651035126e+000f, +11.1225838176250e+000f, +430.960758570174e-003f)),
					}
				),
				new Matrix4x4(
					column0: new Vector4(+999.999462263617e-003f, -1.03462391401174e-003f, -70.8931125552470e-006f, 0),
					column1: new Vector4(+1.03458980407743e-003f, +999.999349849185e-003f, -479.505103974150e-006f, 0),
					column2: new Vector4(+71.3891739114944e-006f, +479.431500835381e-006f, +999.999882524504e-003f, 0),
					column3: new Vector4(-9.76981518931215e-003f, -269.613025185103e-006f, +5.89291830637307e-003f, 1)
				)
			},
            // static points are translated and rotated w.r.t. to model points.
            new object[] {
				new CorrespondenceCollection(
					modelPoints,
					new List<Point>{
						new Point(new Vector3(+2.14451949348396f, +4.78720243050850f, +10.5571918504504f)),
						new Point(new Vector3(+7.38853756969794f, +8.01585369985757f, +10.8215970253847f)),
						new Point(new Vector3(-1.22519442915076f, +9.87432378619885f, +11.4337249437224f)),
						new Point(new Vector3(+5.66159651035126f, +15.1225838176250f, +7.43096075857017f)),
					}
				),
				new Matrix4x4(
					column0: new Vector4(+999.999624988402e-003f, -666.455410843483e-006f, -553.046327127907e-006f, 0),
					column1: new Vector4(+665.356379273881e-006f, +999.997808450991e-003f, -1.98504259483517e-003f, 0),
					column2: new Vector4(+554.368057477860e-006f, +1.98467387751939e-003f, +999.997876870574e-003f, 0),
					column3: new Vector4(+6.85734710804553e-003f, +21.8713420663516e-003f, +44.7270919891988e-003f, 1)
				)
			}
		};
	}
}