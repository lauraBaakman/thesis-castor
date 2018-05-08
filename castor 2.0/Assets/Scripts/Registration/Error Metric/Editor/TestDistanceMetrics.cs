using UnityEngine;
using NUnit.Framework;

using Registration.Error;
using Registration;

namespace Tests
{
	namespace Registration
	{
		namespace Error
		{
			[TestFixture]
			public class SquaredEuclideanDistanceMetricTests
			{

				private static double tolerance = 0.01;

				[Test]
				public void TestSquardEuclideanDistance_One()
				{
					Point a = new Point(new Vector3(1.0f, 2.0f, 3.0f));
					Point b = new Point(new Vector3(2.0f, 3.0f, 4.0f));

					float expected = 3.000000000000000f;
					float actual = DistanceMetrics.SquaredEuclidean(a, b);

					Assert.AreEqual(expected, actual, tolerance);
				}

				[Test]
				public void TestSquardEuclideanDistance_Two()
				{
					Point a = new Point(new Vector3(3.4f, 4.5f, 5.6f));
					Point b = new Point(new Vector3(6.7f, 7.8f, 8.9f));

					float expected = 32.670000000000002f;
					float actual = DistanceMetrics.SquaredEuclidean(a, b);

					Assert.AreEqual(expected, actual, tolerance);
				}

				[Test]
				public void TestSquardEuclideanDistance_Three()
				{
					Point a = new Point(new Vector3(9.1f, 2.3f, 3.4f));
					Point b = new Point(new Vector3(4.5f, 5.6f, 6.7f));

					float expected = 42.939999999999998f;
					float actual = DistanceMetrics.SquaredEuclidean(a, b);

					Assert.AreEqual(expected, actual, tolerance);
				}


				[Test]
				public void TestSquardEuclideanDistance_NegativeElements()
				{
					Point a = new Point(new Vector3(+9.1f, -2.3f, +3.4f));
					Point b = new Point(new Vector3(-4.5f, +5.6f, -6.7f));

					float expected = 349.3800000000000f;
					float actual = DistanceMetrics.SquaredEuclidean(a, b);

					Assert.AreEqual(expected, actual, tolerance);
				}
			}

			[TestFixture]
			public class PointToPlaneDistanceMetricTests
			{

				private static double tolerance = 0.01;
				private Point modelPoint;

				[SetUp]
				public void Init()
				{
					modelPoint = new Point(new Vector3(1, 2, 3), new Vector3(0, 1, 0));
				}

				[Test]
				public void TestPointToPlane_PointInFrontOfPlane()
				{
					Point staticPoint = new Point(new Vector3(3, 5, 7));

					float expected = 3;
					float actual = DistanceMetrics.PointToPlane(staticPoint, modelPoint);

					Assert.AreEqual(expected, actual, tolerance);
				}

				[Test]
				public void TestPointToPlane_PointToBackOfPlane()
				{
					Point staticPoint = new Point(new Vector3(-5, -3, 6));

					float expected = -5;
					float actual = DistanceMetrics.PointToPlane(staticPoint, modelPoint);

					Assert.AreEqual(expected, actual, tolerance);
				}

				[Test]
				public void TestPointToPlane_PointInPlane()
				{
					Point staticPoint = new Point(new Vector3(7, 2, 5));

					float expected = +0;
					float actual = DistanceMetrics.PointToPlane(staticPoint, modelPoint);

					Assert.AreEqual(expected, actual, tolerance);
				}
			}
		}

	}
}