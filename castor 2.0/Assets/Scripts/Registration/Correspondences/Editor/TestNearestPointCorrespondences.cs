using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using NUnit.Framework;

using Registration;


[TestFixture]
public class NearstPointCorrespondenceFinderTests
{
    private List<Vector3> cubeLeft = new List<Vector3> {
            new Vector3(0.5f, 2.0f, 0.3f),
            new Vector3(2.0f, 2.0f, 0.3f),
            new Vector3(0.5f, 6.0f, 0.3f),
            new Vector3(2.0f, 6.0f, 0.3f),
            new Vector3(0.5f, 2.0f, 2.3f),
            new Vector3(2.0f, 2.0f, 2.3f),
            new Vector3(0.5f, 6.0f, 2.3f),
            new Vector3(2.0f, 6.0f, 2.3f)
        };

    private List<Vector3> cubeRight = new List<Vector3> {
                new Vector3(0.7f, 1.2f, 0.5f),
                new Vector3(1.2f, 1.2f, 0.5f),
                new Vector3(0.7f, 4.2f, 0.5f),
                new Vector3(1.2f, 4.2f, 0.5f),
                new Vector3(0.7f, 1.2f, 1.7f),
                new Vector3(1.2f, 1.2f, 1.7f),
                new Vector3(0.7f, 4.2f, 1.7f),
                new Vector3(1.2f, 4.2f, 1.7f)
        };

    private List<Vector3> pyramid = new List<Vector3> {
                new Vector3(0.9f, 3.0f, 0.4f),
                new Vector3(2.5f, 3.0f, 0.4f),
                new Vector3(2.5f, 3.0f, 2.0f),
                new Vector3(0.9f, 3.0f, 2.0f),
                new Vector3(1.7f, 5.0f, 1.2f),
        };

    [Test]
    public void TestFindCorrespondencesStaticEqualsModel()
    {
        ReadOnlyCollection<Vector3> staticPoints = cubeLeft.AsReadOnly();

        ReadOnlyCollection<Vector3> modelPoints = cubeLeft.AsReadOnly();

        List<Correspondence> expected = new List<Correspondence> {
            new Correspondence(
                cubeLeft[0],
                cubeLeft[0]
            ),
            new Correspondence(
                cubeLeft[1],
                cubeLeft[1]
            ),
            new Correspondence(
                cubeLeft[2],
                cubeLeft[2]
            ),
            new Correspondence(
                cubeLeft[3],
                cubeLeft[3]
            ),
            new Correspondence(
                cubeLeft[4],
                cubeLeft[4]
            ),
            new Correspondence(
                cubeLeft[5],
                cubeLeft[5]
            ),
            new Correspondence(
                cubeLeft[6],
                cubeLeft[6]
            ),
            new Correspondence(
                cubeLeft[7],
                cubeLeft[7]
            )
        };
        List<Correspondence> actual = new NearstPointCorrespondenceFinder().Find(staticPoints, modelPoints);
        Assert.That(actual, Is.EquivalentTo(expected));
    }

    [Test]
    public void TestFindCorrespondencesStaticSameSizeAsModel()
    {
        ReadOnlyCollection<Vector3> staticPoints = cubeLeft.AsReadOnly();

        ReadOnlyCollection<Vector3> modelPoints = cubeRight.AsReadOnly();

        List<Correspondence> expected = new List<Correspondence> {
            new Correspondence(
                cubeLeft[0],
                cubeRight[0]
            ),
            new Correspondence(
                cubeLeft[1],
                cubeRight[1]
            ),
            new Correspondence(
                cubeLeft[5],
                cubeRight[5]
            ),
            new Correspondence(
                cubeLeft[2],
                cubeRight[2]
            ),
            new Correspondence(
                cubeLeft[6],
                cubeRight[6]
            ),
            new Correspondence(
                cubeLeft[3],
                cubeRight[3]
            ),
            new Correspondence(
                cubeLeft[7],
                cubeRight[7]
            ),
            new Correspondence(
                cubeLeft[4],
                cubeRight[4]
            )
        };
        List<Correspondence> actual = new NearstPointCorrespondenceFinder().Find(staticPoints, modelPoints);
        Assert.That(actual, Is.EquivalentTo(expected));
    }

    [Test]
    public void TestFindCorrespondencesStaticLargerThanModel()
    {
        ReadOnlyCollection<Vector3> staticPoints = cubeLeft.AsReadOnly();
        ReadOnlyCollection<Vector3> modelPoints = pyramid.AsReadOnly();

        List<Correspondence> expected = new List<Correspondence> {
            new Correspondence(
                cubeLeft[0],
                pyramid[0]
            ),
            new Correspondence(
                cubeLeft[4],
                pyramid[3]
            ),
            new Correspondence(
                cubeLeft[1],
                pyramid[1]
            ),
            new Correspondence(
                cubeLeft[5],
                pyramid[2]
            ),
            new Correspondence(
                cubeLeft[3],
                pyramid[4]
            )
        };
        List<Correspondence> actual = new NearstPointCorrespondenceFinder().Find(staticPoints, modelPoints);
        Assert.That(actual, Is.EquivalentTo(expected));
    }

    [Test]
    public void TestFindCorrespondencesStaticSmallerThanModel()
    {
        ReadOnlyCollection<Vector3> staticPoints = pyramid.AsReadOnly();
        ReadOnlyCollection<Vector3> modelPoints = cubeRight.AsReadOnly();

        List<Correspondence> expected = new List<Correspondence> {
            new Correspondence(
                pyramid[4],
                cubeRight[7]
            ),
            new Correspondence(
                pyramid[0],
                cubeRight[2]
            ),
            new Correspondence(
                pyramid[3],
                cubeRight[6]
            ),
            new Correspondence(
                pyramid[1],
                cubeRight[3]
            ),
            new Correspondence(
                pyramid[2],
                cubeRight[7]
            )
        };
        List<Correspondence> actual = new NearstPointCorrespondenceFinder().Find(staticPoints, modelPoints);
        Assert.That(actual, Is.EquivalentTo(expected));
    }

    [Test]
    public void TestCreateDistanceNodeList()
    {
        ReadOnlyCollection<Vector3> staticPoints = new List<Vector3> { 
            new Vector3(8.10e-01f, 6.30e-01f, 9.60e-01f),
            new Vector3(9.10e-01f, 1.00e-01f, 9.60e-01f),
            new Vector3(1.30e-01f, 2.80e-01f, 1.60e-01f),
            new Vector3(9.10e-01f, 5.50e-01f, 9.70e-01f),
        }.AsReadOnly();
        ReadOnlyCollection<Vector3> modelPoints = new List<Vector3> { 
            new Vector3(9.60e-01f, 8.00e-01f, 4.20e-01f),
            new Vector3(4.90e-01f, 1.40e-01f, 9.20e-01f),
        }.AsReadOnly();

        List<DistanceNode> actual = new NearstPointCorrespondenceFinder().CreateDistanceNodeList(staticPoints, modelPoints);
        List<DistanceNode> expected = new List<DistanceNode> {
            new DistanceNode(staticPoints[1], modelPoints[1], 1.7960e-01f),
            new DistanceNode(staticPoints[0], modelPoints[0], 3.4300e-01f),
            new DistanceNode(staticPoints[0], modelPoints[1], 3.4410e-01f),
            new DistanceNode(staticPoints[3], modelPoints[1], 3.4700e-01f),
            new DistanceNode(staticPoints[3], modelPoints[0], 3.6750e-01f),
            new DistanceNode(staticPoints[2], modelPoints[1], 7.2680e-01f),
            new DistanceNode(staticPoints[1], modelPoints[0], 7.8410e-01f),
            new DistanceNode(staticPoints[2], modelPoints[0], 1.0269e+00f),
        };

        Assert.That(actual, Is.EquivalentTo(expected));
    }
}

[TestFixture]
public class DistanceNodeTests
{
    [Test]
    public void TestCompareTo_SmallerThan()
    {
        DistanceNode thisNode = new DistanceNode(RandomVector(), RandomVector(), 0.2f);
        DistanceNode otherNode = new DistanceNode(RandomVector(), RandomVector(), 0.7f);

        int expected = -1;
        int actual = thisNode.CompareTo(otherNode);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestCompareTo_GreaterThan()
    {
        DistanceNode thisNode = new DistanceNode(RandomVector(), RandomVector(), 0.5f);
        DistanceNode otherNode = new DistanceNode(RandomVector(), RandomVector(), 0.2f);

        int expected = 1;
        int actual = thisNode.CompareTo(otherNode);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestCompareTo_EqualTo()
    {
        float distance = Random.value;
        DistanceNode thisNode = new DistanceNode(new Vector3(1.0f, 2.0f, 3.0f), new Vector3(4.0f, 2.0f, 5.0f), distance);
        DistanceNode otherNode = new DistanceNode(new Vector3(1.0f, 2.0f, 3.0f), new Vector3(4.0f, 2.0f, 5.0f), distance);

        int actual1 = thisNode.CompareTo(otherNode);
        int actual2 = otherNode.CompareTo(thisNode);
        int expected = 0;

        Assert.That(actual1, Is.EqualTo(expected));
        Assert.That(actual2, Is.EqualTo(expected));
    }

    [Test]
    public void TestCompareTo_EqualToDistanceEqual()
    {
        float distance = Random.value;
        DistanceNode thisNode = new DistanceNode(new Vector3(1.0f, 2.0f, 3.0f), RandomVector(), distance);
        DistanceNode otherNode = new DistanceNode(new Vector3(100.0f, 200.0f, 300.0f), RandomVector(), distance);

        int actual = thisNode.CompareTo(otherNode);
        int expected = -1;

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestCompareTo_EqualToDistanceAndStaticEqual()
    {
        float distance = Random.value;
        DistanceNode thisNode = new DistanceNode(new Vector3(1.0f, 2.0f, 3.0f), new Vector3(1.0f, 2.0f, 3.0f), distance);
        DistanceNode otherNode = new DistanceNode(new Vector3(1.0f, 2.0f, 3.0f), new Vector3(100.0f, 200.0f, 300.0f), distance);

        int actual = thisNode.CompareTo(otherNode);
        int expected = -1;

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestEquals_Equal(){
        DistanceNode thisNode = new DistanceNode(
            new Vector3(1.0f, 2.0f, 3.0f), 
            new Vector3(4.0f, 5.0f, 6.0f), 
            7.0f
        );
        DistanceNode otherNode = new DistanceNode(
            new Vector3(1.0f, 2.0f, 3.0f),
            new Vector3(4.0f, 5.0f, 6.0f),
            7.0f
        );

        bool expected = true;
        bool actual1 = thisNode.Equals(otherNode);
        bool actual2 = thisNode.Equals(otherNode);

        Assert.That(expected, Is.EqualTo(actual1));
        Assert.That(expected, Is.EqualTo(actual2));
    }

    [Test]
    public void TestEquals_StaticVectorNotEqual()
    {
        DistanceNode thisNode = new DistanceNode(
            new Vector3(1.0f, 2.0f, 3.0f),
            new Vector3(4.0f, 5.0f, 6.0f),
            7.0f
        );
        DistanceNode otherNode = new DistanceNode(
            new Vector3(1.0f, 5.0f, 3.0f),
            new Vector3(4.0f, 5.0f, 6.0f),
            7.0f
        );

        bool expected = false;
        bool actual1 = thisNode.Equals(otherNode);
        bool actual2 = thisNode.Equals(otherNode);

        Assert.That(expected, Is.EqualTo(actual1));
        Assert.That(expected, Is.EqualTo(actual2));
    }

    [Test]
    public void TestEquals_ModelVectorNotEqual()
    {
        DistanceNode thisNode = new DistanceNode(
            new Vector3(1.0f, 2.0f, 3.0f),
            new Vector3(4.0f, 5.0f, 6.0f),
            7.0f
        );
        DistanceNode otherNode = new DistanceNode(
            new Vector3(1.0f, 2.0f, 3.0f),
            new Vector3(4.0f, 6.0f, 6.0f),
            7.0f
        );

        bool expected = false;
        bool actual1 = thisNode.Equals(otherNode);
        bool actual2 = thisNode.Equals(otherNode);

        Assert.That(expected, Is.EqualTo(actual1));
        Assert.That(expected, Is.EqualTo(actual2));
    }

    [Test]
    public void TestEquals_DistanceNotEqual()
    {
        DistanceNode thisNode = new DistanceNode(
            new Vector3(1.0f, 2.0f, 3.0f),
            new Vector3(4.0f, 5.0f, 6.0f),
            7.0f
        );
        DistanceNode otherNode = new DistanceNode(
            new Vector3(1.0f, 2.0f, 3.0f),
            new Vector3(4.0f, 5.0f, 6.0f),
            3.5f
        );

        bool expected = false;
        bool actual1 = thisNode.Equals(otherNode);
        bool actual2 = thisNode.Equals(otherNode);

        Assert.That(expected, Is.EqualTo(actual1));
        Assert.That(expected, Is.EqualTo(actual2));
    }

    private Vector3 RandomVector()
    {
        return new Vector3(Random.value, Random.value, Random.value);
    }
}