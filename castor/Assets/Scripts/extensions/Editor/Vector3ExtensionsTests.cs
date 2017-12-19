using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class Vector3ExtensionsTests
{

    [Test]
    public void Clamped_xyOutOfRange()
    {
        Vector3 vector = new Vector3(-1.0f, 0.5f, 2.0f);

        Vector3 expected = new Vector3(0.0f, 0.5f, 1.0f);

        Vector3 actual = vector.Clamped(0.0f, 1.0f);

        Assert.That(actual.x, Is.EqualTo(expected.x).Within(0.000001));
        Assert.That(actual.y, Is.EqualTo(expected.y).Within(0.000001));
        Assert.That(actual.z, Is.EqualTo(expected.z).Within(0.000001));

        Assert.That(vector.x, Is.EqualTo(-1.0f).Within(0.000001));
        Assert.That(vector.y, Is.EqualTo(+0.5f).Within(0.000001));
        Assert.That(vector.z, Is.EqualTo(+2.0f).Within(0.000001));
    }

    [Test]
    public void Fill_zeroVector()
    {
        float value = 3.0f;
        Vector3 vector = new Vector3().Fill(value);

        Vector3 expected = new Vector3(value, value, value);


        Assert.That(vector.x, Is.EqualTo(expected.x).Within(0.000001));
        Assert.That(vector.y, Is.EqualTo(expected.y).Within(0.000001));
        Assert.That(vector.z, Is.EqualTo(expected.z).Within(0.000001));
    }

    [Test]
    public void Fill_nonZeroVector()
    {
        float value = 3.0f;
        Vector3 vector = new Vector3(-1.0f, 0.5f, 2.0f).Fill(value);

        Vector3 expected = new Vector3(value, value, value);


        Assert.That(vector.x, Is.EqualTo(expected.x).Within(0.000001));
        Assert.That(vector.y, Is.EqualTo(expected.y).Within(0.000001));
        Assert.That(vector.z, Is.EqualTo(expected.z).Within(0.000001));
    }
}
