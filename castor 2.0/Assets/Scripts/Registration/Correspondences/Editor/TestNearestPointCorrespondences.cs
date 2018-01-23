using UnityEngine;
using NUnit.Framework;

using Registration;
using System.Collections.Generic;

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
        List<Vector3> staticPoints = cubeLeft;

        List<Vector3> modelPoints = cubeLeft;

        List<Correspondence> expected = new List<Correspondence> {
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
            ),
            new Correspondence(
                cubeLeft[8],
                cubeLeft[8]
            )
        };
        List<Correspondence> actual = new NearstPointCorrespondenceFinder().Find(staticPoints, modelPoints);
        Assert.That(actual, Is.EquivalentTo(expected));
    }

    [Test]
    public void TestFindCorrespondencesStaticSameSizeAsModel()
    {
        List<Vector3> staticPoints = cubeLeft;

        List<Vector3> modelPoints = cubeRight;

        List<Correspondence> expected = new List<Correspondence> {
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
            ),
            new Correspondence(
                cubeLeft[8],
                cubeRight[8]
            )
        };
        List<Correspondence> actual = new NearstPointCorrespondenceFinder().Find(staticPoints, modelPoints);
        Assert.That(actual, Is.EquivalentTo(expected));
    }

    [Test]
    public void TestFindCorrespondencesStaticLargerThanModel()
    {
        List<Vector3> staticPoints = cubeLeft;

        List<Vector3> modelPoints = pyramid;
        List<Correspondence> expected = new List<Correspondence> {
            new Correspondence(
                cubeLeft[1],
                pyramid[1]
            ),
            new Correspondence(
                cubeLeft[5],
                pyramid[4]
            ),
            new Correspondence(
                cubeLeft[2],
                pyramid[2]
            ),
            new Correspondence(
                cubeLeft[6],
                pyramid[3]
            ),
            new Correspondence(
                cubeLeft[4],
                pyramid[5]
            )
        };
        List<Correspondence> actual = new NearstPointCorrespondenceFinder().Find(staticPoints, modelPoints);
        Assert.That(actual, Is.EquivalentTo(expected));
    }

    [Test]
    public void TestFindCorrespondencesStaticSmallerThanModel()
    {
        List<Vector3> staticPoints = pyramid;

        List<Vector3> modelPoints = cubeRight;
        List<Correspondence> expected = new List<Correspondence> {
            new Correspondence(
                pyramid[5],
                cubeRight[8]
            ),
            new Correspondence(
                pyramid[1],
                cubeRight[3]
            ),
            new Correspondence(
                pyramid[4],
                cubeRight[7]
            ),
            new Correspondence(
                pyramid[2],
                cubeRight[4]
            ),
            new Correspondence(
                pyramid[3],
                cubeRight[7]
            )
        };
        List<Correspondence> actual = new NearstPointCorrespondenceFinder().Find(staticPoints, modelPoints);
        Assert.That(actual, Is.EquivalentTo(expected));
    }

    [Test]
    public void TestInputParametersNotChanged()
    {
        List<Vector3> actualPyramid = pyramid;
        List<Vector3> actualCube = cubeRight;

        List<Vector3> expectedPyramid = new List<Vector3> {
            new Vector3(0.9f, 3.0f, 0.4f),
            new Vector3(2.5f, 3.0f, 0.4f),
            new Vector3(2.5f, 3.0f, 2.0f),
            new Vector3(0.9f, 3.0f, 2.0f),
            new Vector3(1.7f, 5.0f, 1.2f)
        };

        List<Vector3> expectedCube = new List<Vector3> {
                new Vector3(0.7f, 1.2f, 0.5f),
                new Vector3(1.2f, 1.2f, 0.5f),
                new Vector3(0.7f, 4.2f, 0.5f),
                new Vector3(1.2f, 4.2f, 0.5f),
                new Vector3(0.7f, 1.2f, 1.7f),
                new Vector3(1.2f, 1.2f, 1.7f),
                new Vector3(0.7f, 4.2f, 1.7f),
                new Vector3(1.2f, 4.2f, 1.7f)
        };

        List<Correspondence> actual = new NearstPointCorrespondenceFinder().Find(actualPyramid, actualCube);
        Assert.That(actualPyramid, Is.EquivalentTo(expectedPyramid));
        Assert.That(actualCube, Is.EquivalentTo(expectedCube));
    }
}

[TestFixture]
public class DistanceNodeTests{
    [Test]
    public void TestCompareToSmallerThan(){
        DistanceNode thisNode = new DistanceNode(RandomVector(), RandomVector(), 0.2f);
        DistanceNode otherNode = new DistanceNode(RandomVector(), RandomVector(), 0.7f);

        int expected = -1;
        int actual = thisNode.CompareTo(otherNode);

        Assert.That(actual, Is.EqualTo(expected));        
    }

    [Test]
    public void TestCompareToGreaterThan(){
        DistanceNode thisNode = new DistanceNode(RandomVector(), RandomVector(), 0.5f);
        DistanceNode otherNode = new DistanceNode(RandomVector(), RandomVector(), 0.2f);

        int expected = 1;
        int actual = thisNode.CompareTo(otherNode);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestCompareToEqualTo(){
        float distance = Random.value;
        DistanceNode thisNode = new DistanceNode(RandomVector(), RandomVector(), distance);
        DistanceNode otherNode = new DistanceNode(RandomVector(), RandomVector(), distance);

        int actual1 = thisNode.CompareTo(otherNode);
        int actual2 = otherNode.CompareTo(thisNode);
        int expected = 0;

        Assert.That(actual1, Is.EqualTo(expected));
        Assert.That(actual2, Is.EqualTo(expected));
    }

    private Vector3 RandomVector(){
        return new Vector3(Random.value, Random.value, Random.value);
    }
}