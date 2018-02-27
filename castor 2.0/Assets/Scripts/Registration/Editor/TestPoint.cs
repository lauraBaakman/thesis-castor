using UnityEngine;
using NUnit.Framework;
using Registration;

[TestFixture]
public class PointTests
{

    [Test]
    public void TestEquals_FullyEqualWithoutNormal()
    {
        Vector3 position = new Vector3(Random.value, Random.value, Random.value);
        Vector3 normal = new Vector3(Random.value, Random.value, Random.value);

        Point thisPoint = new Point(position);
        Point otherPoint = new Point(position, normal);

        Assert.IsFalse(thisPoint.Equals(otherPoint));
        Assert.AreNotEqual(thisPoint.GetHashCode(), otherPoint.GetHashCode());
    }

    [Test]
    public void TestEquals_PositionEqualOneWithOneWithoutNormal()
    {
        Vector3 position = new Vector3(Random.value, Random.value, Random.value);

        Point thisPoint = new Point(position);
        Point otherPoint = new Point(position);

        Assert.IsTrue(thisPoint.Equals(otherPoint));
        Assert.AreEqual(thisPoint.GetHashCode(), otherPoint.GetHashCode());
    }

    [Test]
    public void TestEquals_PositionNotEqualWithnoutNormal()
    {
        Point thisPoint = new Point(
            new Vector3(Random.value, Random.value, Random.value)
        );
        Point otherPoint = new Point(
            new Vector3(Random.value, Random.value, Random.value)
        );

        Assert.IsFalse(thisPoint.Equals(otherPoint));
        Assert.AreNotEqual(thisPoint.GetHashCode(), otherPoint.GetHashCode());
    }

    [Test]
    public void TestCompareTo_EqualWithoutNormal()
    {
        Vector3 position = new Vector3(Random.value, Random.value, Random.value);

        Point thisPoint = new Point(position);
        Point otherPoint = new Point(position);

        int actual = thisPoint.CompareTo(otherPoint);
        int expected = 0;

        Assert.AreEqual(actual, expected);
    }

    [Test]
    public void TestCompareTo_ThisSmallerWithoutNormal()
    {
        Point thisPoint = new Point(
            new Vector3(2.0f, 3.0f, 4.0f)
        );
        Point otherPoint = new Point(
            new Vector3(4.0f, 5.0f, 6.0f)
        );

        int actual = thisPoint.CompareTo(otherPoint);
        int expected = -1;

        Assert.AreEqual(actual, expected);
    }

    [Test]
    public void TestCompareTo_ThisGreaterWithoutNormal()
    {
        Point thisPoint = new Point(
            new Vector3(4.0f, 5.0f, 6.0f)
        );
        Point otherPoint = new Point(
            new Vector3(2.0f, 3.0f, 4.0f)
        );

        int actual = thisPoint.CompareTo(otherPoint);
        int expected = +1;

        Assert.AreEqual(actual, expected);
    }

    [Test]
    public void TestEquals_FullyEqualWithNormal()
    {
        Vector3 position = new Vector3(Random.value, Random.value, Random.value);
        Vector3 normal = new Vector3(Random.value, Random.value, Random.value);

        Point thisPoint = new Point(position, normal);
        Point otherPoint = new Point(position, normal);

        Assert.IsTrue(thisPoint.Equals(otherPoint));
        Assert.AreEqual(thisPoint.GetHashCode(), otherPoint.GetHashCode());
    }

    [Test]
    public void TestEquals_PositionNotEqualWithNormal()
    {
        Vector3 position1 = new Vector3(Random.value, Random.value, Random.value);
        Vector3 position2 = new Vector3(Random.value, Random.value, Random.value);
        Vector3 normal = new Vector3(Random.value, Random.value, Random.value);

        Point thisPoint = new Point(position1, normal);
        Point otherPoint = new Point(position2, normal);

        Assert.IsFalse(thisPoint.Equals(otherPoint));
        Assert.AreNotEqual(thisPoint.GetHashCode(), otherPoint.GetHashCode());
    }

    [Test]
    public void TestEquals_NormalNotEqual()
    {
        Vector3 position = new Vector3(Random.value, Random.value, Random.value);
        Vector3 normal1 = new Vector3(Random.value, Random.value, Random.value);
        Vector3 normal2 = new Vector3(Random.value, Random.value, Random.value);

        Point thisPoint = new Point(position, normal1);
        Point otherPoint = new Point(position, normal2);

        Assert.IsFalse(thisPoint.Equals(otherPoint));
        Assert.AreNotEqual(thisPoint.GetHashCode(), otherPoint.GetHashCode());
    }

    [Test]
    public void TestCompareTo_EqualWithNormal()
    {
        Vector3 position = new Vector3(Random.value, Random.value, Random.value);
        Vector3 normal = new Vector3(Random.value, Random.value, Random.value);

        Point thisPoint = new Point(position, normal);
        Point otherPoint = new Point(position, normal);

        Assert.AreEqual(0, thisPoint.CompareTo(otherPoint));
        Assert.AreEqual(0, otherPoint.CompareTo(thisPoint));
    }

    [Test]
    public void TestCompareTo_ThisSmallerPositionWithNormal()
    {
        Vector3 position1 = new Vector3(1.0f, 2.0f, 3.0f);
        Vector3 position2 = new Vector3(5.0f, 6.0f, 7.0f);
        Vector3 normal = new Vector3(Random.value, Random.value, Random.value);

        Point thisPoint = new Point(position1, normal);
        Point otherPoint = new Point(position2, normal);

        Assert.AreEqual(-1, thisPoint.CompareTo(otherPoint));
        Assert.AreEqual(+1, otherPoint.CompareTo(thisPoint));
    }

    [Test]
    public void TestCompareTo_ThisSmallerPositionEqualWithNormal()
    {
        Vector3 position = new Vector3(Random.value, Random.value, Random.value);
        Vector3 normal1 = new Vector3(1.0f, 2.0f, 3.0f);
        Vector3 normal2 = new Vector3(5.0f, 6.0f, 7.0f);

        Point thisPoint = new Point(position, normal1);
        Point otherPoint = new Point(position, normal2);

        Assert.AreEqual(-1, thisPoint.CompareTo(otherPoint));
        Assert.AreEqual(+1, otherPoint.CompareTo(thisPoint));
    }

    [Test]
    public void TestCompareTo_ThisGreaterWithNormal()
    {
        Vector3 position1 = new Vector3(5.0f, 2.0f, 3.0f);
        Vector3 position2 = new Vector3(1.0f, 6.0f, 7.0f);
        Vector3 normal = new Vector3(Random.value, Random.value, Random.value);

        Point thisPoint = new Point(position1, normal);
        Point otherPoint = new Point(position2, normal);

        Assert.AreEqual(+1, thisPoint.CompareTo(otherPoint));
        Assert.AreEqual(-1, otherPoint.CompareTo(thisPoint));
    }

    [Test]
    public void TestCompareTo_ThisGreaterPositionEqual()
    {
        Vector3 position = new Vector3(Random.value, Random.value, Random.value);
        Vector3 normal1 = new Vector3(5.0f, 2.0f, 3.0f);
        Vector3 normal2 = new Vector3(1.0f, 6.0f, 7.0f);

        Point thisPoint = new Point(position, normal1);
        Point otherPoint = new Point(position, normal2);

        Assert.AreEqual(+1, thisPoint.CompareTo(otherPoint));
        Assert.AreEqual(-1, otherPoint.CompareTo(thisPoint));
    }

    [Test]
    public void TestHasNormal_NoNormal()
    {
        Vector3 position = new Vector3(Random.value, Random.value, Random.value);

        Point point = new Point(position);

        Assert.IsFalse(point.HasNormal);
    }

    [Test]
    public void TestHasNormal_WithNormal()
    {
        Vector3 position = new Vector3(Random.value, Random.value, Random.value);
        Vector3 normal = new Vector3(Random.value, Random.value, Random.value);

        Point point = new Point(position, normal);

        Assert.IsTrue(point.HasNormal);
    }

    [Test]
    public void TestGetNormal_NoNormal()
    {
        Assert.Throws(typeof(System.Exception), new TestDelegate(TestGetNormal_NoNormal_helper));
    }

    void TestGetNormal_NoNormal_helper()
    {
        Vector3 position = new Vector3(Random.value, Random.value, Random.value);

        Point point = new Point(position);
        Vector3 actual = point.Normal;
    }

    [Test]
    public void TestGetNormal_WithNormal()
    {
        Vector3 position = new Vector3(Random.value, Random.value, Random.value);
        Vector3 normal = new Vector3(Random.value, Random.value, Random.value);

        Point point = new Point(position, normal);

        Vector3 actual = point.Normal;

        Assert.AreEqual(normal, actual);
    }

    [Test]
    public void ToHomogeneousVector4()
    {
        Vector3 position = new Vector3(1, 2, 3);
        Point point = new Point(position);

        Vector4 expected = new Vector4(1, 2, 3, 1);
        Vector4 actual = point.ToHomogeneousVector4();

        Assert.AreEqual(expected, actual);
    }
}