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
        Assert.AreEqual(thisPoint.GetHashCode(), otherPoint.GetHashCode());
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
    public void TestCompareTo_ThisGreater()
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
}