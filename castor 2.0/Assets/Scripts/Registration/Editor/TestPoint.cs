using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using NUnit.Framework;

using Registration;


[TestFixture]
public class PointTests
{

    [Test]
    public void TestEquals_FullyEqual()
    {
        Vector3 position = new Vector3(Random.value, Random.value, Random.value);

        Point thisPoint = new Point(position);
        Point otherPoint = new Point(position);

        Assert.IsTrue(thisPoint.Equals(otherPoint));
        Assert.Equals(thisPoint.GetHashCode(), otherPoint.GetHashCode());
    }

    [Test]
    public void TestEquals_PositionNotEqual()
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
    public void TestCompareTo_Equal()
    {
        Vector3 position = new Vector3(Random.value, Random.value, Random.value);

        Point thisPoint = new Point(position);
        Point otherPoint = new Point(position);

        int actual = thisPoint.CompareTo(otherPoint);
        int expected = 0;

        Assert.AreEqual(actual, expected);
    }

    [Test]
    public void TestCompareTo_ThisSmaller()
    {
        Point thisPoint = new Point(
            new Vector3(2.0, 3.0, 4.0)
        );
        Point otherPoint = new Point(
            new Vector3(4.0, 5.0, 6.0)
        );

        int actual = thisPoint.CompareTo(otherPoint);
        int expected = -1;

        Assert.AreEqual(actual, expected);
    }

    [Test]
    public void TestCompareTo_LengthEqualsThisOtherwiseSmaller()
    {
        Point thisPoint = new Point(
            new Vector3(Mathf.Sqrt(60), Mathf.Sqrt(15), Mathf.Sqrt(25))
        );
        Point otherPoint = new Point(
            new Vector3(Mathf.Sqrt(50), 5, 5)
        );

        int actual = thisPoint.CompareTo(otherPoint);
        int expected = -1;

        Assert.AreEqual(actual, expected);
    }

    [Test]
    public void TestCompareTo_ThisGreater()
    {
        Point thisPoint = new Point(
            new Vector3(4.0, 5.0, 6.0)
        );
        Point otherPoint = new Point(
            new Vector3(2.0, 3.0, 4.0)
        );

        int actual = thisPoint.CompareTo(otherPoint);
        int expected = +1;

        Assert.AreEqual(actual, expected);
    }

    [Test]
    public void TestCompareTo_LengthEqualsThisOtherwiseGreater()
    {
        Point thisPoint = new Point(
            new Vector3(Mathf.Sqrt(50), 5, 5)
        );
        Point otherPoint = new Point(
            new Vector3(Mathf.Sqrt(60), Mathf.Sqrt(15), Mathf.Sqrt(25))
        );

        int actual = thisPoint.CompareTo(otherPoint);
        int expected = +1;

        Assert.AreEqual(actual, expected);
    }
}