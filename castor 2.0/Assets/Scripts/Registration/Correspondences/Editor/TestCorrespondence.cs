using UnityEngine;
using NUnit.Framework;

using Registration;
using System.Collections.Generic;

[TestFixture]
public class CorrespondenceTests
{
    [Test]
    public void TestEquals_Equals()
    {
        Correspondence thisCorrespondence = new Correspondence(
            new Vector3(1.0f, 2.0f, 2.0f),
            new Vector3(3.0f, 4.0f, 5.0f)
        );
        Correspondence otherCorrespondence = new Correspondence(
            new Vector3(1.0f, 2.0f, 2.0f),
            new Vector3(3.0f, 4.0f, 5.0f)
        );

        bool expected = true;
        bool actual = thisCorrespondence.Equals(otherCorrespondence);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestEquals_StaticPointNotEqual()
    {
        Correspondence thisCorrespondence = new Correspondence(
            new Vector3(1.0f, 2.0f, 2.0f),
            new Vector3(3.0f, 4.0f, 5.0f)
        );
        Correspondence otherCorrespondence = new Correspondence(
            new Vector3(1.0f, 3.0f, 2.0f),
            new Vector3(3.0f, 4.0f, 5.0f)
        );

        bool expected = false;
        bool actual = thisCorrespondence.Equals(otherCorrespondence);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestEquals_ModelPointNotEqual()
    {
        Correspondence thisCorrespondence = new Correspondence(
            new Vector3(1.0f, 2.0f, 2.0f),
            new Vector3(3.0f, 4.0f, 5.0f)
        );
        Correspondence otherCorrespondence = new Correspondence(
            new Vector3(1.0f, 2.0f, 2.0f),
            new Vector3(3.0f, 7.0f, 5.0f)
        );

        bool expected = false;
        bool actual = thisCorrespondence.Equals(otherCorrespondence);

        Assert.That(actual, Is.EqualTo(expected));
    }
}