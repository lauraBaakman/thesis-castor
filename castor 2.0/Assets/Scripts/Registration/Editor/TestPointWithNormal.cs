using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using NUnit.Framework;

using Registration;


[TestFixture]
public class PointWithNormalTests
{

    [Test]
    public void TestEquals_FullyEqual()
    {
        Vector3 position = new Vector3(Random.value, Random.value, Random.value);
        Vector3 normal = new Vector3(Random.value, Random.value, Random.value);

        PointWithNormal thisPoint = new PointWithNormal(position, normal);
        PointWithNormal otherPoint = new PointWithNormal(position, normal);

        Assert.IsTrue(thisPoint.Equals(otherPoint));
        Assert.AreEqual(thisPoint.GetHashCode(), otherPoint.GetHashCode());
    }

    [Test]
    public void TestEquals_PositionNotEqual()
    {
        Vector3 position1 = new Vector3(Random.value, Random.value, Random.value);
        Vector3 position2 = new Vector3(Random.value, Random.value, Random.value);
        Vector3 normal = new Vector3(Random.value, Random.value, Random.value);

        PointWithNormal thisPoint = new PointWithNormal(position1, normal);
        PointWithNormal otherPoint = new PointWithNormal(position2, normal);

        Assert.IsFalse(thisPoint.Equals(otherPoint));
        Assert.AreNotEqual(thisPoint.GetHashCode(), otherPoint.GetHashCode());
    }

    [Test]
    public void TestEquals_NormalNotEqual()
    {
        Vector3 position = new Vector3(Random.value, Random.value, Random.value);
        Vector3 normal1 = new Vector3(Random.value, Random.value, Random.value);
        Vector3 normal2 = new Vector3(Random.value, Random.value, Random.value);

        PointWithNormal thisPoint = new PointWithNormal(position, normal1);
        PointWithNormal otherPoint = new PointWithNormal(position, normal2);

        Assert.IsFalse(thisPoint.Equals(otherPoint));
        Assert.AreNotEqual(thisPoint.GetHashCode(), otherPoint.GetHashCode());
    }


    [Test]
    public void TestCompareTo_Equal()
    {
        Vector3 position = new Vector3(Random.value, Random.value, Random.value);
        Vector3 normal = new Vector3(Random.value, Random.value, Random.value);

        PointWithNormal thisPoint = new PointWithNormal(position, normal);
        PointWithNormal otherPoint = new PointWithNormal(position, normal);

        Assert.AreEqual(0, thisPoint.CompareTo(otherPoint));
        Assert.AreEqual(0, otherPoint.CompareTo(thisPoint));
    }

    [Test]
    public void TestCompareTo_ThisSmallerPosition()
    {
        Vector3 position1 = new Vector3(1.0f, 2.0f, 3.0f);
        Vector3 position2 = new Vector3(5.0f, 6.0f, 7.0f);
        Vector3 normal = new Vector3(Random.value, Random.value, Random.value);

        PointWithNormal thisPoint = new PointWithNormal(position1, normal);
        PointWithNormal otherPoint = new PointWithNormal(position2, normal);

        Assert.AreEqual(-1, thisPoint.CompareTo(otherPoint));
        Assert.AreEqual(+1, otherPoint.CompareTo(thisPoint));
    }

    [Test]
    public void TestCompareTo_ThisSmallerPositionEqual()
    {
        Vector3 position = new Vector3(Random.value, Random.value, Random.value);
        Vector3 normal1 = new Vector3(1.0f, 2.0f, 3.0f);
        Vector3 normal2 = new Vector3(5.0f, 6.0f, 7.0f);

        PointWithNormal thisPoint = new PointWithNormal(position, normal1);
        PointWithNormal otherPoint = new PointWithNormal(position, normal2);

        Assert.AreEqual(-1, thisPoint.CompareTo(otherPoint));
        Assert.AreEqual(+1, otherPoint.CompareTo(thisPoint));
    }

    [Test]
    public void TestCompareTo_ThisGreater()
    {
        Vector3 position1 = new Vector3(5.0f, 2.0f, 3.0f);
        Vector3 position2 = new Vector3(1.0f, 6.0f, 7.0f);
        Vector3 normal = new Vector3(Random.value, Random.value, Random.value);

        PointWithNormal thisPoint = new PointWithNormal(position1, normal);
        PointWithNormal otherPoint = new PointWithNormal(position2, normal);

        Assert.AreEqual(+1, thisPoint.CompareTo(otherPoint));
        Assert.AreEqual(-1, otherPoint.CompareTo(thisPoint));
    }

    [Test]
    public void TestCompareTo_ThisGreaterPositionEqual()
    {
        Vector3 position = new Vector3(Random.value, Random.value, Random.value);
        Vector3 normal1 = new Vector3(5.0f, 2.0f, 3.0f);
        Vector3 normal2 = new Vector3(1.0f, 6.0f, 7.0f);

        PointWithNormal thisPoint = new PointWithNormal(position, normal1);
        PointWithNormal otherPoint = new PointWithNormal(position, normal2);

        Assert.AreEqual(+1, thisPoint.CompareTo(otherPoint));
        Assert.AreEqual(-1, otherPoint.CompareTo(thisPoint));
    }
}