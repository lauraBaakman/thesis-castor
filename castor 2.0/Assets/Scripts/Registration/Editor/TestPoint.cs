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
        Assert.Fail("Not Implemented");
    }

    [Test]
    public void TestCompareTo_ThisSmaller()
    {
        Assert.Fail("Not Implemented");
    }

    [Test]
    public void TestCompareTo_ThisGreater()
    {
        Assert.Fail("Not Implemented");
    }
}