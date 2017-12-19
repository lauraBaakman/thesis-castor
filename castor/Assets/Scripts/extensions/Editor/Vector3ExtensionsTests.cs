using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class Vector3ExtensionsTests
{

    [Test]
    public void Clamp_xyOutOfRange()
    {
        Vector3 vector = new Vector3(-1.0f, 0.5f, 2.0f);

        Vector3 expected = new Vector3(0.0f, 0.5f, 1.0f);
        vector.Clamp(0.0f, 1.0f);

        Assert.That(vector.x, Is.EqualTo(expected.x).Within(0.000001));
        Assert.That(vector.y, Is.EqualTo(expected.y).Within(0.000001));
        Assert.That(vector.z, Is.EqualTo(expected.z).Within(0.000001));
    }
}
