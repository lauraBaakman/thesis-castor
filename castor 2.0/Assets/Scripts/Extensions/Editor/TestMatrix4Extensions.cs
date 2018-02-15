using UnityEngine;
using NUnit.Framework;

[TestFixture]
public class Matrix4ExtensionTests
{
    [Test]
    public void TestExtractTranslation_OnlyTranslationSet()
    {
        Vector3 translation = new Vector3(Random.value, Random.value, Random.value);

        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetTRS(
            pos: translation,
            q: Quaternion.identity,
            s: new Vector3(1.0f, 1.0f, 1.0f)
        );

        Vector3 expected = translation;
        Vector3 actual = matrix.ExtractTranslation();

        Assert.AreEqual(
            actual: actual,
            expected: expected
        );
    }

    [Test]
    public void TestExtractTranslation_TRSSet()
    {
        Vector3 translation = new Vector3(Random.value, Random.value, Random.value);
        Quaternion rotation = Random.rotation;
        Vector3 scale = new Vector3(Random.value, Random.value, Random.value);

        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetTRS(
            pos: translation,
            q: rotation,
            s: scale
        );

        Vector3 expected = translation;
        Vector3 actual = matrix.ExtractTranslation();

        Assert.AreEqual(
            actual: actual,
            expected: expected
        );
    }

    [Test]
    public void TestExtractRotation_OnlyRotationSet()
    {
        Vector3 translation = new Vector3(Random.value, Random.value, Random.value);
        Quaternion rotation = Random.rotation;
        Vector3 scale = new Vector3(Random.value, Random.value, Random.value);

        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetTRS(
            pos: new Vector3(),
            q: rotation,
            s: new Vector3(1.0f, 1.0f, 1.0f)
        );

        Quaternion expected = rotation;
        Quaternion actual = matrix.ExtratRotation();

        //source https://answers.unity.com/answers/288354/view.html
        float angle = Quaternion.Angle(actual, expected);
        Assert.AreEqual(expected: 0.0f, actual: angle);
    }

    [Test]
    public void TestExtractRotation_TRSSet()
    {
        Vector3 translation = new Vector3(Random.value, Random.value, Random.value);
        Quaternion rotation = Random.rotation;
        Vector3 scale = new Vector3(Random.value, Random.value, Random.value);

        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetTRS(
            pos: translation,
            q: rotation,
            s: scale
        );

        Quaternion expected = rotation;
        Quaternion actual = matrix.ExtratRotation();

        //source https://answers.unity.com/answers/288354/view.html
        float angle = Quaternion.Angle(actual, expected);
        Assert.AreEqual(expected: 0.0f, actual: angle);
    }

    [Test]
    public void TestExtractXRotation_TRSSet()
    {
        float expected = Random.value;

        Vector3 translation = new Vector3(Random.value, Random.value, Random.value);
        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = new Vector3(expected, Random.value, Random.value);

        Vector3 scale = new Vector3(Random.value, Random.value, Random.value);

        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetTRS(
            pos: translation,
            q: rotation,
            s: scale
        );

        float actual = matrix.ExtractRotationAroundXAxis();

        Assert.That(actual, Is.EqualTo(expected).Within(0.0001));
    }

    [Test]
    public void TestExtractYRotation_TRSSet()
    {
        float expected = Random.value;

        Vector3 translation = new Vector3(Random.value, Random.value, Random.value);
        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = new Vector3(Random.value, expected, Random.value);

        Vector3 scale = new Vector3(Random.value, Random.value, Random.value);

        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetTRS(
            pos: translation,
            q: rotation,
            s: scale
        );

        float actual = matrix.ExtractRotationAroundYAxis();

        Assert.That(actual, Is.EqualTo(expected).Within(0.0001));
    }

    [Test]
    public void TestExtractZRotation_TRSSet()
    {
        float expected = Random.value;

        Vector3 translation = new Vector3(Random.value, Random.value, Random.value);
        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = new Vector3(Random.value, Random.value, expected);

        Vector3 scale = new Vector3(Random.value, Random.value, Random.value);

        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetTRS(
            pos: translation,
            q: rotation,
            s: scale
        );

        float actual = matrix.ExtractRotationAroundZAxis();

        Assert.That(actual, Is.EqualTo(expected).Within(0.0001));
    }
}