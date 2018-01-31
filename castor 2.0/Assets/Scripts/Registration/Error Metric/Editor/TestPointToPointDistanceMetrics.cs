using UnityEngine;
using NUnit.Framework;

using Registration;

[TestFixture]
public class TestPointToPointDistanceMetrics{

    private static double tolerance = 0.01;

    [Test]
    public void TestSquardEuclideanDistance_One()
    {
        Vector3 a = new Vector3(1.0f, 2.0f, 3.0f);
        Vector3 b = new Vector3(2.0f, 3.0f, 4.0f);

        float expected = 3.000000000000000f;
        float actual = PointToPointDistanceMetrics.SquaredEuclidean(a, b);

        Assert.AreEqual(expected, actual, tolerance);
    }

    [Test]
    public void TestSquardEuclideanDistance_Two()
    {
        Vector3 a = new Vector3(3.4f, 4.5f, 5.6f);
        Vector3 b = new Vector3(6.7f, 7.8f, 8.9f);

        float expected = 32.670000000000002f;
        float actual = PointToPointDistanceMetrics.SquaredEuclidean(a, b);

        Assert.AreEqual(expected, actual, tolerance);
    }

    [Test]
    public void TestSquardEuclideanDistance_Three()
    {
        Vector3 a = new Vector3(9.1f, 2.3f, 3.4f);
        Vector3 b = new Vector3(4.5f, 5.6f, 6.7f);

        float expected = 42.939999999999998f;
        float actual = PointToPointDistanceMetrics.SquaredEuclidean(a, b);

        Assert.AreEqual(expected, actual, tolerance);
    }


    [Test]
    public void TestSquardEuclideanDistance_NegativeElements()
    {
        Vector3 a = new Vector3(+9.1f, -2.3f, +3.4f);
        Vector3 b = new Vector3(-4.5f, +5.6f, -6.7f);

        float expected = 349.3800000000000f;
        float actual = PointToPointDistanceMetrics.SquaredEuclidean(a, b);

        Assert.AreEqual(expected, actual, tolerance);
    }
}